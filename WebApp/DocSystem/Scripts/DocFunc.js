

/*------------------------------------------------------借阅及下载申请方法开始------------------------------------------------*/

function filedownloadapply(gridID, actionUrl) {
    if (!gridID) gridID = "dataGrid";
    var grid = mini.get(gridID);
    var files = grid.getSelecteds(); if (!files || files.length == 0) { alert("请至少选择一个文件"); return }
    DownloadFile(files, files[0]["SpaceID"], actionUrl);
}

function fileborrowapply(gridID, actionUrl) {

}

function nodeborrowapply(gridID, actionUrl) {

}

function loadCarInfo(spaceID, actionUrl) {
    if ($("#borrowNo").length > 0 || $("#downNo").length > 0) {
        addExecuteParam("SpaceID", spaceID);
        execute("getcarinfo", {
            validateForm: false, showLoading: true, refresh: false, onComplete: function (data) {
                $("#borrowNo").html(data[0].borrow);
                $("#downNo").html(data[1].download);
            }
        })
    }
    else if (window.parent) {
        if ($("#borrowNo", window.parent.document).length > 0 || $("#downNo", window.parent.document).length > 0) {
            addExecuteParam("SpaceID", spaceID);
            execute("getcarinfo", {
                validateForm: false, showLoading: true, refresh: false, onComplete: function (data) {
                    $("#borrowNo", window.parent.document).html(data[0].borrow);
                    $("#downNo", window.parent.document).html(data[1].download);
                }
            })
        }
    }
}

/*------------------------------------------------------借阅及下载申请方法结束------------------------------------------------*/


/*------------------------------------------------------文件浏览，下载，借阅开始------------------------------------------------------*/



function Browse(fileid, spaceID) {
    addExecuteParam("FileID", fileid);
    addExecuteParam("SpaceID", spaceID);
    execute("/DocSystem/View/FileView/BrownFile", {
        validateForm: false, showLoading: true, refresh: false, onComplete: function (data) {
            if (data) {
                var url = ViewerUrl + "?FileID=" + data.SWFFileID; //customCtrl.js 定义的在线浏览地址
                if (fileViewMode == 'tilepicviewer')
                    url = ViewerUrl + "?ID=" + data.SWFFileID;
                if (data.DocType == "Image")
                    url += "&Image=true";
                else
                    url += "&Image=false";
                openWindow(url, { title: '文件浏览', width: "90%", height: "90%", funcType: 'view' });
            }
        }
    });
}

function DownloadDirector(files) {
    var attachs = files.split(',');
    var fileIds = "";
    for (var i = 0; i < attachs.length; i++) {
        if (!attachs[i] || attachs[i] == "" || attachs[i] == "undefined") continue;
        fileIds += attachs[i].split('_')[0] + ",";
    }
    fileIds = fileIds.substring(0, fileIds.length - 1);
    if (fileIds == "" || fileIds == null || fileIds == "undefined") {
        mini.alert("该文件没有任何附件，无法下载"); return;
    }
    if (fileIds)
        DownloadFile(fileIds);
}



function DownloadFiles(files, spaceID) {
    addExecuteParam("files", files);
    addExecuteParam("SpaceID", spaceID);
    execute("/DocSystem/Car/Car/ApplyFileDownload", {
        validateForm: false, showLoading: true, async: false, refresh: false, onComplete: function (data) {
            if (data.length <= 0)
                msgUI("已成功加入下载车！");
            loadCarInfo(spaceID);
            if (data.length > 0) {
                var files = "";
                for (var i = 0; i < data.length; i++) {
                    files += data[i]["MainFile"].split('_')[0] + ",";
                    for (var j = 0; j < ArchiveType.length; j++) {
                        var type = ArchiveType[j];
                        if (type == "PdfFile") type = "PDFFile"
                        if (data[i][type])
                            files += data[i][type].split('_')[0] + ","
                    }
                }
                if (files)
                    DownloadFile(files.substring(0, files.length - 1));
            }
        }
    })

}

function BorrowDir(applyID, onSucess, actionUrl) {
    var requestData = { ApplyFormID: applyID };
    if (!actionUrl) actionUrl = "CarAction.aspx";
    executeAsync(actionUrl, "borrowdirector", requestData, onSucess);
}

