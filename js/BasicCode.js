var DELIM = "!#!";  //Delimiter used when sending/receiving data to the server.

//Code that executes when btnSubmit is pressed.
function sendFormData() {
    try {
        var encodedData = synthesizeData();
        //alert(encodedData);
        //make a new object with a property that matches the parameter name of the web method we will call
        var obj = new Object();
        obj.input = encodedData;
        var strData = JSON.stringify(obj);
        //make an AJAX Call
        $.ajax({
            type: "POST",
            url: "CustomPN.aspx/respond",
            data: strData,
            contentType: "application/json; charset utf-8",
            dataType: "json",
            success: function (msg) {
                //Originally, I had a label with id #lblResponse on the web page to accept the result.
                //Well, a label gets rendered as a 'span' by ASP, and that is not an html element whose 'value' you can
                //manipulate with jQuery.
                //Instead, use a 'p' with an ID
                $("#response").html(msg.d);

                //Bind any events to the returned html code in this call
                bindEvents();
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
            }
        }) //ajax

    } catch (err) {
        alert(err.message);
    }
};

/*Finds all input elements with class .txtinput or .cboinput in container <containerSelector> and arranges them in a string
formatted as follows:
IDCODE_0!#!INPUTVALUE_0...IDCODE_N-1!#!INPUTVALUE_N-1
...where IDCODE_i is the ID code of the ith element, and comes from the html control id, e.g., txt_i.  Typically the value
of i comes from the database.
This means that INPUTVALUE_is the value in the ith input control.
THIS CODE DOES NOT EXPECT A VALUE TO BE NOTHING.  A separate error checking function should be called prior to this function
to make sure all controls have a value

NOTE: containerSelector should be a valid jQuery selector, e.g., #<something>, .<something>, etc.
*/
function synthesizeData(containerSelector) {
    try {
        //Iterate over all input elements
        var strReturn = "";
        $(containerSelector).find(".txtinput,.cboinput").each(function (index, element) {
            //if ($(this).css('display') != 'none') { <-- this is an alernate method to check for visibility
            if ($(this).is(":visible"))  {
                //The numeric portion, i, of the control id, e.g., txt_i
                var ctlID = $(this).attr('id').split("_")[1];
                //The value provided by the user for the control; for drop-down boxes, this is the actual (not display) value
                var ctlVal = element.value;
                strReturn = strReturn + ctlID + DELIM + ctlVal + DELIM;
            }
        });
        //Remove the last DELIM we appended before returning the string
        return strReturn.substring(0, strReturn.length - 3);
    } catch (err) {
        alert(err.message);
    }

};

function getCntlIndex(cntlID) {
    try {
        return cntlID.split("_")[1];
    } catch (err) {
        alert("getCntlIndex: " + err.message)
    }
};

function changeControlVisibility(controlID, blMakeVisible) {
    try {
        if (blMakeVisible) {
            $("#visdiv_" + controlID).css('display', 'block');
        } else {
            $("#visdiv_" + controlID).css('display', 'none');
        }
    } catch (err) {

    }
};

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
};

function giveErrorMessage(lblErrorID, msg, lblInputField, bkgndcolor, fontcolor) {
    try {
        //Put message in field
        $(lblErrorID).css("display", "block");
        $(lblErrorID).text(msg);
        //Change color of input field
        try {
            $(lblInputField).css({"background-color": bkgndcolor});
        } catch (err2) {
            alert(err2.message);
        }
        try {
            $(lblInputField).css({ "color": fontcolor });
        } catch (err3) {
            alert(err3.message);
        }
    } catch (err) {
        alert("giveErrorMessage: " + err.message);
    }
}

function OpenDialog(dialogCssSelector, t, msg) {
    try {
  /*      //initialize dialog
        $(dialogCssSelector).dialog({
            autoOpen: false,
            title: t,
            modal: true,
            hide: { effect: "explode", duration: 1000 },
            show: { effect: "blind", duration: 1000 }
        }); */
        //Unsure why I have to initialize this again...
        $(dialogCssSelector).dialog();
        $(dialogCssSelector).html(msg);
        $(dialogCssSelector).dialog('option', 'title', t);
        /*$(dialogCssSelector).dialog('option', 'maxHeight', 'auto');
        $(dialogCssSelector).dialog('option', 'maxWidth', 'auto');*/
        $(dialogCssSelector).dialog('open');
    } catch (err) {
        alert(err.message);
    }
}