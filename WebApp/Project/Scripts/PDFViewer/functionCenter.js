/* 说明
对象：
startX、startY：点击div时记录的clientX、clientY，用于移动时的计算
moveSwitch：是否是选中div时鼠标移动的标志
currentDivs：当前选中div的数组，如果是出图章组合就包含组合内的所有div
currentLefts、currentBottoms：当前选中div的left、bottom，用于移动时的计算
currentPosXs、currentPosYs：当前选中div的中心点位置，保存时从div上记录的中心点位置同步到数据库
newSign：是否是新增加的div的标志
config：签名、会签、二维码、条码的配置信息
fieldObjects：页面上存在的签名域的信息
方法：
initPosition：页面初始化时，指定章的位置，需返回数组
*/

/******************************校审角色、会签序号相关******************************/
var currentScale = 1;
var signIndexEnum = [];
var cosignIndexEnum = [];
for (var i = 1; i < 16; i++) {
    if (i < 10)
        signIndexEnum.push({ text: i, value: i });
    cosignIndexEnum.push({ text: i, value: i });
}
if (PlotSealType)
    PlotSealType.push({ value: "Group", text: "组合章" });
var auditRole = [];
for (var i = 0; i < RoleDefinePDF.length; i++) {
    if (RoleDefinePDF[i].value.toLowerCase() != "cosign") {
        auditRole.push({ text: RoleDefinePDF[i].text.replace(/ /g, ""), value: RoleDefinePDF[i].value, OtherName: RoleDefinePDF[i].OtherName });
    }
}
/******************************校审角色、会签序号相关******************************/

var viewType = getQueryString("ViewType");
var funcType = getQueryString("FuncType").toLowerCase();
document.write('<script src="/Project/Scripts/PDFViewer/ViewTypeLogic/' + viewType + '.js" type="text/javascript"></script>');

$(function () {
    if (typeof (pdfPageInit) != "undefined") {
        pdfPageInit();
    }
    var auditRoleStr = "";
    for (var i = 0; i < auditRole.length; i++) {
        auditRoleStr += '<li onclick="addRole(\'' + auditRole[i]["value"] + '\',\'Sign\')">' + auditRole[i]["text"] + '</li>';
    }
    $(".toolbar").after(''
        + '<ul id="popupMenu" class="mini-contextmenu">'
        + '<li onclick="addStamp(\'Person\')">个人章</li>'
        + '<li onclick="addStamp(\'Company\')">公司章</li>'
        + '<li onclick="addStamp(\'Quality\')">资质章</li>'
        + '<li onclick="addStamp(\'Group\')">组合章</li>'
        + '<li><span>校审签名</span><ul>' + auditRoleStr + '</ul></li>'
        + '<li><span>会签签名</span><ul>'
        + ' <li onclick="addRole(\'Cosign\',\'CoSign\')" >会签签名</li>'
        + ' <li onclick="addRole(\'CosignDate\',\'CoSign\')" >会签日期</li>'
        + ' <li onclick="addRole(\'CosignMajor\',\'CoSign\')" >会签专业名称</li>'
        + '</ul></li>'
        + '<li><span>其他</span><ul>'
        + ' <li onclick="addRole(\'\',\'Barcode\')" >条码</li>'
        + ' <li onclick="addRole(\'\',\'QRcode\')" >二维码</li>'
        + '</ul></li>'
        + '</ul>');
    $("body").append('\
<div id="popWindow" class="mini-window" title="设置签名域大小" style="width: 400px;" showmodal="true" allowresize="false" allowdrag="true" allowmovecolumn="false">\
    <div class="queryDiv">\
        <table style="width:100%;" class="ke-zeroborder" border="0" cellpadding="2">\
			<tbody>\
                <tr><td style="width:15%;"></td><td style="width:35%;"></td><td style="width:15%;"></td><td style="width:35%;"></td></tr>\
                <tr>\
                    <td>图框模板</td>\
                    <td colspan="3"><input name="pop_Template" class="mini-combobox" allowInput="true" style="width: 100%" data="frameInfos" onitemclick="onitemclick"/></td>\
                </tr>\
                <tr>\
                    <td>宽度</td>\
                    <td><input name="pop_Width" class="mini-textbox" style="width: 100%"/></td>\
                    <td>高度</td>\
                    <td><input name="pop_Height" class="mini-textbox" style="width: 100%;"/></td>\
                </tr>\
                <tr>\
                    <td>角度</td>\
                    <td><input name="pop_Angle" class="mini-textbox" style="width: 100%"/></td>\
                    <td>字高</td>\
                    <td><input name="pop_CharHeight" class="mini-textbox" style="width: 100%"/></td>\
                </tr>\
                <tr>\
                    <td class="keyCls">序号</td>\
                    <td class="keyCls"><input name="keyIndex" class="mini-combobox" style="width: 100%"/></td>\
                </tr>\
			</tbody>\
        </table>\
        <div>\
            <a class="mini-button mini-button-plain" onclick="setSize()" iconcls="icon-save" style="margin-right: 20px;">确定</a>\
            <a class="mini-button mini-button-plain" onclick="hideWindow(\'popWindow\')" iconcls="icon-undo">取消</a>\
        </div>\
    </div>\
</div>');
    mini.parse();
});

