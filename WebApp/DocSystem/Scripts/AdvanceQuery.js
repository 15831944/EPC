/*------------------------------------------------------高级查询相关方法开始-----------------------------------------------------*/
var QueryMethod = [{ value: "LK", text: "包含" }, { value: "EQ", text: "等于" }
, { value: "GT", text: "大于" }, { value: "LT", text: "小于" }, { value: "FR", text: "大于等于" }
, { value: "TO", text: "小于等于" }, { value: "UE", text: "不等于" }
];

var QueryLogic = [{ value: "AND", text: "并且" }, { value: "OR", text: "或" }];

function ShowAvanceQueryString(eleid) {
    var str = "";
    var groups = [];
    $.each(advanceQueryData.List, function (index, item) {
        if ($.inArray(item.Group, groups) < 0 && item.Value != "")
            groups.push(item.Group);
    });
    for (var i = 0; i < groups.length; i++) {
        var gStr = "";
        var group = groups[i];
        var gitems = $.grep(advanceQueryData.List, function (item, index) {
            return item.Group == group;
        });
        for (var j = 0; j < gitems.length; j++) {
            var gitem = gitems[j];
            var value = gitem[(gitem.IsEnum == "True" || gitem.DataType == "DateTime") ? "ValueText" : "Value"];
            if (value != "" && value != null) {
                value = value.replace(/，/g, ',');
                if (value.indexOf(',') >= 0) {
                    var _s = "";
                    $.each(value.split(','), function (index, val) {
                        _s += " 或者 " + gitem["Type"] + "的" + setBlue(gitem["AttrName"]) + gitem["MethodText"] + setRed(val);
                    });
                    if (_s != "") {
                        if (value.split(',').length == 1)
                            gStr += _s.substr(4);
                        else
                            gStr += " 并且 (" + _s.substr(4) + ")";
                    }
                }
                else if (value.indexOf(' ') >= 0) {
                    $.each(value.split(' '), function (index, val) {
                        gStr += " 并且 " + gitem["Type"] + "的" + setBlue(gitem["AttrName"]) + gitem["MethodText"] + setRed(val);
                    });
                }
                else
                    gStr += " 并且 " + gitem["Type"] + "的" + setBlue(gitem["AttrName"]) + gitem["MethodText"] + setRed(value);
            }
        }
        if (gStr == "") continue;
        if (groups.length == 1)
            str += gStr.substr(4);
        else
            str += " 或者 (" + gStr.substr(4) + ")";
    }
    if (groups.length != 1)
        str = str.substr(4);
    if (advanceQueryData.QueryNodeName)
        $result = "您需要查询的内容为：" + setBlue(advanceQueryData.QueryTypeName) + "中的" + setRed(advanceQueryData.QueryNodeName) + "<br/>\
        您设置的查询条件为：" + str + "\
        <br/>\
        ";
    else
        $result = "您需要查询的内容为：" + setRed(advanceQueryData.QueryTypeName) + "<br/>\
        您设置的查询条件为：" + str + "\
        <br/>\
        ";
    $("#" + eleid).html($result);
}

function setRed(text) {
    return "<span style='color: red' >" + text + "</span>";
}

function setBlue(text) {
    return "<span style='color: blue' >" + text + "</span>";
}

