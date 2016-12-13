<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="OTSPN.aspx.cs" Inherits="AskAndAnswer.OTSPN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/stylesheets/OTSstyles.css" rel="stylesheet" type="text/css" />
    <script src="/js/OTSGlobals.js"> </script>
    <script src="/js/BasicCode.js"> </script>
    <script src="/js/OTSViewAndEditFunctions.js"> </script>
    <script src="/js/OTSSearchFunctions.js"> </script>
    <script src="/js/OTSNewPNCode.js"> </script>
    <script src="/js/DocReady_CommonBindings.js"> </script>
    <script src="/js/DocReady_OTSPN.js"> </script>
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
        <button id="btnOTSHelp" class="menubutton">Help</button>
    </asp:Panel>
</asp:Content>
