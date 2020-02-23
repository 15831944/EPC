/* 说明
对象：
pdfPositionInfo：成果字段PDFSignPositionInfo相同结构的对象，保存时存到数据库
格式：
{
    "FrameGroup": "",//图框名称
    "FrameType": "",//横竖框
    "FrameSize": "",//图框规格
    "PaperWidth": 0,//宽度
    "PaperHeight": 0,//高度
    "AuditRoles": [],//签名域块名
    "AuditRolesType": [],//签名域块类型：签名、会签、条码、二维码、个人章、资质章、公司章、组合章
    "AuditRolesConfig": [],//签名域块的配置：大小、角度
    "PositionXs": [],//坐标X
    "PositionYs": [],//坐标Y
    "BorderConfigs": []//图框的配置，如果没有AuditRolesConfig，就从BorderConfigs中取配置
}
posDic：将pdfPositionInfo改造成易于使用的结构，只包含中心点位置信息
groupInfo：图章组合信息，包括图章ID，图章大小、从章等信息
tf：图幅
方法：
initPosition：页面初始化时，指定章的位置，需返回数组
*/

var pdfPositionInfo = {};
var posDic = {};
var groupInfo = {};
var tf = "A0";

var productID = getQueryString("ProductID");
var projectInfoID = getQueryString("ProjectInfoID");
var version = getQueryString("Version");
var auditSignUser = {};
var coSignUser = [];
var pdfSignRoleCfg = {};
var isApply = false;

function pdfPageInit() {
    if (funcType != "view") {
        if (getQueryString("IsApply") == "true") {
            $("#numPages").after(''
                + '<a class="toolbarLabel pdfViewButton mini-button" id="savePosition" iconcls="icon-save" onclick="savePosition();">保存</a>'
                + '<a class="toolbarLabel pdfViewButton mini-button" id="addTemplate" iconcls="icon-add" onclick="addTemplate();">套用模板</a>'
                + '<a class="toolbarLabel pdfViewButton mini-menubutton" id="addDiv" iconcls="icon-add" menu="#popupMenu">添加签名域</a>'
                );
        }
        $("#secondaryToolbarToggle").after(''
            + (getQueryString("IsApply") == "true" ? "" : '<a class="toolbarLabel pdfViewButton mini-button" id="reSign" iconcls="mini-pager-reload" onclick="reSign();">重新盖章</a>')
            + '<a class="toolbarLabel pdfViewButton mini-button" id="saveasTemplate" iconcls="icon-project" onclick="saveTempPosition(true);">存为模板</a>'
            );
    }
    if (getQueryString("IsApply") != "true") {
        addExecuteParam("ProductID", productID);
        addExecuteParam("Version", version);
        execute("GetRelateInfo", {
            showLoading: true, async: false, onComplete: function (data) {
                pdfPositionInfo = data["Position"];
                groupInfo = data["GroupInfo"];
                config = getConfig(pdfPositionInfo["BorderConfigs"]);
                tf = data["TF"];
            }
        });
    }
    else if (window.parent && typeof (window.parent.getPosInfo) != "undefined") {
        setData(window.parent.getPosInfo());
    }
}