function getSignPositions(cwidth, cheight, scale, pageNumber, layerDiv) {
    var PDFPostions = [];
    currentScale = scale;
    currentWidth = cwidth;
    currentHeight = cheight;
    if (typeof (initPosition) != "undefined")
        PDFPostions = initPosition(pageNumber);
    layerDiv.onmousedown = onTextLayerClick;
    return PDFPostions;
}

var startX;
var startY;
var moveSwitch = false;
var currentDivs = [];
var currentLefts = [];
var currentBottoms = [];
var currentPosXs = [];
var currentPosYs = [];

var newSign = false;
var config = {};
var fieldObjects = {};

document.onmousemove = function (e) {
    if (moveSwitch && funcType != "view") {
        var x = e.clientX;
        var y = e.clientY;
        var distanceX = x - startX;
        var distanceY = startY - y;
        $.each(currentDivs, function (i, item) {
            item.style.left = (distanceX + currentLefts[i]) + "px";
            item.style.bottom = (distanceY + currentBottoms[i]) + "px";
            item.dataset.left = distanceX + currentLefts[i];
            item.dataset.bottom = distanceY + currentBottoms[i];
            if (item.dataset.posX) {
                var posX = px2mm(distanceX / currentScale) + parseFloat(currentPosXs[i]);
                var posY = px2mm(distanceY / currentScale) + parseFloat(currentPosYs[i]);
                item.dataset.posX = posX;
                item.dataset.posY = posY;
                if (fieldObjects[item.dataset.BlockKey]) {
                    fieldObjects[item.dataset.BlockKey].PosX = posX;
                    fieldObjects[item.dataset.BlockKey].PosY = posY;
                }
            }
        });
    }
}

document.onmouseup = function (e) {
    $.each(currentDivs, function (i, item) {
        //item.style.border = "1px solid"
        item.style.backgroundColor = "";
    });
    currentLefts = [];
    currentBottoms = [];
    currentPosXs = [];
    currentPosYs = [];
    var menu = document.getElementById("rightMenu");
    if (menu)
        menu.style.display = "none";
}

document.onkeyup = function (event) {
    var e = event || window.event;
    if (funcType != "view") {
        var keyCode = e.keyCode || e.which;
        switch (keyCode) {
            case 46:
                deleteDiv();
                break;
            default:
                break;
        }
    }
}

function onTextLayerClick(e) {
    if (newSign) {
        var pageNum = parseInt(e.currentTarget.parentElement.dataset.pageNumber);
        newSign["PageNum"] = pageNum;
        newSign.Left = e.offsetX - newSign["Width"] / 2 - 1;
        newSign.Bottom = currentHeight - (e.offsetY + newSign["Height"] / 2 + 1);
        newSign.PosX = px2mm(e.offsetX / currentScale);
        newSign.PosY = px2mm((currentHeight - e.offsetY) / currentScale);
        for (var i = 0; i < newSign.Follows.length; i++) {
            newSign.Follows[i].Left = e.offsetX + newSign.Follows[i].RelativeX - newSign.Follows[i]["Width"] / 2 - 1;
            newSign.Follows[i].Bottom = currentHeight - (e.offsetY + (-newSign.Follows[i].RelativeY) + newSign.Follows[i]["Height"] / 2 + 1);
            newSign.Follows[i]["PageNum"] = pageNum;
        }
        newSign.Visiable = true;
        if (typeof (pdfSignRoleCfg) != "undefined" && pdfSignRoleCfg[newSign.Category] == false) {
            newSign.Visiable = false;
        }
        appendSign(newSign);
        fieldObjects[newSign.BlockKey] = {
            PageNum: pageNum,
            Width_mm: newSign.Width_mm,
            Height_mm: newSign.Height_mm,
            IDs: [newSign.ID],
            PosX: newSign.PosX,
            PosY: newSign.PosY,
            Angle: newSign.Angle
        };
        for (var i = 0; i < newSign.Follows.length; i++) {
            fieldObjects[newSign.BlockKey].IDs.push(newSign.Follows[i].ID);
        }
        hideTip();
        if (newSign.Visiable == false) {
            var txt = "";
            if (!txt) {
                txt = getEnumText(auditRole, newSign.Category);
                if (!txt) txt = getEnumText(PlotSealType, newSign.Category);
                if (!txt) txt = newSign.BlockKey;
            }
            showTip("当前PDF设置为[" + txt + "]不签名(盖章)，已隐藏");
        }
        newSign = false;
        setTimeout(function () {
            hideTip();
        }, 2000);
    }
}

