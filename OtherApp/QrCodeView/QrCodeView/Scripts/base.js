var $monitor = {
    link: "",
    api: "",
    title: "",
    curRoutine: null,
    data: null,
    //时间戳
    timestamp: 1000 * 60,
    //页面ID
    idName: "",
    cacheCatalogID: "",
    cacheMenuCode: "",
    cacheTaskType: "",
    cacheSelectedUsers: null,
    cacheIsNewFlow: true,
    cacheCurrentProject: "", //缓存当前项目
    cacheIsYDT: false,//是否是从移动通调用
    cacheSSO: false, //是否单点登录
    firstLoad: true,
    listKeyName: "Name",
    //页面跳转
    go: function (link, id, title, callback) {
        if (id == "" || id == undefined) {
            id = link.replace(".", "");
        }

        var page = $(".page-views").find("li#" + id);
        var timestamp = Date.parse(new Date());

        if (link.indexOf('?') >= 0) {
            $monitor.api = link.substring(link.indexOf('?'));
            link = link.substring(0, link.indexOf('?'));
        }
        $monitor.link = link;
        $monitor.idName = id;
        $monitor.title = title;
        if (page.length == 0) {
            this.showLoading("加载中,请稍等...");
            var height = window.screen.height;
            if (getWXQueryString('IsYDT') == 1 || this.cacheIsYDT)
                height = height - getTopDownOffsetInYDT();
            var newnode = $(".page-views").append('<li class="page" style="height:' + height + 'px" id="' + id + '" date-timestamp="' + timestamp + '"><div class="page-view"></div></li>').find("li:nth-last-child(1)");
            var nodehtml = newnode.find(".page-view");
            nodehtml.load($monitor.link, function (responseTxt, statusTxt, xhr) {
                if (statusTxt == "success") {
                    newnode.addClass("md-show");
                    if (title) {
                        title = title.length > 10 ? title.substring(0, 10) + '...' : title;
                        nodehtml.find(".title").html(title);
                    }
                    //$monitor.addAnimation(newnode[0]);   
                    setTimeout("$monitor.hideLoading()", 10000);
                }
            });

        } else {
            var oldtimestamp = parseInt(page.attr("date-timestamp"));
            var dt = (timestamp - oldtimestamp) / $monitor.timestamp;

            //一分钟内不重新加载数据
            if (dt >= 1) {
                page.attr("date-timestamp", timestamp); //重置时间戳
                page.find(".page-view").load(link, function () {
                    page.addClass("md-show");
                });
            } else {
                page.addClass("md-show");
            }
        }
    },
    //添加动画
    addAnimation: function (e) {
        e.addEventListener("webkitTransitionEnd", this.hideLoading(), false);
    },
    //清除动画
    clearAnimation: function (e, callback) {
        e.removeEventListener("webkitTransitionEnd", $monitor.loadData);
    },
    //返回
    goback: function (e) {
        $(e).closest("li").remove();
    },
    scroll: function (wrapper_id, method, paras, main) {
        var pageindex = 1,
                mainClass = main && main != '' ? "." + main : ".main",
                $listWrapper = $('#' + wrapper_id);
        var pullInstance = new Pull($listWrapper, {
            scrollArea: $(mainClass),
            autoLoad: false, // 自动加载，加载完成立即判断是否触发加载 默认 true
            threshold: 80, // 滚动至底部多少距离触发onPullUp。默认 100，单位pxc
            click: true,
            onPullUp: function () {
                if (method) {
                    handlePullUpSuccess();
                }
            },

            // 下拉刷新回调方法，如果不存在该方法，则不加载下拉dom
            onPullDown: function () {
                if (method) {
                    handlePullDownSuccess();
                }
            }
        });

        var pullDownTotal = 0,
                pullUpTotal = 0;


        // 处理下拉刷新成功
        function handlePullDownSuccess() {
            pageindex = 1;
            load();
            pullInstance.pullDownSuccess();
            pullInstance.resetPullUpDone();
        }

        // 处理下拉刷新失败
        function handlePullDownFailed() {
            pullInstance.pullDownFailed();
        }

        //首次加载数据
        if (pageindex == 1) {
            if (method)
                load();
            //$(".bb_pull,.bb_pull-up").hide();
        }

        function load() {
            var obj = {
                "pageindex": pageindex,
                "pagesize": pageSize
            };
            if (paras) {
                for (var key in paras) {
                    obj[key] = paras[key];
                }
            }
            eval(method)(obj, function (result) {
                if (result > 0) {
                    if (result < pageSize) {
                        pageindex = 0;
                        pullInstance.pullUpDone();
                    } else {
                        pageindex = pageindex + 1;
                        pullInstance.pullUpSuccess();
                    }
                } else {
                    pageindex = 0;
                    pullInstance.pullUpDone();
                }
            });
        }

        // 处理上拉加载成功
        function handlePullUpSuccess() {
            load();
        }

        // 处理上拉加载失败
        function handlePullUpFailed() {
            pullInstance.pullUpFailed();
        }
    },
    showLoading: function (value) {
        $("#_loading_text").html(value);
        $(".loading,#bgdiv").show();
    },
    hideLoading: function (e) {
        $(".loading,#bgdiv").hide();
    }
};



//获取时间
function gTimeFormat(time) {
    if (time && time.length >= 10) {
        return time.substring(0, 10);
    } else {
        return "";
    }
}

//获取地址栏参数,如果参数不存在则返回空字符串
function getWXQueryString(key, url) {
    if (typeof (url) == "undefined") {
        url = $monitor.api;
        if (!url)
            url = window.location.href
    }
    var re = new RegExp("[?&]" + key + "=([^\\&]*)", "i");
    var a = re.exec(url);
    if (a == null) return "";
    return a[1];
}

function IsPC() {
    var userAgentInfo = navigator.userAgent;
    var Agents = ["Android", "iPhone",
        "SymbianOS", "Windows Phone",
        "iPad", "iPod"];
    var flag = 40;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = 0;
            break;
        }
    }
    return flag;
}

