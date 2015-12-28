function PO_checkPageOffice() {
    var bodyHtml = document.body.innerHTML;
    if (bodyHtml.indexOf("EC852C85-C2FC-4c86-8D6B-E4E97C92F821") < 0) {
        var poObjectStr = "";
        var explorer = window.navigator.userAgent;
        //ie
        if (explorer.indexOf("MSIE") >= 0) {
            poObjectStr = "<div style=\"background-color:green;width:1px; height:1px;\">" + "\r\n"
			+ "<object id=\"PageOfficeCtrl1\" height=\"100%\" width=\"100%\" classid=\"clsid:EC852C85-C2FC-4c86-8D6B-E4E97C92F821\">"
			+ "</object></div>"
        }
        else {
            poObjectStr = "<div style=\"background-color:green;width:1px; height:1px;\">" + "\r\n"
			+ "<object id=\"PageOfficeCtrl1\" height=\"100%\" width=\"100%\" type=\"application/x-pageoffice-plugin\" clsid=\"{EC852C85-C2FC-4c86-8D6B-E4E97C92F821}\">"
			+ "</object></div>"
        }

        $(document.body).append(poObjectStr);
    } 
    
	try {
		var sCap = document.getElementById("PageOfficeCtrl1").Caption;
		if (sCap == null) {
			return false;
		}
		else {
			return true;
		}
	}
	catch (e) { return false; }
}