function mouseDown(e) {
    e.stopPropagation();
    $.each(currentDivs, function (i, item) {
        //item.style.border = "1px solid"
        item.style.backgroundColor = "";
    });
    currentDivs = [];
    currentLefts = [];
    currentBottoms = [];
    currentPosXs = [];
    currentPosYs = [];

    e = e ? e : window.event;
    var currentDiv = e.target;
    if (viewType == "CorrectPos") {
        //currentDiv.style.border = "1px solid #00188c";
        currentDiv.style.backgroundColor = "darkgrey";
        currentDivs.push(currentDiv);
        currentLefts.push(parseFloat(currentDiv.dataset.left));
        currentBottoms.push(parseFloat(currentDiv.dataset.bottom));
    }
    else {
        if (currentDiv.dataset.groupID) {
            var divs = currentDiv.parentNode.childNodes;
            $.each(divs, function (i, item) {
                if (item.dataset.groupID == currentDiv.dataset.groupID) {
                    //item.style.border = "1px solid #00188c";
                    item.style.backgroundColor = "darkgrey";
                    currentDivs.push(item);
                    currentLefts.push(parseFloat(item.dataset.left));
                    currentBottoms.push(parseFloat(item.dataset.bottom));
                    if (item.dataset.posX) {
                        currentPosXs.push(parseFloat(item.dataset.posX));
                        currentPosYs.push(parseFloat(item.dataset.posY));
                    }
                    else {
                        currentPosXs.push(0);
                        currentPosYs.push(0);
                    }

                }
            });
        }
        else {
            //currentDiv.style.border = "1px solid #00188c";
            currentDiv.style.backgroundColor = "darkgrey"
            currentDivs.push(currentDiv);
            currentLefts.push(parseFloat(currentDiv.dataset.left));
            currentBottoms.push(parseFloat(currentDiv.dataset.bottom));
            currentPosXs.push(parseFloat(currentDiv.dataset.posX));
            currentPosYs.push(parseFloat(currentDiv.dataset.posY));
        }
    }
    startX = e.clientX;
    startY = e.clientY;
    if (e.button == 0)
        moveSwitch = true;
}

function mouseUp(e) {
    e.stopPropagation();
    moveSwitch = false;
}

function oncontextmenu(ev) {
    var menu = document.getElementById("rightMenu");
    if (menu) {
        if (currentDivs.length > 0) {
            if (currentDivs[0].dataset.category.toLowerCase() == "group") { $(".rotateCls").hide(); }
            else { $(".rotateCls").show(); }
        }
        var oEvent = ev || event;
        menu.style.display = "block";
        menu.style.left = oEvent.clientX + "px";
        menu.style.top = oEvent.clientY + "px";
        return false;
    }
}

function fillGroupSign(groupInfo) {
    var sign = {
        ID: createRandomId(),
        BlockKey: groupInfo["BlockKey"],
        Name: groupInfo["Name"],
        Left: 0,
        Bottom: 0,
        Width_mm: groupInfo["Width_mm"],
        Height_mm: groupInfo["Height_mm"],
        Width: mm2px(groupInfo["Width_mm"]) * currentScale,
        Height: mm2px(groupInfo["Height_mm"]) * currentScale,
        Angle: groupInfo["Angle"],
        GroupID: groupInfo["GroupID"],
        GroupName: groupInfo["GroupName"],
        Follows: [],
        SealID: groupInfo["MainID"],
        PosX: 0,
        PosY: 0,
        IsMain: true,
        Category: groupInfo["Category"]
    };
    if (groupInfo["Follows"] && groupInfo["Follows"].length > 0) {
        var fs = groupInfo["Follows"];
        for (var j = 0; j < fs.length; j++) {
            sign.Follows.push({
                ID: createRandomId(),
                SealID: fs[j]["FollowID"],
                BlockKey: "",
                Name: fs[j]["Name"],
                Width: mm2px(fs[j]["Width"]) * currentScale,
                Height: mm2px(fs[j]["Height"]) * currentScale,
                Angle: fs[j]["Angle"],
                GroupID: groupInfo["GroupID"],
                GroupName: groupInfo["GroupName"],
                IsMain: false,
                RelativeX: mm2px(fs[j]["CorrectPosX"]) * currentScale,
                RelativeY: mm2px(fs[j]["CorrectPosY"]) * currentScale,
                Left: 0,
                Bottom: 0
            });
        }
    }
    return sign;
}

