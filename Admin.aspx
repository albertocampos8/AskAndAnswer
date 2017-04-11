<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMultiPartForm.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="AskAndAnswer.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=cssAdminStyles%>" rel="stylesheet" type="text/css" />
    <script src="<%=jsBasicCode%>"></script>
    <script src="<%=jsDocReady_CommonBindings%>"></script>
    <script src="<%=jsDocReady_Admin%>"></script>
</asp:Content>
<asp:Content ID="divRight" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div id="divAdminAccordion">
        <h3><a href="#location">Location Addresses</a></h3>
        <div id="location">
            <div id ="locationTabs">
                <ul>
                    <li><a href="#defineLoc">Define New Location</a></li>
                    <li><a href="#viewLoc">View Existing Locations</a></li>
                </ul>
                <div id="defineLoc">
                    <div id="divLocMsg">

                    </div>
           
                    <asp:Label ID="lblAddress" runat="server" Text="Address:" CssClass="lblLocInput"></asp:Label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="txtLocInput txtCap"></asp:TextBox>

                    <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="lblLocInput"></asp:Label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="txtLocInput txtCap"></asp:TextBox>

                    <asp:Label ID="lblStateProvince" runat="server" Text="State/Province:"></asp:Label>
                    <asp:TextBox ID="txtStateProvince" runat="server" CssClass="txtLocInput txtCap"></asp:TextBox>

                    <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="txtLocInput txtCap"></asp:TextBox>

                    <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="lblLocInput"></asp:Label>
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="txtLocInput txtCap"></asp:TextBox>

                    <p>Other Details for this Location:</p>
                    <asp:Label ID="lblFloor" runat="server" Text="Floor:" CssClass="lblLocInput"></asp:Label>
                    <asp:TextBox ID="txtFloor" runat="server" CssClass="txtLocInput"></asp:TextBox>

                    <asp:Label ID="lblDetail" runat="server" Text="Detail:" CssClass="lblLocInput"></asp:Label>
                    <asp:TextBox ID="txtDetails" runat="server" CssClass="txtLocInput"></asp:TextBox>

                    <asp:Button ID="btnSubmitLocation" runat="server" Text="Submit Location" />
                </div>
                <div id="viewLoc">
                    <p><i>Please wait... querying database...</i></p>
                </div>
            </div>
        </div>

        
        <h3><a href="#other">Other Tests</a></h3>
        <div id="other">

        </div>
    </div>
</asp:Content>
<asp:Content ID="divLeft" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
</asp:Content>
