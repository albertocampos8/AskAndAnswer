/**
 * @author Al
 * Client-side code used to control how the objects in Default.aspx go here
 */
$(document).ready(function () {
    try {
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
        //***********************************************************************************

    } catch(err) {
        alert(err.message)
    } finally {

    }

})







