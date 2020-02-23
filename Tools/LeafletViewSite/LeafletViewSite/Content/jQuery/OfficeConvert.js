state = {
    map: null,
    drawnItems: null,
    menuLayer: null,
    layer: null,
    layerType: "",
    version: {},
    versions: [],
    comments: [],
    annotations: [],
    selLi: [], //选择的&，#，@列表
    selInputLi: [], //在input选择的@列表
    isShowDiscuss: false,
    isShowUserDetail: false,
    imageZoomLevel: 0,
    highHeightUnit:0,
    suffix: "",
    users: [],
    curUser: null
}

//初始化
this.initAllVersions();


//获取地址栏参数,如果参数不存在则返回空字符串
function getQueryString(key, url) {
    if (typeof (url) == "undefined")
        url = window.location.search;
    var re = new RegExp("[?&]" + key + "=([^\\&]*)", "i");
    var a = re.exec(url);
    if (a == null) return "";
    return a[1];
}

function reloadUrl(paramName, replaceWith) {
    var oUrl = window.location.href.toString();
    var re = eval('/(' + paramName + '=)([^&]*)/gi');
    if (oUrl && oUrl.indexOf(paramName) >= 0)
        window.location.href = oUrl.replace(re, paramName + '=' + replaceWith);
    else
        window.location.href = oUrl + '&' + paramName + '=' + replaceWith;
}


function setState(obj) {
    if (obj) {
        if (obj.map)
            this.state.map = obj.map;
        if (obj.drawnItems)
            this.state.drawnItems = obj.drawnItems;
        if (obj.layer)
            this.state.layer = obj.layer;
        if (obj.layerType)
            this.state.layerType = obj.layerType;
        if (obj.menuLayer)
            this.state.menuLayer = obj.menuLayer;
        if (obj.version)
            this.state.version = obj.version;
        if (obj.versions)
            this.state.versions = obj.versions;
        if (obj.comments)
            this.state.comments = obj.comments;
        if (obj.annotations)
            this.state.annotations = obj.annotations;
        if (obj.selLi)
            this.state.selLi = obj.selLi;
        if (obj.selInputLi)
            this.state.selInputLi = obj.selInputLi;
        if (obj.isShowDiscuss)
            this.state.isShowDiscuss = obj.isShowDiscuss;
        if (obj.isShowUserDetail)
            this.state.isShowUserDetail = obj.isShowUserDetail;
        if (obj.imageZoomLevel)
            this.state.imageZoomLevel = obj.imageZoomLevel;
        if (obj.highHeightUnit)
            this.state.highHeightUnit = obj.highHeightUnit;
        if (obj.suffix)
            this.state.suffix = obj.suffix;
        if (obj.users)
            this.state.users = obj.users;
    }
}

