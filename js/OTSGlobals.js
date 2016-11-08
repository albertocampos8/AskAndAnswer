
//Contorl ID Number in Div divOTSFind, where the user supplies search criteria
//These numbers are appended to div, txt, cbo, etc. to create unique IDs in the page
var ID_OTSSearch_Method = "24";
var ID_OTSSearch_ByText = "25";
var ID_OTSSearch_ByBU = "26";
var ID_OTSSearch_ByUser = "27";
var ID_OTSSearch_ByDate = "28";


//Contstant names for the Search Methods, as contained in OTSSEarch_Method drop down box (and defined in the database)
var SearchBy_PN = "1";
var SearchBy_BU = "2";
var SearchBy_Desc = "8";
var SearchBy_Requestor = "3";
var SearchBy_Vendor = "4";
var SearchBy_VendorPN = "5";
var SearchBy_RequestDate = "6";
var SearchBy_RequestForProduct = "7";

//Indices for fields when requesting OTS Part Numbers
var RequestOTS_VPN = "18";
var RequestOTS_Vendor = "19";
var RequestOTS_User = "20";
var RequestOTS_BU = "21";
var RequestOTS_ForProduct = "22";
var RequestOTS_PartType = "23";
var RequestOTS_PartSubType = "13";
var RequestOTS_Package = "14";
var RequestOTS_Value = "15";
var RequestOTS_Tol = "16";
var RequestOTS_Size = "17";
var RequestOTS_Attachment = "29";
//OTS AJAX Call Codes
var OTSAJAX_Search = 0;

function FormatTable_OTSStyle(selector) {
    try {
        $(selector).removeClass("otsTable");
        $(selector).addClass("otsTable");

        $(selector).find("td,th").each(function (index) {
            $(this).addClass("otsTableCells");
        })
    } catch (err) {
    }
}