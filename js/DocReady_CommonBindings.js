function CommonBindings() {
    try {
        //alert("common bindings");
        //initialize dialog
        $("#dialog").dialog({
            autoOpen: false,
            title: "",
            modal: true,
            hide: { effect: "explode", duration: 1000 },
            show: { effect: "blind", duration: 1000 }
        });

        //assign tooltip
        $(document).tooltip();

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

        //Set all txtinput/cboinput classes yellow when user enters...
        $(".txtinput,.cboinput").on("focus", function () {
            try {
                $(this).css({ "background-color": "yellow" });
                $(this).css({ "color": "black" });
                $("document").tooltip("close");
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        //...and white when reverting
        //Force board rev to be numeric
        $(".txtinput,.cboinput").on("focusout", function () {
            try {
                $(this).css({ "background-color": "white" });
                $(this).css({ "color": "black" });
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        //React to user making a change in an error box
        $(".txtinput").on("keyup", function () {
            try {
                var currIndex = getCntlIndex($(this).attr('id'));
                //Assume error has been resolved, hide the error label and set its value to ''
                $("#lblError_" + currIndex).css('style', 'display:none');
                $("#lblError_" + currIndex).text('');
            } catch (err) {
                alert('ERROR txtinput keyup: ' + err.message);
            }
        });

        //bindings for attachments on text boxes
        $(".fileinput").on("change", function () {
            try {
                alert("Not yet implemented!  Displayed file name is for demo purposes only.  Please upload your file to Box Manually.");
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });
    } catch (err) {
        alert("Error in Common Bindings: " + err.message);
    }
}