/*初始化地图*/
function initMap() {
    var map = this.state.map;
    var drawnItems = this.state.drawnItems;
    //L.CRS.Simple.scale = function (zoom) {
    //    return 1024 * Math.pow(2, zoom);
    //}
    /*地图参数设置*/
    var mapOption = {
        center: new L.LatLng(0, 0), //地图的初始地理中心Leaflet
        zoomControl: false, //放大缩小按钮是否显示
        zoom: this.state.suffix.indexOf('dwg') >= 0 ? 0 : this.state.imageZoomLevel, //初始地图缩放级别
        minZoom: this.state.suffix.indexOf('dwg') >= 0 ? 0 : this.state.imageZoomLevel, //地图的最小缩放级别
        scrollwheel: true,
        legends: true,
        infoControl: false,
        attributionControl: false, //是否显示版权信息
        preferCanvas: false  //是否Path应在渲染Canvas器上渲染。默认情况下，所有Paths都在SVG渲染器中呈现
    };
    /*创建地图*/
    map = new L.Map('map', mapOption);
    map.setView(map.unproject([420, 210]));
    var _bounds = map.getBounds();
    var _nw = _bounds.getNorthWest();
    var _se = _bounds.getSouthEast();
    //map.setView(new L.latLng(0.8476453278440058, -0.08525812499999176));

    if (mapOption.zoom == 0) {
        var _bias = map.layerPointToLatLng([320, -610]);

        var n_nw = L.latLng(_nw.lat + _bias.lat, _nw.lng + _bias.lng);
        var n_se = L.latLng(_se.lat + _bias.lat, _se.lng + _bias.lng);
        map.setMaxBounds((new L.LatLngBounds(n_nw, n_se)).pad(0.1));
    }
    else {
        var sizeS = map.unproject([0, 0], mapOption.zoom);
        var sizeE = map.unproject([300, 300], mapOption.zoom);

        var perHeight = sizeS.lat - sizeE.lat;
        var deepHeightUnit = this.state.highHeightUnit;
        var n_nw = L.latLng(_nw.lat, _nw.lng);
        var n_se = L.latLng(_se.lat - perHeight * deepHeightUnit, _se.lng);
        //map.fitBounds(_bounds);
        map.setMaxBounds((new L.LatLngBounds(n_nw, n_se)));
    }

    //

    /*批注信息*/
    drawnItems = L.featureGroup().addTo(map);
    if (this.state.suffix.indexOf('dwg') >= 0) {
        /*放大缩小按钮*/
        L.control.zoom({ position: "bottomleft" }).addTo(map);
        L.Icon.Default.imagePath = "./Content/Leaflet/images/";
    }
    /*工具初始化*/
    var drawTool = new L.Control.Draw({
        edit: false,
        draw: {
            polyline: {
                shapeOptions: {
                    color: "red",
                    //opacity: 0.5,
                    //weight: 4
                }
            },
            polygon: {
                shapeOptions: {
                    color: "red",
                    //weight: 4,
                    //opacity: 0.5,
                },
                allowIntersection: false,
                showArea: true
            },
            rectangle: {
                shapeOptions: {
                    color: "red",
                    //weight: 4,
                    //opacity: 0.5,
                }
            },
            circle: {
                shapeOptions: {
                    color: "red",
                    //weight: 4,
                    //opacity: 0.5
                }
            },
            marker: {
                icon: new L.Icon.Default()
            }
        }
    });
    map.addControl(drawTool);

    /*全屏*/
    map.addControl(new L.Control.Fullscreen());

    /*添加图层*/
    map.on(L.Draw.Event.CREATED, function (event) {
        var layer = event.layer;
        state.drawnItems.addLayer(layer);

        //保存当前layer，layerType
        //this.setState({ layer: layer, layerType: event.layerType });

        var popup = layer.bindPopup(addAnnotationHtml(layer, event.layerType));
        popup.openPopup();
        function annotation() {
            $("#btn_annotation_cancel").on("click", function (e) {
                closeAnnotation(layer, 1)
            });
            $("#btn_annotation_ok").on("click", function (e) {
                saveAnnotation(layer, event.layerType)
            });
        }
        setTimeout(annotation(), 500);
        /*阻止弹出框关闭*/
        _popupState = true;
        popup.on("popupclose", function (e) {
            var that = this;
            if (_popupState) {
                setTimeout(function () {
                    that.openPopup();
                }, 1);
            }
        });
    });

    /*编辑图层*/
    map.on(L.Draw.Event.EDITED, function (event) {

    });
    /*删除图层*/
    map.on(L.Draw.Event.DELETED, function (event) {
    });
    this.setState({ map: map, drawnItems: drawnItems });
}



