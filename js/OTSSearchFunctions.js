//Call this function to intialize the Search block
function InitializeOTSSearchCSS() {
    try {
        //The drop down box that let users select the search method:
        $("#divSearch").css('display', 'block');
        //Get some space from the top
        $("#divOTSFind").css('margin-top', '15px');
        $("#visdiv_" + ID_OTSSearch_Method).css('display', 'inline');
        $("#cbo_" + ID_OTSSearch_Method).css('display', 'inline');
        //----------------------------------------
        //Call the cbo changed method to display the appropriate input field:
        HandleChangedSearchMethod()
        //We also want to show the button in line
        $("#btnLook").css('display', 'inline');

        //...and also let the message be visible
        $("#divMessage").css('display', 'block');

        //Padding
        $("#visdiv_" + ID_OTSSearch_ByBU).css('margin', '5px');
        $("#visdiv_" + ID_OTSSearch_ByUser).css('margin', '5px');
        $("#visdiv_" + ID_OTSSearch_ByDate).css('margin', '5px');
        $("#visdiv_" + ID_OTSSearch_ByText).css('margin', '5px');

        //Initialize the datepicker
        $("#txt_" + ID_OTSSearch_ByDate).datepicker();
        //Bind the change event on ID_OTSSearch_Method
        $("#cbo_" + ID_OTSSearch_Method).off("change", HandleChangedSearchMethod);
        $("#cbo_" + ID_OTSSearch_Method).on("change", HandleChangedSearchMethod);

        //Bind the AJAX call for btnLook
        $("#btnLook").off("click", ExecuteSearch);
        $("#btnLook").on("click", ExecuteSearch);
        //Set color
        $("#btnLook").addClass("inputButton");

    } catch (err) {
        alert("Error in InitializeOTSSearchCSS: " + err.message);
    }

}


function HandleChangedSearchMethod() {
    try {
        //Hide everything
        $("#visdiv_" + ID_OTSSearch_ByText).css('display', 'none');
        $("#visdiv_" + ID_OTSSearch_ByBU).css('display', 'none');
        $("#visdiv_" + ID_OTSSearch_ByUser).css('display', 'none');
        $("#visdiv_" + ID_OTSSearch_ByDate).css('display', 'none');
        //alert("changed detected for " + $("#cbo_" + ID_OTSSearch_Method).val());
        //What we show depends on what is selected
        switch ($("#cbo_" + ID_OTSSearch_Method).val()) {
            //case SearchBy_PN:
            //case SearchBy_Desc:
            //case SearchBy_Vendor:
            //case SearchBy_VendorPN:
            //case SearchBy_RequestForProduct:
            //   $("#visdiv_" + ID_OTSSearch_ByText).css('display', 'inline');
            //    $("#txt" + ID_OTSSearch_ByText).css('display', 'inline');
            //    break;
            case SearchBy_BU:
                $("#visdiv_" + ID_OTSSearch_ByBU).css('display', 'inline');
                $("#cbo" + ID_OTSSearch_ByBU).css('display', 'inline');
                break;
            case SearchBy_Requestor:
                $("#visdiv_" + ID_OTSSearch_ByUser).css('display', 'inline');
                $("#cbo" + ID_OTSSearch_ByUser).css('display', 'inline');
                break;
            case SearchBy_RequestDate:
                $("#visdiv_" + ID_OTSSearch_ByDate).css('display', 'inline');
                $("#txt" + ID_OTSSearch_ByDate).css('display', 'inline');
                break;
            default:
                $("#visdiv_" + ID_OTSSearch_ByText).css('display', 'inline');
                $("#txt" + ID_OTSSearch_ByText).css('display', 'inline');
                break;
        }

    } catch (err) {
        alert('ERROR in HandleChangedSearchMethod: ' + err.message);
    }
}

