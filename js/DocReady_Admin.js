//Detect firefox, which supports accordion (can't get it to work with IE or chorme... yet
var isFirefox = navigator.userAgent.toLowerCase().indexOf('firefox') > -1;

//function from which to initiate autocomplete on certain text boxes
function AJAX_InitAutoComplete() {
    try {
        $("#txtAddress").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/GetStreetAddress",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtAddress").val() + "'}",
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (response) {
                        response("");
                        alert("Error: " + res.responseText);
                    }
                }) //ajax
            },
            minLength: 1
        });

        $("#txtCity").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/GetCity",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtCity").val() + "'}",
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (response) {
                        response("");
                        alert("Error: " + res.responseText);
                    }
                }) //ajax
            },
            minLength: 1
        });

        $("#txtStateProvince").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/GetStateProvince",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtStateProvince").val() + "'}",
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (response) {
                        response("");
                        alert("Error: " + res.responseText);
                    }
                }) //ajax
            },
            minLength: 1
        });

        $("#txtPostalCode").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/GetPostalCode",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtPostalCode").val() + "'}",
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (response) {
                        response("");
                        alert("Error: " + res.responseText);
                    }
                }) //ajax
            },
            minLength: 1
        });

        $("#txtCountry").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/GetCountry",
                    //data: strValForTerm,
                    data: "{'term':'" + $("#txtCountry").val() + "'}",
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (response) {
                        response("");
                        alert("Error: " + res.responseText);
                    }
                }) //ajax
            },
            minLength: 1
        });
    } catch (err) {
        alert(err.message);
    }
}

$(function () {
    try {
        //Set up tabs
        //$("#divAdminTabs").tabs();
        //Setup accordion
        if (isFirefox) {
            $("#divAdminAccordion").accordion({
                collapsible: true,
                active: false,
                heightStyle: content
            })
        }

        $(".txtLocInput").focus(function() {
            $(this).css({ "background-color": "yellow" });
        });

        $(".txtLocInput").blur(function () {
            $(this).css({ "background-color": "white" });
        });

        $(".txtCap").blur(function () {
            $(this).val($(this).val().toUpperCase());
        });

        $("#txtAddress").blur(function () {
            //Apply corrections to the address
            $("#txtAddress").val($("#txtAddress").val().toUpperCase().
                    replace(" STREET"," ST").
                    replace(" AVENUE"," AVE").
                    replace(" BOULEVARD"," BLVD").
                    replace(" SUITE"," STE").
                    replace(" COURT"," CT").
                    replace(" ST.", " ST").
                    replace(" AVE.", " AVE").
                    replace(" BLVD.", " BLVD").
                    replace(" STE.", " STE").
                    replace(" CT.", " CT"));
            autocompleteAddress();
        })
        //Validator for btnSubmitLoc
        $("#btnSubmitLocation").on("click", function (e) {
            try {
                errmsg = "";
                if ($("#txtAddress").val() == "") {
                    errmsg = "<p>You must supply a valid Address!</p>";
                    $("#txtAddress").css({ "background-color": "red" });
                }
                if ($("#txtCity").val() == "") {
                    errmsg = errmsg + "<p>You must supply a valid City!</p>";
                    $("#txtCity").css({ "background-color": "red" });
                }
                if ($("#txtStateProvince").val() == "") {
                    errmsg = errmsg + "<p>You must supply a valid State/Province!</p>";
                    $("#txtStateProvince").css({ "background-color": "red" });
                }
                if ($("#txtCountry").val() == "") {
                    errmsg = errmsg + "<p>You must supply a valid Country!</p>";
                    $("#txtCountry").css({ "background-color": "red" });
                }
                if (errmsg.length > 0) {
                    $("#divLocMsg").html(errmsg);
                    $("#divLocMsg").css({ "color": "red" });
                    e.preventDefault;
                    return false;
                } else {
                    $("#divLocMsg").html("<p>Submit data and report</p>");
                    $("#divLocMsg").css({ "color": "green" });
                    registerLocation();
                    e.preventDefault;
                    return false;
                }

            } catch (err) {
                alert(err.message);
            }
        });

        //Init autocomplete
        AJAX_InitAutoComplete();

        //Function to register a location in the database
        function registerLocation() {
            try {
                var encodedData = $("#txtAddress").val() + DELIM +
                                    $("#txtCity").val() + DELIM +
                                    $("#txtStateProvince").val() + DELIM +
                                    $("#txtPostalCode").val() + DELIM +
                                    $("#txtCountry").val() + DELIM +
                                    $("#txtFloor").val() + DELIM +
                                    $("#txtDetails").val();
                //alert(encodedData);
                //make a new object with a property that matches the parameter name of the web method we will call
                var obj = new Object();
                obj.input = encodedData;
                var strData = JSON.stringify(obj);
                //make an AJAX Call
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/registerLocation",
                    data: strData,
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //Originally, I had a label with id #lblResponse on the web page to accept the result.
                        //Well, a label gets rendered as a 'span' by ASP, and that is not an html element whose 'value' you can
                        //manipulate with jQuery.
                        //Instead, use a 'p' with an ID
                        $("#divLocMsg").html(msg.d);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
                    }
                }) //ajax

            } catch (err) {
                alert(err.message);
            }
        };

        //Function to autocomplete an address
        function autocompleteAddress() {
            try {
                var encodedData = $("#txtAddress").val()
                //alert(encodedData);
                //make a new object with a property that matches the parameter name of the web method we will call
                var obj = new Object();
                obj.input = encodedData;
                var strData = JSON.stringify(obj);
                //make an AJAX Call
                $.ajax({
                    type: "POST",
                    url: "Admin.aspx/autoCompleteAddress",
                    data: strData,
                    contentType: "application/json; charset utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //Originally, I had a label with id #lblResponse on the web page to accept the result.
                        //Well, a label gets rendered as a 'span' by ASP, and that is not an html element whose 'value' you can
                        //manipulate with jQuery.
                        //Instead, use a 'p' with an ID
                        if (msg.d != "") {
                            var arr = msg.d.split(DELIM);
                            //arr[0] is txtAddress, which user input
                            $("#txtCity").val(arr[1]);
                            $("#txtStateProvince").val(arr[2]);
                            $("#txtPostalCode").val(arr[3]);
                            $("#txtCountry").val(arr[4]);
                            //arr[5]=floor, and arr[6]=detail, which the user is expected to enter
                            $("#txtFloor").focus();
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        alert("Error Thrown: " + errorThrown + "\nStatus: " + textStatus + "\nResponse: " + xhr.responseText);
                    }
                }) //ajax

            } catch (err) {
                alert(err.message);
            }
        };
    } catch (err) {
        aler(err)
    }

})