/*初始化版本信息*/
function initAllVersions() {
    // L.control.layers(baseMaps, overlayMaps).addTo(map); 覆盖图层
    var curVersion = getQueryString("Version");
    $.getJSON("api/Office/Versions/" + getQueryString("ID")).done(function (data) {
        if (data) {
            $(".office-display").hide();
            $(".preview-container,.comment-container").show();

            var docVersions = JSON.parse(data);
            if (docVersions) {
                docVersions.Versions.map(function (item, index) {
                    if (!curVersion) {
                        if ((docVersions.Version && docVersions.Version == item.ID)) {
                            version = item;
                        }
                    } else {
                        if ((docVersions.Version && curVersion == item.ID)) {
                            version = item;
                        }
                    }
                });
                var suffix = version.FullPath.indexOf('.dwg') >= 0 ? 'dwg' : "office";
                setState({ imageZoomLevel: version.ImageZoomLevel, highHeightUnit: version.HighHeightUnit, suffix: suffix });
                if (!map)
                    initMap();
                var map = state.map;
                var drawnItems = state.drawnItems;
                var menuLayer = state.menuLayer;
                var layers = {};
                var version = {};

                var southWest = L.latLng(9999999, -9999999);
                var northEast = L.latLng(9999999, -9999999);
                var mybounds = L.latLngBounds(southWest, northEast);
                docVersions.Versions.map(function (item, index) {
                    //版本参数设置
                    var tileLayerOption = {
                        tileSize: state.suffix.indexOf('dwg') >= 0 ? 1024 : 300,
                        noWrap: true, //是否环绕
                        maxZoom: item.ImageZoomLevel,
                        id: item.ID
                        //maxBounds: new L.LatLngBounds([0, 0], [4096, 4096])
                        //bounds: mybounds
                    };

                    layers[item.Name] = L.tileLayer(docVersions.Versions[index].ImagePath + "{z}-{x}-{y}.jpg", tileLayerOption);

                    if (!curVersion) {
                        if ((docVersions.Version && docVersions.Version == item.ID)) {
                            layers[item.Name].addTo(map);
                            version = item;
                        }
                    } else {
                        if ((docVersions.Version && curVersion == item.ID)) {
                            layers[item.Name].addTo(map);
                            version = item;
                        }
                    }
                });
                //menuLayer=L.control.layers(layers, { '显示批注信息': drawnItems }, { position: 'topright', collapsed: true }).addTo(map);
                map._onResize();

                setState({ map: map, drawnItems: drawnItems, menuLayer: menuLayer, versions: docVersions.Versions, version: version, annotations: version.Annotations, comments: version.Comments });
            };
            initLayerData();

            $.getJSON("api/User/Users/").done(function (data) {
                setState({ users: JSON.parse(data) });
                setUser();
                initMsg();
                onMouseOverMsg('', '');
            }).fail(function (jqXHR, textStatus, err) {
            });
        } else {
            $(".preview-container,.comment-container").hide();
            $(".office-display").show();
        }
    }).fail(function (jqXHR, textStatus, err) {
        $(".preview-container,.comment-container").hide();
        $(".office-display").show();
    });

}

function setUser() {
    var formData = new FormData();
    var user = Decrypt(getQueryString('User'));
    var userID = user.split('.')[0];
    if (gUser(userID) == '') {
        var userName = user.split('.')[1];
        var dept = user.split('.')[2];
        var data = [{ UserID: userID, UserName: userName, Dept: dept, Ico: "nopicture.jpg" }];
        formData.append("Data", JSON.stringify(data));

        $.ajax({
            type: "post",
            url: 'api/User/SyncUser',
            async: false,
            data: formData,
            contentType: false,
            processData: false,
            success: function (data, status) {
                state.users.push({ UserID: userID, UserName: userName, Dept: dept, Ico: "nopicture.jpg" });
            },
            error: function (xhr, status, err) {
                alert("ajax调用异常: " + status + "," + err);
            }
        });
    }
    state.curUser = { UserID: userID, UserName: userName, Dept: dept, Ico: "nopicture.jpg" };
}

function changeVersion(e) {
    var value = $('.version-select option:selected').val();
    reloadUrl('Version', value);
}

function gFileName() {
    var fullPath = state.version.FullPath;
    if (fullPath) {
        return fullPath.substring(fullPath.lastIndexOf('\\') + 1);
    } else { return ""; }
}
function download() {
    window.open('api/File/DownloadAttachment/' + gFileName());
}

function gUser(userID) {
    var item = '';
    var users = state.users;
    for (var i = 0; i < users.length; i++) {
        if (users[i].UserID == userID) {
            item = users[i];
            break;
        }
    }
    return item;
}

