<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMultiPartForm.Master" AutoEventWireup="true" CodeBehind="UploadOTS.aspx.cs" Inherits="AskAndAnswer.UploadOTS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    <link href=<%=cssUploadOTS%> rel="stylesheet" type="text/css" />
    <script src="<%=jsBasicCode%>" ></script>
    <script src="<%=jsDocReady_CommonBindings%>" ></script>
    <script src="<%=jsDocReady_UploadOTS%>" ></script>
</asp:Content>
<asp:Content ID="ContentRight" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="otsUploadTabs" runat="server">
        <ul>
            <li><a href="#divManualImport">Manual Import</a></li>
            <li><a href="#divAutoImport">Auto Import</a></li>
        </ul>
        <asp:Label ID="lblImportType" runat="server" Text="Select the File Type that defines this Import." CssClass="lblinput"></asp:Label>
        <asp:RadioButton ID="rdbOTSImport" GroupName="rblFTypes" CssClass="uploadOpts" ClientIDMode="Static" runat="server" Text="Update OTS Part Database" />
        <asp:RadioButton ID="rdbOTSInventory" GroupName="rblFTypes" CssClass="uploadOpts" ClientIDMode="Static" runat="server" Text="Upload Inventory Updates" />
        <div id="divForError" style="color:red"></div>
        <div id="divForInventory" style="display:none">
            <asp:Label ID="lblInvComment" runat="server" 
                Text="Enter the comment that will be used for the Inventory Transaction (note this will apply to ALL successful transactions that result from reading the file)."></asp:Label>
            <asp:TextBox ID="txtInvComment" CssClass="txtinput" runat="server"></asp:TextBox>
        </div>
        <asp:Panel ID="divManualImport" runat="server">
            
             <asp:Panel ID="divBrowse" runat="server">
                    <asp:Label ID="lblFileUpload" runat="server" Text="Select a file and press Upload to insert its contents into the database." CssClass="lblinput"></asp:Label>
                    <asp:FileUpload ID="filFileUpload" runat="server" CssClass="filControl" BackColor="Black" ForeColor="White" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btninput"  BackColor="Lime" OnClick="btnUpload_Click" />  
            </asp:Panel>
            <div id="divResult" runat="server">

            </div>
        </asp:Panel>
        <asp:Panel ID="divAutoImport" runat="server">
        
        </asp:Panel>

    </asp:Panel>
    
</asp:Content>
<asp:Content ID="ContentLeft" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <button id="btnOTSBack" class="menubutton">Back to OTS Menu</button>
</asp:Content>