function fillSign(groupInfo, bc) {
    var sign = {};
    if (bc)
        sign = {
            ID: createRandomId(),
            BlockKey: groupInfo["BlockKey"],
            Name: groupInfo["Name"],
            Left: 0,
            Bottom: 0,
            Width_mm: bc["Width"],
            Height_mm: bc["Height"],
            Width: mm2px(bc["Width"]) * currentScale,
            Height: mm2px(bc["Height"]) * currentScale,
            CharHeight: bc["CharHeight"],
            Angle: bc["Angle"],
            GroupID: "",
            GroupName: "",
            Follows: [],
            SealID: "",
            PosX: 0,
            PosY: 0,
            IsMain: true,
            Category: groupInfo["Category"]
        };
    return sign;
}

function appendSign(sign, pageNumber) {
    if (pageNumber && pageNumber != sign.PageNum) return;
    var textLayer = $(".textLayer")[sign.PageNum - 1];
    if (textLayer) {
        var textDiv = document.createElement('div');
        var isOver = false;
        if (textLayer.offsetWidth < sign.Left) {
            isOver = true;
            sign.Left = textLayer.offsetWidth - sign.Width - 1;
            sign.PosX = px2mm((sign.Left + sign.Width / 2 + 1) / currentScale);
        }
        if (textLayer.offsetHeight < sign.Bottom) {
            isOver = true;
            sign.Bottom = textLayer.offsetHeight - sign.Height;
            sign.PosY = px2mm((sign.Bottom + sign.Height / 2 + 1) / currentScale);
        }
        if (isOver) {
            waringArray.push("[" + sign.BlockKey + "]超出页面范围，已移动到边缘位置");
        }

        textDiv.dataset.width_mm = sign.Width_mm;
        textDiv.dataset.height_mm = sign.Height_mm;
        textDiv.dataset.width = sign.Width;
        textDiv.dataset.height = sign.Height;
        textDiv.dataset.left = sign.Left;
        textDiv.dataset.bottom = sign.Bottom;
        if (sign.Angle)
            textDiv.dataset.angle = sign.Angle;
        else
            textDiv.dataset.angle = 0;
        textDiv.dataset.category = sign.Category;
        textDiv.dataset.isMain = sign.IsMain;
        textDiv.dataset.visiable = sign.Visiable;
        textDiv.dataset.BlockKey = sign.BlockKey;
        textDiv.dataset.charHeight = sign.CharHeight;

        textDiv.id = sign.ID;
        textDiv.style.width = textDiv.dataset.width + "px";
        textDiv.style.height = textDiv.dataset.height + "px";
        textDiv.style.lineHeight = textDiv.dataset.height + "px";
        textDiv.style.textAlign = "center";
        textDiv.style.left = textDiv.dataset.left + 'px';
        textDiv.style.bottom = textDiv.dataset.bottom + 'px';
        textDiv.style.fontFamily = "serif";
        if (sign.Height - 3 <= 12)
            textDiv.style.fontSize = (sign.Height - 3) + "px";
        else
            textDiv.style.fontSize = "12px";
        if (sign.CharHeight)
            textDiv.style.fontSize = sign.CharHeight + "pt";
        textDiv.style.color = "red";
        if (sign.Name != sign.BlockKey)
            textDiv.style.color = "midnightblue";
        textDiv.style.border = "1px solid";
        textDiv.style.cursor = "move";
        textDiv.style.overflow = "hidden";
        if (sign.Visiable == false) {
            //textDiv.classList.add('blur2');
            textDiv.style.visibility = "hidden";
        }
        else {
            //textDiv.classList.remove('blur2');
            textDiv.style.visibility = "visible";
        }
        if (sign.Angle && sign.Angle !== 0) {
            textDiv.style.transform = "rotate(" + (0 - sign.Angle) + "deg)";
            textDiv = calAngle(textDiv, sign.Angle);
        }
        if (sign.PosX) {
            textDiv.dataset.posX = sign.PosX;
            textDiv.dataset.posY = sign.PosY;
        }
        if (sign.GroupID) {
            var src = "/Project/AutoUI/PlotSealInfo/GetSealPic?ID=" + sign.SealID;
            textDiv.style.backgroundImage = "url(" + src + ")";
            textDiv.style.backgroundSize = "100% 100%";
            textDiv.dataset.groupID = sign.GroupID;
            textDiv.dataset.groupName = sign.GroupName;
            textDiv.dataset.sealID = sign.SealID;
            if (sign.Follows && sign.Follows.length > 0) {
                for (var i = 0; i < sign.Follows.length; i++) {
                    appendSign(sign.Follows[i]);
                }
            }
        }
        else {
            textDiv.textContent = sign.Name;
        }
        textDiv.title = sign.BlockKey;

        textDiv.onmousedown = mouseDown;
        textDiv.onmouseup = mouseUp;
        textDiv.oncontextmenu = oncontextmenu;
        textLayer.appendChild(textDiv);
    }
}