function myBrowser() {
    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
    var isOpera = userAgent.indexOf("Opera") > -1;
    var browser = "IE";
    if (isOpera) {
        browser = "Opera"
    }; //判断是否Opera浏览器
    if (userAgent.indexOf("Firefox") > -1) {
        browser = "FF";
    } //判断是否Firefox浏览器
    if (userAgent.indexOf("Chrome") > -1) {
        browser = "Chrome";
    }
    if (userAgent.indexOf("Safari") > -1) {
        browser = "Safari";
    } //判断是否Safari浏览器
    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
        browser = "IE";
    }; //判断是否IE浏览器
    return browser;
}

function Encrypt(str, pwd) {
    if (str == "") return "";
    str = escape(str);
    if (!pwd || pwd == "") { var pwd = "abcd123456"; }
    pwd = escape(pwd);
    if (pwd == null || pwd.length <= 0) {
        alert("Please enter a password with which to encrypt the message.");
        return null;
    }
    var prand = "";
    for (var I = 0; I < pwd.length; I++) {
        prand += pwd.charCodeAt(I).toString();
    }
    var sPos = Math.floor(prand.length / 5);
    var mult = parseInt(prand.charAt(sPos) + prand.charAt(sPos * 2) + prand.charAt(sPos * 3) + prand.charAt(sPos * 4) + prand.charAt(sPos * 5));
    var incr = Math.ceil(pwd.length / 2);
    var modu = Math.pow(2, 31) - 1;
    if (mult < 2) {
        alert("Algorithm cannot find a suitable hash. Please choose a different password. /nPossible considerations are to choose a more complex or longer password.");
        return null;
    }
    var salt = Math.round(Math.random() * 1000000000) % 100000000;
    prand += salt;
    while (prand.length > 10) {
        prand = (parseInt(prand.substring(0, 10)) + parseInt(prand.substring(10, prand.length))).toString();
    }
    prand = (mult * prand + incr) % modu;
    var enc_chr = "";
    var enc_str = "";
    for (var I = 0; I < str.length; I++) {
        enc_chr = parseInt(str.charCodeAt(I) ^ Math.floor((prand / modu) * 255));
        if (enc_chr < 16) {
            enc_str += "0" + enc_chr.toString(16);
        } else
            enc_str += enc_chr.toString(16);
        prand = (mult * prand + incr) % modu;
    }
    salt = salt.toString(16);
    while (salt.length < 8) salt = "0" + salt;
    enc_str += salt;
    return enc_str;
}

function Decrypt(str, pwd) {
    if (str == "") return "";
    if (!pwd || pwd == "") { var pwd = "abcd123456"; }
    pwd = escape(pwd);
    if (str == null || str.length < 8) {
        alert("A salt value could not be extracted from the encrypted message because it's length is too short. The message cannot be decrypted.");
        return;
    }
    if (pwd == null || pwd.length <= 0) {
        alert("Please enter a password with which to decrypt the message.");
        return;
    }
    var prand = "";
    for (var I = 0; I < pwd.length; I++) {
        prand += pwd.charCodeAt(I).toString();
    }
    var sPos = Math.floor(prand.length / 5);
    var mult = parseInt(prand.charAt(sPos) + prand.charAt(sPos * 2) + prand.charAt(sPos * 3) + prand.charAt(sPos * 4) + prand.charAt(sPos * 5));
    var incr = Math.round(pwd.length / 2);
    var modu = Math.pow(2, 31) - 1;
    var salt = parseInt(str.substring(str.length - 8, str.length), 16);
    str = str.substring(0, str.length - 8);
    prand += salt;
    while (prand.length > 10) {
        prand = (parseInt(prand.substring(0, 10)) + parseInt(prand.substring(10, prand.length))).toString();
    }
    prand = (mult * prand + incr) % modu;
    var enc_chr = "";
    var enc_str = "";
    for (var I = 0; I < str.length; I += 2) {
        enc_chr = parseInt(parseInt(str.substring(I, I + 2), 16) ^ Math.floor((prand / modu) * 255));
        enc_str += String.fromCharCode(enc_chr);
        prand = (mult * prand + incr) % modu;
    }
    return unescape(enc_str);
}

