function id_comparison_Insert(btn, number, name, amount, ref_table, ref_id) {
    ws_exec(btn, "POST", "IdComparison", "Insert", {
        "number": number, "name": name, "amount": amount, "ref_table": ref_table, "ref_id": ref_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_comparison_Update(btn, id, number, name, amount, ref_table, ref_id) {
    ws_exec(btn, "PUT", "IdComparison", "Update", {
        "number": number, "name": name, "amount": amount, "ref_table": ref_table, "ref_id": ref_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_comparison_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "IdComparison", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_comparison_GetOne(id) {
    ws_get_json("", "IdComparison", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", id_comparison_GetOne_sucFunc);
}
function id_comparison_GetOne_sucFunc(table, page, json) {
    if (json != null) {
        fill_input_nde("所属单位i", json.dept_no);
        fill_input_nde("警号i", json.police_no);
        fill_input_nde("姓名i", json.name);
        fill_input_nde("身份证i", json.id_card_no);
        if (json.auth_ids) {
            var arr = json.auth_ids.split(',');
            for (var i = 0; i < arr.length; i++) {
                fill_a_attr("pcb" + arr[i], "checked", "checked");
            }
        }
    }
}
function id_comparison_GetList(btn, table, page, ref_table, ref_id) {
    ws_qry(btn, table, page, "IdComparison", "GetList", {
        "ref_table": ref_table, "ref_id": ref_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}