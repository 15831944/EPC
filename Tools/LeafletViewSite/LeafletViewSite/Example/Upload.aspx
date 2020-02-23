<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Leaflet.API.Upload" %>

<!DOCTYPE html>
<head>
    <title>文件上传</title>
    <script src="Content/jQuery/jQuery-2.2.0.min.js" type="text/javascript"></script>
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
        //获取地址栏参数,如果参数不存在则返回空字符串
        function getQueryString(key, url) {
            if (typeof (url) == "undefined")
                url = window.location.search;
            var re = new RegExp("[?&]" + key + "=([^\\&]*)", "i");
            var a = re.exec(url);
            if (a == null) return "";
            return a[1];
        }

        function Upload() {

            var file = $("#tbFileName").val();

            if ((file == null || file == "")) {
                alert("选择要上传的文件。");
                return;
            }

            var formData = new FormData();

            if (file != null && file != "") {
                var files = $("#tbFileName").get(0).files;
                if (files.length > 0) {
                    formData.append("File", files[0]);  // Add the uploaded image content to the form data collection
                }
            }

            if (getQueryString('RelevanceID') != '')
            {
                formData.append("RelevanceID", getQueryString('RelevanceID'));
            }

            $.ajax({
                type: "post",
                url: 'api/File/Upload',
                async: false,
                data: formData,
                contentType: false,
                processData: false,
                success: function (data, status) {
                    if (data)
                        alert("文件上传成功!");
                    location.reload();
                },
                error: function (xhr, status, err) {
                    alert("ajax调用异常: " + status + "," + err);
                }
            });

        }
    </script>
</head>
<body>
    <form id="Form1" enctype="multipart/form-data">
        <div align="center">
            <table border="0" style="text-align:center;">
                <tr style="height: 32px;text-align:left;">
                    <td>文件</td>
                    <td>
                        <input id="tbFileName" name="tbFileName" type="file" style="width: 96%;" multiple="multiple" />
                    </td>
                </tr>
                <tr style="height: 42px">
                    <td style="height: 42px;" colspan="2">
                        <input type="button" id="bnUpload" value="上传文件" onclick="Upload()" style="width: 232px; height: 32px;" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>

</html>
