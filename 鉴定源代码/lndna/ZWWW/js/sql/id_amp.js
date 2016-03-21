function id_amp_Insert(btn, number, name, shelf_type, id_day,
                method, machine, kit, amount, batch, circle, board, sample_ids) {
    ws_exec(btn, "POST", "IdAmp", "Insert", {
        "number": number, "name": name, "shelf_type": shelf_type, "id_day": id_day,
        "method": method, "machine": machine, "kit": kit, "amount": amount, "batch": batch,
        "circle": circle, "board": board, "sample_ids": sample_ids,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_amp_Update(btn, id, number, name, shelf_type, id_day,
                method, machine, kit, amount, batch, circle, board) {
    ws_exec(btn, "PUT", "IdAmp", "Update", {
        "id": id, "number": number, "name": name, "shelf_type": shelf_type, "id_day": id_day,
        "method": method, "machine": machine, "kit": kit, "amount": amount, "batch": batch,
        "circle": circle, "board": board,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_amp_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "IdAmp", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_amp_GetOne(id) {
    ws_get_json("", "IdAmp", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", id_amp_GetOne_sucFunc);
}
function id_amp_GetOne_sucFunc(table, page, json) {
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
function id_amp_GetList(btn, table, page, number, user_id, id_days, id_daye, page_size, page_index) {
    ws_qry(btn, table, page, "IdAmp", "GetList", {
        "number": number, "user_id": user_id, "id_days": id_days, "id_daye": id_daye,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function id_amp_GetCount(number, user_id, id_days, id_daye) {
    ws_exec("", "GET", "IdAmp", "GetCount", {
        "number": number, "user_id": user_id, "id_days": id_days, "id_daye": id_daye,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}