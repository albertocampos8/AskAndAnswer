function GetNewCustomPN() {
    try {
        if (validateCustomPartNumberForm()) {
            alert("Ajax call here");
        }
    } catch (err) {
        alert("Error in GetNewCustomPN: " + err.message);
    }
}

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

function validateCustomPartNumberForm() {
    try {
        alert("Insert Validation code here.");
        return true;
    } catch (err) {
        alert("Error in validateCustomPartNumberForm: " + err.message);
    }
}