var ItemNameList = [];
$(function () {
    if (getQueryString("ShowAdvanceQuery").toLowerCase() == "true") {
        $(".mini-toolbar").find("td").last().prepend("<a class='mini-button' id='btnAdvanceQuery' iconcls='icon-search' onclick='showQueryDiv();' plain='true'>高级查询</a>");
        addExecuteParam("spaceID", getQueryString("SpaceID"));
        addExecuteParam("configID", getQueryString("ConfigID"));
        addExecuteParam("type", getQueryString("QueryType"));
        execute("/DocSystem/Common/GetItemEnums", {
            showLoading: false, async: false, onComplete: function (data) {
                ItemNameList = data;
            }
        });
        var str =
                "<div id='divQueryAdvance' style='background-color: #fff;width: 100%;display:none;'>" +
                "    <table id='tableQuery' style='width: 100%;'>" +
                "       <tr>" +
                "           <td style='width: 100%; white-space: nowrap;'>" +
                "               <ul id='ulQueryAdvance' style='list-style-type:none;padding: 0 10px;'>" +
                getQueryLi(true) +
                "               </ul>" +
                "           </td>" +
                "           <td style='white-space: nowrap; vertical-align: top;'>" +
                "               <div style='margin-top:15px;'>" +
                "                   <a class='mini-button' onclick='queryAdvance();' iconcls='icon-find' plain='true'>开始查询</a>" +
                "                   <a class='mini-button' onclick='clearAdvance();' iconcls='icon-undo' plain='true'>清空</a>" +
                "               </div>" +
                "           </td>" +
                "       </tr>" +
                "    </table>" +
                "    <div id='divMoreQuery' class='menu_indent' style='cursor: hand;'>" +
                "       <div onclick='ShowHideMoreQuery()' class='tab_menu_img_up'></div>" +
                "   </div>" +
                "</div>";
        $("#btnAdvanceQuery").parents(".mini-toolbar").after($(str));
        mini.parse();
    }
})

function ShowHideMoreQuery() {
    var img = $("#divMoreQuery").find("div");
    if ($(img).hasClass("tab_menu_img_down")) {
        $("#tableQuery").show();
        $(img).removeClass("tab_menu_img_down").addClass("tab_menu_img_up");
    }
    else {
        $("#tableQuery").hide();
        $(img).removeClass("tab_menu_img_up").addClass("tab_menu_img_down");
    }
    setDataGridHeight();
}

function setDataGridHeight() {
    $(".mini-fit").each(function () {
        var layout = mini.get("#" + $(this).attr("id"));
        if (layout)
            layout.doLayout();
    });
}

function getQueryLi(isFirst) {
    var id = Math.random().toString(36).substr(2, 15);
    var str =
        "<li id=" + id + " style='margin-bottom:3px'>" +
        "   <input id='" + id + "_ItemName' name='ItemName' class='mini-treeselect' allowinput='false' valuefromselect='true' style='width:25%;' onbeforenodeselect='beforenodeselect' emptytext='请选择属性名称...' data='ItemNameList' expandOnLoad='true' onvaluechanged='onItemChange'/>" +
        "   <input id='" + id + "_Method' name='Method' class='mini-combobox' textfield='text' valuefield='value' allowinput='false' valuefromselect='true' style='width:10%;' value='LK' data='QueryMethod' />" +
        "   <input id='" + id + "_Value' name='Value' class='mini-textbox' emptytext='请输入关键词' style='width:50%;' visible='true' onenter='queryAdvance'/>" +
        "   <input id='" + id + "_Select' name='Select' class='mini-combobox' emptytext='请选择关键词' textfield='text' valuefield='value' showNullItem='true' allowinput='false' valuefromselect='true' multiSelect='true' style='width:50%;' visible='false'/>" +
        "   <input id='" + id + "_Logic' name='Logic' class='mini-combobox' textfield='text' valuefield='value' showNullItem='true' allowinput='false' valuefromselect='true' style='width:10%;' enabled='false' data='QueryLogic' />";
    if (isFirst)
        str += "   <a id='" + id + "_Add' class='mini-button' onclick='addLi();' iconcls='icon-add' plain='true'>&nbsp;</a>";
    else
        str += "   <a id='" + id + "_Remove' class='mini-button' onclick='removeLi(this);' iconcls='icon-removered' plain='true'>&nbsp;</a>";
    str += "</li>";
    return str;
}

var isShowed = false;
function showQueryDiv() {
    if (isShowed) {
        $("#divQueryAdvance").hide();
        $("#divQueryForm").show();
        $("#btnAdvanceQuery").removeClass("hover");
        $("#btnSearch").show();
        $("#btnClear").show();
    }
    else {
        $("#divQueryAdvance").show();
        $("#divQueryForm").hide();
        $("#btnAdvanceQuery").addClass("hover");
        $("#btnSearch").hide();
        $("#btnClear").hide();
    }
    setDataGridHeight();
    isShowed = !isShowed;
}

