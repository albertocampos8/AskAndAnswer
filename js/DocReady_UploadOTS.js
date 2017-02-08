$(document).ready(function () {
    try {
        //Start by assigning common bindings:
        CommonBindings();
        //Assign tabs
        $("#otsUploadTabs").tabs();

        //Code to handle menu buttons that go to a link
        $("#btnOTSBack").on("click", function (e) {
            try {
                location.href = './OTSPN.aspx';
                e.preventDefault();
            } catch (err) {
                alert('ERROR: ' + err);
            }
        });

        $("#btnUpload").click(function() {
            var source = new EventSource("UploadOTS.aspx");
            source.onmessage = function (event) {
                $("#divMsg").innerHTML = event.data + "<br>" + $("#divMsg").innerHTML;
            }
        })
    } catch (ex) {
        alert(ex.message);
    }
});