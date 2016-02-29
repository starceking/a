//把数组转化成json字符串，这样就可以存放到localstorage中。通过eval()函数可以将json转为array
function arrayToJson(o) {
    var r = [];
    if (typeof o == "string") return "\"" + o.replace(/([\'\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
    if (typeof o == "object") {
        if (!o.sort) {
            for (var i in o) r.push(i + ":" + arrayToJson(o[i]));
            if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                r.push("toString:" + o.toString.toString());
            }
            r = "{" + r.join() + "}";
        }
        else {
            for (var i = 0; i < o.length; i++) {
                r.push(arrayToJson(o[i]));
            }
            r = "[" + r.join() + "]";
        }
        return r;
    }
    return o.toString();
}
//获取网页参数
function get_url_para(source, name)//alert(get_url_para(window.location.href,"id"));
{
    var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
    if (reg.test(source)) return RegExp.$2; return "";
}
//日期字符串处理
function get_date_str(date) {
    if (date.length == 0) return date;
    if (date.split(" ").length > 1) return date.split(" ", 1);
    else if (date.split("T").length > 1) return date.split("T", 1);
    return date.replace("年", "-").replace("月", "-").replace("日", "");
}
function get_today_str() {
    var today = new Date();
    return (today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate());
}
//控制列表字符串长度
function cut_str_len(str) {
    if (str.length > 12) return (str.substr(0, 11) + "...");
    return str;
}
function cut_str_len_for_td(str) {
    var arr = new Array();
    if (str.length > 12) {
        arr[0] = (str.substr(0, 11) + "...");
        arr[1] = "onmouseover='show_tip(\"" + str + "\")' onmouseout='hide_tip()'";
    }
    else {
        arr[0] = str;
        arr[1] = "";
    }
    return arr;
}
//提示框
function show_tip(msgStr) {
    var d_dialog = document.getElementById('tipdiv');
    d_dialog.innerHTML = msgStr;
    d_dialog.style.left = (document.body.scrollLeft + event.clientX + 10 + "px");
    d_dialog.style.top = (document.body.scrollTop + event.clientY + "px");

    d_dialog.style.visibility = 'visible';
}
function hide_tip() {
    var d_dialog = document.getElementById('tipdiv');
    d_dialog.style.visibility = 'hidden';
}
//日期差
function getdiffdays(s1, s2) {
    s1 = new Date(s1);
    s2 = new Date(s2);
    var days = s2.getTime() - s1.getTime();
    var time = parseInt(days / (1000 * 60 * 60 * 24));
    return time;
}
function date_add_days(dd, dadd) {
    var a = new Date(dd);
    a = a.valueOf();
    a = a + dadd * 24 * 60 * 60 * 1000;
    a = new Date(a);
    return get_date_fm(a, "yyyy-MM-dd");
}
function get_date_fm(str, format) {
    if (str == null || str.length == 0) return "";
    return new Date(str).format(format);
}
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
function getdifftimeStr(s1, s2) {
    if (s1.length == 0 || s2.length == 0) return "";
    s1 = new Date(s1);
    s2 = new Date(s2);
    var date3 = s2.getTime() - s1.getTime();
    //计算出相差天数
    var days = Math.floor(date3 / (24 * 3600 * 1000));
    //计算出小时数
    var leave1 = date3 % (24 * 3600 * 1000);  //计算天数后剩余的毫秒数
    var hours = Math.floor(leave1 / (3600 * 1000));
    //计算相差分钟数
    var leave2 = leave1 % (3600 * 1000);     //计算小时数后剩余的毫秒数
    var minutes = Math.floor(leave2 / (60 * 1000));
    //计算相差秒数
    var leave3 = leave2 % (60 * 1000);  //计算分钟数后剩余的毫秒数
    var seconds = Math.round(leave3 / 1000);

    if (days > 0) days = days + "天";
    else days = "";
    if (hours > 0) hours = hours + "小时";
    else hours = "";
    if (minutes > 0) minutes = minutes + "分钟";
    else minutes = "";
    if (days.length == 0 && hours.length == 0 && minutes.length == 0) minutes = seconds + "秒";
    return days + hours + minutes;
}
//数据库bit显示的转换
function bit_show(b, t, f) {
    if (b.length == 0 || b == "False" || b == "0") return f;
    else return t;
}
//字符串trim
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}
function ltrim(str) {
    return str.replace(/(^\s*)/g, "");
}
function rtrim(str) {
    return str.replace(/(\s*$)/g, "");
}
function format_money(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = "";
    //&& parseFloat(x[1]) > 0
    if (x.length > 1) {
        x2 = ('.' + x[1]);
    }

    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function formatMoney(money) {
    if (!isNaN(money)) {
        if (money.toString().length > 0) {
            money = parseFloat(money);
            if (money != 0) return money.toFixed(2);
        }
    }
    return "0";
}
function getDeleteFlag(json) {
    return (json.delete_flag == 0 ? "√" : "×");
}
var alertMsg = function (config) {
    var __config = this.config = $.extend({
        msg: "none",
        timeout: 0,
        shadeCss: {
            'display': 'block',
            'position': 'absolute',
            'top': '0px',
            'left': '0px',
            'width': '100%',
            'height': '100%',
            'z-index': '20001',
            'background': 'url(/images/fade.png) repeat'
        },
        msgCss: {
            'position': 'absolute',
            'top': '40%',
            'left': '45%',
            'background': '#FF6600',
            'color': '#ffffff',
            'width': 'auto',
            'z-index': '20000',
            'height': 'auto',
            'padding': '18px',
            'font-size': '14px',
            'font-weight': 'bold',
            'border': 'solid 1px #ffffff',
            'border-radius': '6px'
        },
        position: { left: '0.5', top: '0.5' },
        shade: false
    }, config);
    var __id = this.id = "alertMsg" + (Math.round(Math.random() * 100000) + 100000);

    var $dialog = this.overlay = $('<div id="' + __id + '"></div>');
    var $msg = $('<div>' + __config.msg + '</div>').css(__config.msgCss);
    if (__config.shade) {
        $dialog.css(__config.shadeCss);
    }
    $dialog.append($msg);
    //时间
    var __settimeout = null;
    //function
    this.open = function () {
        $("body").append($dialog);
        initPosition();
        if (__config.timeout > 0) {
            __settimeout = setTimeout('$("#' + this.id + '").remove()', __config.timeout);
        }
        return this;
    }
    this.close = function () {
        $("#" + this.id).remove();
    }
    function initPosition() {
        var dialog = $("#" + __id);
        var item = dialog.find("> div").eq(0);
        var left = __config.position.left;
        var top = __config.position.top;
        if (left < 1) {
            left = (dialog.width() - item.width()) * __config.position.left;
        }
        if (top < 1) {
            top = ((dialog.height() - item.height()) * __config.position.top);
        }
        item.css({ "left": left + "px", "top": top + "px" });
    }
    this.setMsgCss = function (css) {
        var _css = $.extend(__config.msgCss, css);
        __config.msgCss = _css;
        this.overlay.find("> div").eq(0).css(_css);
    }
    this.setShadeCss = function (css) {
        var _css = $.extend(__config.shadeCss, css);
        __config.shadeCss = _css;
        this.overlay.css(_css);
    }
    this.setMsg = function (msg) {
        __config.msg = _css;
        this.overlay.find("> div").eq(0).html(msg);
        initPosition();
    }
}