function getPhoneType() {
    var isAndroid = true;
    var userAgentInfo = navigator.userAgent;
    var Agents = ["iPhone", "iPad", "iPod"]
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            isAndroid = false;
            break;
        }
    }
    return isAndroid;
}

function isJSON(str) {
    if (typeof str == 'string') {
        try {
            var obj = JSON.parse(str);
            if (typeof obj == 'object' && obj) {
                return true;
            } else {
                return false;
            }

        } catch (e) {
            return false;
        }
    }
}

function showMsg(content, type) {
    layer.open({
        content: content,
        btn: '确认'
    });
}

function setMsgState(e) {
    var val = $(e).attr('value');
    if (val == "1") {
        $(e).removeClass('but-state');
        e.value = "0";
    } else {
        $(e).addClass('but-state');
        e.value = "1";
    }
}

function setReadonly(input) {
    $(input).attr("readonly", "readonly");
    $(input).css("-webkit-user-select", "none");
}

function getFileName(fullName) {
    if (fullName.lastIndexOf('_') <= fullName.indexOf('_')) {
        return fullName.substring(fullName.indexOf('_') + 1);
    } else {
        if (fullName.lastIndexOf('.') < fullName.lastIndexOf('_'))
            return fullName.substring(fullName.indexOf('_') + 1, fullName.lastIndexOf('_'));
        else
            return fullName.substring(fullName.indexOf('_') + 1);
    }
}


function getSendTime(time) {
    var date = new Date(time.substring(0, 16).replace('T', ' ')).getTime() - new Date(new Date().Format("yyyy-MM-dd hh:mm:ss")).getTime();
    var minutes = parseInt(Math.abs(date) / 1000 / 60);
    if (Math.abs(minutes) > 10) {
        if (Math.abs(minutes) < 60)
            return Math.abs(minutes) + "分钟前";
        else if (Math.abs(minutes) > 60 && Math.abs(minutes) < 1440)
            return parseInt(Math.abs(minutes) / 60) + "小时前";
        else
            return time.substring(0, 16).replace('T', ' ');
    } else
        return "刚刚";
}


function convertTime(value) {
    if (!value) value = new Date().Format("yyyy-MM-dd hh:mm:ss");
    if (value && value.indexOf('T') >= 0)
        value = value.replace('T', ' ');
    value = value.replace(/^([^\s]+).*$/, '$1').replace(/[^\d]/g, '-');
    if (value.indexOf('-') >= 0) {
        var t = value.split('-');
        if (t.length == 3) {
            value = t[0] + "-" + (t[1].length < 2 ? "0" + t[1] : t[1]) + "-" + (t[2].length < 2 ? "0" + t[2] : t[2]);
        }
    }
    return value;
}

//获取附件名称
function gFileName(title) {
    if (title)
        return title.substring(title.indexOf('_') + 1, title.lastIndexOf('_'));
    else
        return "";
}

function gUserIcoByID(userID) {
    var userIco = "../../Scripts/css/images/user-ico.png";
    if (WXUsers) {
        for (var i = 0; i < WXUsers.length; i++) {
            if (WXUsers[i].ID == userID) {
                userIco = WXUsers[i].Avatar && WXUsers[i].Avatar != '' ? WXUsers[i].Avatar : userIco;
                break;
            }
        };
    }

    return userIco;
}