function initMsg() {
    var version = state.version;
    var yScroll = (document.documentElement.scrollHeight > document.documentElement.clientHeight) ? document.documentElement.scrollHeight : document.documentElement.clientHeight;
    if (myBrowser() == 'IE') {
        $(".version-title").css('margin-left', "50%");
        $(".comment-container").css('height', yScroll - 20);
    }
    $(".content").css('min-height', yScroll - 168);
    $(".version-title").html('<span class="cur-version-item">当前版本：' + version.Name +
        '</span><span class="cur-version-item">更新日期：' + version.ModifyTime +
        '</span><span class="cur-version-item">文件大小：' + version.Size +
        '</span>');
    function gVersionSelect() {
        var selHtml = "";
        var curVersion = getQueryString("Version");
        if (state.versions.length > 0) {
            state.versions.forEach(function (item, index, array) {
                var selected = curVersion == item.ID ? 'selected=true' : '';
                selHtml += '<option ' + selected + ' value="' + item.ID + '">' + item.Name + '</option>';
            });
        }
        return selHtml;
    }
    $(".version-select").html('<select onchange="changeVersion(this)">' + gVersionSelect() + '</select>');

    $(".doc-name").html(gFileName());
    function gRepaly() {
        var html = "";
        var comments = state.comments;
        var annotations = [];
        var comments_subitem = [];

        var h_subitem =
                    '<div class="subitem-area-wrapper">' +
                             '<div class="arrow"></div>{Subitem}' +
                    '</div>';
        var subitem =
                    '<div class="subitem-item-area" id="{ID}">' +
                        '<div class="title-item">' +
                            '<img class="u_icon" src="Scripts/UserIco/{Image}">' +
                            '<div class="middle-body"><span class="text-ellipsis" style="max-width: 260px;">' +
                                '<a class="lable-uname">{UserName}</a><sapn class="lable-company">{Dept}</sapn></span>' +
                                '<span style="display: block;"><span class="version-name">' +
                                   '版本：{Version}</span>' +
                            '{Time}</span></div>' +
                        '<img src="Scripts/Images/delete.png" class="_subitemDelete" value="{Display}" onclick="deleteMsg(\'{ID}\')"/>' +
                    '</div>' +
                    '<span class="subitem-msg-item">{Content}</span>' +
                    '</div>';
        var area = '<div class="item-area-wrapper" id="{ID}" onclick="_onSelectComment(this)" onmouseover="onMouseOverMsg(\'{ID}\',\'{UserID}\')" onmouseout="onMouseOutMsg(\'{ID}\')">' +
                    '<div class="item-area">' +
                        '<div class="title-item">' +
                            '<img class="u_icon" src="Scripts/UserIco/{Image}"><div class="middle-body">' +
                                '<span class="text-ellipsis" style="max-width: 280px;"><a class="lable-uname">{UserName}</a><sapn class="lable-company">{Dept}</sapn></span><span style="display: block;"><span class="version-name">版本：{Version}</span>' +
                                    '{Time}' +
                                    '</span></div>' +
                                    '<img src="Scripts/Images/repaly.png" class="_msg" onclick="_showReMessageInput(\'{ID}\')"/>' +
                                    '<img src="Scripts/Images/delete.png" class="_delete" onclick="deleteMsg(\'{ID}\')"/>' +
                                '</div>' +
                                '<span class="msg-item">{Content}</span>' +
                                '{Subitem}' +
                            '</div>' +
                    '<span class="ant-input-affix-wrapper">' +
                        '<input type="text" id="input_{ID}" class="ant-input" onkeydown="onKeyDown(\'{ID}\')" placeholder="请输入评论内容 按Enter键发布">' +
                        '<span class="ant-input-suffix"><i class="anticon anticon-arrow-right"></i></span></span></div>';
        comments.forEach(function (item, index, array) {
            if (item.PID == '')
                annotations.push(item);
            else
                comments_subitem.push(item);
        });
        function gSubitem(id) {
            var s = "";
            var userID = state.curUser.UserID;
            comments_subitem.forEach(function (item, index, array) {
                if (item.PID == id) {
                    var user = gUser(item.UserID);
                    s += subitem.replace('{UserName}', user.UserName)
                         .replace('{Dept}', user.Dept)
                         .replace('{Version}', version.Name)
                         .replace('{Time}', item.Time)
                         .replace('{Display}', item.UserID == userID ? 1 : 0)
                         .replace(new RegExp("{ID}", "gm"), item.ID)
                         .replace('{Image}', user.Ico)
                         .replace('{Content}', item.Content);
                }
            });

            return s != "" ? h_subitem.replace('{Subitem}', s) : "";
        }
        state.annotations.forEach(function (item, index, array) { annotations.push(item) });
        annotations.sort(function (a, b) {
            return a['Time'] > b['Time'];
        });
        annotations.forEach(function (item, index, array) {
            var user = gUser(item.UserID);
            html += area.replace(new RegExp("{UserID}", "gm"), item.UserID)
                    .replace('{UserName}', user.UserName)
                    .replace('{Dept}', user.Dept)
                    .replace('{Version}', version.Name)
                    .replace('{Time}', item.Time)
                    .replace(new RegExp("{ID}", "gm"), item.ID)
                    .replace('{Subitem}', gSubitem(item.ID))
                    .replace('{Image}', user.Ico)
                    .replace('{Content}', item.Content);
        });
        return html;
    }
    $(".content").html(gRepaly());
}