function calAngle(div, angle) {
    switch (angle) {
        case 90:
            div.dataset.left = (parseFloat(div.dataset.left) + (parseFloat(div.dataset.width) / 2 - parseFloat(div.dataset.height) / 2));
            div.dataset.bottom = (parseFloat(div.dataset.bottom) - (parseFloat(div.dataset.width) / 2 + parseFloat(div.dataset.height) / 2));
            div.style.left = div.dataset.left + "px";
            div.style.bottom = div.dataset.bottom + "px";
            break;
        case 180:
            div.dataset.left = (parseFloat(div.dataset.left) + parseFloat(div.dataset.width));
            div.dataset.bottom = (parseFloat(div.dataset.bottom) - parseFloat(div.dataset.height));
            div.style.left = div.dataset.left + "px";
            div.style.bottom = div.dataset.bottom + "px";
            break;
        case 270:
        case -90:
            div.dataset.left = (parseFloat(div.dataset.left) + (parseFloat(div.dataset.width) / 2 + parseFloat(div.dataset.height) / 2));
            div.dataset.bottom = (parseFloat(div.dataset.bottom) + (parseFloat(div.dataset.width) / 2 - parseFloat(div.dataset.height) / 2));
            div.style.left = div.dataset.left + "px";
            div.style.bottom = div.dataset.bottom + "px";
            break;
        default:
            break;
    }
    return div;
}

function deleteDiv() {
    if (currentDivs.length <= 0) return;
    msgUI("确认删除吗？", 2, function (data) {
        if (data == "ok") {
            for (var i = 0; i < currentDivs.length; i++) {
                var currentDiv = currentDivs[i];
                delDiv(currentDiv, true);
            }
            currentLefts = [];
            currentBottoms = [];
            currentDivs = [];
        }
    })
}

function delDiv(div, isDelFromObj) {
    if (fieldObjects[div.dataset.BlockKey] && isDelFromObj) {
        delete fieldObjects[div.dataset.BlockKey];
    }
    var _parentElement = div.parentNode;
    if (_parentElement) {
        _parentElement.removeChild(div);
    }
}

function saveTempPosition(isNew) {
    var textlayers = $(".textLayer");
    var posData = { GroupList: [] };
    $.each(textlayers, function (num, item) {
        var divs = item.childNodes;
        for (var i = 0; i < divs.length; i++) {
            var div = divs[i];
            if (div.dataset.isMain == "true") {
                var pos = {};
                pos["GroupID"] = div.dataset.groupID;
                pos["GroupName"] = div.dataset.groupName;
                pos["PositionXs"] = parseFloat(div.dataset.posX).toFixed(4);
                pos["PositionYs"] = parseFloat(div.dataset.posY).toFixed(4);
                pos["BlockKey"] = div.dataset.BlockKey;
                pos["Category"] = div.dataset.category;
                pos["PageNum"] = num + 1;
                pos["Width"] = parseFloat(div.dataset.width_mm).toFixed(4);
                pos["Height"] = parseFloat(div.dataset.height_mm).toFixed(4);
                pos["Angle"] = parseInt(div.dataset.angle);
                var cHeight = parseFloat(div.dataset.charHeight);
                if (isNaN(cHeight)) cHeight = 0;
                pos["CharHeight"] = cHeight;
                var txt = getEnumText(auditRole, div.dataset.category);
                if (txt) pos["Category"] = "签名";
                else if (div.dataset.category == "会签专业" || div.dataset.category == "会签日期") pos["Category"] = "会签";
                posData.GroupList.push(pos);
            }
        }
    });
    if (isNew) {
        var projectinfoID = getQueryString("ProjectInfoID");
        var fileid = getQueryString('FileID');
        var url = "/MvcConfig/UI/Form/PageView?TmplCode=PlotSealTemplate&ProjectInfoID=" + projectinfoID + "&FileID=" + fileid;
        openWindow(url, {
            data: posData, addQueryString: false,
            title: "盖章模板", height: 430, width: 600, onDestroy: function (data) {

            }
        });
    }
    else
        closeWindow(posData);
}

