function id_extract_Insert(btn, number, name, shelf_type, id_day,
                machine, remark, sample_ids) {
    ws_exec(btn, "POST", "IdExtract", "Insert", {
        "number": number, "name": name,  "shelf_type": shelf_type, "id_day": id_day,
        "machine": machine, "remark": remark, "sample_ids": sample_ids,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_extract_Update(btn, id, number, name, shelf_type, id_day,
                machine, remark) {
    ws_exec(btn, "PUT", "IdExtract", "Update", {
        "id": id, "number": number, "name": name, "shelf_type": shelf_type, "id_day": id_day,
        "machine": machine, "remark": remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_extract_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "IdExtract", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function id_extract_GetOne(id) {
    ws_get_json("", "IdExtract", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", id_extract_GetOne_sucFunc);
}
function id_extract_GetOne_sucFunc(table, page, json) {
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
function id_extract_GetList(btn, table, page, number, user_id, id_days, id_daye, page_size, page_index) {
    ws_qry(btn, table, page, "IdExtract", "GetList", {
        "number": number, "user_id": user_id, "id_days": id_days, "id_daye": id_daye,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function id_extract_GetCount(number, user_id, id_days, id_daye) {
    ws_exec("", "GET", "IdExtract", "GetCount", {
        "number": number, "user_id": user_id, "id_days": id_days, "id_daye": id_daye,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}