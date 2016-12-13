$(document).ready(function () {

    try {
        //alert("doc ready custompn");
        //Start by assigning common bindings:
        CommonBindings();

        //Bind click event for btnSubmit
        try {
            $("#btnSubmit").on("click", GetNewCustomPN);
        } catch (err) {

        }

        //Force board rev to be numeric
        $(".txtinput,.cboinput").on("focusout", function () {
            try {
                var cntlID = getCntlIndex($(this).attr('id'));
                if (cntlID == 0) {
                    // Example of how to check a value in the database and give feedback to user through AJAX call.
                } else if (cntlID == 1) {
                    //Example of how to force numeric input.
                    $(this).val($(this).val().replace(/\D/g, ''));
                }
            } catch (err) {
                alert('ERROR: ' + err);
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


    } catch (err) {
        alert("Error in doc ready custompn: " + err.message);
    }
});