function getEnumText(elist, v) {
    var t = "";
    for (var i = 0; i < elist.length; i++) {
        if (elist[i].value == v) {
            t = elist[i].text;
            break;
        }
    }
    return t;
}

function createRandomId() {
    return (Math.random() * 10000000).toString(16).substr(0, 4) + '-'
        + (new Date()).getTime() + '-' + Math.random().toString().substr(2, 5);
}

function getConfig(configArray) {
    var c = {};
    if (configArray)
        for (var i = 0; i < configArray.length; i++) {
            c[configArray[i]["Category"].toLowerCase()] = configArray[i];
        }
    return c;
}

/********************DPI相关********************/
var dpi = [];
function mm2px(mm) {
    if (dpi.length == 0)
        dpi = getDPI();
    var pixel = parseFloat(mm) / 25.4 * dpi[0];
    return (Math.round(pixel * 100000000) / 100000000)
}

function px2mm(px) {
    if (dpi.length == 0)
        dpi = getDPI();
    var mm = parseFloat(px) * 25.4 / dpi[0];
    return (Math.round(mm * 100000000) / 100000000)
}

function getDPI() {
    var arrDPI = new Array();
    if (window.screen.deviceXDPI != undefined) {
        arrDPI[0] = window.screen.deviceXDPI;
        arrDPI[1] = window.screen.deviceYDPI;
    } else {
        var tmpNode = document.createElement("DIV");
        tmpNode.style.cssText = "width:1in;height:1in;position:absolute;left:0px;top:0px;z-index:99;visibility:hidden";
        document.body.appendChild(tmpNode);
        arrDPI[0] = parseInt(tmpNode.offsetWidth);
        arrDPI[1] = parseInt(tmpNode.offsetHeight);
        tmpNode.parentNode.removeChild(tmpNode);
    }
    return arrDPI;
}
/********************DPI相关********************/

/********************签名块相关********************/
function addStamp(type) {
    var url = "/MvcConfig/UI/List/PageView?TmplCode=PlotSealGroupSingleSelector&Type=" + type;
    openWindow(url, {
        onDestroy: function (data) {
            if (data != "close" && data && data.length > 0) {
                addExecuteParam("GroupID", data[0].ID);
                execute("GetNewSealInfo?GroupID= " + data[0].ID + "&Type=" + type, {
                    showLoading: false, onComplete: function (newInfo) {
                        newInfo["Width_mm"] = newInfo.Width;
                        newInfo["Height_mm"] = newInfo.Height;
                        var stamp = fillGroupSign(newInfo);

                        if (fieldObjects[newInfo.BlockKey]) {
                            var item = fieldObjects[newInfo.BlockKey];
                            for (var i = 0; i < item.IDs.length; i++) {
                                var id = item.IDs[i];
                                var divs = $("#" + id);
                                if (divs.length > 0) {
                                    delDiv(divs[0]);
                                }
                            }
                            item.Angle = 0;
                            item.IDs = [stamp.ID];
                            stamp["PageNum"] = item.PageNum;
                            var poxX = mm2px(item["PosX"]) * currentScale;//左下为圆心
                            var poxY = mm2px(item["PosY"]) * currentScale;
                            stamp.PosX = item["PosX"];
                            stamp.PosY = item["PosY"];
                            stamp.Left = poxX - stamp["Width"] / 2 - 1;
                            stamp.Bottom = poxY - stamp["Height"] / 2 - 1;
                            for (var j = 0; j < stamp.Follows.length; j++) {
                                //RelativeX 以main为原点的坐标系
                                stamp.Follows[j].Left = poxX + stamp.Follows[j].RelativeX - stamp.Follows[j]["Width"] / 2 - 1;
                                stamp.Follows[j].Bottom = poxY + stamp.Follows[j].RelativeY - stamp.Follows[j]["Height"] / 2 - 1;
                                stamp.Follows[j]["PageNum"] = item.PageNum;
                                item.IDs.push(stamp.Follows[j].ID);
                            }
                            appendSign(stamp);
                            showTip("已将原[" + newInfo.BlockKey + "]的图章替换");
                            setTimeout(function () {
                                hideTip();
                            }, 2000);
                        }
                        else {
                            newSign = stamp;
                            newSign["Angle"] = 0;
                            newSign["Width_mm"] = newInfo.Width;
                            newSign["Height_mm"] = newInfo.Height;
                        }
                    }
                });
            }
        }
    });
}

