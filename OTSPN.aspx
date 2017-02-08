<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="OTSPN.aspx.cs" Inherits="AskAndAnswer.OTSPN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--<Scripts are added in the PageLoad method, to allow appending GET Variable to refresh cach with new version release-->
        <link href=<%=cssOTSStyles%> rel="stylesheet" type="text/css" />
        <script src="<%=jsOTSGlobals%>" ></script>
        <script src="<%=jsBasicCode%>" ></script>
        <script src="<%=jsOTSViewAndEditFunctions%>" ></script>
        <script src="<%=jsOTSSearchFunctions%>" ></script>        
        <script src="<%=jsOTSNewPNCode%>" ></script>
        <script src="<%=jsDocReady_CommonBindings%>" ></script>        
        <script src="<%=jsDocReady_OTSPN%>" ></script>
</asp:Content>
<asp:Content ID="OTSRight" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--NOTE: Set ClientIDMode = static to prevent VS from messing with ID names on form -->
    <asp:Panel id="otsdivs" runat="server"></asp:Panel>
    <!--The following is used as a dialog box -->
    <div id="dialog"></div>
</asp:Content>
<asp:Content ID="OTSLeft" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br/>
    <asp:Panel id="divMenuButtons" runat="server">
        <button id="btnOTSToMain" class="menubutton">Back to Main Menu</button>
        <button id="btnOTSNew" class="menubutton">Get New Part Number</button>
        <button id="btnOTSFind" class="menubutton">Find/Edit Part Number</button>
        <button id="btnOTSAdmin" class="menubutton">Administer</button>
        <button id="btnUploadOTS" class="menubutton">Upload OTS Data</button>
        <button id="btnOTSHelp" class="menubutton">Help</button>
    </asp:Panel>
</asp:Content>