function initPosition(pageNumber) {
    var PDFPostions = [];
    if (pdfPositionInfo["AuditRoles"]) {
        for (var i = 0; i < pdfPositionInfo.AuditRoles.length; i++) {
            var signName = pdfPositionInfo.AuditRoles[i];
            var isMajorCosignBlock = isMajorCosign(signName)

            var auditRolesType = "";
            if (pdfPositionInfo["AuditRolesType"] && pdfPositionInfo.AuditRolesType[i])
                auditRolesType = pdfPositionInfo.AuditRolesType[i];

            var auditRolesConfig = null;
            if (pdfPositionInfo["AuditRolesConfig"] && pdfPositionInfo.AuditRolesConfig[i])
                auditRolesConfig = pdfPositionInfo.AuditRolesConfig[i];

            var pageNum = 1;
            if (pdfPositionInfo["AuditRolesPageNum"] && pdfPositionInfo.AuditRolesPageNum[i])
                pageNum = pdfPositionInfo.AuditRolesPageNum[i];
            if (pageNumber != pageNum) continue;

            var angle = 0; var width = 0; var height = 0; var charHeight = 0;
            var groupID = ""; var groupName = ""; var correntX = 0; var correntY = 0;
            var follows = []; var sealID = ""; var category = "";
            var id = createRandomId();
            var name = signName;
            var visiable = true;

            if (signName.indexOf("章") > -1) {
                if (groupInfo[signName]) {
                    angle = groupInfo[signName]["Angle"];
                    width = groupInfo[signName]["Width"];
                    height = groupInfo[signName]["Height"];
                    groupID = groupInfo[signName]["GroupID"];
                    groupName = groupInfo[signName]["GroupName"];
                    sealID = groupInfo[signName]["MainID"];
                    category = groupInfo[signName]["Category"];
                    if (groupInfo[signName]["Follows"].length > 0) {
                        var fs = groupInfo[signName]["Follows"];
                        for (var j = 0; j < fs.length; j++) {
                            follows.push({
                                SealID: fs[j]["FollowID"],
                                Name: fs[j]["Name"],
                                Left: mm2px(pdfPositionInfo.PositionXs[i] + fs[j]["CorrectPosX"] - fs[j]["Width"] / 2) * currentScale - 1,
                                Bottom: mm2px(pdfPositionInfo.PositionYs[i] + fs[j]["CorrectPosY"] - fs[j]["Height"] / 2) * currentScale - 1,
                                Width: mm2px(fs[j]["Width"]) * currentScale,
                                Height: mm2px(fs[j]["Height"]) * currentScale,
                                Angle: fs[j]["Angle"],
                                GroupID: groupID,
                                GroupName: groupName,
                                IsMain: false,
                                PageNum: pageNum,
                                Visiable: true,
                                ID: createRandomId()
                            });
                        }
                    }
                }
                if (auditRolesConfig && !($.isEmptyObject(auditRolesConfig))) {
                    angle = auditRolesConfig["Angle"];
                    width = auditRolesConfig["Width"];
                    height = auditRolesConfig["Height"];
                    if (pdfSignRoleCfg && pdfSignRoleCfg[category] == false) {
                        visiable = false;
                        for (var k = 0; k < follows.length; k++) {
                            follows[k]["Visiable"] = false;
                        }
                    }
                }
            }
            else if (signName.indexOf("条码") > -1) {
                category = "条码";
                if (auditRolesConfig && auditRolesConfig != {}) {
                    angle = auditRolesConfig["Angle"];
                    height = auditRolesConfig["Height"];
                    if (pdfSignRoleCfg && pdfSignRoleCfg[category] == false) {
                        visiable = false;
                    }
                }
                else if (config["barcode"]) {
                    angle = config["barcode"]["Angle"];
                    height = config["barcode"]["Height"];
                    correntX = config["barcode"]["CorrectPosX"];
                    correntY = config["barcode"]["CorrectPosY"];
                }
            }
            else if (signName.indexOf("二维码") > -1) {
                category = "二维码";
                if (auditRolesConfig && !($.isEmptyObject(auditRolesConfig))) {
                    angle = auditRolesConfig["Angle"];
                    width = auditRolesConfig["Width"];
                    height = auditRolesConfig["Height"];
                    if (pdfSignRoleCfg && pdfSignRoleCfg[category] == false) {
                        visiable = false;
                    }
                }
                else if (config["qrcode"]) {
                    angle = config["qrcode"]["Angle"];
                    width = config["qrcode"]["Width"];
                    height = config["qrcode"]["Height"];
                    correntX = config["qrcode"]["CorrectPosX"];
                    correntY = config["qrcode"]["CorrectPosY"];
                }
            }
            else if (auditRolesType.indexOf("会签") > -1 || signName.indexOf("会签") > -1 || isMajorCosignBlock) {
                category = "会签";
                if (signName.indexOf("日期") > -1)
                    category = "会签日期";
                if (signName.indexOf("专业") > -1)
                    category = "会签专业";
                if (auditRolesConfig && !($.isEmptyObject(auditRolesConfig))) {
                    angle = auditRolesConfig["Angle"];
                    width = auditRolesConfig["Width"];
                    height = auditRolesConfig["Height"];
                    if (pdfSignRoleCfg && pdfSignRoleCfg[category] == false) {
                        visiable = false;
                    }
                    charHeight = auditRolesConfig["CharHeight"];
                }
                else if (config["cosign"]) {
                    angle = config["cosign"]["Angle"];
                    width = config["cosign"]["Width"];
                    height = config["cosign"]["Height"];
                    correntX = config["cosign"]["CorrectPosX"];
                    correntY = config["cosign"]["CorrectPosY"];
                    charHeight = config["cosign"]["CharHeight"];
                }
                var roleUser = adaptRoleUser(signName, category);
                name = roleUser.Name;
                if (!name) name = signName;
            }
            else {
                category = "签名";
                var roleUser = adaptRoleUser(signName, category);
                name = roleUser.Name;
                category = roleUser.Category;
                if (!name) name = signName;

                if (auditRolesConfig && !($.isEmptyObject(auditRolesConfig))) {
                    angle = auditRolesConfig["Angle"];
                    width = auditRolesConfig["Width"];
                    height = auditRolesConfig["Height"];
                    if (pdfSignRoleCfg && pdfSignRoleCfg[category] == false) {
                        visiable = false;
                    }
                    charHeight = auditRolesConfig["CharHeight"];
                }
                else if (config["sign"]) {
                    angle = config["sign"]["Angle"];
                    width = config["sign"]["Width"];
                    height = config["sign"]["Height"];
                    correntX = config["sign"]["CorrectPosX"];
                    correntY = config["sign"]["CorrectPosY"];
                    charHeight = config["cosign"]["CharHeight"];
                }
            }

            PDFPostions.push({
                ID: id,
                PageNum: pageNum,
                BlockKey: signName,
                Name: name,
                Left: mm2px(pdfPositionInfo.PositionXs[i] + correntX - width / 2) * currentScale - 1,
                Bottom: mm2px(pdfPositionInfo.PositionYs[i] + correntY - height / 2) * currentScale - 1,
                Width_mm: width,
                Height_mm: height,
                Width: mm2px(width) * currentScale,
                Height: mm2px(height) * currentScale,
                CharHeight: charHeight,
                Angle: angle,
                GroupID: groupID,
                GroupName: groupName,
                Follows: follows,
                SealID: sealID,
                PosX: pdfPositionInfo.PositionXs[i],
                PosY: pdfPositionInfo.PositionYs[i],
                IsMain: true,
                Visiable: visiable,
                Category: category
            });
            fieldObjects[signName] = {
                PageNum: pageNum,
                PosX: pdfPositionInfo.PositionXs[i],
                PosY: pdfPositionInfo.PositionYs[i],
                Width_mm: width,
                Height_mm: height,
                SealID: sealID,
                IDs: [id],
                Angle: angle,
                CharHeight: charHeight
            }
            for (var j = 0; j < follows.length; j++) {
                fieldObjects[signName].IDs.push(follows[j].ID);
            }
        }
    }
    return PDFPostions;
}

