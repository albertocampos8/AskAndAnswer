<%@ Page Title="" Language="C#" MasterPageFile="~/TestDBMaster.master" AutoEventWireup="true" CodeBehind="TestDBConnection.aspx.cs" Inherits="InvTracker.TestDBConnection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DisplayContentPlaceHolder" runat="server">
        <h2>Database Connection</h2>
    <p>Press button to verify you can make connection to database.</p>
    <p>NOTE: You must change the connection string in Procedure btnTestDB_Click in the Code-behind file.</p>
        <asp:Button ID="btnTestDB" runat="server" OnClick="btnTestDB_Click" Text="Test Database" />
        <asp:Label ID="lblTestResult" runat="server"></asp:Label>
    <asp:Button ID="btnTestEmail" runat="server" OnClick="btnEmail_Click" Text="Test Email" />
    <asp:TextBox ID="txtServer" runat="server">mailhost.Broadcom.net</asp:TextBox>
    <asp:TextBox ID="txtToAddress" runat="server">alberto.campos@broadcom.com</asp:TextBox>
</asp:Content>
