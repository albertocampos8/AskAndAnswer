<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" EnableViewState="false" Inherits="AskAndAnswer.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/CustomizePage.js"> </script>
    <script src="/js/BasicCode.js"> </script>
</asp:Content>
<asp:Content ID="rightColContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlOutput" runat="server">
        <div id="response">
            <p>Instructions:</p>
            <p>Answer the questions on the left and press 'Submit' after scrolling to the bottom of the form.</p>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="leftColContent" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Panel ID="pnlInput" runat="server">
    </asp:Panel>
</asp:Content>