/*初始化图层信息*/
function initLayerData() {
    var drawnItems = this.state.drawnItems;
    var annotations = this.state.annotations;
    drawnItems.clearLayers();
    annotations.map(function (anno, index) {
        if (!anno.LayerID) {
            return;
        }
        var item = anno.GraphData;
        //$scope.layersData.push(item);
        L.geoJson(item.geoJSON, {
            pointToLayer: function (feature, latlng) {
                if (item.layerType == L.Draw.Circle.TYPE) {
                    return L.circle(latlng, item.style);
                } else if (item.layerType == L.Draw.Marker.TYPE) {
                    return L.marker(latlng, { icon: new L.Icon.Default() });
                }
            },
            style: function (feature) {
                return item.style;
            },
            onEachFeature: function (feature, layer) {
                if (layer.getLayers) {
                    layer.getLayers().forEach(function (l) {
                        l._leaflet_id = item.id;
                        drawnItems.addLayer(l);

                        /*注册绑定事件*/
                        this.layerClick(l, anno);
                    })
                } else {
                    layer._leaflet_id = item.id;
                    /*注册绑定事件*/
                    this.layerClick(layer, anno);
                    drawnItems.addLayer(layer);
                }
            }.bind(this)
        });
    })
    this.setState({ drawnItems: drawnItems });
};

/*点击图层*/
function layerClick(layer, anno) {
    var that = this;
    layer.on({
        click: function (e) {
            var html = "<div class='_attention_desc_'>" + anno.Content + "</div>";
            /*点击弹出详情框*/
            this.bindPopup(html);
            /*解决弹框不响应bug*/
            if (!this._popup._container) {
                var popup = L.popup({}).setContent(this._popup._content);
                this.bindPopup(popup).openPopup();
            }
            //点击时右侧对应条目置为选中状态
            if (anno) {
                $(".item-area-wrapper").each(function () {
                    if ($(this).attr("id") == anno.ID) {
                        if (!$(this).hasClass("selected")) {
                            $(this).addClass("selected");
                        }
                    } else {
                        $(this).removeClass("selected");
                    }
                })
            }
        }
    });
};

/* 新建批注 */
function addAnnotationHtml(layer, layerType) {
    var addEl = "<div class='_add_input_'>" +
            "<div class='common-annotation-area' >" +
            "<textarea  class='mini-scroll-common _textarea' placeholder='输入批注意见' ></textarea>" +
            "<div class='btn-area active-btn'>" +
            "<div id='btn_annotation_ok' class='_btn-ok'>确定</div>" +
            "<div id='btn_annotation_cancel' class='_btn-cancel'>取消</div>" +
            "</div>" +
            "</div>" +
        "</div>";
    return addEl;
};


