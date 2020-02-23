<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Leaflet.API.User" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户列表</title>
    <script src="Content/jQuery/jQuery-2.2.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        table {
            padding: 0;
            margin: 0;
            border-collapse: collapse;
            width: 100%;
        }

        td {
            border: 1px solid #C1DAD7;
            background: #fff;
            font-size: 11px;
            padding: 6px 6px 6px 12px;
            color: #4f6b72;
            font-family: 'Microsoft YaHei';
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        function Upload() {

            var file = $("#FileName").val();
            var userID = $("#UserID").val();
            var userName = $("#UserName").val();
            var dept = $("#Dept").val();
            if ((userID == null || userID == "")
                || (userName == null || userName == "")
                || (dept == null || dept == "")) {
                alert("请把必填信息填写完整。");
                return;
            }

            var formData = new FormData();

            if (file != null && file != "") {
                var files = $("#FileName").get(0).files;
                if (files.length > 0) {
                    formData.append("File", files[0]);  
                }
            }

            var data = [{ UserID: userID, UserName: userName, Dept: dept }];
            formData.append("Data", JSON.stringify(data));

            $.ajax({
                type: "post",
                url: 'api/User/SyncUser',
                async: false,
                data: formData,
                contentType: false,
                processData: false,
                success: function (data, status) {
                    location.reload();
                },
                error: function (xhr, status, err) {
                    alert("ajax调用异常: " + status + "," + err);
                }
            });
        }
        $.ajax({
            url: 'api/User/Users',
            success: function (data, status) {
                var $table = "<table>";
                $table = $table + "<tr><td style='text-align: center;'>用户ID</td>" +
                    "<td style='text-align: center;'>用户名</td>" +
                    "<td style='text-align: center;'>部门</td>";
                JSON.parse(data).map(function (item, index) {
                    $table = $table + "<tr><td>" + item.UserID +
                        "</td><td>" + item.UserName +
                        "</td><td>" + item.Dept +
                        "</td></tr>";
                });
                $table = $table + "</table>";
                $("#_userList").html($table);
            },
            error: function (xhr, status, err) {
                alert("ajax调用异常: " + status + "," + err);
            }
        });
        function openWindow(id) {
            window.open("http://" + window.location.host + "/View.aspx?ID=" + id + "&UserID=a87e0111-4603-4731-947e-860d5d552b9a");
        }
    </script>
</head>
<body>
    <form id="Form1" enctype="multipart/form-data">
        <div align="center" style="margin-bottom: 20px;">
            <table border="0">
                <tr style="height: 32px">
                    <td align="left" colspan="2" style="width: 50%">
                        <input id="FileName" name="tbFileName" type="file" style="width: 96%;" multiple="multiple" />
                    </td>
                    <td style="width: 15%">用户ID</td>
                    <td style="width: 35%">
                        <input id="UserID" name="tbUserID" type="text" /></td>
                </tr>
                <tr style="height: 32px">
                    <td>用户名</td>
                    <td align="left">
                        <input id="UserName" name="tbUserName" type="text" />
                    </td>
                    <td>部门</td>
                    <td>
                        <input id="Dept" name="tbDept" type="text" /></td>
                </tr>
                <tr style="height: 42px">
                    <td style="height: 42px;text-align:center;" colspan="4">
                        <input type="button" id="bnUpload" value="同步用户" onclick="Upload()" style="width: 232px; height: 32px;" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="_userList"></div>
    </form>
</body>
</html>
