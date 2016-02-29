function addCookie(name, value, expire, cookieType) {
    if (value != null) {
        if (value.length == null || value.length > 0) {
            if (cookieType == 1) store.set(name, value);
            else Cookies.set(name, value);
        }
    }
}
function getCookie(name, cookieType) {
    if (cookieType == 1) return store.get(name);
    else return Cookies.get(name);
}
function deleteCookie(name, cookieType) {
    if (cookieType == 1) store.remove(name);
    else Cookies.remove(name);
}
function clearCookie(cookieType) {
    //if (cookieType == 1) store.clear();
    deleteCookie("sys_user_id", 1);
    deleteCookie("sys_access_id", 1);
    deleteCookie("sys_access_token", 1);
}
//https://github.com/js-cookie/js-cookie
//https://github.com/marcuswestin/store.js/