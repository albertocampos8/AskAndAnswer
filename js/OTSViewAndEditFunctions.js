//Call this function to intialize the TTable shown when the user retrieves part numbers.
var insertedRowIndex = -1;  //All rows we insert will have a negative index.
var flagSelectedTab = 0; //A global variable used to indicate which tab to select when doing a refresh

function InitializeOTSViewAndEdit() {
    try {
        //format the table
        $("#otsSearchResults").removeClass("otsTable");
        $("#otsSearchResults").addClass("otsTable");

        $("#otsSearchResults").find("td,th").each(function (index) {
            $(this).addClass("otsTableCells");
        });
        $(".withExpandButton").css('padding', '0');
        $(".withExpandButton").on("resize", function () {
            $(this).find(button).first().width("100%");
            $(this).find(button).first().height("100%");
        });

        $(".otsbtnFoundExpand").addClass("tableCellButton btnExpand");
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
            $(this).val('Expand');
            $(this).prop('title','Expand to get more information about the Part Number');
            $("#otsExpandResultsRow_" + id).toggle();
        } else {
            //Since it's not visible, show it
            $("#otsExpandResultsRow_" + id).toggle();
            $(this).prop('title', 'Hide to reclaim browser space');
            //Since this row was invisible, its color was not set; make it match the color of this row.
            $("#otsExpandResultsRow_" + id).css('background-color', $(this).closest('tr').css('background-color'));
            $(this).val('Hide');
            if ($("#otsDisplayAreaFor_" + id).html() == '') {
                //We only need to do the ajax call if the row is empty
                $("#otsDisplayAreaFor_" + id).html("<p>Please wait... querying database...</p>");
                AJAX_GetOTSPNDetail(id);
            }
        }
    } catch (err) {
        alert("Error in ShowOTSPNDetail: " + err.message);
    }

}

/*Executes an AJAX event to get OTS data from the database.
id is required, as it tells us which section of the page will accept the data*/
function AJAX_GetOTSPNDetail(id) {
    try {
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
                //alert(msg.d);
                //Initialize the returned HTML

            },
            error: function () {
                alert(status);
            }
        }) //ajax
    } catch (err) {
        alert("Error in GetOTSPNDetail: " + err.message);
    }
}

function FormatOTSPNDetail(divSelector) {
    try {
        var id = divSelector.split("_")[1];
        $("#" + divSelector).tabs();
        //Set the active tab
        $("#" + divSelector).tabs("option", "active", flagSelectedTab);
        
        //Make txt/cbo fields in divPNInfo wide
        //$("#divPNInfo_" + id + " .txtinput,.cboinput").width("500px");
        $("#divPNInfo_" + id).find(".txtinput,.cboinput").width("500px");

        //Set button colors
        $(".editotsinfo_" + id).addClass("activeEditButton");
        $(".editvpn_" + id).addClass("activeEditButton");
        $(".viewpnhistory_" + id).addClass("activeViewButton");

        var buttonID = ""
        $("#tblVPNInfo_" + id).find(".btnViewVendorWhereUsed").each(function (index, value) {
            $(this).addClass("activeViewButton");
            buttonID = $(this).attr('id');
            //Couldn't figure out why the following binding didn't work:
            //$("#" + buttonID).on("click", "AJAX_DoVPNWhereUsed");
            //...so just bind directly
            $("#" + buttonID).on("click", function () {
                var id = $(this).attr('id').split("_")[2];
                //The ID is the only data we need to send to the server.
                var obj = new Object();
                obj.input = id;
                var strData = JSON.stringify(obj);
                //make an AJAX Call
                $.ajax({
                    type: "POST",
                    url: "OTSPN.aspx/WhereUsedForVPNID",
                    data: strData,
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //The result goes in a dialog box
                        //$("#dialog").dialog("option", "width", 700);
                        OpenDialog("#dialog", "WHERE USED FOR ", msg.d);
                    },
                    error: function () {
                        alert(status);
                    }
                }) //ajax
            });
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
        //Enable fields
        $("#divPNInfo_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("inactiveInputField");
            $(this).addClass("activeInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly','');
            } else {
                $(this).prop('disabled','');
            }
        });

    } catch (err) {
        alert("Error in editOTSInfo_Click: " + err.message);
    }
}

function saveOTSInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[2];
        StopOTSButtonEditMode(id);
        //we need to encode the data in the table.
        var data = synthesizeData("#divPNInfo_" + id);
        //Since the ID is not part of the fields we scanned, we need to add it manually to the datastring
        data = "-1" + DELIM + id + DELIM + data;
        var obj = new Object();
        obj.input = data;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/UpdatePNData",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                var resultPortion = msg.d.split(DELIM)[0];
                var htmlPortion = msg.d.split(DELIM)[1];
                $("#otsDisplayAreaFor_" + id).html(htmlPortion);
                OpenDialog("#dialog","UPDATE RESULT", resultPortion);
                //alert(resultPortion);
                //Format this
                FormatOTSPNDetail("pnsummary_" + id);
            },
            error: function () {
                alert(status);
            }
        }) //ajax
    } catch (err) {
        alert("Error in saveOTSInfo_Click: " + err.message);
    }
}

function cancelOTSInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[2];
        StopOTSButtonEditMode(id);
        //We need to reset the content by re-calling info from the database.
        //Put things back to the initial condition:
        flagSelectedTab = 0;
        AJAX_GetOTSPNDetail(id);
    } catch (err) {
        alert("Error in cancelOTSInfo_Click: " + err.message);
    }
}

function StopOTSButtonEditMode(id) {
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
        //disable fields
        $("#divPNInfo_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("activeInputField");
            $(this).addClass("inactiveInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly','true');
            } else {
                $(this).prop('disabled','true');
            }
        });
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
        //toggle the fields
        $("#tblVPNInfo_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("inactiveInputField");
            $(this).addClass("activeInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly', '');
            } else {
                $(this).prop('disabled', '');
            }
        });
        //Field 0 and 1 should render as inactive
        $("#tblVPNInfo_" + id + " tr").each(function (index, value) {
            $(this).find("td").eq(0).children().first().addClass("inactiveInputField");
            $(this).find("td").eq(1).children().first().addClass("inactiveInputField");
        });
    } catch (err) {
        alert("Error in editVPNInfo_Click: " + err.message);
    }
}

function saveVPNInfo_Click() {
    try {
        id = $(this).attr('id').split("_")[1];
        StopVPNButtonEditMode(id);
        data = EncodeVPNTable("#tblVPNInfo_" + id);
        //alert(data);
        var obj = new Object();
        obj.input = data;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/UpdateVPNData",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                var htmlPortion = msg.d.split(DELIM)[0];
                var msgPortion = msg.d.split(DELIM)[1];
                $("#otsDisplayAreaFor_" + id).html(htmlPortion);
                OpenDialog("#dialog", "UPDATE RESULT", msgPortion);
                flagSelectedTab = 1;

                //alert(resultPortion);
                //Format this
                FormatOTSPNDetail("pnsummary_" + id);
            },
            error: function () {
                alert(status);
            }
        }) //ajax
    } catch (err) {
        alert("Error in saveVPNInfo_Click: " + err.message);
    }
}

function cancelVPNInfo_Click() {
    try {
        id = $(this).attr('id').split("_")[1];
        StopVPNButtonEditMode(id);
        //Put things back to the initial condition, but first, set a flag to indicate we are in 
        flagSelectedTab = 1;
        //VPNView mode
        AJAX_GetOTSPNDetail(id);
    } catch (err) {
        alert("Error in cancelVPNInfo_Click: " + err.message);
    }
}

function StopVPNButtonEditMode(id) {
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
        //revert fields
        $("#tblVPNInfo_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("activeInputField");
            $(this).addClass("inactiveInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly', 'true');
            } else {
                $(this).prop('disabled', 'true');
            }
        });
    } catch (err) {
        alert("Error in cancelOTSInfo_Click: " + err.message);
    }
}


function addVPNInfo_Click() {
    try {
        id = $(this).attr('id').split("_")[1];
        AddRowToVPNTable(id);
    } catch (err) {
        alert("Error in addVPNInfo_Click: " + err.message);
    }
}

