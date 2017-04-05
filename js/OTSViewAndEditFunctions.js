//Call this function to intialize the Table shown when the user retrieves part numbers.
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
        alert("Error in InitializeOTSViewAndEdit: " + err.message);
    }
}

function ShowOTSPNDetail() {
    try {
        var id = $(this).attr('id').split("_")[1];
        var pn = $(this).attr('id').split("_")[2];
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
                FormatINVPNDetail("#divPNInvInfo_" + id)

            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in GetOTSPNDetail: " + err.message);
    }
}

function FormatOTSPNDetail(divSelector) {
    try {
        if (divSelector == null) {
            return;
        }
        id = divSelector.split("_")[1];
        $("#" + divSelector).tabs({
            activate: function (event, ui) {
                try {
                    var activePanelName = ui.newPanel.attr('id');
                    //We only need to worry if the user selected the divPNTransactions_ID panel, since we need to refresh the
                    //transaction log with an AJAX Call...
                    if (activePanelName.indexOf("divPNTransaction") > -1) {
                        AJAX_GetInvPNHistory(activePanelName.split("_")[1]);
                    }
                  

                } catch (err) {
                    alert(err.message);
                }

            }
        });
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

            //To get the Vendor Part Number, get the button's parent (cell)'s parent (row), 
            var rowID = $("#" + buttonID).parent().parent().attr('id');
            //alert($("#" + rowID + " td:nth-child(1) input:nth-child(1)").attr('id'));
            //then get child(1) [cell]'s child (textbox) value for the Vendor, and
            var vendor = $("#" + rowID + " td:nth-child(1) input:nth-child(1)").val();
            // and child(3) [cell]'s child (text box) for the VPN
            var vpn = $("#" + rowID + " td:nth-child(3) input:nth-child(1)").val();

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
                        OpenDialog("#dialog", "WHERE USED FOR " + vendor + " PART NUMBER " + vpn, msg.d);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
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

        //Format the Where Used table
        $("#tbl_pn" + id).addClass("tblWhereUsed");
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
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
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
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
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
        //alert(id);
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
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in AJAX_DoVPNWhereUsed: " + err.message);
        return "";
    }
}

function editINVInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[1];
        $(".editinv." + id).prop('disabled', 'true');
        $(".saveinv." + id).prop('disabled', '');
        $(".cancelinv." + id).prop('disabled', '');
        $(".btnAddLoc." + id).prop('disabled', '');

        //Show the ACTION and DELTA columns
        $(".toggleCol." + id).removeClass('hidden');
        //Change the colors
        $(".editinv." + id).removeClass('activeEditButton');
        $(".saveinv." + id).removeClass('disabledButton');
        $(".cancelinv." + id).removeClass('disabledButton');
        $(".btnAddLoc." + id).removeClass('disabledButton');

        $(".editinv." + id).addClass('disabledButton');
        $(".saveinv." + id).addClass('activeSaveButton');
        $(".cancelinv." + id).addClass('activeCancelButton');
        $(".btnAddLoc." + id).addClass('activeAddButton');

        $("#divInvCmt_" + id).show('slow');

        //Enable fields
        $("#divPNInvInfo_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("inactiveInputField");
            $(this).addClass("activeInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly', '');
            } else {
                $(this).prop('disabled', '');
            }
        });

        //Autocomplete handler for subinventory
        $(".subinv." + id).autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "OTSPN.aspx/GetSubInventory",
                    //data: strValForTerm,
                    data: "{'term':'" + $(this).val() + "'}",
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (response) {
                        response("");
                        alert("Error: " + res.responseText);
                    }
                }) //ajax
            },
            minLength: 1
        });
    } catch (err) {
        alert("Error in editInvInfo_Click: " + err.message);
    }
}

function saveINVInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[1];

        //Reset everything to active (in case user is trying to re-attempt a save after making corrections)
        $(".txtinput.inv." + id).removeClass("invalidField duplicateField").addClass("activeInputField");
        $(".cboinput.inv." + id).removeClass("invalidField duplicateField").addClass("activeInputField");
        $(".clsInvCellInfo." + id + " ").removeClass("duplicateField").addClass("displayField");
        var msg = ""    //This will hold the html that is displayed in divInvMsg_pnID if any errors are detected.

        //we need to encode the data in the table.
        //Format is as follows:
        //[comment]DELIM[invBulk.ID]DELIM[QTY]DELIM[DELTA]DELIM[SubInv]DELIM[LocationID]DELIM[OwnerID]DELIM[VPNID]...
        data = ""
        $("#tblInvInfo_" + id + " tr").each(function (index, value) {
            if ($(this).attr('id').split('_').length > 2) {
                uid = $(this).attr('id').split('_')[1] + "_" +
                      $(this).attr('id').split('_')[2] + "_" +
                      $(this).attr('id').split('_')[3];
                
                qty = $("#invQty_" + uid).val();
                loc = $("#invLocCode_" + uid).val();
                if (loc == null) {
                    loc = -1;
                }
                owner = $("#invContactCode_" + uid).val();
                if (owner == null) {
                    owner = -1;
                }
                subinv = $("#invSubInv_" + uid).val();
                vpnid = $(this).attr('id').split('_')[1];
                vpn = $("#invVendorPN_" + uid).val();
                delta = $("#invDelta_" + uid).val();

                if (!isNumeric(delta) || delta.indexOf(".") != -1) {
                    setFieldInvalid("#invDelta_" + uid,true);
                    msg = msg + "<p>Delta Quantity must be an integer.  Set to 0 if you do not want to change the quantity for '" +
                        vpn + "'.</p>";
                } else if (delta < 0) {
                    setFieldInvalid("#invDelta_" + uid, true);
                    msg = msg + "<p>Always specify Delta Quantity greater than 0.  Please remove the negative sign in the 'DELTA' field for '" +
                        vpn + "'.</p>";   
                } else {
                    //Since we have a legal delta value, check if the delta value makes sense;
                    //To do this, we need to know whether user wants to ADD or REMOVE
                    switch ($("#invSign_" + uid).val()) {
                        case "1":
                            //User wants to REMOVE
                            if (qty - delta < 0) {
                                setFieldInvalid("#invDelta_" + uid,true);
                                msg = msg + "<p>You are removing " + delta + " parts of '" + vpn +
                                    "', but there are only " + qty +
                                    " parts in the location you selected.  Reduce the delta quantity to a number less than " +
                                    qty + " to continue.</p>";
                            } else {
                                delta = -delta;
                            }
                            break;
                       case "2":
                            //User wants to ADD
                            break;
                        default:
                            //User did not specify ADD or REMOVE; this is only a problem if the delta is <> 0
                            if (delta != 0) {
                                setFieldInvalid("#invSign_" + uid, true);
                                msg = msg + "<p>You must specify whether you want to ADD or REMOVE " + delta + " parts for '" + vpn +
                                    "' in inventory.  Please make a selection in the 'ACTION' drop-down box.</p>";
                            }

                    };
                    //Also, now that we know we have a valid integer delta, we need to make sure the use has selected
                    //a LOCATION and OWNER for this line
                    if ($("#invDelta_" + uid).val() > 0) {
                        if (loc == -1) {
                            setFieldInvalid("#invLocCode_" + uid,true);
                            msg = msg + "<p>You must specify a location where the inventory for '" + vpn +
                                "' will reside.  Please make a selection in the 'LOCATION' drop-down box.</p>";
                        }

                        if (owner == -1) {
                            setFieldInvalid("#invContactCode_" + uid,true);
                            msg = msg + "<p>You must specify the primary contact who will control the inventory for '" + vpn +
                                "'.  Please make a selection in the 'OWNER' drop-down box.</p>";
                        }
                    }

                }

                //Add data
                data = data + $(this).attr('id').split('_')[3] + DELIM + qty + DELIM + delta + DELIM + subinv + DELIM +
                    loc + DELIM + owner + DELIM + vpnid + DELIM;
            }

        });

        //Trim and check that a comment has been supplied
        var cmt = $("#txtInvCmt_" + id).val().trim();
        //...and validate
        if (cmt == "") {
            setFieldInvalid("#txtInvCmt_" + id,true);
            msg = msg + "<p>You must provide a comment describing why you are making this change.</p>";
        } else {
            //cmt is the first element of data
            data = cmt + DELIM + data;
        }

        //Verify we have no duplicates

        var dctUniqueEntries = new Object();
        $("#tblInvInfo_" + id + " tr").each(function (index, value) {
            var unqvpnid = $(this).attr('id').split("_")[1];
            var unqpnid = $(this).attr('id').split("_")[2];
            var unqinvbulkid = $(this).attr('id').split("_")[3];
            var unquid = "_" + unqvpnid + "_" + unqpnid + "_" + unqinvbulkid;
            var key = $("#invVendor" + unquid).val() + "!!!" +
                      $("#invVendorPN" + unquid).val() + "!!!" +
                      $("#invLocCode" + unquid).val() + "!!!" +
                      $("#invContactCode" + unquid).val() + "!!!" +
                      $("#invSubInv" + unquid).val();
            if (dctUniqueEntries[key] == null) {
                dctUniqueEntries[key] = unquid;
            } else {
                $("#invLocCode" + unquid).addClass("duplicateField");
                $("#invContactCode" + unquid).addClass("duplicateField");
                $("#invSubInv" + unquid).addClass("duplicateField");
                $("#invVendor" + unquid).removeClass("displayField").addClass("duplicateField");
                $("#invVendorPN" + unquid).removeClass("displayField").addClass("duplicateField");
                $("#invLocCode" + dctUniqueEntries[key]).addClass("duplicateField");
                $("#invContactCode" + dctUniqueEntries[key]).addClass("duplicateField");
                $("#invSubInv" + dctUniqueEntries[key]).addClass("duplicateField");
                $("#invVendor" + dctUniqueEntries[key]).removeClass("displayField").addClass("duplicateField");
                $("#invVendorPN" + dctUniqueEntries[key]).removeClass("displayField").addClass("duplicateField");
                msg = msg + "<p>SubInventory, Location, and Owner must be unique for " + $("#invVendor" + unquid).val() +
                    " part number '" + $("#invVendorPN" + unquid).val() + "'.  "  + 
                    "The offending rows have been highlighted orange.  Please change the relevant values in one of the rows, or, if you have accidentally added a duplicate row, remove the duplicate value.</p>";
            }
        });

        //Validation over-- update contents of #divInvMsg_pnID...
        $("#divInvMsg_" + id).html(msg);
        //...and abort, if needed.
        if (msg != "") {
            return false;
        }

        //If we did not return, then contiue.
        //Remove last delim
        data = data.substring(0, data.length - DELIM.length);

        var obj = new Object();
        obj.input = data;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/updatePartInventory",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d != "") {
                    OpenDialog("#dialog", "ERRORS OCCURRED WHEN UPDATING THE DATABASE", msg.d);
                }
                //Regardless of whether or not an error occurred, refresh what is displayed to the user.
                //This is important, because even though an error occurred, remember each row in the table is
                //processed by the server seperately, so some of the rows may have been processed.
                AJAX_GetInvPNDetail(id);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in saveINVInfo_Click: " + err.message);
    }
}

