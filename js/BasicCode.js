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
            url: "Default.aspx/respond",
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
            error: function () {
                alert(status);
            }
        }) //ajax

    } catch (err) {
        alert(err.message);
    }
};

//Loops over the *VISIBLE* input elements that have class .txtinput and .cboinput and returns a string, formatted as follows:
//IDCODE_0!#!INPUTVALUE_0...IDCODE_N-1!#!INPUTVALUE_N-1
//...where IDCODE_i is the ID code of the ith element, and comes from the html control id, e.g., txt_i.  Typically the value
//of i comes from the database.
//INPUTVALUE_0 is the value in the ith input control
//THIS CODE DOES NOT EXPECT A VALUE TO BE NOTHING.  A separate error checking function should be called prior to this function
//to make sure all controls have a value
function synthesizeData() {
    try {
        //Iterate over all input elements
        var strReturn = "";
        $(".txtinput,.cboinput").each(function (index, element) {
            //if ($(this).css('display') != 'none') { <-- this is an alernate method to check for visibility
            if ($(this).is(":visible")) {
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