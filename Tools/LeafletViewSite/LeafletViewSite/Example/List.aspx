<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Leaflet.API.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文件列表</title>
    <script src="Content/jQuery/jQuery-2.2.0.min.js" type="text/javascript"></script>
    <script src="Content/jQuery/OfficeConvert.js" type="text/javascript"></script>
    <style type="text/css">
        table {
            padding: 0;
            margin: 0;
            border-collapse: collapse;
            width:100%;
        }

        td {
            border: 1px solid #C1DAD7;
            background: #fff;
            font-size: 11px;
            padding: 6px 6px 6px 12px;
            color: #4f6b72;
            font-family:'Microsoft YaHei';
            font-size:12px;
        }
    </style>
    <script type="text/javascript">

        $.ajax({
            url: 'api/File/GetFiles',
            success: function (data, status) {
                var $table = "<table>";
                $table = $table + "<tr><td style='text-align: center;'>文件名称</td>"+
                    "<td style='text-align: center;'>后缀</td>" +
                    "<td style='text-align: center;'>状态</td>" +
                    "<td style='text-align: center;'>版本个数</td>" +
                    "<td style='text-align: center;'>操作</td></tr>";
                data.map(function (item, index) {
                    $table = $table + "<tr><td>" + item.Name +
                        "</td><td>" + item.Suffix +
                        "</td><td>" + (item.State.indexOf('New') >= 0 ? "新增" : item.State.indexOf('Success') >= 0 ? "成功" : "失败") +
                        "</td><td style='text-align: center;'>" + item.Versions.length +
                        '</td><td style="text-align: center;"><a href="#" style="margin-right:20px;" onclick="openWindow(\'' + item.ID + '\',\'Upload\')">升版</a><a href="#" onclick="openWindow(\'' + item.ID + '\',\'View\')">浏览</a></td></tr>';
                });
                $table = $table + "</table>";
                $("#_docList").html($table);
            },
            error: function (xhr, status, err) {
                alert("ajax调用异常: " + status + "," + err);
            }
        });
        function openWindow(id, page) {
            var user = Encrypt("ConvertTestID.测试帐号.部门");
            var url = "http://" + window.location.host + "/" + page + ".aspx?User=" + user + '&ID=' + id;
            if (page == 'Upload')
                url = "http://" + window.location.host + "/" + page + ".aspx?RelevanceID=" + id;
            window.open(url);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="_docList">
        </div>
    </form>
</body>
</html>