function BorrowFile(files, spaceID, actionUrl) {
    if (!actionUrl) actionUrl = "/DocSystem/Car/Car/ApplyFileBorrow";
    addExecuteParam("SpaceID", spaceID);
    addExecuteParam("files", mini.encode(files));
    addExecuteParam("Type", "Borrow");
    execute(actionUrl, {
        validateForm: false, showLoading: true, refresh: false, async: false, onComplete: function (data) {
            msgUI("已成功加入借阅车！");
            loadCarInfo(spaceID);
        }
    });
}

function BorrowNode(nodes, spaceID) {
    addExecuteParam("SpaceID", spaceID);
    addExecuteParam("nodes", nodes);
    execute("/DocSystem/Car/Car/ApplyNodeBorrow", {
        validateForm: false, async: false, showLoading: true, refresh: false, onComplete: function (data) {
            msgUI("已成功加入借阅车！");
            loadCarInfo(spaceID);
            if (typeof (RefreshPicDocCar) != undefined)
                RefreshPicDocCar();
        }
    });
}

function ReturnDetail(actionUrl) {
    if (!actionUrl) actionUrl = "/DocSystem/DocSystemUI/Borrow/ReturnDetail";
    execute(actionUrl, {
        validateForm: false, showLoading: true, refresh: true
    })
}
function ReturnFile(spaceID, actionUrl) {
    addExecuteParam("SpaceID", spaceID);
    if (!actionUrl) actionUrl = "/DocSystem/DocSystemUI/Borrow/ReturnFile";
    execute(actionUrl, {
        validateForm: false, showLoading: true, refresh: true
    })
}
function ReturnNode(spaceID, actionUrl) {
    addExecuteParam("SpaceID", spaceID);
    if (!actionUrl) actionUrl = "/DocSystem/DocSystemUI/Borrow/ReturnNode";
    execute(actionUrl, {
        validateForm: false, showLoading: true, refresh: true
    })
}

/*------------------------------------------------------文件浏览，下载，借阅结束-----------------------------------------------------*/


function openTreeView(nodeID, spaceID) {
    if (!spaceID) { alert("未指定SpaceID，无法转到树形图"); return; }
    if (!nodeID) { alert("未指定节点ID，无法转到树形图"); return; }
    var url = "/DocSystem/View/NodeView/Tree?ID=" + nodeID + "&SpaceID=" + spaceID;
    openWindow(url, { title: '图档树形图', width: "90%", height: "90%", funcType: 'view' });
}

function openTree(nodeID, spaceID) {
    if (!spaceID) { alert("未指定SpaceID，无法转到树形图"); return; }
    if (!nodeID) { alert("未指定节点ID，无法转到树形图"); return; }
    var url = "/DocSystem/Manager/NodeManager/Tree?ID=" + nodeID + "&SpaceID=" + spaceID;
    openWindow(url, { title: '图档树形图', width: $(window.parent.parent).width() - 50, height: $(window).height() + 150, funcType: 'view' });
}

/*----------------------------------------------------------------批量属性修改开始-------------------------------------------------------------*/
function multiEdit(url) {
    var rows = mini.get("dataGrid").getSelecteds();
    if (rows.length == 0) return msgUI("需要选择至少一条操作记录，请重新确认！");
    var list = [];
    for (var i = 0; i < rows.length; i++) {
        list.push({ ID: rows[i].ID, Name: rows[i].Name });
    }
    var data = { URL: url, List: list };
    openWindow("/DocSystem/Common/List", { data: data, title: "批量属性编辑" });
}

/*----------------------------------------------------------------批量属性修改结束-------------------------------------------------------------*/


/*----------------------------------------------------------------全文检索相关开始-------------------------------------------------------------*/
//所有目录节点列表：打开树形图open、点击节点链接viewnode、借阅borrownode、查看文件openfilelist
//所有文件节点列表：列表按钮下载download、借阅borrowfile、在线浏览（单文件）view，
//全文检索点击标题viewfile、下载download、借阅borrowfile、在线浏览（单文件）view，
//记录日志显示于首页
function addHistory(Name, FileID, NodeID, SpaceID, OperateType) {
    addExecuteParam("Name", Name);
    addExecuteParam("FileID", FileID);
    addExecuteParam("NodeID", NodeID);
    addExecuteParam("SpaceID", SpaceID);
    addExecuteParam("OperateType", OperateType);
    execute("/DocSystem/Common/AddHistory", {
        validateForm: false, showLoading: true, async: true, refresh: false
    });
}

/*----------------------------------------------------------------全文检索相关结束-------------------------------------------------------------*/

