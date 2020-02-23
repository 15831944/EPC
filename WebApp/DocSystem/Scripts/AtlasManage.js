/******************************图集管理界面图片上传******************************/
$(function () {
    if (isAtlas) {
        var str = '\
<fieldset>\    <legend>图片附件</legend>\    <table width="100%">\
        <tr>\
            <td>\
                <div style="width:100%;">\
                    <div id="toolbar2" class="mini-toolbar" style="border-bottom:0;padding:0px;">\
                        <table style="width:100%;">\
                            <tr>\
                                <td id="tdUpload" style="width:78px;text-align:right">\
                                    <input id="pictureFile" name="pictureFile" type="file" style="float:left;" /></div>\
                                </td>\
                                <td>\
                                    <a id="btnUp" class="mini-button" iconCls="icon-up" onclick="moveUp()" plain="true">上移</a>\
                                    <a id="btnDown" class="mini-button" iconCls="icon-down" onclick="moveDown()" plain="true">下移</a>\
                                </td>\
                                <td align="right" style="width:80px">\
                                    <a class="mini-button" iconCls="icon-extend-preview" onclick="preview()" plain="true">预览</a>\
                                </td>\
                            </tr>\
                        </table>\
                    </div>\
                </div>\
                <div id="AtlasFile" class="mini-datagrid" showPager="false" style="width:100%;height:380px;" allowCellEdit="true" allowCellSelect="true" >\
                    <div property="columns"> \
                        <div type="indexcolumn" headerAlign="center">序号</div>\
                        <div field="PictureName" width="100" headerAlign="center" allowMove="false">图片名称</div>\
                        <div field="Description" headerAlign="center" width="200" allowMove="false">描述\
                            <input property="editor" class="mini-textbox" style="width:100%;"/>\
                        </div>\
                        <div name="action" width="20" headerAlign="center" visible="true" align="center" renderer="onActionRenderer" cellStyle="padding:0;"></div>\
                    </div>\
                </div>\
            </td>\
        </tr>\
    </table>\
</fieldset>\
';
        $("#dataForm").append(str);
        mini.parse();

        addH5Upload({
            ID: "pictureFile",
            title: "批量上传",
            buttonClass: "uploadify-button",
            fileType: "image/*",
            closeWindow: false,
            width: 85,
            height: 25,
            isMulti: true,
            url: "/DocSystem/Common/UploadPicture",
            onComplete: function (data) {
                var grid = mini.get("AtlasFile");
                var d = mini.decode(data);
                grid.addRow({ PictureName: getMiniFileName(d.PictureName), PictureFileID: d.PictureName, ThumbFileID: d.ThumbName })
            }
        });
    }
});

function preview() {
    var grid = mini.get("AtlasFile");
    var data = {};
    data["GallaryList"] = grid.getData();
    var form = new mini.Form("#dataForm");
    var _formData = form.getData();
    data["FormData"] = _formData;
    if (data["GallaryList"].length > 0) {
        var title = "预览";
        allowResizeOpenWindow = false;
        openWindow("/DocSystem/Common/Gallery?ID=" + mini.getbyName("ID").getValue() + "&SpaceID=" + getQueryString("SpaceID"), {
            data: data,
            width: "92%",
            height: "95%",
            title: title,
            showMaxButton: false
        });
    }
    else
        msgUI("请上传图片附件");
}

function onActionRenderer(e) {
    var grid = e.sender;
    var record = e.record;
    var uid = record._uid;
    var rowIndex = e.rowIndex;
    var s = '<a href="javascript:delRow(\'' + uid + '\')" title="删除"><img border="0px" src="/CommonWebResource/RelateResource/image/ico16/del.gif" width="16px" height="16px" align="absmiddle" /></a>';
    return s;
}

function delRow(row_uid) {
    var grid = mini.get("AtlasFile");
    var row = grid.getRowByUID(row_uid);
    if (row) {
        msgUI("确认要删除该记录？", 2, function (action) {
            if (action == "ok") {
                grid.removeRow(row);
            }
        });
    }
}

function moveUp() {
    var grid = mini.get("AtlasFile");
    var row = grid.getSelected();
    if (row) {
        var index = grid.indexOf(row);
        grid.moveRow(row, index - 1);
    }
}

function moveDown() {
    var grid = mini.get("AtlasFile");
    var row = grid.getSelected();
    if (row) {
        var index = grid.indexOf(row);
        grid.moveRow(row, index + 2);
    }
}
/******************************图集管理界面图片上传******************************/