﻿$(document).ready(function () {
    try {
        //Start by assigning common bindings:
        CommonBindings();

        //VERY IMPORTANT!!  PRevents the Enter Key from taking user to previous page, so enter key can now be used to trigger search...
        $(document).keypress(function (e) {
            var keyCode = (window.event) ? e.which : e.keyCode;
            if (keyCode && keyCode == 13) {
                e.preventDefault();
                return false;
            }
        });

        //alert("doc ready otspn");
        //
        //Need to call this function if we are viewing data via an html string
        try {
            $(".pnsummary").each(function (index, value) {
                FormatOTSPNDetail($(this).attr('id'));
            });
        } catch (err) {
        }

        //The following is needed if viewin PN history via an html string
        try {
            //Something is not getting formatted; issue has to do with the fact the original table is in a TAB...
            FormatTable_OTSStyle("#tblInvInfo_" + $(".getID").attr('id').split("_")[1]);
            FormatINVPNDetail($(".getID").attr('id'));
        } catch (err) {
        }

        //Set width for input elements-- note, this applies to OTSPN.aspx page only
        try {
            var w = "250px"
            $("#txt_" + RequestOTS_VPN).width(w);
            $("#txt_" + RequestOTS_Vendor).width(w);
            $("#cbo_" + RequestOTS_User).width(w);
            $("#cbo_" + RequestOTS_BU).width(w);
            $("#txt_" + RequestOTS_ForProduct).width(w);
            $("#cbo_" + RequestOTS_PartType).width(w);
            $("#cbo_" + RequestOTS_PartSubType).width(w);
            $("#txt_" + RequestOTS_Package).width(w);
            $("#txt_" + RequestOTS_Value).width(w);
            $("#txt_" + RequestOTS_Tol).width(w);
            $("#txt_" + RequestOTS_Size).width(w);
            $("#txt_" + RequestOTS_Attachment).width(w);
        } catch (err) {

        }

        $(".menubutton").on("mouseenter", function () {
            $(this).css({ "background-color": "yellow" });
            $(this).css({ "color": "black" });
        });
        $(".menubutton").on("mouseleave", function () {
            $(this).css({ "background-color": "maroon" });
            $(this).css({ "color": "white" });
        });


        //Code to handle showing the proper div when clicking a menu button
        //The server must generate the divs such that the names are the same as those below,
        //with 'btn' replaced by 'div'
        $("#btnOTSNew, #btnOTSFind, #btnOTSAdmin").on("click", function (e) {
            try {
                //Hide all Divs...
                $("#otsdivs div").css('display', 'none');
                //To get the div we want, replace btn with div
                var divID = $(this).attr('id').replace("btn", "div");
                $("#" + divID).css('display', 'block');
                if (divID == "divOTSNew") {
                    //Unsure why the following is needed, but if I leave it out, divSearch is hidden
                    $("#" + divID + " *").css('display', 'block');
                } else if (divID == "divOTSFind") {
                    //alert($("#" + divID).html());
                    InitializeOTSSearchCSS();
                }
                //The above was commented out because it shows extra divs; make only divSearch displayed:
                //$("#" + divID + " #divSearch").css('display', 'block');
                //Do not post back to server
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        //Handler for getting new OTS PN
        $("#btnOTSNewIn").on("click", GetNewOTSPN);

        //Code to handle menu buttons that go to a link
        $("#btnOTSToMain").on("click", function (e) {
            try {
                location.href = './Default.aspx';
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        //Code to handle menu buttons that go to a link
        $("#btnUploadOTS").on("click", function (e) {
            try {
                location.href = './UploadOTS.aspx';
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $("#btnOTSHelp").on("click", function (e) {
            try {
                location.href = './Help.aspx';
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        //Set Tabs
        $("#divOTSFind, #divLook").tabs();

        //Handler that helps user determine if the Vendor Part Number they're entering has already been entered.
        $("#txt_" + RequestOTS_VPN + "," + "#txt_" + RequestOTS_Vendor).on("focusout", function () {
            try {
                var vpn = $("#txt_" + RequestOTS_VPN).val();
                var v = $("#txt_" + RequestOTS_Vendor).val();
                if ((vpn == "" && v == "") || (vpn == "")) {
                    $("#divOTSNewOut").html('');
                    return;
                }
                var data = "1" + DELIM + vpn + DELIM + v;
                var obj = new Object();
                obj.input = data;
                var strData = JSON.stringify(obj);
                //make an AJAX Call
                $.ajax({
                    type: "POST",
                    url: "OTSPN.aspx/WhereUsedForVPN",
                    data: strData,
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //The result goes in...
                        if (msg.d == "") {
                            $("#divOTSNewOut").html('<p style="color:green"><b>Good!</b></p><p>The vendor/vendor part number combination you have entered does not yet exist in the database.</p>');
                        } else {
                            $("#divOTSNewOut").html(msg.d);
                            FormatTable_OTSStyle("#tbl_" + vpn);
                        }
                        //alert(msg.d);

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
                    }
                }) //ajax
            } catch (err) {
                alert('ERROR in document.ready - Handler to check if Vendor Part Number exists: ' + err);
            }
        });

        //Search when use hits enter in txt_25 (text field)
        $("#txt_25").bind('keyup', function (event) {
            if (event.which == 13) {
                ExecuteSearch();
            }
            event.preventDefault();
        });


    } catch (err) {
        alert("Error in docready for ostpn: " + err.message);
    }


});




