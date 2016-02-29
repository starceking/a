function stock_info_GetNowPriceMul(nums) {
    ws_get_json("", "StockInfo", "GetNowPriceMul", {
        "nums": nums
    }, "", "", stock_info_GetNowPriceMul_sucFunc);
}
function stock_info_GetNowPriceMul_sucFunc(table, page, json) {
    if (json != null) {
        var arr = json.split(';');
        var tableObj = document.getElementById("查询列表");

        for (var j = 0; j < arr.length; j++) {
            var suba = arr[j].split(',');
            var num = suba[0];
            var val = suba[1];

            for (var i = 0; i < tableObj.rows.length; i++) {
                if (tableObj.rows[i] && tableObj.rows[i].cells[13] && tableObj.rows[i].cells[3] &&
                    tableObj.rows[i].cells[8] && tableObj.rows[i].cells[7] && tableObj.rows[i].cells[6]) {
                    if ((tableObj.rows[i].cells[13].innerText == "持仓中" ||
                        tableObj.rows[i].cells[13].innerText == "待平仓") && tableObj.rows[i].cells[3].innerText == num) {
                        if (val == "停牌") {
                            tableObj.rows[i].cells[8].innerText = "停牌";
                        }
                        else {
                            var profit = Math.round((parseFloat(val) - parseFloat(tableObj.rows[i].cells[7].innerText)) *
                             parseFloat(tableObj.rows[i].cells[6].innerText), 2);
                            tableObj.rows[i].cells[8].innerText = profit;
                            if (profit >= 0) tableObj.rows[i].cells[8].style.color = "red";
                            else tableObj.rows[i].cells[8].style.color = "green";
                        }
                    }
                }
            }
        }
    }
}
function choose_stock(key) {
    $.ajax({
        type: 'GET',
        url: "http://suggest3.sinajs.cn/suggest/type=111&key=" + key + "&name=suggestdata",
        data: {},
        cache: true,
        async: false,
        dataType: 'script',
        success: function (data) {
            console.log(suggestdata);
            var DICT_id = new Array();
            var DICT_name = new Array();
            var idx = 0;
            var arr = suggestdata.split(';');
            for (var i = 0; i < arr.length; i++) {
                var data = arr[i].split(",");
                DICT_id[idx] = data[2];
                DICT_name[idx] = data[4];
                idx++;
                if (idx == 1) {
                    $("#股票代码i").val(data[2]);
                    $("#股票名称i").val(data[4]);
                }
            }
            init_select("检索股票s", "", DICT_id, DICT_name);
        }
    });
}