/*取消批注*/
function closeAnnotation(layer, type) {
    _popupState = false;
    layer.closePopup();
    /*移除临时标记*/
    if (type == 1) {
        layer.remove();
    }
};


/*保存批注*/
function saveAnnotation(layer, layerType) {
    var content = $("._textarea").val();
    if (!content) {
        return alert("意见不能为空！")
    }
    if (content.indexOf("#") >= 0 || content.indexOf("&") >= 0) {
        return alert("不允许输入 # & 关键字！")
    }
    var annotation = {};
    var graphData = {
        id: layer._leaflet_id,
        layerType: layerType,
        style: layer.options,
        geoJSON: layer.toGeoJSON()
    };
    var fileName = gFileName(this.state.version.FullPath);
    var userID = state.curUser.UserID;
    annotation = {
        DocumentID: getQueryString("ID"),
        VersionID: this.state.version.ID,
        Content: content,
        UserID: userID,
        //ProofreadType:proofType,
        //Flag:0,// 0：svg图批注，1：全文档批注
        //Num:$scope.docAnnotaions.length,
        //Type:type,
        GraphData: JSON.stringify(graphData),
        LayerID: layer._leaflet_id,
        LayerType: layerType,
        DocumentName: fileName
    };


    $.post('api/Office/SetAnnotation', {
        '': JSON.stringify(annotation)
    }, function (data) {
        /*注册绑定事件*/
        //layerClick(layer, data);
        _popupState = false;
        layer.closePopup();
        layer.unbindPopup();
        setState({ annotations: JSON.parse(data) });
        initLayerData();
        initMsg();
    });
};


function deleteMsg(id) {
    var version = state.version;
    var annotations = state.annotations;
    var commentModel = {
        DocumentID: getQueryString("ID"),
        VersionID: version.ID,
        DeleteID: id
    };
    var annotation;
    for (var i = 0; i < annotations.length; i++) {
        if (annotations[i].ID == id) {
            annotation = annotations[i];
            break;
        }
    }

    $.post('api/Office/Del', {
        '': JSON.stringify(commentModel)
    }, function (data) {
        if (annotation) {
            deleteAnnotation(annotation);
            setState({ annotations: JSON.parse(data) });
        } else {
            setState({ comments: JSON.parse(data) });
        }
        initMsg();
    });
}

/*删除批注*/
function deleteAnnotation(annotation) {
    var map = this.state.map;
    var drawnItems = this.state.drawnItems;
    var layer = map._layers[annotation.LayerID];
    layer.closePopup();
    drawnItems.removeLayer(layer);
};



