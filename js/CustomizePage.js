/**
 * @author Al
 * Client-side code used to control how the objects in Default.aspx go here
 */

$(document).ready(function () {
    try {
        //Need to call this function if we are viewing data via an html string
        try {
            $(".pnsummary").each(function (index, value) {
                FormatOTSPNDetail($(this).attr('id'));
            });
        } catch (err) {

        }
        
        //tooltip
        $(document).tooltip();
        //alert("HERE");
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


        //Assigns the AJAX Call (in BasicCode.js)-- do not change this.  You want to change function SynthesizeInput()
        $(".inputButton").click(function (e) {
            try {
                if (!validateForm($(this).attr('id'), $(this).attr('id').replace("btn","div"))) {
                   return "";
                }
                sendFormData();
            }
            catch (err) {
                alert(err.message)
            } finally {
                //This prevents the method from posting back to the server
                e.preventDefault();
            }

        }); //click


        //Use the following to influence what happens after user leaves a text box.
        //Structure is as follows:
        //<Label (user prompt)><Text Box for Input -- this is $(this)>
        //<Label for Help Message>
        //<Label for Alert Message>
        //Use selector "".class"" to take effect for all functions, or "#ID" to specify for a particular text box
        $(".kvpfield").on("focusout", function () {
            try {
                if ($(this).next().val() == '') {
                    //var items = $(this).data("autocomplete");
                    console.log($('#' + this.id).data('ui-autocomplete'));
                    //alert(items);
                }
            
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $(".txtinput,.cboinput").on("focus", function () {
            try {
                $(this).css({ "background-color": "yellow" });
                $(this).css({ "color": "black" });
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $(".txtinput,.cboinput").on("focusout", function () {
            try {
                var cntlID = getCntlIndex($(this).attr('id'));
                if (cntlID == 0) {
                    // Example of how to check a value in the database and give feedback to user through AJAX call.
                } else if (cntlID == 1) {
                    //Example of how to force numeric input.
                    $(this).val($(this).val().replace(/\D/g, ''));
                }
                $(this).css({ "background-color": "white" });
                $(this).css({ "color": "black" });
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $("#txt1").on("focusout", function () {
            try {
                //Validation specific to this id here
            } catch(err) {
                alert('ERROR: ' + err);
            } finally {

            }
        });

        //Toggle dropdown visibility depending on value of selected dropdown
        $(".cboinput").on("change", function () {
            try {
                var currIndex = getCntlIndex($(this).attr('id'));
                var selVal = $(this).val();
                //NOTE: selVal = 0 means 'NO', meaning the related control should not be displayed.
                //This is consistent with the definition of the parameters of changeControlVisibility
                if (currIndex == 4) {
                    changeControlVisibility(5, selVal == 1)
                    //6 and 7 depend on 5 and 4; should they be visible if the value for 4 and 5 is 1?
                    changeControlVisibility(6, $("#cbo_4").val() == 1 && $("#cbo_5").val() == 1);
                    changeControlVisibility(7, $("#cbo_4").val() == 1 && $("#cbo_5").val() == 1);
                } else if (currIndex == 5) {
                    changeControlVisibility(6, $("#cbo_5").val() == 1);
                    changeControlVisibility(7, $("#cbo_5").val() == 1);
                }
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $(".fileinput").on("change", function () {
            try {
                alert("Not yet implemented!  Displayed file name is for demo purposes only.  Please upload your file to Box Manually.");
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });
        //***********************************************************************************
        //******** SECTION FOR OTS PN
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

        //Another case where this doesn't apply if using customPN.aspx

        try {
            //Handler that helps user determine if the Vendor Part Number they're entering has already been entered.
            $("#txt_" + RequestOTS_VPN + "," + "#txt_" + RequestOTS_Vendor).on("focusout", function () {
                try {
                    var vpn = $("#txt_" + RequestOTS_VPN).val();
                    var v = $("#txt_" + RequestOTS_Vendor).val();
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
                        error: function () {
                            alert("Ajax check vpn value: " + status);
                        }
                    }) //ajax
                } catch (err) {
                    alert('ERROR in document.ready - Handler to check if Vendor Part Number exists: ' + err);
                }
            });
        } catch (err) {

        }
 
        //***********************************************************************************

    } catch(err) {
        alert(err.message)
    } finally {

    }

})

function bindEvents() {
    //Code to higlight the table of a row
    $(".outputtablecell").on("click", function () {
        try {
            //Clear back ground colors of all rows
            $(".outputtablecell").css({ "background-color": "white" })
            //Set this row to white
            $(this).parent().children().css({ "background-color": "yellow" })
        } catch (err) {
            alert('ERROR: ' + err);
        }
    });
}

function GetNewOTSPN() {
    try {
        //Validate
        if (!validateForm($(this).attr('id'), $(this).attr('id').replace("btn", "div"))) {
            return "";
        }
        alert("passed validatiion");
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
            error: function () {
                alert(status);
            }
        }) //ajax
    } catch (err) {
        alert(err.message);
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
