<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Leaflet.API.Index" %>


<!DOCTYPE html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线批注</title>
    <script src="Content/jQuery/jQuery-2.2.0.min.js" type="text/javascript"></script>
    <link href="Content/Leaflet/Leaflet.css" rel="stylesheet" type="text/css" />
    <script src="Content/Leaflet/Leaflet.js" type="text/javascript"></script>
    <link href="Content/Index.css" rel="stylesheet" type="text/css" />

</head>

<script src="Content/jQuery/OfficeConvert.js" type="text/javascript"></script>
<body>
    <div class="preview-container">
        <div class="map-wrapper">
            <span class="version-title"></span>
            <div id="map"></div>
        </div>
    </div>
    <div class="comment-container">
        <div class="top-head">
            <div class="version-info">
                <div class="version-item">选择版本:</div>
                <div class="version-select"></div>
                <div class="version-item version-download" onclick="download()">下载</div>
            </div>
            <span class="doc-name"></span>
        </div>
        <div class="comment-body">
            <div class="content">
            </div>
            <div class="send-container">
                <textarea rows="3" placeholder="请输入评论内容" class="ant-input-textarea"></textarea><div class="btn-container">
                    <span class="btn-sel-user"></span>
                    <button type="button" class="ant-btn-primary" onclick="_sendComment(this)"><span>发 送</span></button>
                </div>
            </div>
        </div>
    </div>
    <div class="office-display" style="line-height: 300px; display: none; border: 0px;text-align:center;width:100%;">
        浏览文件正在处理，请稍后再试！
    </div>
</body>
</html>



