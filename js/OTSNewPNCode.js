//Returns true if all visible input dropdown boxes in divID have a selected value
//divID = the div element controlling the dropdown boxes; ID ONLY (no css selector)
function validateAllVisibleDropDownsHaveSelection(divID) {
    try {
        blAllHaveValue = true;
        //$("#" + divID + " .cboInput").each(function (index, element) {
        $("#" + divID).find(".cboinput").each(function (index, element) {
            ctlID = getCntlIndex($(this).attr('id'));
            if ($(this).is(":visible") && element.value == "") {
                {
                    giveErrorMessage("#lblError_" + ctlID, "Selection Required.", "#cbo_" + ctlID, "red", "black");
                    blAllHaveValue = false;
                }
            }
        });
        return blAllHaveValue;
    } catch (err) {
        alert("validateAllVisibleDropDownsHaveSelection: " + err.message);
    }
}


//Performs from validation.; Returns true if OK to submit form
//btnID; the ID of the button that was pressed
//divID: the ID of the div containing the controls you want to validate
function validateForm(btnID, divID) {
    try {
        formValid = true;
        switch (btnID) {
            case "btnOTSNewIn":
                //Ensure user selected a part type, even if it's undefined.
                formValid = validateAllVisibleDropDownsHaveSelection("divOTSNewIn")
                //MPN is required
                if ($("#txt_18").val() == "") {
                    formValid = false;
                    giveErrorMessage("#lblError_18", "You must provide a Vendor Part Number.", "#txt_18", "red", "black");
                }
                //...as is a vendor is required
                if ($("#txt_19").val() == "") {
                    formValid = false;
                    giveErrorMessage("#lblError_19", "You must provide a Vendor Name.", "#txt_19", "red", "black");
                }
                break;
            default:
                break;
        }
        return formValid;
    } catch (err) {
        alert("validateForm: " + err.message);
    }

}

function GetNewOTSPN() {
    try {
        //Validate
        if (!validateForm($(this).attr('id'), $(this).attr('id').replace("btn", "div"))) {
            return "";
        }
        //alert("passed validation");
        //encode the data in divOTSNewIn
        data = synthesizeData("#divOTSNewIn");
        //alert(data);
        var obj = new Object();
        obj.input = data;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/getNewOTSPN",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                //The result goes in...
                $("#divOTSNewOut").html(msg.d);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert(err.message);
    }
}