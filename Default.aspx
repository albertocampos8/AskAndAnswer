<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AskAndAnswer.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="MainRight" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Main Menu</h1>
    <p><a href="./CustomPN.aspx">Custom Part Numbers</a></p>
    <p><a href="./OTSPN.aspx">Off the Shelf (OTS) Part Numbers</a></p>
    <p><a href="./BOMViewUpload.aspx">View/Upload Bill of Material (BOM)</a></p>
</asp:Content>
<asp:Content ID="MainLeft" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

</asp:Content>
