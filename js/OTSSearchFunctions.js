//Call this function to intialize the Search block
function InitializeOTSSearchCSS() {
    try {
        //The drop down box that let users select the search method:
        $("#divSearch").css('display', 'block');
        $("#visdiv_" + ID_OTSSearch_Method).css('display', 'inline');
        $("#cbo_" + ID_OTSSearch_Method).css('display', 'inline');
        //----------------------------------------
        //Call the cbo changed method to display the appropriate input field:
        HandleChangedSearchMethod()
        //We also want to show the button in line
        $("#btnLook").css('display', 'inline');

        //...and also let the message be visible
        $("#divMessage").css('display', 'block');

        //Bind the change event on ID_OTSSearch_Method
        $("#cbo_" + ID_OTSSearch_Method).off("change", HandleChangedSearchMethod);
        $("#cbo_" + ID_OTSSearch_Method).on("change", HandleChangedSearchMethod);

        //Bind the AJAX call for btnLook
        $("#btnLook").off("click", ExecuteSearch);
        $("#btnLook").on("click", ExecuteSearch);

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
                //alert(resultHTML);
                //Initialize the table returned to this area
                InitializeOTSViewAndEdit();
            },
            error: function () {
                alert(status);
            }
        }) //ajax
    } catch (err) {
        alert('ERROR in ExecuteSearch: ' + err.message);
    }
}