function _showReMessageInput(parentID) {
    $(".ant-input-affix-wrapper").each(function () {
        if ($(this).parent().attr('id') == parentID) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function onMouseOverMsg(id, userID) {
    $("#" + id).css('background-color', '#f4fcfc');
    var uID = state.curUser.UserID;
    $("._msg,._delete,._subitemDelete").each(function () {
        if ($(this).parents(".item-area-wrapper").attr('id') == id) {
            if (($(this).hasClass('_delete') && userID == uID) || $(this).hasClass('_msg') || ($(this).hasClass('_subitemDelete') && $(this).attr('value') == '1'))
                $(this).show();
        } else {
            $(this).hide();
        }
    });

}

function onKeyDown(id) {
    var e = window.event || arguments.callee.caller.arguments[0];
    var version = this.state.version;
    if (e.keyCode == 13) {
        var content = $("#input_" + id).val();
        if (content == "") {
            return alert("评论不能为空！")
        }
        var userID = state.curUser.UserID;
        var commentModel = {
            DocumentID: getQueryString("ID"),
            UserID: userID,
            PID: id,
            VersionID: version.ID,
            DocumentName: gFileName(version.FullPath),
            Content: content,
            ToUsers: ""
        };

        $.post('api/Office/SetComment', {
            '': JSON.stringify(commentModel)
        }, function (data) {
            setState({ comments: JSON.parse(data) });
            initMsg();
        });
    }

}

function onMouseOutMsg(id) {
    $("#" + id).removeAttr('style');
    $("._msg,._delete,._subitemDelete").each(function () {
        $(this).hide();
    });
}


function _sendComment(e) {
    var version = selLi = this.state.version;
    var content = $("textarea.ant-input-textarea").val();
    if (content == "") {
        return alert("评论不能为空！")
    }
    var userID = state.curUser.UserID;
    var commentModel = {
        DocumentID: getQueryString("ID"),
        UserID: userID,
        PID: '',
        VersionID: version.ID,
        DocumentName: gFileName(version.FullPath),
        Content: content,
        ToUsers: ""
    };

    $.post('api/Office/setComment', {
        '': JSON.stringify(commentModel)
    }, function (data) {
        setState({ comments: JSON.parse(data) });
        initMsg();
        $("textarea.ant-input-textarea").val('');
    });

}

function _sendCommentReply(e, parentId) {
    e.preventDefault ? e.preventDefault() : (e.returnValue = false);
    var doc = prjId = this.props;
    var version = selInputLi = this.state
    var content = e.target.value;
    if (content == "") {
        return util.alert("warning", "评论不能为空！")
    }
    e.target.value = "";

    var commentModel = {
        prjID: prjId,
        type: 1,
        businessID: version.id,
        documentName: doc.Name + doc.Suffix,
        content: "#" + doc.Name + doc.Suffix + " " + content
    };
    if (!!parentId) {
        commentModel.parentID = parentId;
    }
    //接受人处理
    var toUsers = [];
    var contents = commentModel.content.split(" ");
    for (var i = 0; i < contents.length; i++) {
        var item = contents[i];
        var indexUser = item.indexOf("@");
        if (indexUser >= 0) {
            var name = item.substring(indexUser + 1, item.length);
            var toUser = selInputLi.find(name);
            if (toUser) {
                toUsers.push(toUser);
            }
        }
    }
    commentModel.toUsers = toUsers;

    var data = addComment(commentModel);
    if (!data.status) {
        return util.alert(false, data.info);
    }

}


function _removeComment(comment) {
    var map = drawnItems = annotations = comments = this.state;
    var resultData = removeComment(comment.id);
    if (resultData.status) {
        for (var i = comments.length - 1; i >= 0; i--) {
            var item = comments[i];
            if (item.id == comment.id) {
                comments.splice(i, 1);
            } else {
                if (!!comment.parentID) {
                    var chlidComments = item.chlids.filter(comment.id);
                    item.chlids = chlidComments;
                }
            }
        }
        if (!comment.parentID) {
            var annotation = annotations.find(comment.id);
            if (!!annotation) {
                var layer = map._layers[annotation.layerID];
                layer.closePopup();
                drawnItems.removeLayer(layer);
                annotations = annotations.filter(item.id != annotation.id)
            }
        }
        //this.setState({map:map,drawnItems:drawnItems,comments:comments,annotations:annotations});
    }
}


function _onSelectComment(data) {
    var commentId = data.id;
    var annotation;
    for (var i = 0; i < state.annotations.length; i++) {
        if (state.annotations[i].ID == commentId) {
            annotation = state.annotations[i];
            break;
        }
    }
    var drawnItems = this.state.drawnItems;
    if (annotation) {
        drawnItems.getLayers().forEach(function (item) {
            if (item._leaflet_id == annotation.LayerID) {
                var html = "<div class='_attention_desc_'>" +
                            annotation.Content
                "</div>";
                item.closePopup();
                item.unbindPopup();
                item.bindPopup(html);
                if (!item._popup._container) {
                    var popup = L.popup({}).setContent(item._popup._content);
                    item.bindPopup(popup).openPopup();
                }
            }
        })
    } else {
        drawnItems.getLayers().forEach(function (item) {
            item.closePopup();
            item.unbindPopup();
        });
    }
    $(".item-area-wrapper").each(function () {
        if ($(this).attr("id") == commentId) {
            if (!$(this).hasClass("selected")) {
                $(this).addClass("selected");
            }
        } else {
            $(this).removeClass("selected");
        }
    })
}
