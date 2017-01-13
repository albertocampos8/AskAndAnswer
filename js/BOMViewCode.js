var successReleaseBOMMessage = "<p>The Status of <b>[P]</b> Revision <b>[PR]</b> (BOM Revision <b>[BR]</b>)" +
                        " has successfully been changed to  <b><span style='color:red;'>RELEASED</span></b>.</p>" +
                        "<p>The BOM is now archived and protected from any inadvertent changes.</p>" +
                        "<p>If you want to upload a new BOM for this product,  you will have to change either the Assembly Revision " +
                        "or the BOM Revision.</p>";

var statusCheckShowsReleased = "<p><b>[P]</b> Revision <b>[PR]</b> (BOM Revision <b>[BR]</b>)" +
                        " is <b><span style='color:red;'>RELEASED</span></b>.</p><p>This means you can only view the BOM.</p>" +
                        "<p>If you want to upload a new BOM for this product,  you must change either the Assembly Revision " +
                        "or the BOM Revision.</p>";

var RELEASEDKEY = "released"; //text that corresponds to Status 2 in keyAssyStatus
var PRELIMINARYKEY = "preliminary"; //text that corresponds to Status 1 in keyAssyStatus

function AJAX_InitBOMViewAutoComplete() {
    try {

        //AJAX_InitAutoComplete($("#txtProduct"), "BOMViewUpload.aspx/GetAssyNames")
        //Customize options for #txtProduct
        //$("#txtProduct").autocomplete("option", "minLength", 3);

        $("#txtProduct").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "BOMViewUpload.aspx/GetAssyNames",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtProduct").val() + "'}",
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
            minLength: 3
        });

        //The following autocomplete depends on multiple fields, so they can't be generalized
        $("#txtProductRev").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "BOMViewUpload.aspx/GetAssyRevs",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtProduct").val() + DELIM + $("#txtProductRev").val() + "'}",
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

        $("#txtBOMRev").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "BOMViewUpload.aspx/GetAssyBOMRevs",
                    data: "{'term':'" + $("#txtProduct").val() + DELIM + $("#txtProductRev").val() + "'}",
                    //data: "{'term':'" + $("#txtProduct").val() + DELIM + $("#txtProductRev").val() +
                    //     DELIM + $("#txtBOMRev").val() + "'}",
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
                
            }
        });

    } catch (err) {
        alert(err);
    }
}

