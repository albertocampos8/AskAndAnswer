//Call this function to intialize the TTable shown when the user retrieves part numbers.
function InitializeOTSViewAndEdit() {
    try {
        //format the table
        $("#otsSearchResults").removeClass("otsTable");
        $("#otsSearchResults").addClass("otsTable");

        $("#otsSearchResults").find("td,th").each(function (index) {
            $(this).addClass("otsTableCells");
        });

        //Bind a function to the otsbtnFoundExpand class
        $(".otsbtnFoundExpand").on("click", ShowOTSPNDetail);

    } catch (err) {
        alert("Error in InitializeOTSSearchCSS: " + err.message);
    }
}

function ShowOTSPNDetail() {
    try {
        var id = $(this).attr('id').split("_")[1];

        if ($("#otsExpandResultsRow_" + id).is(':visible')) {
            //Since it's visible, hide it.
            $(this).val('EXPAND');
            $("#otsExpandResultsRow_" + id).toggle();
        } else {
            //Since it's not visible, show it
            $("#otsExpandResultsRow_" + id).toggle();
            $(this).val('HIDE');
            if ($("#otsDisplayAreaFor_" + id).html() == '') {
                //We only need to do the ajax call if the row is empty
                $("#otsDisplayAreaFor_" + id).html("<p>Please wait... querying database...</p>");
                //The ID is the only data we need to send to the server.
                var obj = new Object();
                obj.input = id;
                var strData = JSON.stringify(obj);
                //make an AJAX Call
                $.ajax({
                    type: "POST",
                    url: "OTSPN.aspx/getHTMLForPartNumberID",
                    data: strData,
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //The result goes in...
                        $("#otsDisplayAreaFor_" + id).html(msg.d);

                        //Format this
                        FormatOTSPNDetail("pnsummary_" + id);
                        alert(msg.d);
                        //Initialize the returned HTML

                    },
                    error: function () {
                        alert(status);
                    }
                }) //ajax
            }
        }
    } catch (err) {
        alert("Error in ShowOTSPNDetail: " + err.message);
    }

}

function FormatOTSPNDetail(divSelector) {
    try {
        var id = divSelector.split("_")[1];
        $("#" + divSelector).tabs();

        //Make txt/cbo fields in divPNInfo wide
        $("#divPNInfo_" + id + " .txtinput,.cboinput").width("500px");

        //Set button colors
        $(".editotsinfo_" + id).addClass("activeEditButton");
        $(".editvpn_" + id).addClass("activeEditButton");
        $(".viewpnhistory_" + id).addClass("activeViewButton");

        $("#tblVPNInfo_" + id).find(".btnViewVendorWhereUsed").each(function (index, value) {
            $(this).addClass("activeViewButton");
        });
        
        //Bind button functions
        $(".editotsinfo_" + id).on("click", editOTSInfo_Click);
        $(".saveotsinfo_" + id).on("click", saveOTSInfo_Click);
        $(".cancelotsinfo_" + id).on("click", cancelOTSInfo_Click);

        $(".editvpn_" + id).on("click", editVPNInfo_Click);
        $(".addvpn_" + id).on("click", addVPNInfo_Click);
        $(".savevpn_" + id).on("click", saveVPNInfo_Click);
        $(".cancelvpn_" + id).on("click", cancelVPNInfo_Click);
    } catch (err) {
        alert("Error in FormatOTSPNDetail: " + err.message);
    }
}

function editOTSInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[2];
        $(".editotsinfo_" + id).prop('disabled', 'true');
        $(".saveotsinfo_" + id).prop('disabled', '');
        $(".cancelotsinfo_" + id).prop('disabled', '');
        //Change the colors
        $(".editotsinfo_" + id).removeClass('activeEditButton');
        $(".saveotsinfo_" + id).removeClass('disabledButton');
        $(".cancelotsinfo_" + id).removeClass('disabledButton');
        $(".editotsinfo_" + id).addClass('disabledButton');
        $(".saveotsinfo_" + id).addClass('activeSaveButton');
        $(".cancelotsinfo_" + id).addClass('activeCancelButton');
    } catch (err) {
        alert("Error in editOTSInfo_Click: " + err.message);
    }
}

function saveOTSInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[2];
        PutOTSButtonsInEditMode(id);
    } catch (err) {
        alert("Error in saveOTSInfo_Click: " + err.message);
    }
}

function cancelOTSInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[2];
        PutOTSButtonsInEditMode(id);
    } catch (err) {
        alert("Error in cancelOTSInfo_Click: " + err.message);
    }
}

function PutOTSButtonsInEditMode(id) {
    try {
        $(".editotsinfo_" + id).prop('disabled', '');
        $(".saveotsinfo_" + id).prop('disabled', 'true');
        $(".cancelotsinfo_" + id).prop('disabled', 'true');
        //Change the colors
        $(".editotsinfo_" + id).removeClass('disabledButton');
        $(".saveotsinfo_" + id).removeClass('activeSaveButton');
        $(".cancelotsinfo_" + id).removeClass('activeCancelButton');
        $(".editotsinfo_" + id).addClass('activeEditButton');
        $(".saveotsinfo_" + id).addClass('disabledButton');
        $(".cancelotsinfo_" + id).addClass('disabledButton');
    } catch (err) {
        alert("Error in cancelOTSInfo_Click: " + err.message);
    }
}
function editVPNInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[1];
        $(".editvpn_" + id).prop('disabled', 'true');
        $(".savevpn_" + id).prop('disabled', '');
        $(".cancelvpn_" + id).prop('disabled', '');
        $(".addvpn_" + id).prop('disabled', '');
        //Change the colors
        $(".editvpn_" + id).removeClass('activeEditButton');
        $(".savevpn_" + id).removeClass('disabledButton');
        $(".cancelvpn_" + id).removeClass('disabledButton');
        $(".addvpn_" + id).removeClass('disabledButton');
        $(".editvpn_" + id).addClass('disabledButton');
        $(".savevpn_" + id).addClass('activeSaveButton');
        $(".cancelvpn_" + id).addClass('activeCancelButton');
        $(".addvpn_" + id).addClass('activeAddButton');
    } catch (err) {
        alert("Error in editVPNInfo_Click: " + err.message);
    }
}

function saveVPNInfo_Click() {
    try {
        id = $(this).attr('id').split("_")[1];
        PutVPNButtonsInEditMode(id);
    } catch (err) {
        alert("Error in saveVPNInfo_Click: " + err.message);
    }
}

function cancelVPNInfo_Click() {
    try {
        id = $(this).attr('id').split("_")[1];
        PutVPNButtonsInEditMode(id);
    } catch (err) {
        alert("Error in cancelVPNInfo_Click: " + err.message);
    }
}

function PutVPNButtonsInEditMode(id) {
    try {
        $(".editvpn_" + id).prop('disabled', '');
        $(".savevpn_" + id).prop('disabled', 'true');
        $(".cancelvpn_" + id).prop('disabled', 'true');
        $(".addvpn_" + id).prop('disabled', 'true');
        //Change the colors
        $(".editvpn_" + id).removeClass('disabledButton');
        $(".savevpn_" + id).removeClass('activeSaveButton');
        $(".cancelvpn_" + id).removeClass('activeCancelButton');
        $(".addvpn_" + id).removeClass('activeAddButton');
        $(".editvpn_" + id).addClass('activeEditButton');
        $(".savevpn_" + id).addClass('disabledButton');
        $(".cancelvpn_" + id).addClass('disabledButton');
        $(".addvpn_" + id).addClass('disabledButton');
    } catch (err) {
        alert("Error in cancelOTSInfo_Click: " + err.message);
    }
}


function addVPNInfo_Click() {
    try {

    } catch (err) {
        alert("Error in addVPNInfo_Click: " + err.message);
    }
}