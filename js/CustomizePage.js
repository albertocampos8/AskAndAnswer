/**
 * @author Al
 * Client-side code used to control how the objects in Default.aspx go here
 */

$(document).ready(function () {
    try {

        //alert("HERE");

        //Assigns the AJAX Call (in BasicCode.js)-- do not change this.  You want to change function SynthesizeInput()
        $(".inputButton").click(function (e) {
            try {
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
                $(this).css({"background-color": "yellow"});
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $(".txtinput,.cboinput").on("focusout", function () {
            try {
                $(this).css({ "background-color": "white" });
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


    } catch(err) {
        alert(err.message)
    } finally {

    }

}
)

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