function onItemChange(e) {
    var item = e.source.getSelectedNode();
    if (item.isEnum) {
        var method = mini.get(e.sender.el.parentElement.id + "_Method");
        var value = mini.get(e.sender.el.parentElement.id + "_Value");
        var select = mini.get(e.sender.el.parentElement.id + "_Select");
        method.setEnabled(false);
        method.setValue("IN");
        method.setText("包含");
        value.hide();
        select.setUrl("/DocSystem/Common/GetEnum?EnumKey=" + item.enumKey);
        select.show();
    }
    else {
        var method = mini.get(e.sender.el.parentElement.id + "_Method");
        var value = mini.get(e.sender.el.parentElement.id + "_Value");
        var select = mini.get(e.sender.el.parentElement.id + "_Select");
        method.setEnabled(true);
        method.setValue("LK");
        method.setText("包含");
        value.show();
        select.hide();
    }
}

function addLi() {
    $("#ulQueryAdvance").append(getQueryLi(false));
    mini.parse();
    var liList = $("#ulQueryAdvance").children("li");
    for (var i = 0; i < liList.length - 1; i++) {
        mini.get(liList[i].id + "_Logic").setEnabled(true);
        mini.get(liList[i].id + "_Logic").setRequired(true);
        if (i == liList.length - 2) {
            mini.get(liList[i].id + "_Logic").setValue("AND");
        }
    }
}

function removeLi(e) {
    e.el.parentElement.parentElement.removeChild(e.el.parentElement);
    var liList = $("#ulQueryAdvance").children("li");
    mini.get(liList[liList.length - 1].id + "_Logic").setEnabled(false);
    mini.get(liList[liList.length - 1].id + "_Logic").setRequired(false);
    mini.get(liList[liList.length - 1].id + "_Logic").setValue("");
    mini.get(liList[liList.length - 1].id + "_Logic").setText("");
    setDataGridHeight();
}

function clearAdvance() {
    var liList = $("#ulQueryAdvance").children("li");
    for (var i = 0; i < liList.length; i++) {
        if (i == 0) {
            mini.get(liList[i].id + "_ItemName").setValue("");
            mini.get(liList[i].id + "_Value").setValue("");
            mini.get(liList[i].id + "_Logic").setValue("");
            mini.get(liList[i].id + "_Logic").setText("");
        }
        else
            liList[i].parentElement.removeChild(liList[i]);
    }
    mini.get(liList[0].id + "_Logic").setEnabled(false);
    mini.get(liList[0].id + "_Logic").setRequired(false);
    setDataGridHeight();
}

function queryAdvance() {
    var queryList = [];
    var liList = $("#ulQueryAdvance").children("li");
    for (var i = 0; i < liList.length; i++) {
        var ItemName = mini.get(liList[i].id + "_ItemName").getValue();
        var Method = mini.get(liList[i].id + "_Method").getValue();
        var Value = mini.get(liList[i].id + "_Value").getValue();
        if (Value == "" || Value == null || Value == undefined) {
            Value = mini.get(liList[i].id + "_Select").getValue();
        }
        var Logic = "AND"
        if (i > 0)
            Logic = mini.get(liList[i - 1].id + "_Logic").getValue();
        if ((ItemName == "" || ItemName == null || ItemName == undefined) ||
            (Method == "" || Method == null || Method == undefined) ||
            (Value == "" || Value == null || Value == undefined) ||
            (Logic == "" || Logic == null || Logic == undefined))
            continue;
        queryList.push({ ItemName: ItemName, Method: Method, Value: Value, Logic: Logic });
    }
    var grid = mini.get("#dataGrid");
    if (grid) {
        grid.load({ QueryItems: mini.encode(queryList) });
    }
    else if (loading) {
        addExecuteParam("QueryItems", mini.encode(queryList));
        loading()
    }
    setDataGridHeight();
}

function beforenodeselect(e) {
    if (e.isLeaf == false) e.cancel = true;
}
/*------------------------------------------------------高级查询相关方法结束-----------------------------------------------------*/