function AddRowToVPNTable(id) {
    try {
        //Get a row object that is the *clone* of the last row in the table:
        $("#tblVPNInfo_" + id + " tbody").append(
            $("#tblVPNInfo_" + id).find("tr").last().clone(true, true)
            );
        //The structure of the id is [x]_[vpnID]_[id].
        //Since we are adding a vendor we will make our vpnID the next available *negative* number.
        var defaultRowID =
        $("#tblVPNInfo_" + id).find("tr").last().attr('id', "row_" + insertedRowIndex + "_" + id);
        // Change the ID of all the input fields in this last row
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(0).children().first().attr('id', "txtinput_0_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(1).children().first().attr('id', "txtinput_1_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(2).children().first().attr('id', "txtinput_2_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(3).children().first().attr('id', "cboinput_3_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(4).children().first().attr('id', "txtinput_4_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(5).children().first().attr('id', "txtinput_5_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(6).children().first().attr('id', "txtinput_6_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(7).children().first().attr('id', "txtinput_7_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(8).children().first().attr('id', "txtinput_8_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(9).children().first().attr('id', "cboinput_9_" + insertedRowIndex + "_" + id);
        $("#row_" + insertedRowIndex + "_" + id + " td").eq(10).children().first().attr('id', "btninput_10_" + insertedRowIndex + "_" + id);

        //Only the Vendor and Vendor PN fields are editable, because the part they are adding may already exist, and we don't want
        //users to inadvertently overwrite existing part information.  Instead, let them add the part; they will see what, if any
        //data exists for the part after it is added.  At that time, they can decide if they want to change any information for the given part.
        //revert fields
        //Start by shutting all fields down
        $("#row_" + insertedRowIndex + "_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("activeInputField");
            $(this).addClass("inactiveInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly', 'true');
                $(this).val('');
            } else {
                $(this).prop('disabled', 'true');
                $(this).val('1');
            }
        });
        //Only let Vendor, Vendor part number be active
        $("#txtinput_0_" + insertedRowIndex + "_" + id).removeClass("inactiveInputField");
        $("#txtinput_0_" + insertedRowIndex + "_" + id).val('');
        $("#txtinput_2_" + insertedRowIndex + "_" + id).removeClass("inactiveInputField");
        $("#txtinput_0_" + insertedRowIndex + "_" + id).addClass("activeAddButton");
        $("#txtinput_2_" + insertedRowIndex + "_" + id).addClass("activeAddButton");
        $("#txtinput_0_" + insertedRowIndex + "_" + id).prop('readonly','');
        $("#txtinput_2_" + insertedRowIndex + "_" + id).prop('readonly', '');

        //disable the view button, since it's useless
        $("#btninput_10_" + insertedRowIndex + "_" + id).prop('disabled', true);
        //get the next negative index, in case another row is added.
        insertedRowIndex = insertedRowIndex - 1;

        //alert($("#tblVPNInfo_" + id).html());
    } catch (err) {
        alert("Error in AddRowToVPNTable: " + err.message);
    }
}

/*
Encodes the data in a Vendor Part Number table as follows:
OTS_ID[]START[]VPN_ID[]ColumnIndex[]ColumnValue....
      []START[]VPN_ID[]ColumnIndex[]ColumnValue....
      END
Remember, the ID of each row in the table is of the form:
row_VPNID_OTSID
and of each cell
cell_ColumnIndex_VPNID_OTSID
*/
function EncodeVPNTable(tableIDCssSelector) {
    try {
        otsID = tableIDCssSelector.split("_")[1];
        var strData = "";
        $(tableIDCssSelector).children().first().children().each(function (index, value) {
            strData = strData + DELIM + "START" + DELIM + $(this).attr('id').split("_")[1]
            $(this).find(".txtinput,.cboinput").each(function (i, v) {
                strData = strData + DELIM + $(this).attr('id').split("_")[1] + DELIM + $(this).val();
            });
        });

        return otsID + strData + DELIM + "END";
    } catch (err) {
        alert("Error in EncodeVPNTable: " + err.message);
        return "";
    }
}

function AJAX_DoVPNWhereUsed() {
    try {
        var id = $(this).attr('id').split("_")[2];
        //The ID is the only data we need to send to the server.
        var obj = new Object();
        obj.input = id;
        alert(id);
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/WhereUsedForVPNID",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                //The result goes in a dialog box
                alert(msg.d);
                OpenDialog("#dialog","WHERE USED FOR ", msg.d);
            },
            error: function () {
                alert(status);
            }
        }) //ajax
    } catch (err) {
        alert("Error in AJAX_DoVPNWhereUsed: " + err.message);
        return "";
    }
}