//The AJAX call to send data to the server and display results to the user.
function ExecuteSearch() {
    try {
        //Validate
        if (!SearchReadyToSubmit()) {
            return "";
        }
        $("#divMessage").html("<p>Please wait while the server executes your search request..</p>");
        var encodedData = synthesizeData("#divSearch");
        //alert(encodedData);
        //make a new object with a property that matches the parameter name of the web method we will call
        var obj = new Object();
        obj.input = encodedData;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/findMatchingPN",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                var resultMsg = msg.d.split(DELIM)[0];
                var resultHTML = msg.d.split(DELIM)[1];
                $("#divMessage").html(resultMsg);
                $("#divLook").html(resultHTML);
                $("#divLook").css('display', 'block');
                //Initialize the table returned to this area
                InitializeOTSViewAndEdit();
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert('ERROR in ExecuteSearch: ' + err.message);
    }
}

//Returns TRUE if the correct inputs have been entered for a search
function SearchReadyToSubmit() {
    try {
        blReady = true;
        var ctlID = "";
        var validatingCBO = false;
        msg = "";
        if (!isNumeric($("#cbo_" + ID_OTSSearch_Method).val())) {
            ctlID = "cbo_" + ID_OTSSearch_Method;
            msg = "Please select a method to use for your search."
            blReady = false;
        } else {
            switch ($("#cbo_" + ID_OTSSearch_Method).val()) {
                case SearchBy_PN:
                    ctlID = "txt_" + ID_OTSSearch_ByText;
                    msg = "Please enter a Part Number to search for."
                    break;
                case SearchBy_BU:
                    ctlID = "cbo_" + ID_OTSSearch_ByBU;
                    msg = "Please Select a BU."
                    validatingCBO = true;
                    break;
                case SearchBy_Desc:
                    ctlID = "txt_" + ID_OTSSearch_ByText;
                    msg = "Please enter a Description to search for."
                    break;
                case SearchBy_Requestor:
                    ctlID = "cbo_" + ID_OTSSearch_ByUser;
                    msg = "Please Select a User Name."
                    validatingCBO = true;
                    break;
                case SearchBy_Vendor:
                    ctlID = "txt_" + ID_OTSSearch_ByText;
                    msg = "Please enter a Vendor to search for."
                    break;
                case SearchBy_VendorPN:
                    ctlID = "txt_" + ID_OTSSearch_ByText;
                    msg = "Please enter a Vendor Part Number to search for."
                    break;
                case SearchBy_RequestDate:
                    ctlID = "txt_" + ID_OTSSearch_ByDate;
                    msg = "<p>Please enter a Date from which to begin your search.</p>" +
                        "<p>NOTE: This program uses jQuery's DatePicker widget; you should see a calendar pop-up when you " +
                        "put your cursor in the input field.  If you do not, your browser may not support this functionality, " +
                        "and an immediate workaround is to try another browser.</p>";
                    break;
                case SearchBy_RequestForProduct:
                    ctlID = "txt_" + ID_OTSSearch_ByText;
                    msg = "Please enter a Product to search for."
                    break;
            }

            if (validatingCBO) {
                if (!isNumeric($("#" + ctlID).val())) {
                    blReady = false;
                }
            } else {
                if ($("#" + ctlID).val() == '') {
                    blReady = false;
                }
            }
        }

        if (!blReady) {
            $("#" + ctlID).css('background-color', 'red');
            OpenDialog("#dialog", "MISSING DATA", msg);
        }

        return blReady;
    } catch (err) {
        alert('ERROR in SearchReadyToSubmit: ' + err.message);
    }
}



//Contstant names for the Search Methods, as contained in OTSSEarch_Method drop down box (and defined in the database)
var SearchBy_PN = "1";
var SearchBy_BU = "2";
var SearchBy_Desc = "8";
var SearchBy_Requestor = "3";
var SearchBy_Vendor = "4";
var SearchBy_VendorPN = "5";
var SearchBy_RequestDate = "6";
var SearchBy_RequestForProduct = "7";