function savePosition() {
    var textlayers = $(".textLayer");
    var groupIDs = {}; var groupNames = []; var keys = [];
    var allIDs = []; var gIDs = []; var sIDs = [];
    pdfPositionInfo["AuditRoles"] = [];
    pdfPositionInfo["AuditRolesType"] = [];
    pdfPositionInfo["AuditRolesConfig"] = [];
    pdfPositionInfo["AuditRolesPageNum"] = [];
    pdfPositionInfo["PositionXs"] = [];
    pdfPositionInfo["PositionYs"] = [];
    pdfPositionInfo["BorderConfigs"] = [];
    pdfPositionInfo["FrameProperty"] = [];
    pdfPositionInfo["PDFSignRoleConfig"] = {};
    $.each(textlayers, function (num, item) {
        pdfPositionInfo["FrameProperty"].push({
            PaperWidth: px2mm(item.offsetWidth / currentScale),
            PaperHeight: px2mm(item.offsetHeight / currentScale),
            FrameType: item.offsetWidth > item.offsetHeight ? "横框" : "竖框"
        });
        var divs = item.childNodes;
        for (var i = 0; i < divs.length; i++) {
            var div = divs[i];
            if (div.dataset.isMain == "true") {
                var category = div.dataset.category.toLowerCase();
                if (category == "group" || category == "person" || category == "company" || category == "quality") {
                    if (category == "group")
                        gIDs.push(div.dataset.groupID);
                    else
                        sIDs.push(div.dataset.groupID);
                    groupNames.push(div.dataset.groupName);
                    keys.push(div.dataset.BlockKey);
                    allIDs.push(div.dataset.groupID);
                }
                pdfPositionInfo["AuditRoles"].push(div.dataset.BlockKey);
                pdfPositionInfo["AuditRolesType"].push(div.dataset.category);
                pdfPositionInfo["AuditRolesConfig"].push({
                    Width: parseFloat(div.dataset.width_mm),
                    Height: parseFloat(div.dataset.height_mm),
                    Angle: parseFloat(div.dataset.angle),
                    CharHeight: parseFloat(div.dataset.charHeight),
                    Visiable: div.dataset.visiable != "false"
                });
                pdfPositionInfo["PositionXs"].push(parseFloat(div.dataset.posX));
                pdfPositionInfo["PositionYs"].push(parseFloat(div.dataset.posY));
                pdfPositionInfo["AuditRolesPageNum"].push(num + 1);
            }
            pdfPositionInfo["PDFSignRoleConfig"][div.dataset.category] = div.dataset.visiable != "false";
        }
        groupIDs["Groups"] = gIDs.join(",");
        groupIDs["Seals"] = sIDs.join(",");
        groupIDs["All"] = allIDs.join(",");
        for (let key in config) {
            pdfPositionInfo["BorderConfigs"].push(config[key]);
        }
    });
    msgUI("确认保存吗？", 2, function (data) {
        if (data == "ok") {
            if (!isApply) {
                addExecuteParam("ProductID", productID);
                addExecuteParam("Version", version);
                addExecuteParam("GroupIDs", mini.encode(groupIDs));
                addExecuteParam("GroupNames", groupNames.join(','));
                addExecuteParam("GroupKeys", keys.join(','));
                addExecuteParam("PdfPositionInfo", mini.encode(pdfPositionInfo));
                execute("SavePosition", {
                    showLoading: true, refresh: false, onComplete: function (data) {
                        if (top["win"].savePosition)
                            top["win"].savePosition(productID, mini.encode(pdfPositionInfo), mini.encode(groupIDs), groupNames.join(','), keys.join(','));
                        msgUI("保存成功。");
                    }, validateForm: false
                });
            }
            else if (top["win"].savePosition) {
                top["win"].savePosition(productID, mini.encode(pdfPositionInfo), mini.encode(groupIDs), groupNames.join(','), keys.join(','));
                msgUI("保存成功。");
            }
        }
    });
}

