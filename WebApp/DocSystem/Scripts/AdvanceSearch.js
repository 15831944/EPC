
var QueryLiCount = 3; //查询条件行数
var QuerySpaceID = getQueryString("SpaceID");

/***********************整体控件绘制***************************/
var Data_SpaceCheckBoxEnum = [];//Space复选框数据源
//绘制整体控件
function DrawBar(spaceId, queryList) {
    execute("/DocSystem/View/MyPortal/GetSpaceTable", {
        showLoading: false, async: false, onComplete: function (data) {
            $.each(data, function (i) {
                var spaceID = data[i].ID;
                var spaceName = data[i].Name;
                Data_SpaceCheckBoxEnum.push({ id: spaceID, text: spaceName });
            });
        }
    });
    var itemLiStr = "";
    var liCount = 1;
    var str =
    "<div class='row content indexqv' >" +
            "<div id='divQueryAdvance' style='background-color: #fff;width: 100%;'>" +
            "    <table id='tableQuery' style='width: 100%;'>" +
            "<tr style='height: 10px;'><tr>" +
            "       <tr>" +
            "          <td> <div style='float:left; font-weight: 700; font-size:12pxfont-size: 14px;margin-left: 10px;}'>请输入搜索条件：</div></td>" +
            "          <td> <div id='close' style='float:right; font-weight:500; font-size:35px;cursor: pointer;margin-top: -15px;'>×</div></td>" +
            "       </tr>" +
            "<tr style='height: 10px;'><tr>" +
            "       <tr>" +
            "           <td> <div id='cblSpace' class='mini-checkboxlist'  multiSelect='false' repeatItems='1' repeatDirection='vertical' onvaluechanged='onSpaceValueChanged' \
                                         textField='text' valueField='id' \
                                         data=Data_SpaceCheckBoxEnum>\
                                     </div></td>" +
            "           <td> <a class='btn btn-primary btn-sm' onclick='queryAdvance();' style='width:58%;margin-left:25px' plain='true'>搜索</a></td>" +
            "       </tr>" +
            "<tr style='height: 10px;'><tr>" +
            "       <tr>" +
            "           <td style='width: 85%; white-space: nowrap;'>" +
            "               <ul id='ulQueryAdvance' style='list-style-type:none;padding: 0 10px;'>" +
             getItemLiStr(0) +
            "               </ul>" +
            "           </td>" +
            "           <td style='white-space: nowrap; vertical-align: top;'>" +
            "               <div>" +
            "                   <a class='btn btn-default btn-sm ' onclick='clearAdvance();' style='width:58%;margin-left:25px' plain='true' >清空</a>" +
            "               </div>" +
            "           </td>" +
            "       </tr>" +
            "    </table>" +
            "</div>"+
    "</div>";
    $('.content').after($(str));
    mini.parse();
    //高级搜索栏选项样式调整
    $("#cblSpace table label").css({ "vertical-align": "sub","margin-right":"10px"});
    $("#cblSpace table input").css({ "margin-right": "3px" });
    if (spaceId) {
        QuerySpaceID = spaceId;
        var spaceCB = mini.get('cblSpace');
        spaceCB.setValue(QuerySpaceID);
        spaceCB.doValueChanged();
    }
    if (queryList) {
        autoAddLi(queryList);
    }
    //为cblSpace输入条件控件添加样式
    $('#cblSpace').css({"padding-left":"11px"});
}

function autoAddLi(queryList) {
    var dotIndex = 1;
    var dot = setInterval(function () {
        if (dotIndex >= queryList.length) {
            setDefaulQueryData(queryList);
            clearInterval(dot);
        }
        else
            addLi();
        dotIndex = dotIndex + 1;
    }, 100);
}

function setDefaulQueryData(queryList) {
    //根据传入的查询条件列表，给条件行各控件赋值
    $.each(queryList, function (index, item) {
        var id = "LiQueryItem_" + index;
        mini.get(id + "_ItemName").setValue(item["ItemName"]);
        mini.get(id + "_ItemName").doValueChanged();
        mini.get(id + "_Method").setValue(item["Method"]);

        if (index < queryList.length - 1) {
            mini.get(id + "_Logic").setValue(item["Logic"]);
            mini.get(id + "_Logic").setEnabled(true);
        }
        mini.get(id + "_Value").setValue(item["Value"]);
        mini.get(id + "_Select").setValue(item["Value"]);
    })
}

//Space选择事件：根据Space设置查询属性下拉枚举数据源
function onSpaceValueChanged(e) {
    QuerySpaceID = e.sender.getValue();
    clearAdvance();
    addExecuteParam("spaceID", QuerySpaceID);
    execute("/DocSystem/View/MyPortal/GetItemEnums", {
        showLoading: false, async: false, onComplete: function (data) {
            Data_ItemNameTreeList = data;
            SetItemNameData();
        }
    });
}

