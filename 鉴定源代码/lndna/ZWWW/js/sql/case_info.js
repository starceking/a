function case_info_Insert(btn, type_id, cg_name, cg_mobile,
                cg_fax, cg_day, cg_addr, cg_postcode, cg_man1, cg_mobile1,
                cg_cre_type1, cg_cre_number1, cg_duty1, cg_man2, cg_mobile2, cg_cre_type2,
                cg_cre_number2, cg_duty2, lab_no, id_request, cg_summary, id_src_info, id_reason,
                cg_remark, case_name, case_type, case_property, case_day,
                case_addr_number, case_addr, case_level, case_summary, ref_sys_number1,
                ref_sys_number2, ref_sys_number3) {
    ws_exec(btn, "POST", "CaseInfo", "Insert", {
        "type_id": type_id, "cg_name": cg_name, "cg_mobile": cg_mobile, "cg_fax": cg_fax,
        "cg_day": cg_day, "cg_addr": cg_addr, "cg_postcode": cg_postcode, "cg_man1": cg_man1, "cg_mobile1": cg_mobile1,
        "cg_cre_type1": cg_cre_type1, "cg_cre_number1": cg_cre_number1, "cg_duty1": cg_duty1, "cg_man2": cg_man2, "cg_mobile2": cg_mobile2,
        "cg_cre_type2": cg_cre_type2, "cg_cre_number2": cg_cre_number2, "cg_duty2": cg_duty2, "lab_no": lab_no, "id_request": id_request,
        "cg_summary": cg_summary, "id_src_info": id_src_info, "id_reason": id_reason, "cg_remark": cg_remark,
        "case_name": case_name, "case_type": case_type, "case_property": case_property, "case_day": case_day,
        "case_addr_number": case_addr_number, "case_addr": case_addr, "case_level": case_level, "case_summary": case_summary,
        "ref_sys_number1": ref_sys_number1, "ref_sys_number2": ref_sys_number2, "ref_sys_number3": ref_sys_number3,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_info_Update(btn, id, cg_mobile,
                cg_fax, cg_day, cg_addr, cg_postcode, cg_man1, cg_mobile1,
                cg_cre_type1, cg_cre_number1, cg_man2, cg_mobile2, cg_cre_type2,
                cg_cre_number2, lab_no, id_request, cg_summary, id_src_info, id_reason,
                cg_remark, case_name, case_type, case_property, case_day,
                case_addr_number, case_addr, case_level, case_summary, ref_sys_number1,
                ref_sys_number2, ref_sys_number3) {
    ws_exec(btn, "PUT", "CaseInfo", "Update", {
        "id": id, "cg_mobile": cg_mobile, "cg_fax": cg_fax,
        "cg_day": cg_day, "cg_addr": cg_addr, "cg_postcode": cg_postcode, "cg_man1": cg_man1, "cg_mobile1": cg_mobile1,
        "cg_cre_type1": cg_cre_type1, "cg_cre_number1": cg_cre_number1, "cg_man2": cg_man2, "cg_mobile2": cg_mobile2,
        "cg_cre_type2": cg_cre_type2, "cg_cre_number2": cg_cre_number2, "lab_no": lab_no, "id_request": id_request,
        "cg_summary": cg_summary, "id_src_info": id_src_info, "id_reason": id_reason, "cg_remark": cg_remark,
        "case_name": case_name, "case_type": case_type, "case_property": case_property, "case_day": case_day,
        "case_addr_number": case_addr_number, "case_addr": case_addr, "case_level": case_level, "case_summary": case_summary,
        "ref_sys_number1": ref_sys_number1, "ref_sys_number2": ref_sys_number2, "ref_sys_number3": ref_sys_number3,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_info_PreAccept(btn, id) {
    if (!confirm("确定提交？")) return;
    ws_exec(btn, "PUT", "CaseInfo", "PreAccept", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_info_Accept(btn, id, case_status_id, accept_remark) {
    if (!confirm("确定提交？")) return;
    ws_exec(btn, "PUT", "CaseInfo", "Accept", {
        "id": id, "case_status_id": case_status_id, "accept_remark": accept_remark,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_info_Delete(btn, id) {
    if (!confirm("确定删除？")) return;
    ws_exec(btn, "DELETE", "CaseInfo", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "后退");
}
function case_info_GetOne(id) {
    ws_get_json("", "CaseInfo", "GetOne", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "", case_info_GetOne_sucFunc);
}
function case_info_GetOne_sucFunc(table, page, json) {
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
function case_info_GetList(btn, table, page, type_id, cg_number, cg_days, cg_daye,
                case_name, case_days, case_daye, consign_number, accept_number,
                case_status_id, lab_no, accept_user_id, page_size, page_index) {
    ws_qry(btn, table, page, "CaseInfo", "GetList", {
        "type_id": type_id, "cg_number": cg_number, "cg_days": cg_days, "cg_daye": cg_daye,
        "case_name": case_name, "case_days": case_days, "case_daye": case_daye,
        "consign_number": consign_number, "accept_number": accept_number,
        "case_status_id": case_status_id, "lab_no": lab_no, "accept_user_id": accept_user_id,
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}
function case_info_GetCount(type_id, cg_number, cg_days, cg_daye,
                case_name, case_days, case_daye, consign_number, accept_number,
                case_status_id, lab_no, accept_user_id) {
    ws_exec("", "GET", "CaseInfo", "GetCount", {
        "type_id": type_id, "cg_number": cg_number, "cg_days": cg_days, "cg_daye": cg_daye,
        "case_name": case_name, "case_days": case_days, "case_daye": case_daye,
        "consign_number": consign_number, "accept_number": accept_number,
        "case_status_id": case_status_id, "lab_no": lab_no, "accept_user_id": accept_user_id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "");
}