function addTemplate() {
    openWindow("/MvcConfig/UI/List/PageView?TmplCode=PlotSealTemplateSingleSelector&PrjID=" + projectInfoID, {
        title: "选择模板", addQueryString: false, onDestroy: function (data) {
            if (data != "close" && data && data.length > 0) {
                waringArray = [];
                addExecuteParam("TemplateID", data[0].ID);
                execute("GetTemplate", {
                    showLoading: true, addQueryString: false, onComplete: function (result) {
                        var groups = result.GroupList;
                        for (var i = 0; i < groups.length; i++) {
                            var group = groups[i];
                            if (fieldObjects[group.BlockKey]) {
                                var item = fieldObjects[group.BlockKey];
                                for (var ji = 0; ji < item.IDs.length; ji++) {
                                    var id = item.IDs[ji];
                                    var divs = $("#" + id);
                                    if (divs.length > 0) {
                                        delDiv(divs[0]);
                                    }
                                }
                            }
                            group["Width_mm"] = group.Width;
                            group["Height_mm"] = group.Height;
                            var stamp = {
                                BlockKey: group["BlockKey"],
                                Name: group["BlockKey"],
                                Category: group["Category"]
                            };
                            if (stamp.Category == "签名" || stamp.Category == "会签") {
                                var roleUser = adaptRoleUser(stamp.BlockKey, stamp.Category);
                                if (roleUser)
                                    stamp.Category = roleUser.Category;
                            }
                            var isShow = true;
                            if (pdfSignRoleCfg[stamp.Category] == false)
                                isShow = false;
                            var poxX = mm2px(group["PositionXs"]) * currentScale;//左下为圆心
                            var poxY = mm2px(group["PositionYs"]) * currentScale;
                            var category = group["Category"].toLowerCase();
                            if (category == "group" || category == "person" || category == "company" || category == "quality")
                                stamp = fillGroupSign(group);
                            else {
                                var stampConfig = { Width: group["Width"], Height: group["Height"], Angle: group["Angle"], CharHeight: group["CharHeight"] };
                                stamp = fillSign(stamp, stampConfig);
                            }
                            stamp["Angle"] = group["Angle"];
                            stamp["PageNum"] = group["PageNum"];
                            stamp.PosX = group["PositionXs"];
                            stamp.PosY = group["PositionYs"];
                            stamp.Left = poxX - stamp["Width"] / 2 - 1;
                            stamp.Bottom = poxY - stamp["Height"] / 2 - 1;
                            for (var j = 0; j < stamp.Follows.length; j++) {
                                //RelativeX 以main为原点的坐标系
                                stamp.Follows[j].Left = poxX + stamp.Follows[j].RelativeX - stamp.Follows[j]["Width"] / 2 - 1;
                                stamp.Follows[j].Bottom = poxY + stamp.Follows[j].RelativeY - stamp.Follows[j]["Height"] / 2 - 1;
                                stamp.Follows[j]["PageNum"] = group["PageNum"];
                            }
                            fieldObjects[group["BlockKey"]] = {
                                PageNum: group["PageNum"],
                                PosX: group["PositionXs"],
                                PosY: group["PositionYs"],
                                Width_mm: group["Width"],
                                Height_mm: group["Height"],
                                SealID: "",
                                IDs: [stamp.ID],
                                Angle: group["Angle"]
                            }
                            for (var j = 0; j < stamp.Follows.length; j++) {
                                fieldObjects[group["BlockKey"]].IDs.push(stamp.Follows[j].ID);
                            }
                            stamp.Visiable = isShow;
                            appendSign(stamp);
                        }
                        showWaringTip();
                    }
                });
            }
        }
    });
}