var newRole = "";
var newType = "";
function addRole(role, borderType) {
    mini.getbyName("pop_Template").setValue("");
    mini.getbyName("pop_Width").setValue("");
    mini.getbyName("pop_Height").setValue("");
    mini.getbyName("pop_Angle").setValue("");
    mini.getbyName("pop_CharHeight").setValue("");
    mini.getbyName("keyIndex").setValue("");

    $(".keyCls").hide();
    newRole = role;
    newType = borderType;
    if (borderType == "Barcode") {
        if (fieldObjects[borderType] || fieldObjects["条码"])
            return msgUI("已存在条码，不能重复添加");
    }
    else if (borderType == "QRcode") {
        if (fieldObjects[borderType] || fieldObjects["二维码"])
            return msgUI("已存在二维码，不能重复添加");
    }
    else {
        $(".keyCls").show();
        if (borderType == "Sign") {
            mini.getbyName("keyIndex").setData(signIndexEnum)
        }
        else {
            mini.getbyName("keyIndex").setData(cosignIndexEnum)
        }
    }
    showWindow("popWindow");
}

function onitemclick(e) {
    var bc = e.item.borderConfig;
    var c = bc.filter(function (item) {
        return item.Category == newType;
    });
    if (c.length > 0) {
        mini.getbyName("pop_Width").setValue(c[0].Width);
        mini.getbyName("pop_Height").setValue(c[0].Height);
        mini.getbyName("pop_Angle").setValue(c[0].Angle);
        mini.getbyName("pop_CharHeight").setValue(c[0].CharHeight);
    }
}

function setSize() {
    hideWindow("popWindow");
    var width = mini.getbyName("pop_Width").getValue();
    if (isNaN(width)) width = 0; else width = parseFloat(width);
    var height = mini.getbyName("pop_Height").getValue();
    if (isNaN(height)) height = 0; else height = parseFloat(height);
    var angle = mini.getbyName("pop_Angle").getValue();
    if (isNaN(angle)) angle = 0; else angle = parseFloat(angle);
    var charHeight = mini.getbyName("pop_CharHeight").getValue();
    if (isNaN(charHeight)) charHeight = 0; else charHeight = parseFloat(charHeight);
    var keyIndex = mini.getbyName("keyIndex").getText();

    var c = { Width: width, Height: height, Angle: angle, CharHeight: charHeight };
    var sign = {};
    if (newType == "Barcode") {
        if (!width) c.Width = 15;
        sign = {
            BlockKey: "条码",
            Name: "条码",
            Category: "条码"
        };
    }
    else if (newType == "QRcode") {
        sign = {
            BlockKey: "二维码",
            Name: "二维码",
            Category: "二维码"
        };
    }
    else if (newType == "Sign") {
        var bk = ""; var name = ""; var cate = ""
        bk = getEnumText(auditRole, newRole) + keyIndex + "签名";
        if (typeof (adaptRoleUser) != "undefined") {
            var roleUser = adaptRoleUser(bk, "签名");
            name = roleUser.Name;
            cate = roleUser.Category;
        }
        if (!name) name = bk;
        if (fieldObjects[bk])
            return msgUI("已存在序号为[" + keyIndex + "]的" + getEnumText(auditRole, newRole) + "签名，不能重复添加");
        sign = {
            BlockKey: bk,
            Name: name,
            Category: cate
        };
    }
    else if (newType == "CoSign") {
        var bk = ""; var name = ""; var cate = ""
        if (newRole == "Cosign") {
            cate = "会签";
            bk = "会签" + keyIndex + "签名";
            if (fieldObjects[bk])
                return msgUI("已存在序号为[" + keyIndex + "]的会签签名，不能重复添加");
        }
        else if (newRole == "CosignDate") {
            cate = "会签日期";
            bk = "会签" + keyIndex + "日期";
            if (fieldObjects[bk])
                return msgUI("已存在序号为[" + keyIndex + "]的会签日期，不能重复添加");
        }
        else if (newRole == "CosignMajor") {
            cate = "会签专业";
            bk = "会签" + keyIndex + "专业名称";
            if (fieldObjects[bk])
                return msgUI("已存在序号为[" + keyIndex + "]的会签专业名称，不能重复添加");
        }
        if (typeof (adaptRoleUser) != "undefined") {
            var roleUser = adaptRoleUser(bk, cate);
            name = roleUser.Name;
            cate = roleUser.Category;
        }
        if (!name) name = bk;
        sign = {
            BlockKey: bk,
            Name: name,
            Category: cate
        };
    }
    newSign = fillSign(sign, c);
    showTip("请点击屏幕，以确定新增签名域的位置");
}

