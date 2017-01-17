$(document).ready(function () {

    try {
        
        CommonBindings();
        
        $(document).tooltip({
            position: { my: "right-75 top" }
        });
        
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
            minLength: 0
        });

        if ($("#txtProductStatus").val().toLowerCase == RELEASEDKEY) {
            $("#divBrowse").hide();
            $("#btnUpload").hide();
            $("#btnRelease").hide();
            $("#btnDelete").hide();
        }
        //Call this ots function in case any BOM is preloaded
        InitializeOTSViewAndEdit();

        timeout = null;
        //Wait 500ms before checking values the user entered in the form
        $(".userinput").on("keyup paste", function () {
            clearTimeout(timeout);
            timeout = setTimeout(function () {
                var p = $("#txtProduct").val();
                var pRev = $("#txtProductRev").val();
                var bRev = $("#txtBOMRev").val();
                if (p == "" || bRev == "" || pRev == "") {
                    $("#divMsg").html("");
                    $("#divBrowse").hide("slow");
                    $("#btnUpload").prop('disabled', true);
                } else {
                    AJAX_CheckProductStatus(p, pRev, bRev,statusCheckShowsReleased.replace("[P]", p.toUpperCase()).
                    replace("[PR]",pRev.toUpperCase()).replace("[BR]",padLeftZ(bRev.toUpperCase(),2)));
                }
                
            }, 500);
        });

        $("#btnViewBOM").on("click", function (e) {
            
            var p = $("#txtProduct").val();
            var pRev = $("#txtProductRev").val();
            var bRev = $("#txtBOMRev").val();
            if ($("#txtProductStatus").val().toLowerCase() != PRELIMINARYKEY &&
                $("#txtProductStatus").val().toLowerCase() != RELEASEDKEY) {
                OpenDialog("#dialog", "NO BOM", "You can only View BOMs that have been uploaded to the database.  " +
                    "This means the Product Status should PRELIMINARY or RELEASED.");
                
                e.preventDefault();
                return false;
            }
            window.location.href = "BOMViewUpload.aspx?p=" + p + "&pR=" + pRev + "&bR=" + bRev;
            e.preventDefault();
        });

        $("#btnRelease").on("click", function (e) {
            if (ConfirmAndRelease()) {
                var p = $("#txtProduct").val();
                var pRev = $("#txtProductRev").val();
                var bRev = $("#txtBOMRev").val();
                AJAX_releaseBOM(p, pRev, bRev);
                //Double-check the status, update the form 
                AJAX_CheckProductStatus(p, pRev, bRev, successReleaseBOMMessage.replace("[P]", p.toUpperCase()).
                    replace("[PR]", pRev.toUpperCase()).replace("[BR]", padLeftZ(bRev.toUpperCase(),2)));
            };
            e.preventDefault();
        });

        $("#btnDelete").on("click", function (e) {
            if (ConfirmAndRelease()) {
                var p = $("#txtProduct").val();
                var pRev = $("#txtProductRev").val();
                var bRev = $("#txtBOMRev").val();
                AJAX_deleteBOM(p, pRev, bRev);
            };
            e.preventDefault();
        });

        $("#btnUpload").on("click", function (e) {
            //Validation here
            if ($("#filFileUpload").val() == "") {
                OpenDialog("#dialog", "FILE SELECTION REQUIRED", "You must select a valid file to upload.");
                e.preventDefault();
                return false;
            } else {
                if ($("#txtProductStatus").val().toLowerCase()==PRELIMINARYKEY && !confirm("Are you sure you want to upload a BOM?  The current BOM will be OVERWRITTEN and its contents can no longer be recovered!")) {
                    e.preventDefault();
                    return false;
                }
            }
            
        });
        $("#btnShowBOMRevs").on("click", function (e) {
            $("#txtBOMRev").autocomplete("search", "");
           e.preventDefault();
        });

        $("#btnHome").on("click", function (e) {
            try {
                location.href = './Default.aspx';
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $("#btnHistory").on("click", function (e) {
            var p = $("#txtProduct").val();
            location.href = "./BOMViewUpload.aspx?h=" + p;
            e.preventDefault();
        });

        $(".btnToggleRelNotes").on("click", ToggleReleaseNotes);

        $(".relEdit").on("click", function (e) {
            try {
                $("#txtReasonForChange").prop('readonly', '');
                $("#btnEditRelNote").prop('disabled', 'true');
                $("#btnSaveRelNote").prop('disabled', '');
                $("#btnCancelRelNote").prop('disabled', '');
                initRelNote = $("#txtReasonForChange").val();
                e.preventDefault();
            } catch (err) {
                alert("Error in EditReleaseNote: " + err.message);
            }
        });

        $(".relSave").on("click", function (e) {
            try {
                $("#txtReasonForChange").prop('readonly', 'true');
                $("#btnEditRelNote").prop('disabled', '');
                $("#btnSaveRelNote").prop('disabled', 'true');
                $("#btnCancelRelNote").prop('disabled', 'true');
                AJAX_EditReleaseNote($("#txtProduct").val(),
                     $("#txtProductRev").val(),
                     $("#txtBOMRev").val(),
                     $("#txtReasonForChange").val());
                e.preventDefault();
            } catch (err) {
                alert("Error in SaveReleaseNote: " + err.message);
            }
        });

        $(".relCancel").on("click", function (e) {
            try {
                $("#txtReasonForChange").prop('readonly', 'true');
                $("#btnEditRelNote").prop('disabled', '');
                $("#btnSaveRelNote").prop('disabled', 'true');
                $("#btnCancelRelNote").prop('disabled', 'true');
                $("#txtReasonForChange").val(initRelNote);
                e.preventDefault();
            } catch (err) {
                alert("Error in CancelReleaseNote: " + err.message);
            }
        });

        $(".viewBOM").on("click", function (e) {
            var p = $(this).attr('id').split("_")[1];
            var pR = $(this).attr('id').split("_")[2];
            var bR = $(this).attr('id').split("_")[3];
            window.open("BOMViewUpload.aspx?p=" + p + "&pR=" + pR + "&bR=" + bR);
            e.preventDefault();
        });

    } catch (err) {
        alert("Error in docready for BOMView: " + err.message);
    }

});

