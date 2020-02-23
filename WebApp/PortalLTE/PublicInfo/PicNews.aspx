<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PicNews.aspx.cs" Inherits="Portal.PublicInfo.PicNews" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="/CommonWebResource/CoreLib/Basic/jQuery/jquery-1.6.2.min.js" type="text/javascript"></script>  
    <script src="/CommonWebResource/CoreLib/MiniUI/miniui.js" type="text/javascript"></script>  
    <script src="/CommonWebResource/CoreLib/MiniUI/baseMiniuiExt.js" type="text/javascript"></script>   
    <script src="/MvcConfig/miniuiExt.js" type="text/javascript"></script>           
    <script src="/CommonWebResource/Theme/Default/MiniCssInc.js" type="text/javascript"></script>
    <%=StyleType %>
</head>
<body>
	<div id="slideBox">
		<ul id="show_pic" style="left:0px">
            <%=PicHTML %>
		</ul>
		<div id="slideText"></div>
		<ul id="iconBall">
            <%=IconBallHTML%>
		</ul>
		<ul id="textBall">
            <%=TextBallHTML%>
		</ul>
	</div><!--slideBox end-->

<script type="text/javascript"> 
    $("#slideBox,#show_pic li,#textBall li,#slideText,#show_pic li img").css('width', document.body.clientWidth);
    var glide = new function () {
        function $id(id) { return document.getElementById(id); };
        this.layerGlide = function (auto, oEventCont, oTxtCont, oSlider, sSingleSize, second, fSpeed, point) {
            var oSubLi = $id(oEventCont).getElementsByTagName('li');
            var oTxtLi = $id(oTxtCont).getElementsByTagName('li');
            var interval, timeout, oslideRange;
            var time = 1;
            var speed = fSpeed
            var sum = oSubLi.length;
            var a = 0;
            var delay = second * 3000;
            var setValLeft = function (s) {
                return function () {
                    oslideRange = Math.abs(parseInt($id(oSlider).style[point]));
                    $id(oSlider).style[point] = -Math.floor(oslideRange + (parseInt(s * sSingleSize) - oslideRange) * speed) + 'px';
                    if (oslideRange == [(sSingleSize * s)]) {
                        clearInterval(interval);
                        a = s;
                    }
                }
            };
            var setValRight = function (s) {
                return function () {
                    oslideRange = Math.abs(parseInt($id(oSlider).style[point]));
                    $id(oSlider).style[point] = -Math.ceil(oslideRange + (parseInt(s * sSingleSize) - oslideRange) * speed) + 'px';
                    if (oslideRange == [(sSingleSize * s)]) {
                        clearInterval(interval);
                        a = s;
                    }
                }
            }

            function autoGlide() {
                for (var c = 0; c < sum; c++) { oSubLi[c].className = ''; oTxtLi[c].className = ''; };
                clearTimeout(interval);
                if (a == (parseInt(sum) - 1)) {
                    for (var c = 0; c < sum; c++) { oSubLi[c].className = ''; oTxtLi[c].className = ''; };
                    a = 0;
                    oSubLi[a].className = "active";
                    oTxtLi[a].className = "active";
                    interval = setInterval(setValLeft(a), time);
                    timeout = setTimeout(autoGlide, delay);
                } else {
                    a++;
                    oSubLi[a].className = "active";
                    oTxtLi[a].className = "active";
                    interval = setInterval(setValRight(a), time);
                    timeout = setTimeout(autoGlide, delay);
                }
            }

            if (auto) { timeout = setTimeout(autoGlide, delay); };
            for (var i = 0; i < sum; i++) {
                oSubLi[i].onmouseover = (function (i) {
                    return function () {
                        for (var c = 0; c < sum; c++) { oSubLi[c].className = ''; oTxtLi[c].className = ''; };
                        clearTimeout(timeout);
                        clearInterval(interval);
                        oSubLi[i].className = "active";
                        oTxtLi[i].className = "active";
                        if (Math.abs(parseInt($id(oSlider).style[point])) > [(sSingleSize * i)]) {
                            interval = setInterval(setValLeft(i), time);
                            this.onmouseout = function () { if (auto) { timeout = setTimeout(autoGlide, delay); }; };
                        } else if (Math.abs(parseInt($id(oSlider).style[point])) < [(sSingleSize * i)]) {
                            interval = setInterval(setValRight(i), time);
                            this.onmouseout = function () { if (auto) { timeout = setTimeout(autoGlide, delay); }; };
                        }
                    }
                })(i)
            }
        }
    }

    //调用语句
    glide.layerGlide(
	    true,         //设置是否自动滚动
	    'iconBall',   //对应索引按钮
	    'textBall',   //标题内容文本
	    'show_pic',   //焦点图片容器
	    document.body.clientWidth,          //设置滚动图片位移像素
	    2, 		  //设置滚动时间2秒 
	    0.1,          //设置过渡滚动速度
	    'left'		  //设置滚动方向“向左”
    );
</script>
<script type="text/javascript">
    function openGallary(id, title) {
        allowResizeOpenWindow = false;
        title = title.length > 40 ? title.substring(0, 40) + "..." : title;
        var url = "/base/PortalBlock/NewsImage/Gallery?ID=" + id;
        if (parent.NoPopupLayer && parent.NoPopupLayer != "true") {
            openWindow(url, {
                width: "92%",
                height: "95%",
                title: title,
                showMaxButton: false
            });
        } else {
            window.open(url);
        }
    }
</script>
</body>
</html>