function rotate(angleTODO) {
    var div = null;
    if (currentDivs.length > 0)
        div = currentDivs[0];
    if (div) {
        var bKey = div.dataset.BlockKey;
        if (fieldObjects[bKey]) {
            var item = fieldObjects[bKey];
            item.Angle += angleTODO;
            if (item.Angle >= 360)
                item.Angle -= 360;
            if (item.Angle < 0)
                item.Angle += 360;
            for (var i = 0; i < item.IDs.length; i++) {
                var id = item.IDs[i];
                var divs = $("#" + id);
                if (divs.length > 0) {
                    delDiv(divs[0]);
                }
            }
            var stamp = {
                BlockKey: bKey,
                Name: div.title,
                Category: div.dataset.category,
                Width_mm: parseInt(div.dataset.width_mm),
                Height_mm: parseInt(div.dataset.height_mm),
                Angle: item.Angle
            };
            if (div.dataset.sealID) {
                stamp["GroupID"] = div.dataset.groupID;
                stamp["GroupName"] = div.dataset.groupName;
                stamp["MainID"] = div.dataset.sealID;
                stamp = fillGroupSign(stamp);
                stamp.ID = item.IDs[0];
            }
            else {
                var c = { Width: item.Width_mm, Height: item.Height_mm, Angle: item.Angle, CharHeight: item.CharHeight };
                stamp = fillSign(stamp, c);
                stamp.ID = item.IDs[0];
            }
            stamp["PageNum"] = item.PageNum;
            var poxX = mm2px(item["PosX"]) * currentScale;//左下为圆心
            var poxY = mm2px(item["PosY"]) * currentScale;
            stamp.PosX = item["PosX"];
            stamp.PosY = item["PosY"];
            stamp.Left = poxX - stamp["Width"] / 2 - 1;
            stamp.Bottom = poxY - stamp["Height"] / 2 - 1;
            appendSign(stamp);
        }
    }
}

function changePage(num) {
    var targetLayer = $(".textLayer")[num - 1];
    var div = null;
    if (currentDivs.length > 0) {
        var mainDiv = currentDivs.filter(function (d) {
            if (d.dataset.isMain == "true") return true;
        });
        div = mainDiv.length > 0 ? mainDiv[0] : null;
    }
    if (div) {
        var bKey = div.dataset.BlockKey;
        if (fieldObjects[bKey]) {
            var item = fieldObjects[bKey];
            item.PageNum = num;
            for (var i = 0; i < item.IDs.length; i++) {
                var id = item.IDs[i];
                var divs = $("#" + id);
                if (divs.length > 0) {
                    targetLayer.appendChild(divs[0]);
                }
            }
        }
    }
}
/********************签名块相关********************/

/********************签名块的右键菜单，在view.js中页面加载完时调用********************/
function addContextMenu() {
    if ($("#rightMenu").length > 0) {
        $("#rightMenu").remove();
    }
    var pageNumStr = "";
    for (var i = 1; i <= PDFViewerApplication.pagesCount; i++) {
        pageNumStr += '<li onclick="changePage(' + i + ')">第 ' + i + ' 页</li>';
    }
    $(".toolbar").after(''
        + '<ul id="rightMenu" class="mini-contextmenu">'
        + '<li onclick="deleteDiv();" iconcls="icon-remove">删除</li>'
        + '<li><span iconcls="goto-copy">移动到</span><ul>' + pageNumStr + '</ul></li>'
        + '<li class="rotateCls" onclick="rotate(-90);"  iconcls="rotate-right">顺时针旋转90°</li>'
        + '<li class="rotateCls" onclick="rotate(90);" iconcls="rotate-left">逆时针旋转90°</li>'
        + '</ul>');
    $("#viewer").prepend('<div class="tip" style="display: none;"><div id="tipContent"></div></div>');
    mini.parse();
}
/********************签名块的右键菜单，在view.js中页面加载完时调用********************/

var waringArray = [];
function showWaringTip() {
    if (waringArray.length > 0) {
        msgUI(waringArray.join("<br/>"));
    }
}

function showTip(tipContent, time) {
    $("#tipContent").html(tipContent);
    $(".tip").show();
    if (time) {
        setTimeout(function () {
            hideTip();
        }, time)
    }
}

function hideTip() {
    $(".tip").hide();
}