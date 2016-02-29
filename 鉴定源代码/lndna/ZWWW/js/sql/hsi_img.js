function hsi_img_Insert(form, id) {
    $("#" + form).ajaxSubmit({
        url: apiUrl + "HsiImg/Insert/" + id,
        type: 'POST',
        success: function (json) {
            hsi_img_GetList("", "照片", "hsi_img_list", id);
        }
    });
}
function hsi_img_Delete(btn, disk) {
    if (!confirm("确定？")) return;
    ws_exec(btn, "DELETE", "HsiImg", "Delete", {
        "disk": disk,
        "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    }, "", "刷新");
}
function hsi_img_GetList(btn, table, page, id) {
    ws_qry(btn, table, page, "HsiImg", "GetList", {
        "id": id, "access_id": getCookie("sys_access_id", 1), "access_token": getCookie("sys_access_token", 1)
    });
}