function reSign() {
    msgUI("确认重新盖章吗？", 2, function (data) {
        if (data == "ok") {
            var list = [{ ProductID: productID, Version: version }];
            addExecuteParam("Products", mini.encode(list));
            execute("ReSign", {
                showLoading: true, refresh: false, onComplete: function (data) {
                    msgUI("保存成功。");
                }, validateForm: false
            });
        }
    });
}

function setCategoryShow(category, isShow, pID) {
    pdfSignRoleCfg[category] = isShow;
    if (pID == productID) {
        var textlayers = $(".textLayer");
        $.each(textlayers, function (num, item) {
            var divs = item.childNodes;
            for (var i = 0; i < divs.length; i++) {
                var div = divs[i];
                if (div.dataset.category == category) {
                    div.dataset.visiable = isShow;
                    if (isShow) {
                        //div.classList.remove('blur2');
                        div.style.visibility = "visible";
                    }
                    else {
                        //div.classList.add('blur2');
                        div.style.visibility = "hidden";
                    }
                }
            }
        });
    }
}

function onLoadComplete(pageNumber) {
    if (pageNumber == PDFViewerApplication.pagesCount) {
        if (window.parent && typeof (window.parent.getPDFSignRole) != "undefined") {
            var roleCfg = window.parent.getPDFSignRole();
            for (let key in roleCfg) {
                setCategoryShow(key, roleCfg[key] == "true", productID);
            }
        }
        showWaringTip();
    }
}

