$(document).ready(function () {
    try {
        //Start by assigning common bindings:
        CommonBindings();
        //Assign tabs
        $("#otsUploadTabs").tabs();

        //Determine whether #divForInventory (the comment) should be shown when first displayed
        if ($("#rdbOTSInventory").is(":checked")) {
            $("#divForInventory").show();
        }

        //Code to handle menu buttons that go to a link
        $("#btnOTSBack").on("click", function (e) {
            try {
                location.href = './OTSPN.aspx';
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $("#btnUpload").click(function (e) {
            try {
                //Validation
                $("#divForError").html('');
                //At least one radio button must be checked
                //NOTE: Below only works because I have one group of radio buttons.  If more than one group is present,
                //I would need to find a way to check each group.  ASP modifies the group name of radio buttons in a master page,
                //so I'm not sure how to do that...
                if ($("input[type=radio]:checked").length == 0) {
                    $(".uploadOpts").css({ "color": "red" });
                    $("#divForError").html("You must select one of the import options to proceed.");
                    e.preventDefault();
                    return;
                }

                //If inventory upload, comment required
                if ($("#rdbOTSInventory").is(":checked")) {
                    if ($("#txtInvComment").val() == "") {
                        $("#txtInvComment").css('background-color', 'red');
                        $("#divForError").html("You must enter a comment to proceed.");
                        e.preventDefault();
                        return;
                    }
                }
                var source = new EventSource("UploadOTS.aspx");
                source.onmessage = function (event) {
                    $("#divMsg").innerHTML = event.data + "<br>" + $("#divMsg").innerHTML;
                }
            } catch (err) {
                alert(err.message);
            }

        });

        $("#rdbOTSImport, #rdbOTSInventory").change(function () {
            try {
                $(".uploadOpts").css({ "color": "black" });
                $("#divForError").html('');
                if ($("#rdbOTSInventory").is(":checked")) {
                    $("#divForInventory").show('slow');
                } else {
                    $("#divForInventory").hide();
                }
            } catch (err) {
                alert(err.message);
            }

        });
    } catch (ex) {
        alert(ex.message);
    }
});