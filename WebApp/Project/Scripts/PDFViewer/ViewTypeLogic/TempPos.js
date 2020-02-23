
//initPosition 页面初始化时，指定章的位置，需返回数组
//onDrawCanvas 初始化画布前
//盖章模板预览
var tempGroups = [];

function pdfPageInit() {
    if (funcType != "view") {
        $("#numPages").after(''
            + '<a class="toolbarLabel pdfViewButton mini-button" id="saveTempPosition" iconcls="icon-save" onclick="saveTempPosition(false);">保存</a>'
            + '<a class="toolbarLabel pdfViewButton mini-menubutton" id="addDiv" iconcls="icon-add" menu="#popupMenu">添加签名域</a>'
            );
    }
}

function onDrawCanvas(canvasWrapper) {
    //canvasWrapper.classList.add('blur');
}

function setData(data) {
    if (data) {
        if (data["GroupList"]) tempGroups = data["GroupList"];
    }
}

function initPosition(pageNumber) {
    var pos = [];
    for (var i = 0; i < tempGroups.length; i++) {
        var group = tempGroups[i];
        var pageNum = group["PageNum"];
        if (pageNum != pageNumber) continue;
        var category = group["Category"].toLowerCase();
        var poxX = mm2px(group["PositionXs"]) * currentScale;//左下为圆心
        var poxY = mm2px(group["PositionYs"]) * currentScale;
        var stamp = {
            BlockKey: group["BlockKey"],
            Name: group["BlockKey"],
            Category: group["Category"]
        };
        if (category != "group" && category != "person" && category != "company" && category != "quality") {
            var stampConfig = { Width: group["Width"], Height: group["Height"], Angle: group["Angle"], CharHeight: group["CharHeight"] };
            stamp = fillSign(stamp, stampConfig);
        }
        else {
            addExecuteParam("GroupID", group["GroupID"]);
            execute("GetNewSealInfo?GroupID=" + group["GroupID"] + "&Type=" + group["Category"], {
                async: false, showLoading: false, onComplete: function (newSealGroupInfo) {
                    newSealGroupInfo["Width_mm"] = newSealGroupInfo.Width;
                    newSealGroupInfo["Height_mm"] = newSealGroupInfo.Height;
                    stamp = fillGroupSign(newSealGroupInfo);
                    for (var j = 0; j < stamp.Follows.length; j++) {
                        //RelativeX 以main为原点的坐标系
                        stamp.Follows[j].Left = poxX + stamp.Follows[j].RelativeX - stamp.Follows[j]["Width"] / 2 - 1;
                        stamp.Follows[j].Bottom = poxY + stamp.Follows[j].RelativeY - stamp.Follows[j]["Height"] / 2 - 1;
                        stamp.Follows[j]["PageNum"] = group["PageNum"];
                    }
                }
            });
        }
        stamp["Angle"] = group["Angle"];
        stamp["PageNum"] = group["PageNum"];
        stamp.PosX = group["PositionXs"];
        stamp.PosY = group["PositionYs"];
        stamp.Left = poxX - stamp["Width"] / 2 - 1;
        stamp.Bottom = poxY - stamp["Height"] / 2 - 1;
        pos.push(stamp);

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
    }
    return pos;
}

function adaptRoleUser(blockKey, category) {
    var rtn = { Name: blockKey, Category: category };
    return rtn;
}


function onLoadComplete(pageNumber) {
    if (pageNumber == PDFViewerApplication.pagesCount) {
        showWaringTip();
    }
}