function setData(data) {
    isApply = data.IsApply;
    if (data && data.IsApply) {
        pdfPositionInfo = data["PDFSignPositionInfo"];
        auditSignUser = data["AuditSignUser"];
        coSignUser = mini.decode(data["CoSignUser"]);
        if (!pdfPositionInfo)
            pdfPositionInfo = {};
        else if (pdfPositionInfo["BorderConfigs"])
            config = getConfig(pdfPositionInfo["BorderConfigs"]);
        pdfSignRoleCfg = pdfPositionInfo["PDFSignRoleConfig"];
        if (!pdfSignRoleCfg)
            pdfSignRoleCfg = {};

        if (data["PlotSealGroup"])
            addExecuteParam("GroupIDs", data["PlotSealGroup"]);
        execute("GetPlotSealGroupInfos", {
            showLoading: true, async: false, onComplete: function (rtn) {
                groupInfo = rtn["GroupInfo"];
                var leftBottom = 100;
                if (!pdfPositionInfo.AuditRoles || !pdfPositionInfo.AuditRoles.length) {
                    pdfPositionInfo["AuditRoles"] = []; pdfPositionInfo["PositionXs"] = []; pdfPositionInfo["PositionYs"] = [];
                }
                var l = [];
                for (var g in groupInfo) {
                    if (pdfPositionInfo["AuditRoles"].indexOf(g) < 0) {
                        pdfPositionInfo["AuditRoles"].push(g);
                        pdfPositionInfo["PositionXs"].push(leftBottom);
                        pdfPositionInfo["PositionYs"].push(leftBottom);
                        l.push(g)
                    }
                }
                if (l.length > 0) {
                    waringArray.push("[" + l.join("]、[") + "]未设置位置，请移动后点击保存");
                }
            }
        });
    }
}

function adaptRoleUser(blockKey, category) {
    var rtn = { Name: blockKey, Category: category };
    var index = parseInt(blockKey.replace(/[^0-9]+/g, ''));
    if (isNaN(index)) index = 1;
    if (category == "会签" || category == "会签日期" || category == "会签专业") {
        var user = null;
        var majors = Major.filter(function (item) {
            return blockKey.indexOf(item.text) > -1;
        });
        if (majors.length > 0 && coSignUser) {
            var users = coSignUser.filter(function (item) {
                return item.MajorName == majors[0].text;
            });
            if (users.length > 0)
                user = users[0];
        }
        if (coSignUser && coSignUser.length >= index) {
            var user = coSignUser[index - 1];
        }
        if (user) {
            rtn = { Name: user.UserName, Category: category };
            if (blockKey.indexOf("日期") > -1)
                rtn = { Name: (new Date(user.SignDate).format("yyyy/MM/dd")), Category: category };
            if (blockKey.indexOf("专业") > -1)
                rtn = { Name: user.MajorName, Category: category };
        }
    }
    else {
        var role = blockKey.replace("签名", "").replace(/[0-9]/g, "");
        var aRoles = auditRole.filter(function (item) {
            var otherName = item.OtherName;
            if (!otherName) otherName = "";
            return item.text == role || otherName.split(',').indexOf(role) > -1;
        });
        if (aRoles.length > 0) {
            var aRole = aRoles[0].value;
            rtn.Category = aRole;
            var userNameStr = auditSignUser[aRole];
            if (userNameStr) {
                var userNames = userNameStr.split(',');
                if (userNames.length >= index)
                    rtn.Name = userNames[index - 1];
            }
        }
    }
    return rtn;
}

function isMajorCosign(blockKey) {
    var major = Major.filter(function (item) {
        return blockKey.indexOf(item.text) > -1;
    });
    if (major.length > 0) return true;
    return false;
}