///Asks the user three tims to confirm they want to release before releasing the BOM
function ConfirmAndRelease() {
    try {
        if (confirm("WARNING!  Clicking this button will release this BOM, and you will no longer be able to upload " +
            "a Bill of Material for this Revision of this Product at this BOM Revision!")) {
            if (confirm("Are  you sure?  If you click 'OK', the BOM will be permanently released.")) {
                if (confirm("This is your last chance to turn back.  Click 'CANCEL' if you think you may still need to " +
                    "upload a BOM update in the future.")) {
                    return true;
                }
            }
        }
        return false;
    } catch (err) {
        alert("Error in ConfirmAndRelease for BOMView: " + err.message);
        return false;
    }
}
/*Executes an AJAX event to get OTS data from the database.
id is required, as it tells us which section of the page will accept the data*/
function AJAX_CheckProductStatus(p, pRev, bRev, htmlForReleasedResult) {
    try {
        //The ID is the only data we need to send to the server.
        var obj = new Object();
        obj.input = p + DELIM + pRev + DELIM + bRev;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "BOMViewUpload.aspx/GetProductStatus",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                $("#txtProductStatus").val(msg.d);
                if (msg.d.toLowerCase() == RELEASEDKEY) {
                    $("#divMsg").html(htmlForReleasedResult);
                    $("#divBrowse").hide("1000");
                    $("#btnUpload").prop('disabled', 'true');
                    $("#divBrowse, #btnUpload, #btnRelease, #btnDelete").hide();
                } else {
                    //enable/show some things
                    $("#divMsg").html("");
                    $("#btnUpload").prop('disabled', '');
                    $("#divBrowse, #btnUpload, #btnRelease, #btnDelete").show("slow");
                    //If the bom status is not released, is it preliminary?
                    //If not, then we should clear divResult
                    if (msg.d.toLowerCase() != PRELIMINARYKEY) {
                        $("#divResult").html("");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in GetOTSPNDetail: " + err.message);
    }
}

/*Executes an AJAX event to get OTS data from the database.
id is required, as it tells us which section of the page will accept the data*/
function AJAX_releaseBOM(p, pRev, bRev) {
    try {
        var obj = new Object();
        obj.input = p + DELIM + pRev + DELIM + bRev;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "BOMViewUpload.aspx/releaseBOM",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.toLowerCase() == "1") {
                    $("#divMsg").html("");
                    //$("#txtProductStatus").val(RELEASEDKEY.toUpperCase());
                } else if (parseInt(msg.d) > 1) {
                    $("#divMsg").html("<b><span style='color:red;'><p>Trying to release " + p.toUpperCase() + " Revision " + pRev.toUpperCase() + " (BOM Revision " + bRev + ")" +
                        " resulted in " + msg.d + " BOMs changing to RELEASED.</p>" +
                        "<p>This is a bug that should not happen.  Please inform the administrator.</p></span></b>");
                } else if (parseInt(msg.d) < 1) {
                    $("#divMsg").html("<span style='color:red;'><b><p>Trying to release " + p.toUpperCase() + " Revision " + pRev.toUpperCase() + " (BOM Revision " + bRev + ")" +
                        " resulted in no records changing in the database, meaning your BOM was not released.</p>" +
                        "<p>This is a bug that should not happen.  Please inform the administrator.</p></b></span>");
                } else {
                    $("#divMsg").html(msg.d);
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                //alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in GetOTSPNDetail: " + err.message);
    }
}

/*Executes an AJAX event to delete a row from asyBOM.*/
function AJAX_deleteBOM(p, pRev, bRev) {
    try {
        var obj = new Object();
        obj.input = p + DELIM + pRev + DELIM + bRev;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "BOMViewUpload.aspx/deleteBOM",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                $("#divResult").html("");
                if (msg.d.toLowerCase() == "1") {
                    $("#divResult").html("<b><span style='color:red;'><p>" + p.toUpperCase() + " Revision " + pRev.toUpperCase() + " (BOM Revision " + bRev + ")</span></b>" +
                       " has been successfully DELETED.</p>");
                    //$("#txtProductStatus").val(RELEASEDKEY.toUpperCase());
                } else if (parseInt(msg.d) > 1) {
                    $("#divMsg").html("<b><span style='color:red;'><p>Trying to delete " + p.toUpperCase() + " Revision " + pRev.toUpperCase() + " (BOM Revision " + bRev + ")" +
                        " resulted in " + msg.d + " BOMs being Deleted.</p>" +
                        "<p>This is a (very bad) bug that should not happen.  Please inform the administrator.</p></span></b>");
                } else if (parseInt(msg.d) < 1) {
                    $("#divMsg").html("<span style='color:red;'><b><p>Trying to delete " + p.toUpperCase() + " Revision " + pRev.toUpperCase() + " (BOM Revision " + bRev + ")" +
                        " resulted in no records changing in the database, meaning your BOM was not deleted.</p>" +
                        "<p>This is a bug that should not happen.  Please inform the administrator.</p></b></span>");
                } else {
                    $("#divMsg").html(msg.d);
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                //alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax
    } catch (err) {
        alert("Error in GetOTSPNDetail: " + err.message);
    }
}

function hideFileBrowser() {
    try {
        var prod = $("#txtProduct").val();
        var prodRev = $("#txtProductRev").val();
        var bomRev = $("#txtBOMRev").val();

        if (prod == "" || prodRev == "" || bomRev == "") {
            $("#divBrowse").hide();
        } else {
            $("#divBrowse").show();
        }
    } catch (err) {

    }
}


function ToggleReleaseNotes() {
    try {
        var rowInd = $(this).attr('id').split("_")[1];
        var relNoteID = $(this).attr('id').split("_")[2];
        if ($("#otsExpandResultsRow_" + rowInd).is(':visible')) {
            //Since it's visible, hide it.
            $(this).val('Expand');
            $(this).prop('title','Expand to get more information about the Part Number');
            $("#otsExpandResultsRow_" + rowInd).toggle();
        } else {
            //Since it's not visible, show it
            $("#otsExpandResultsRow_" + rowInd).toggle();
            $(this).prop('title', 'Hide to reclaim browser space');
            //Since this row was invisible, its color was not set; make it match the color of this row.
            $("#otsExpandResultsRow_" + rowInd).css('background-color', $(this).closest('tr').css('background-color'));
            $(this).val('Hide');
            if ($("#otsDisplayAreaFor_" + rowInd + "_" + relNoteID).html() == '') {
                //We only need to do the ajax call if the row is empty
                $("#otsDisplayAreaFor_" + rowInd + "_" + relNoteID).html("<p>Please wait... querying database...</p>");
                alert("Insert AJAX Call to get Release Notes here!");
            }
        }
    } catch (err) {
        alert("Error in ToggleReleaseNotes: " + err.message);
    }

}