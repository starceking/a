function case_person_Insert(btn, case_info_id, person_case_type, name, gender,
                nation, id_card_no, id_type, id_number, person_type, spec, birthday,
                country, alias, hjd_number, hjd_addr, xzz_number, xzz_addr,
                remark, age, missing_day, missing_addr, relative_id, relative_type) {
    ws_exec(btn, "POST", "CasePerson", "Insert", {
        "case_info_id": case_info_id, "person_case_type": person_case_type, "name": name, "gender": gender,
        "nation": nation, "id_card_no": id_card_no, "id_type": id_type, "id_number": id_number,
        "person_type": person_type, "spec": spec, "birthday": birthday, "country": country,
        "alias": alias, "hjd_number": hjd_number, "hjd_addr": hjd_addr, "xzz_number": xzz_number,
        "xzz_addr": xzz_addr, "remark": remark, "age": age, "missing_day": missing_day,
        "missing_addr": missing_addr, "relative_id": relative_id, "relative_type": relative_type,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_person_Update(btn, id, name, gender,
                nation, id_card_no, id_type, id_number, person_type, spec, birthday,
                country, alias, hjd_number, hjd_addr, xzz_number, xzz_addr,
                remark, age, missing_day, missing_addr, relative_type) {
    ws_exec(btn, "PUT", "CasePerson", "Update", {
        "id": id, "name": name, "gender": gender,
        "nation": nation, "id_card_no": id_card_no, "id_type": id_type, "id_number": id_number,
        "person_type": person_type, "spec": spec, "birthday": birthday, "country": country,
        "alias": alias, "hjd_number": hjd_number, "hjd_addr": hjd_addr, "xzz_number": xzz_number,
        "xzz_addr": xzz_addr, "remark": remark, "age": age, "missing_day": missing_day,
        "missing_addr": missing_addr, "relative_type": relative_type,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_person_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "CasePerson", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_person_GetOne(id) {
    ws_get_json("", "CasePerson", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", case_person_GetOne_sucFunc);
}
function case_person_GetOne_sucFunc(table, page, json) {
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
function case_person_GetList(btn, table, page, case_info_id) {
    ws_qry(btn, table, page, "CasePerson", "GetList", {
        "case_info_id": case_info_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}