function cancelINVInfo_Click() {
    try {
        //disable this button, enable the others
        id = $(this).attr('id').split("_")[1];
        //StopINVButtonEditMode(id);
        AJAX_GetInvPNDetail(id);
    } catch (err) {
        alert("Error in cancelOTSInfo_Click: " + err.message);
    }
}

function StopINVButtonEditMode(id) {
    try {
        $(".editinv." + id).prop('disabled', '');
        $(".saveinv." + id).prop('disabled', 'true');
        $(".cancelinv." + id).prop('disabled', 'true');
        $(".btnAddLoc." + id).prop('disabled', 'true');
        //Change the colors
        $(".editinv." + id).removeClass('disabledButton');
        $(".saveinv." + id).removeClass('activeSaveButton');
        $(".cancelinv." + id).removeClass('activeCancelButton');
        $(".btnAddLoc." + id).removeClass('activeAddButton');
        $(".editinv." + id).addClass('activeEditButton');
        $(".saveinv." + id).addClass('disabledButton');
        $(".cancelinv." + id).addClass('disabledButton');
        $(".btnAddLoc." + id).addClass('disabledButton');
        //disable fields
        $("#divPNInvInfo_" + id).find(".toggle").each(function (index, value) {
            $(this).removeClass("activeInputField");
            $(this).addClass("inactiveInputField");
            if ($(this).hasClass("txtinput")) {
                $(this).prop('readonly', 'true');
            } else {
                $(this).prop('disabled', 'true');
            }
        });
    } catch (err) {
        alert("Error in StopINVButtonEditMode: " + err.message);
    }
}

function FormatINVPNDetail(divSelector) {
    try {
        if (divSelector == null) {
            return;
        }
        var id = divSelector.split("_")[1];
        //alert($(divSelector).html());
        //Set button colors
        $(".editinv." + id).addClass("activeEditButton");

        //Bind button functions
        $(".editinv." + id).on("click", editINVInfo_Click);
        $(".saveinv." + id).on("click", saveINVInfo_Click);
        $(".cancelinv." + id).on("click", cancelINVInfo_Click);
        $(".btnAddLoc." + id).on("click", AddRowToInventoryTable)
        $(".btnAddLoc." + id).addClass("activeAddButton");

        //hide columns that aren't visible until user presses edit-
        $(".toggleCol." + id).addClass('hidden');
        //Take are of the comment section
        $("#divInvCmt_" + id).addClass('hidden');
        $("#txtInvCmt_" + id).addClass('invCommentInput')
        $("#lblInvCmt_" + id).addClass('invCommentLabel')

        //Set up so color goes to yellow after user leaves a cell
        $(".txtinput.inv." + id).blur(function () {
            setFieldInvalid("#" + $(this).attr('id'),false);           
        })
        $(".cboinput.inv." + id).change(function () {
            setFieldInvalid("#" + $(this).attr('id'),false); 
        })

        //Synch the value of the Inventory Cell in the main table (i.e., for otsParts.ID) to the updated value in this html's header
        $(".onhand." + id).html($("#invhdr_" + id).text().split(":")[1].trim());

    } catch (err) {
        alert("Error in FormatINVPNDetail: " + err.message);
    }
}

