<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="CustomPN.aspx.cs" EnableViewState="false" Inherits="AskAndAnswer.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <!--<Scripts are added in the PageLoad method, to allow appending GET Variable to refresh cach with new version release-->
    <script src="<%=jsBasicCode%>" ></script>
    <script src="<%=jsCustomizePNCode%>" ></script>
    <script src="<%=jsDocReady_CommonBindings%>" ></script>
    <script src="<%=jsDocReady_CustomPN%>" ></script>
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