/***********************查询条件行相关***************************/
var Data_ItemNameTreeList = [];
var Data_ItemLogicEnum = [{ value: "AND", text: "并且" }, { value: "OR", text: "或" }];
var Data_ItemMethodEnum = [{ value: "LK", text: "包含(like)" }, { value: "EQ", text: "等于" }, { value: "IN", text: "包含(in)" }
, { value: "GT", text: "大于" }, { value: "LT", text: "小于" }, { value: "FR", text: "大于等于" }
, { value: "TO", text: "小于等于" }, { value: "UE", text: "不等于" }
];
//获取查询条件行Li模板
function getItemLiStr(index) {
    var id = "LiQueryItem_" + index;
    var str =
        "<li id='" + id + "' style='margin-bottom:3px'>" +
        "   （   <input id='" + id + "_ItemName' name='" + id + "_ItemName' class='mini-treeselect' allowinput='false' valuefromselect='false' textField='text' valueField='id' parentField='pid' style='width:22%;' onbeforenodeselect='onBeforeItemNameTreeNodeSelect' emptytext='请选择属性名称...' expandOnLoad='true' onvaluechanged='onItemChange' data=Data_ItemNameTreeList />" +
        "   <input id='" + id + "_Method' name='" + id + "_Method' class='mini-combobox' textfield='text' valuefield='value' allowinput='false' valuefromselect='true' style='width:90px;' value='LK' data='Data_ItemMethodEnum' />" +
        "   <input id='" + id + "_Value' name='" + id + "_Value' class='mini-textbox' emptytext='请输入关键词' style='width:50%;' visible='true' onenter='queryAdvance()'/>" +
        "   <input id='" + id + "_Select' name='" + id + "_Select' class='mini-combobox' emptytext='请选择关键词' textfield='text' valuefield='value' showNullItem='true' allowinput='false' valuefromselect='true' multiSelect='true' style='width:50%;' visible='false' data=[] />   ）" +
        "   <input id='" + id + "_Logic' name='" + id + "_Logic' class='mini-combobox' textfield='text' valuefield='value' showNullItem='false' allowinput='false' valuefromselect='true' style='width:10%;' enabled='false' data=Data_ItemLogicEnum />";
    if (index == 0)
        str += "   <a id='" + id + "_Add' class='mini-button' onclick='addLi();' iconcls='icon-add' plain='true'>&nbsp;</a>";
    else
        str += "   <a id='" + id + "_Remove' class='mini-button' onclick='removeLi(this);' iconcls='icon-removered' plain='true'>&nbsp;</a>";
    str += "</li>";
    return str;
}

//增加查询条件行
function addLi() {
    var existCount = $("#ulQueryAdvance>li").length;
    if (existCount >= QueryLiCount)
        return;

    $("#ulQueryAdvance").append(getItemLiStr(existCount));
    mini.parse();
    setLastLi();
}
//删除查询条件行
function removeLi(e) {
    e.el.parentElement.parentElement.removeChild(e.el.parentElement);
    setLastLi();
    mini.parse();
}
//清空所有行，并新增一空行
function clearAdvance() {
    $("#ulQueryAdvance").html("");
    addLi();
}

//最后一行条件的与或逻辑置灰
function setLastLi() {
    for (var i = 0; i < $("#ulQueryAdvance>li").length; i++) {
        var itemLogic = mini.get("LiQueryItem_" + i + "_Logic")
        if (i != $("#ulQueryAdvance>li").length - 1) {
            if (!itemLogic.getValue())
                itemLogic.setValue("AND");
            itemLogic.setEnabled(true);
        }
        else {
            itemLogic.setValue("");
            itemLogic.setEnabled(false);
        }
    }
}

//设置所有查询属性下拉框的数据源
function SetItemNameData() {
    var liList = $("#ulQueryAdvance").children("li");
    for (var i = 0; i < liList.length; i++) {
        mini.get("LiQueryItem_" + i + "_ItemName").setData(Data_ItemNameTreeList);
    }
}

//查询属性事件，只能选择叶子阶段
function onBeforeItemNameTreeNodeSelect(e) {
    if (e.isLeaf == false) e.cancel = true;
}
//查询属性事件，值选择后改变
function onItemChange(e) {
    var item = e.source.getSelectedNode();
    if (typeof (item) != 'undefined') {
        var method = mini.get(e.sender.el.parentElement.id + "_Method");
        var value = mini.get(e.sender.el.parentElement.id + "_Value");
        var select = mini.get(e.sender.el.parentElement.id + "_Select");
        method.setValue("LK");
        if (item.isEnum) {
            select.setUrl("/DocSystem/Common/GetEnum?EnumKey=" + item.enumKey);
            method.setValue("IN");
            value.setValue("");
            select.setValue("");
            method.setEnabled(false);
            select.show();
            value.hide();
        }
        else {
            method.setEnabled(true);
            value.show();
            select.hide();
        }
    }
}

//根据查询条件，创建查询条件列表QueryList
function CreateQueryListByAdvance() {
    var queryList = [];
    var existCount = $("#ulQueryAdvance>li").length;
    for (var i = 0; i < existCount; i++) {
        var id = "LiQueryItem_" + i;
        var ItemName = mini.get(id + "_ItemName").getValue();
        var Method = mini.get(id + "_Method").getValue();
        var Value = mini.get(id + "_Value").getValue();
        if (mini.get(id + "_Select").visible) {
            Value = mini.get(id + "_Select").getValue();
        }
        var Logic = "AND"
        if (i < existCount - 1)
            Logic = mini.get(id + "_Logic").getValue();
        if ((ItemName == "" || ItemName == null || ItemName == undefined) ||
            (Method == "" || Method == null || Method == undefined) ||
            (Value == "" || Value == null || Value == undefined) ||
            (Logic == "" || Logic == null || Logic == undefined))
            continue;
        var queryItem = { ItemName: ItemName, Method: Method, Value: Value, Logic: Logic };
        queryList.push(queryItem);
    }
    return queryList;
}