function refreshNotice(isFlow) {
    $.ajax({
        url: goodwayApiURL + "/api/EntryMenus",
        type: 'GET',
        cache: false,
        async: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Custom-Auth-WeiXin-User', userInfo.Account);
        },
        success: function (data, textStatus) {
            if (data.length > 0) {
                data.forEach(function (item, index, array) {
                    if (indexVue) {
                        var menus = indexVue.index_Menu_items;
                        if (menus) {
                            for (var i = 0; i < menus.length; i++) {
                                var datas = menus[i].Datas;
                                for (var j = 0; j < datas.length; j++) {
                                    if (datas[j].ID == item.ID) {
                                        datas[j].Count = item.Count;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (isFlow) {
                        if (typeof (flowListVue) != "undefined") {
                            var flowList = flowListVue.flowlist_items;
                            if (flowList) {
                                for (var i = 0; i < flowList.length; i++) {
                                    if (flowList[i].ID == item.ID) {
                                        flowList[i].Count = item.Count;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getImageType(suffix) {
    var path = "../../Scripts/css/images/";
    switch (suffix) {
        case '.png':
        case '.jpg':
        case '.jpeg':
        case '.gif':
        case '.bmp':
            path += "img.png";
            break;
        case '.doc':
        case '.docx':
            path += "word.png";
            break;
        case '.xls':
        case '.xlsx':
            path += "excel.png";
            break;
        case '.dwg':
            path += "dwg.png";
            break;
        case '.txt':
            path += "file.png";
            break;
        case '.pdf':
            path += "pdf.png";
            break;
        default:
            path += "other.png";
            break;
    }
    return path;
}

function getWeather() {
    return [{ ID: "clear", Name: "晴天" }, { ID: "cloud", Name: "多云" }, { ID: "rain", Name: "雨天" }, { ID: "mist", Name: "雾天" }, { ID: "snow", Name: "下雪" }, { ID: "thunderstorm", Name: "雷雨" }];
}

function previewImage(curImgUrl) {
    wx.previewImage({
        current: curImgUrl, // 当前显示图片的http链接
        urls: [curImgUrl] // 需要预览的图片http链接列表
    });
}

function downloadFile(fileName) {
    var url = goodwayApiURL + "/api/Files/" + encodeURI(fileName);
    if (IsPC() > 0) {
        window.open(url);
    } else {
        wx.previewFile({
            url: url, // 需要预览文件的地址(必填，可以使用相对路径)
            name: getFileName(fileName), // 需要预览文件的文件名(不填的话取url的最后部分)
            size: 1 // 需要预览文件的字节大小(必填)
        });
    }
}

function viewAttachment(isMis, suffix, fileName) {
    var url = goodwayApiURL + "/api/Files/" + encodeURI(fileName);
    switch (suffix) {
        case '.png':
        case '.jpg':
        case '.jpeg':
        case '.gif':
        case '.bmp':
            if (IsPC() > 0) {
                return "downloadFile('" + fileName + "')";
            } else {
                if (isMis)
                    return "previewImage('" + url + "')";
                else
                    return "previewImage('" + fileName + "')";
            }
            break;
        default:
            if (isMis || IsPC() > 0)
                return "downloadFile('" + fileName + "')";
            else {
                return "previewImage('" + fileName + "')";
            }
            break;
    }
}

function inputType(valueType) {
    return "text";
}

function getDataEnum(json) {
    if (json) {
        var lists = new Array();
        var data = JSON.parse(json);
        for (var i = 0; i < data.length; i++) {
            lists.push(data[i].Name);
        }
        return lists.join(',');
    } else {
        return '';
    }
}

function closeLayout() {
    layer.closeAll();
}


function getTableCount(json) {
    if (json) {
        var data = JSON.parse(json);
        return '(' + data.length + '条)';
    }
}

function setDate(e) {
    var now = new Date();
    $(e).mobiscroll().date({
        theme: 'mobiscroll',
        lang: 'zh',
        display: 'bottom',
        dateOrder: 'yymmdd',
        dateFormat: 'yy-mm-dd',
        startYear: now.getFullYear() - 10,
        endYear: now.getFullYear() + 10,
        labels: ['年', '月', '日'],
        showLabel: true, //是否显示labels  
        minWidth: 100
    });
    //alert('22')
    //if (getWXQueryString('IsYDT') == 1 || $monitor.cacheIsYDT) {

    //    var currentBottom = parseInt($(".dwwr").css("bottom"), 10);
    //    alert(currentBottom)
    //    $(".dwwr").css("bottom", currentBottom + getBotUpOffsetInYDT());
    //}
}

function getFields(isDetail) {
    if (isDetail)
        return flowDetailVue.detail_FormDic_items;
    else {
        if ($monitor.cacheIsNewFlow)
            return flowLaunchVue.flow_FormDic_items;
        else
            return flowStorageVue.flow_storage_FormDic_items;
    }
}


function setFormDic(isDetail) {
    var fields = getFields(isDetail);
    if (fields && fields.length > 0) {
        for (var i = 0; i < fields.length; i++) {
            if (eval(fields[i].IsEdit)) {
                switch (fields[i].ValueType.toLocaleLowerCase()) {
                    case 'date':
                    case 'text':
                    case 'textarea':
                        var inputs = $("#" + (isDetail ? '_detail_formDic' : ($monitor.cacheIsNewFlow ? '_launch_formDic' : '_storage_formDic'))).find((fields[i].ValueType.toLocaleLowerCase() == 'textarea' ? 'textarea' : 'input') + '[id="' + fields[i].Key + '"]');
                        if (inputs.length > 0)
                            var value = inputs[0].value;
                        fields[i].Value = value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

function enumOnFocus(key, isDetail) {
    var enumArray = new Array();
    var data = getFields(isDetail);
    if (data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].Key == key) {
                if (data[i].ValueType == 'enum') {
                    var enumJson = JSON.parse(data[i].Settings).SelectList;
                    if (enumJson) {
                        var enumData = JSON.parse(enumJson);
                        for (var j = 0; j < enumData.length; j++) {
                            var newEnumModel = {
                                ID: enumData[j].ID,
                                Name: enumData[j].Name,
                                Key: key,
                                Onclick: "enumSelect('" + enumData[j].ID + "','" + enumData[j].Name + "','" + key + "','0',''," + isDetail + ")"
                            };
                            enumArray.push(newEnumModel);
                        }
                    }
                }
                if (data[i].ValueType == 'list') {
                    var data = JSON.parse(data[i].Settings);
                    if (data.type) {

                        enumArray = getListEnum(data, key, 0, "", isDetail, getListParams(key, isDetail));
                    }
                }
            }
        }
    }
    var arr = enumArray.length > 0 ? enumArray : null;
    if (isDetail)
        flowDetailVue.detail_Enum_items = arr;
    else {
        if ($monitor.cacheIsNewFlow)
            flowLaunchVue.flow_Enum_items = arr;
        else
            flowStorageVue.flow_storage_Enum_items = arr;
    }
    setFormDic(isDetail);
}


function getListParams(key, isDetail) {
    var formDic = null;
    var params = "";
    if (isDetail)
        formDic = flowDetailVue.detail_FormDic_items;
    else {
        if ($monitor.cacheIsNewFlow)
            formDic = flowLaunchVue.flow_FormDic_items;
        else
            formDic = flowStorageVue.flow_storage_FormDic_items;
    }
    if (formDic && formDic != null) {
        for (var i = 0; i < formDic.length; i++) {
            if (formDic[i].Key == key) {
                var data = formDic[i];
                if (data && data.Settings) {
                    data = JSON.parse(data.Settings);
                    if (typeof (data.urlParam) != "undefined") {
                        if (data.urlParam && data.urlParam.length > 0) {
                            for (var p = 0; p < data.urlParam.length; p++) {
                                switch (data.urlParam[p].type) {
                                    case 'main':
                                        var value = "";
                                        if (formDic != null) {
                                            formDic.forEach(function (item, index, array) {
                                                if (item.Key == data.urlParam[p].key) {
                                                    switch (item.ValueType) {
                                                        case 'enum':
                                                        case 'list':
                                                            var enumData = JSON.parse(item.Value);
                                                            var enumArray = new Array();
                                                            if (enumData && enumData.length > 0) {
                                                                for (var i = 0; i < enumData.length; i++) {
                                                                    enumArray.push(enumData[i].ID);
                                                                }
                                                            }
                                                            value = enumArray.join(',');
                                                            break;
                                                        default:
                                                            value = item.Value;
                                                            break;
                                                    }
                                                }
                                            });
                                        }
                                        params += "&" + data.urlParam[p].key + "=" + value;
                                        break;
                                    case 'hidden-main':
                                        params += "&" + data.urlParam[p].key + "=" + data.urlParam[p].value;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    return params;
}

function getListEnum(data, key, formType, subID, isDetail, params) {
    var enumArray = new Array();
    $.ajax({
        url: goodwayApiURL + "/api/SelectorList?ID=" + data.type + (typeof (params) != "undefined" ? params : ""),
        type: 'GET',
        async: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Custom-Auth-WeiXin-User', userInfo.Account);
        },
        success: function (result, textStatus) {
            if (result.length > 0) {
                for (var j = 0; j < result.length; j++) {
                    var newEnumModel = {
                        ID: result[j].ID,
                        Name: result[j].Name,
                        Key: key,
                        Style: data.multiple == "1" ? "width:calc(100% - 18px);float:left;" : "",
                        Onclick: "listEnumSelect(this, '" + result[j].ID + "','" + result[j].Name + "','" + key + "', '" + data.multiple + "', '" + formType + "','" + subID + "', " + isDetail + ")"
                    };
                    enumArray.push(newEnumModel);
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    return enumArray;
}

function listEnumSelect(event, id, name, key, multiple, formType, subID, isDetail) {
    var sel = $(event).find('i');
    if (multiple == "1") {
        if (sel.hasClass('display')) {
            sel.removeClass('display');
        } else {
            sel.addClass('display');
        }
    } else {
        enumSelect(id, name, key, formType, subID, isDetail);
    }
}

function textFormat(value, valueType) {
    switch (valueType) {
        case 'date':
            value = convertTime(value);
            break;
        case 'enum':
        case 'list':
            value = getDataEnum(value);
            break;
        case 'table':
        case 'complexTable':
            value = getTableCount(value);
            break;
        case 'attachment':
        case 'multiAttachment':
            value = '(' + (value ? value.split(',').length : 0) + '个)';
            break;
        default:
            break;
    }
    return value;
}

function getFormFiled(isDetail) {
    var formDic = null;
    var formType = getWXQueryString("FormType");
    switch (eval(formType)) {
        case 0:
            formDic = getFields(isDetail);
            break;
        case 1:
            var sublist = flowSublistDetailVue.sublistDetail_items;
            if (sublist && sublist.length > 0) {
                var subID = getWXQueryString("SubID");
                for (var i = 0; i < sublist.length; i++) {
                    if (sublist[i].ID == subID) {
                        formDic = sublist[i].Datas;
                        break;
                    }
                }
            }
            break;
        case 2:
            var arrs = new Array();
            arrs.push(flowDetailVue.detail_Comment_Attachment);
            formDic = arrs;
            break;
        case 3:
            var arrs = new Array();
            var msgAtt = sendMessageListVue.send_message_attachments;
            if (msgAtt)
                arrs.push(msgAtt);
            formDic = arrs;
            break;
        case 4:
            var arrs = new Array();
            var msgAtt = messageReplayVue.send_message_attachments;
            if (msgAtt)
                arrs.push(msgAtt);
            formDic = arrs;
            break;
        default:
            break;
    }
    return formDic;
}

function getFormDicOnClick(formDic, isDetail, isRowTable) {
    var value = "", type = isRowTable ? 'complexTable' : getWXQueryString("Type");
    var isEdit = eval(formDic.IsEdit);
    switch (formDic.ValueType) {
        case 'enum':
            if (isEdit) {
                var layer = ($monitor.cacheIsNewFlow || isDetail) ? "_enum_layer" : "_storage_enum_layer";
                value = "openLayer('" + layer + "'," + isDetail + ")";
            }
            break;
        case 'list':
            if (isEdit) {
                var layer = ($monitor.cacheIsNewFlow || isDetail) ? "_list_enum_layer" : "_storage_list_enum_layer";
                value = "openListLayer('" + layer + "','" + formDic.Key + "', 0, '', " + isDetail + ")";
            }
            break;
        case 'table':
        case 'complexTable':
            value = "showCurrentPage('/Flow/Sublist?SublistKey=" + formDic.Key + "&Type=" + type + "&IsDetail=" + isDetail + "', '_sublist" + formDic.Key + "','" + formDic.KeyName + "')"
            break;
        case 'attachment':
        case 'multiAttachment':
            value = "showCurrentPage('/Flow/Attachment?AttachmentKey=" + formDic.Key + "&Type=" + type + "&FormType=0&IsDetail=" + isDetail + "', '_attachment" + formDic.Key + "','" + formDic.KeyName + "')"
            break;
        default:
            break;

    }
    return value;
}

//底部对话框
function openLayer(id, isDetail) {
    if (isDetail)
        flowDetailVue.detail_SelectedUsers = null;
    else {
        if ($monitor.cacheIsNewFlow)
            flowLaunchVue.flow_SelectedUsers = null;
        else
            flowStorageVue.flow_storage_SelectedUsers = null;
    }
    layer.open({
        btn: ['取消'],
        skin: 'footer',
        className: 'task_footer_layer',
        shadeClose: true,
        content: $("#" + id).html()
    });

    if (getWXQueryString('IsYDT') == 1 || $monitor.cacheIsYDT) {
        var currentBottom = parseInt($(".layui-m-layer-footer").css("bottom"), 10);
        $(".layui-m-layer-footer").css("bottom", currentBottom + getBotUpOffsetInYDT());
    }
}

function openCommentLayer(routinID, isDetail) {
    var routins = null;
    if (isDetail)
        routins = flowDetailVue.detail_Button_items;
    else {
        if ($monitor.cacheIsNewFlow)
            routins = flowLaunchVue.flow_Button_items;
        else
            routins = flowStorageVue.flow_storage_Button_items;
    }
    $monitor.curRoutine = null; //防止重复执行
    if (routins) {
        routins.forEach(function (item, index, array) {
            if (item.ID == routinID) {
                openComment(item);
                return;
            }
        });
    }
}

//弹出意见
function openComment(item) {
    if (!item) item = $monitor.curRoutine;
    $("#_task_selected_users").html('');
    if (item.OwnerType == "1" || item.OwnerType == "2")
        $("#_taskNextUser").show();
    else
        $("#_taskNextUser").hide();
    $("#_comment_submit").html("确定(" + item.RoutineName + ")");
    $monitor.curRoutine = item;
    layer.open({
        skin: 'footer',
        className: 'comment_bg',
        shadeClose: true,
        content: $("#_comment_layer").html()
    });
}

function setDefaultComment(e) {
    var textareas = $("textarea");
    for (var i = 0; i < textareas.length; i++) {
        if (textareas[i].id == '_taskComment') {
            textareas[i].innerText = e.innerText;
            textareas[i].value = e.innerText;
        }
    }
}

function getLocalImgData(id) {
    wx.getLocalImgData({
        localId: id, // 图片的localID
        success: function (res) {
            return res.localData; // localData是图片的base64数据，可以用img标签显示
        }
    });
}

function getFormDic(formDic, isDetail) {
    var fields = getFields(isDetail);
    var arr = new Array();
    if (fields && fields.length > 0) {
        arr.push("\"ID\":\"" + formDic.FormInstanceID + "\"");
        for (var i = 0; i < fields.length; i++) {
            //if (eval(fields[i].IsEdit)) {
            var value = "";
            switch (fields[i].ValueType) {
                case 'enum':
                    var enumValues = new Array();
                    var enums = JSON.parse(fields[i].Value);
                    if (enums.length > 0) {
                        for (var j = 0; j < enums.length; j++) {
                            enumValues.push(enums[j].ID);
                        }
                    }
                    value = enumValues.length > 0 ? enumValues.join(',') : '';
                    break;
                case 'date':
                    value = convertTime(fields[i].Value);
                    break;
                case 'table':
                case 'complexTable':
                    var table = JSON.parse(fields[i].Value);
                    for (var t = 0; t < table.length; t++) {
                        if (table[t].ID && table[t].ID.indexOf('_sublist_') >= 0) {
                            table[t].ID = "";
                        }
                    }
                    value = JSON.stringify(table).replace(/\\/g, "").replace(/\"/g, "\\\"");
                    break;
                case 'list':
                    var IDs = new Array(), Names = new Array();
                    var enums = JSON.parse(fields[i].Value);
                    if (enums.length > 0) {
                        for (var j = 0; j < enums.length; j++) {
                            IDs.push(enums[j].ID);
                            Names.push(enums[j].Name);
                        }
                    }
                    var listKey = fields[i].Key;
                    var listKeyName = fields[i].Key + $monitor.listKeyName;
                    if (listKey && listKey.indexOf($monitor.listKeyName) >= 0) {
                        listKey = fields[i].Key.replace($monitor.listKeyName, "");
                        listKeyName = fields[i].Key;
                    }
                    arr.push("\"" + listKey + "\":\"" + (IDs.length > 0 ? IDs.join(',') : '') + "\"");
                    arr.push("\"" + listKeyName + "\":\"" + (Names.length > 0 ? Names.join(',') : '') + "\"");
                    break;
                default:
                    value = fields[i].Value;
                    break;

            }
            if (fields[i].ValueType != 'list') {
                arr.push("\"" + fields[i].Key + "\":\"" + value + "\"");
            }
            // }
        }
    }
    return arr.length > 0 ? '{' + arr.join(',') + '}' : "";
}

function flowSave(data, isDetail) {
    var formDic = getFormDic(data, isDetail);
    $.ajax({
        url: goodwayApiURL + "/api/FormDef/" + data.FormInstanceID + "/Save?FlowCode=" + data.FlowCode,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ FormDic: formDic }),
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Custom-Auth-WeiXin-User', userInfo.Account);
        },
        success: function (data, textStatus) {
            if (data) {
                showMsg("暂存成功!");
            }
            $monitor.hideLoading();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //提示
            layer.open({
                content: eval(jqXHR.responseText)
                        , btn: '确认'
            });
            $monitor.hideLoading();
        }
    });
}

function openFlowSelectUser(e, isDetail) {
    setLayerZIndex(-1);
    showCurrentPage('/Org/User?vue=' + (isDetail ? 'flow' : 'start'), '_txtTaskNextUser', '')
}

function setLayerZIndex(value) {
    $(".layui-m-layer").css('z-index', value);
}

function setTaskUsers(isDetail) {
    var textareas = $("textarea");
    for (var i = 0; i < textareas.length; i++) {
        var users = null;
        if (isDetail)
            users = flowDetailVue.detail_SelectedUsers;
        else {
            if ($monitor.cacheIsNewFlow)
                users = flowLaunchVue.flow_SelectedUsers;
            else
                users = flowStorageVue.flow_storage_SelectedUsers;
        }
        if (users && users.length > 0) {
            var arr = new Array();
            users.forEach(function (item, index, array) {
                arr.push(item.Name);
            });
        }
        if (textareas[i].id == '_task_selected_users')
            textareas[i].innerText = arr.join(',');
    }
}

function getUserIDs(curRoutine, isDetail) {
    var userIDs = "";
    switch (curRoutine.OwnerType) {
        case 1:
        case 2:
            var users = new Array();
            var data = null;
            if (isDetail)
                data = flowDetailVue.detail_SelectedUsers;
            else {
                if ($monitor.cacheIsNewFlow)
                    data = flowLaunchVue.flow_SelectedUsers;
                else
                    data = flowStorageVue.flow_storage_SelectedUsers;
            }
            if (data) {
                data.forEach(function (item, index, array) {
                    users.push(item.ID);
                });
            }
            userIDs = users.join(',');
            break;
        case 3:
            userIDs = curRoutine.OwnerUserIDs;
            break;
        default:
            break;
    }
    return userIDs;
}

function submitTask(isClose, isDetail) {
    setFormDic(isDetail);
    var curRoutine = $monitor.curRoutine;
    var data = null;
    if (isDetail)
        data = flowDetailVue.detail_Data;
    else {
        if ($monitor.cacheIsNewFlow)
            data = flowLaunchVue.flow_Data;
        else
            data = flowStorageVue.flow_storage_Data;
    }
    if (curRoutine && curRoutine.ID == "Save") {
        $monitor.showLoading("暂存中,请稍后！");
        closeLayout();
        flowSave(data, isDetail);
        return;
    }
    var isPass = true;
    if (curRoutine.OwnerType == "1" || curRoutine.OwnerType == "2") {
        var users = null;
        if (isDetail)
            users = flowDetailVue.detail_SelectedUsers;
        else {
            if ($monitor.cacheIsNewFlow)
                users = flowLaunchVue.flow_SelectedUsers;
            else
                users = flowStorageVue.flow_storage_SelectedUsers;
        }
        if (!users || users.length <= 0) {
            showMsg("审批人不能为空!");
            isPass = false;
            return;
        }
    }
    var comments = $('textarea[id="_taskComment"]');
    var comment = comments[comments.length - 1].value;
    if (((isDetail && !flowDetailVue.detail_HideAdvice) || (!isDetail)) && curRoutine.MustInputComment) {
        if (!comment) {
            showMsg("审批意见不能为空!");
            isPass = false;
            return;
        }
    }
    if (data.FormDic != null) {
        data.FormDic.forEach(function (item, index, array) {
            var notNull = item.NotNull;
            if (notNull) {
                notNull = JSON.parse(notNull);
                var isExistRoutine = false;
                for (var j = 0; j < notNull.length; j++) {
                    if (notNull[j].Key == curRoutine.ID && eval(notNull[j].Value.toLocaleLowerCase()))
                        isExistRoutine = true;
                }
                var div = isDetail ? '_detail_formDic' : ($monitor.cacheIsNewFlow ? '_launch_formDic' : '_storage_formDic');
                var inputs = $("#" + div).find('input[id="' + item.Key + '"]');
                for (var i = 0; i < inputs.length; i++) {
                    if (item.ValueType != 'table' && item.ValueType != 'complexTable') {
                        if (!inputs[i].value && isExistRoutine) {
                            showMsg(item.KeyName + "不能为空!");
                            isPass = false;
                            return;
                        }
                    } else {
                        if (inputs[i].value.indexOf('0') >= 0 && isExistRoutine) {
                            showMsg(item.KeyName + "不能为空!");
                            isPass = false;
                            return;
                        }
                    }
                }
            }
        });
    }

    if (curRoutine && data && isPass) {
        $monitor.showLoading("处理中,请稍后！");
        var taskDTO = {
            NextRoutingID: curRoutine.ID,
            TaskExecID: data.ID,
            FormInstanceID: data.FormInstanceID,
            FormInstanceCode: isDetail ? data.FormInstanceCode : data.FlowCode,
            FlowCode: data.FlowCode,
            UserIDs: getUserIDs(curRoutine, isDetail),
            Comment: comment,
            ExecUserID: userInfo.ID,
            FormDic: getFormDic(data, isDetail)
        };
        closeLayout();
        $.ajax({
            url: goodwayApiURL + "/api/Tasks/" + (isDetail ? taskID : 0) + "/SubmitTask",
            type: 'POST',
            data: taskDTO,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Custom-Auth-WeiXin-User', userInfo.Account);
            },
            success: function (data, textStatus) {
                if (isClose) {
                    closeWindow();
                } else {
                    $("#_task_detail_back").click();
                    if (!$monitor.cacheIsNewFlow) {
                        $("#_storage_back").click();
                        var obj = {
                            "pageindex": 1,
                            "pagesize": pageSize,
                            "FlowCode": flowCode
                        };
                        loadMyTasks(obj, function (result) { });
                    }
                    refreshNotice(true);
                    if (isDetail)
                        undo();
                }
                $monitor.hideLoading();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //提示
                layer.open({
                    content: eval(jqXHR.responseText)
                        , btn: '确认'
                });
                $monitor.hideLoading();
            }
        });
    }
}


function textSize(text) { //计算文字的宽度
    var span = document.createElement("span");
    var result = {};
    result.width = span.offsetWidth;
    result.height = span.offsetWidth;
    span.style.visibility = "hidden";
    document.body.appendChild(span);
    if (typeof span.textContent != "undefined")
        span.textContent = text;
    else span.innerText = text;
    result.width = span.offsetWidth - result.width;
    result.height = span.offsetHeight - result.height;
    span.parentNode.removeChild(span);
    return result;
}


function idIsNull(data, index) {
    if (data.ID != "undefined" && data.ID != undefined && data.ID != '') {
        return data.ID;
    } else {
        return '_sublist_' + index;
    }
}

function openListLayer(id, key, formType, subID, isDetail, keyName) {
    if (isDetail)
        flowDetailVue.detail_SelectedUsers = null;
    else {
        if ($monitor.cacheIsNewFlow)
            flowLaunchVue.flow_SelectedUsers = null;
        else
            flowStorageVue.flow_storage_SelectedUsers = null;
    }

    layer.open({
        btn: ['确认'],
        skin: 'footer',
        className: 'task_footer_layer',
        shadeClose: true,
        content: $("#" + id).html(),
        yes: function (index) {
            var lists = new Array();
            var is = $(".task_footer_layer").find('li i');
            $(is).each(function (index, event) {
                if (!$(event).hasClass('display')) {
                    var li = $(event).closest("li");
                    lists.push({ ID: li.attr('id'), Name: li.attr('name') });
                }
            });
            if (lists.length > 0) {
                var formDic = null;
                switch (eval(formType)) {
                    case 0:
                        formDic = getFields(isDetail);
                        break;
                    case 1:
                        var sublist = flowSublistDetailVue.sublistDetail_items;
                        if (sublist && sublist.length > 0) {
                            for (var i = 0; i < sublist.length; i++) {
                                if (sublist[i].ID == subID) {
                                    formDic = sublist[i].Datas;
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                for (var i = 0; i < formDic.length; i++) {
                    if (formDic[i].Key == key) {
                        switch (eval(formType)) {
                            case 0:
                                formDic[i].Value = JSON.stringify(lists);
                                formDic[i].Value = JSON.stringify(lists);
                                break;
                            case 1:
                                var IDs = new Array(), Names = new Array();
                                for (var j = 0; j < lists.length; j++) {
                                    IDs.push(lists[j].ID);
                                    Names.push(lists[j].Name);
                                }

                                setListObjKey(key, subID, IDs.join(','), Names.join(','), isDetail, keyName);
                                formDic[i].Value = IDs.join(',');
                                break;
                        }
                    }
                }
            }
            closeLayout();
        }
    });

    if (getWXQueryString('IsYDT') == 1 || $monitor.cacheIsYDT) {
        var currentBottom = parseInt($(".layui-m-layer-footer").css("bottom"), 10);
        $(".layui-m-layer-footer").css("bottom", currentBottom + getBotUpOffsetInYDT());
    }
}

function enumSelect(id, name, key, formType, subID, isDetail) {
    var formDic = null;
    switch (eval(formType)) {
        case 0:
            formDic = getFields(isDetail);
            break;
        case 1:
            var sublist = flowSublistDetailVue.sublistDetail_items;
            if (sublist && sublist.length > 0) {
                for (var i = 0; i < sublist.length; i++) {
                    if (sublist[i].ID == subID) {
                        formDic = sublist[i].Datas;
                        break;
                    }
                }
            }
            break;
        default:
            break;
    }
    if (formDic) {
        for (var i = 0; i < formDic.length; i++) {
            if (formDic[i].Key == key) {
                var lists = new Array();
                lists.push({ ID: id, Name: name });
                formDic[i].Value = JSON.stringify(lists);
                var IDs = new Array(), Names = new Array();
                for (var j = 0; j < lists.length; j++) {
                    IDs.push(lists[j].ID);
                    Names.push(lists[j].Name);
                }
                setListObjKey(key, subID, IDs.join(','), Names.join(','), isDetail);
            }
        }
    }
    closeLayout();
}

function getListKeyAndName(data, key) {
    var keyName = new Array();
    if (data) {
        var index = key.toLowerCase().indexOf('name');
        var k = index >= 0 ? key.substring(0, index) : key;
        for (var item in data) {
            if (item.indexOf(k) >= 0 && key != item) {
                keyName.push(k);
                keyName.push(item);
                break;
            }
        }
    }
    return keyName;
}

function setListObjKey(key, subID, ids, names, isDetail, keyName) {
    var subList = [];
    if (isDetail)
        subList = flowDetailVue.sublist_lists;
    else {
        if ($monitor.cacheIsNewFlow)
            subList = flowLaunchVue.sublist_lists;
        else
            subList = flowStorageVue.sublist_storage_lists;
    }
    if (subList && subList.length > 0) {
        for (var l = 0; l < subList.length; l++) {
            var ID = idIsNull(subList[l], l);
            if (ID == subID) {
                var isExistKey = false;
                if ((typeof (keyName) == "undefined" || keyName == "") && key != "")
                    keyName = key + $monitor.listKeyName;
                for (var k in subList[l]) {
                    if (k == key) {
                        isExistKey = true;
                        subList[l][k] = ids;
                        subList[l][keyName] = names;
                        break;
                    }
                }
                if (!isExistKey) {
                    subList[key] = ids;
                    subList[keyName] = names;
                }
            }
        }
    }
}



function gImageID(imageName) {
    if (imageName && imageName.indexOf('_') >= 0)
        return imageName.substring(0, imageName.indexOf('_'));
    else
        return imageName;
}

function gUrl(page) {
    window.location.href = page;
}

function gTitle(title) {
    if (title && title.length > 10)
        return encodeURI(title.substring(0, 10) + '...');
    else
        return encodeURI(title);
}

function setTitle() {
    var title = decodeURI(getWXQueryString("Title"));
    $('.title').html(title);
}


// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function getFileSize(bytes) {
    if (bytes === 0) return '0 B';
    var k = 1024;
    sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    i = Math.floor(Math.log(bytes) / Math.log(k));
    return (bytes / Math.pow(k, i)).toFixed(0) + ' ' + sizes[i];
}

function setLayuiZIndex(isBottom) {
    if ($('.layui-m-layer').length > 0) {
        $('.layui-m-layer').css('z-index', isBottom ? '-1' : '999999');
    }
}

function getMessageContent(value) {
    var newString = value.replace(/\n/g, '_@').replace(/\r/g, '_#');
    newString = newString.replace(/_#_@/g, '<br/>');
    newString = newString.replace(/_@/g, '<br/>');
    newString = newString.replace(/\s/g, '&nbsp;');
    return newString;
}

function getOrgUser(orgList, depid) {
    var arr = new Array();
    if (orgList && orgList.length > 0) {
        orgList.forEach(function (items, indexs, arrays) {
            if (depid) {
                if (items && items.ID == depid) {
                    if (items.UserList && items.UserList.length > 0) {
                        items.UserList.forEach(function (item, index, array) {
                            arr.push(item);
                        });
                    }
                }
            } else {
                if (items) {
                    if (items.UserList && items.UserList.length > 0) {
                        items.UserList.forEach(function (item, index, array) {
                            arr.push(item);
                        });
                    }
                }
            }
        });
    }
    var userArr = new Array();
    var vue = getWXQueryString('vue');
    if (vue == 'flow' || vue == 'start') {
        var curRoutine = $monitor.curRoutine;
        if (curRoutine && curRoutine.ChooseUserType != '1') {
            var ownerUserIDs = curRoutine.OwnerUserIDs;
            arr.forEach(function (item, index) {
                var userIDs = ownerUserIDs.split(',');
                if (userIDs.length > 0) {
                    for (var i = 0; i < userIDs.length; i++) {
                        if (userIDs[i] == item.ID)
                            userArr.push(item);
                    }
                }
            });
            if (userArr.length <= 0 && (ownerUserIDs.length <= 0))
                userArr = arr;
        } else {
            userArr = arr;
        }
    } else {
        userArr = arr;
    }
    return userArr;
}

function parseParam(param, key) {
    var paramStr = "";
    if (param instanceof String || param instanceof Number || param instanceof Boolean) {
        paramStr += "&" + key + "=" + encodeURIComponent(param);
    } else {
        $.each(param, function (i) {
            var k = key == null ? i : key + (param instanceof Array ? "[" + i + "]" : "." + i);
            paramStr += '&' + parseParam(this, k);
        });
    }
    return paramStr.substr(1);
};

function showCurrentPage(link, idname, title) {
    if (link && link.indexOf('http://') >= 0) {
        gUrl(link);
    } else {
        $monitor.go(link, idname, title, function (info) { });
    }
}

function getWXMobileHeight() {
    var availHeight = window.screen.availHeight;
    var clientHeight = document.body.clientHeight;
    if (availHeight > 0 && clientHeight > 0 && availHeight >= clientHeight) {
        return availHeight - clientHeight;
    } else {
        return 70;
    }
}

function getToken(account) {
    var token = '';
    $.ajax({
        url: "/Main/GetToken?Account=" + account,
        type: 'GET',
        async: false,
        success: function (data, textStatus) {
            token = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    return token;
}


function drag(id) {
    var _x_start, _y_start, _x_move, _y_move, _x_end, _y_end, left_start, top_start;
    document.getElementById(id).addEventListener("touchstart", function (e) {
        _x_start = e.touches[0].pageX;
        _y_start = e.touches[0].pageY;
        left_start = $("#" + id).css("left");
        top_start = $("#" + id).css("top");

    })
    document.getElementById(id).addEventListener("touchmove", function (e) {
        _x_move = e.touches[0].pageX;
        _y_move = e.touches[0].pageY;
        $("#" + id).css("left", parseFloat(_x_move) - parseFloat(_x_start) + parseFloat(left_start) + "px");
        $("#" + id).css("top", parseFloat(_y_move) - parseFloat(_y_start) + parseFloat(top_start) + "px");
    })
    document.getElementById(id).addEventListener("touchend", function (e) {
        var _x_end = e.changedTouches[0].pageX;
        var _y_end = e.changedTouches[0].pageY;
    })
}

function home() {
    var id = '_home';
    if (showBackHomeButtom) {
        $("#" + id).show();
        drag(id);
    }
}

function hideHome() {
    var id = '_home';
    $("#" + id).hide();
}

function goHome() {
    hideHome();
    var page = $(".page-views").find(".page");
    page.each(function (index, event) {
        if (event.id && event.id != '') {
            $(event).remove();
        }
    });
}

function closeWindow() {
    //WeixinJSBridge.invoke('closeWindow', {}, function (res) {
    //});
    wx.closeWindow();
}

//----------------------移动通专用start---------------------------
var $ydtHelper = {
    rnConfig: {
        doQRScan: "doQRScan", //触发二维码扫描 html->rn
        doQRScanCallback: "doQRScanCallback", //触发二维码扫描回调 rn->html
        closeWindow: "closeWindow", //关闭当前窗体 html->rn
        selectImage: "selectImage", //触发选择照片，拍照 html->rn
        selectImageCallback: "selectImageCallback", //触发选择照片，拍照 回调 rn->html
    },
    sendScan: function () {
        var sendData = {
            actionType: this.rnConfig.doQRScan, //触发二维码扫描 html->rn
            info: {}
        };
        if (window.WebViewBridge) {
            window.WebViewBridge.send(JSON.stringify(sendData));
        };
    },
    sendScanCallBack: null,
    closeWindow: function (dataObj) {
        var sendData = {
            actionType: this.rnConfig.closeWindow,
            info: dataObj ? dataObj : {}
        };
        if (window.WebViewBridge) {
            window.WebViewBridge.send(JSON.stringify(sendData));
        };
    },
    selectImage: function (url) {
        var sendData = {
            actionType: this.rnConfig.selectImage,
            info: { url: url, multipleImage: true }
        };
        if (window.WebViewBridge) {
            window.WebViewBridge.send(JSON.stringify(sendData));
        };
    },
    selectImageCallback: null,
    doCallBack: function (obj, actionType) {
        if (!actionType) {
            alert('未定义actionType')
        }
        else {
            if (actionType == this.rnConfig.doQRScanCallback) {
                if (this.sendScanCallBack) {
                    this.sendScanCallBack(obj);
                }
                else {
                    alert('$ydtHelper.sendScanCallBack未指定')
                }
            }
            else if (actionType == this.rnConfig.selectImageCallback) {
                if (this.selectImageCallback) {
                    this.selectImageCallback(obj);
                }
                else {
                    alert('$ydtHelper.selectImageCallback未指定')
                }
            }
            else {
                alert(actionType + '在$ydtHelper中没有回调方法定义');
            }
        }
    }
}

window.onload = function () {
    setTimeout(function () {
        if (window.WebViewBridge) {
            window.WebViewBridge.onMessage = function (message) {
                //所有的回掉都通过解析message处理
                var messageObj = JSON.parse(message);
                $ydtHelper.doCallBack(messageObj, messageObj.actionType);
            };
        }
    });
};

//在企业微信显示正常，在移动通内显示异常
//这里的异常特指dom元素在底部超出的情况
//此时需要进行相应的向上偏移,偏移值如下
function getBotUpOffsetInYDT() {
    //根据可视区域高度进行分类 
    var height = $(window).height();

    switch (height) {
        case 640:
            return 20;
        case 748:
            return 30;
        default:
            return 0;
    }
}

//顶部向下偏移
function getTopDownOffsetInYDT() {
    //根据可视区域高度进行分类 
    var height = $(window).height();
    if (getPhoneType()) {
        return 0;
    } else {
        return 20;
    }
}

function getHeightInYDT() {
    return $(window).height() - getTopDownOffsetInYDT();
}


//----------------------移动通专用end---------------------------