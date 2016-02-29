function notice_Delete(btn, id) {
    if (!confirm("删除？")) return;
    ws_exec(btn, "DELETE", "Notice", "Delete", {
        "id": id,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "保存成功", "刷新");
}
function notice_GetList(btn, table, page, page_size, page_index) {
    ws_qry(btn, table, page, "Notice", "GetList", {
        "page_size": page_size, "page_index": page_index,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}