/*Executes an AJAX event to get inventory data from the database.
id is required, as it tells us which section of the page will accept the data*/
function AJAX_GetInvPNDetail(id) {
    try {
        //The ID is the only data we need to send to the server.
        var obj = new Object();
        obj.input = id;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/getHTMLForPartNumberIDInventory",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                //The result goes in...
                $("#divPNInvInfo_" + id).html(msg.d);
                //Format this
                FormatINVPNDetail("#divPNInvInfo_" + id);
                //alert(msg.d);
                //Initialize the returned HTML

            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in GetOTSPNDetail: " + err.message);
    }
}

/*Executes an AJAX event to get inventory history table from the database.
id is required, as it tells us which section of the page will accept the data*/
function AJAX_GetInvPNHistory(id) {
    try {
        //The ID is the only data we need to send to the server.
        var obj = new Object();
        obj.input = id;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "OTSPN.aspx/MakePartNumberInventoryHistoryTable",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                //The result goes in...
                $("#divPNTransactions_" + id).html(msg.d);
                //Format required??

            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in AJAX_GetInvPNHistory: " + err.message);
    }
}

//Sets a field valid or invalid
//id: css Selector; Remember to include "#", "." etc as prefix
//blSetInvalidField:
//true: field will be marked invalid
//false: field will be marked active
function setFieldInvalid(id, blSetInvalidField) {
    try {
        if (blSetInvalidField == true) {
            $(id).removeClass("activeInputField").addClass("invalidField");
        } else {
            $(id).removeClass("invalidField").addClass("activeInputField");
        }
        
    } catch (err) {
        alert("Error in setFieldInvalid: " + err.message);
    }
}

//Insert a row object that is the *clone* of the selected row in the table:
//addFromRowID should be of the form base_[vpnID]_[pnID]_[invBulkID]
function AddRowToInventoryTable() {
    try {
        vpnID = $(this).attr('id').split("_")[1];
        pnID = $(this).attr('id').split("_")[2];
        invBulkID = $(this).attr('id').split("_")[3];
        //Make a clone of the current row and change is vpnID:
        //The structure of the id is [vpnID]_[ID]_[invBulkID].
        //Since we are adding a new invBulkID, we will make our invBulkID be the next available *negative* number.
        newRowID = vpnID + "_" + pnID + "_" + insertedRowIndex;
        $("#invrow_" + vpnID + "_" + pnID + "_" + invBulkID).after(
            $("#invrow_" + vpnID + "_" + pnID + "_" + invBulkID).clone(true, true).
                attr('id',"invrow_" + newRowID)
            );
        //Update insertedRowIndex
        insertedRowIndex = insertedRowIndex - 1;

        // Change the ID of all the input fields in this last row
        $("#invrow_" + newRowID + " td").eq(0).children().first().attr('id', "invVendor_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(1).children().first().attr('id', "invVendorPN_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(2).children().first().attr('id', "invQty_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(3).children().first().attr('id', "invSign_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(4).children().first().attr('id', "invDelta_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(5).children().first().attr('id', "invSubInv_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(6).children().first().attr('id', "invLocCode_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(7).children().first().attr('id', "invContactCode_" + newRowID);
        $("#invrow_" + newRowID + " td").eq(8).children().first().attr('id', "btnRemoveRow_" + newRowID);

        //Replace the html for btnRemoveRow with
        $("#btnRemoveRow_" + newRowID).off();
        $("#btnRemoveRow_" + newRowID).removeClass("btnAddLocInv");
        $("#btnRemoveRow_" + newRowID).removeClass("activeAddButton");
        $("#btnRemoveRow_" + newRowID).addClass("btnRemoveLocInv");
        $("#btnRemoveRow_" + newRowID).prop('value', 'Remove');
        $("#btnRemoveRow_" + newRowID).click(function () {
            $("#btnRemoveRow_" + newRowID).parents("tr").first().remove();
        })
        $("#btnRemoveRow_" + newRowID).prop('title', 'Cancel the addition of this row.');
        //Reset some values
        $("#invLocCode_" + newRowID).val('');
        $("#invContactCode_" + newRowID).val('');
        $("#invSubInv_" + newRowID).val('');
        //The only action is ADD for a new row...
        $("#invSign_" + newRowID).val('2');
        $("#invSign_" + newRowID).removeClass('activeInputField');
        $("#invSign_" + newRowID).addClass('inactiveInputField');
        $("#invSign_" + newRowID).prop('disabled', 'true');
        $("#invQty_" + newRowID).val('0');
        $("#invDelta_" + newRowID).val('');

        //alert($("#invrow_" + newRowID).html());
    } catch (err) {
        alert("Error in AddRowToInventoryTable: " + err.message);
    }
}

