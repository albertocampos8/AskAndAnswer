<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMultiPartForm.Master" AutoEventWireup="true" CodeBehind="BOMViewUpload.aspx.cs" Inherits="AskAndAnswer.BOMViewUpload" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
        <link href=<%=cssOTSStyles%> rel="stylesheet" type="text/css" />
        <link href=<%=cssBOMViewStyles%> rel="stylesheet" type="text/css" />
        <script src="<%=jsBasicCode%>" ></script>
        <script src="<%=jsBOMViewCode%>" ></script>
        <script src="<%=jsOTSViewAndEditFunctions%>" ></script>
        <script src="<%=jsBOMViewDocReady_CommonBindings%>" ></script>        
        <script src="<%=jsDocReady_BOMView%>" ></script>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- Right Side-->
    
     <asp:Panel ID="divRight" runat="server">
         <asp:Panel ID="divMsg" runat="server"></asp:Panel>
         <asp:Panel ID="divResult" runat="server"></asp:Panel>
     </asp:Panel>
</asp:Content>
<asp:Content ID="contentLeft" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Left side-->
    
    <asp:Panel ID="divLeft" runat="server">
        <br />
        <asp:Button ID="btnHome" runat="server" Text="Back to Main Page" CssClass ="menubutton" />
        <asp:Button ID="btnHelp" runat="server" Text="Help" CssClass ="menubutton" />
        <asp:Label ID="lblProduct" runat="server" Text="Product Name" CssClass="lblinput"></asp:Label>
        <asp:Button ID="btnHistory" runat="server" Text="Show History" title ="View Product History" />
        <asp:TextBox ID="txtProduct" runat="server" CssClass="txtinput userinput" title="Name of the product.">TESTBOM1</asp:TextBox>

        <asp:Label ID="lblProductStatus" runat="server" Text="Product Status" CssClass="lblinput"></asp:Label>
        <asp:TextBox ID="txtProductStatus" runat="server" CssClass="txtinput" title="Released BOMs cannot be changed" Enabled="False">UNKNOWN</asp:TextBox>

        <asp:Label ID="lblProductRev" runat="server" Text="Product Revision" CssClass="lblinput"></asp:Label>
        <asp:TextBox ID="txtProductRev" runat="server" CssClass="txtinput userinput" title="Revision of the Product">P101</asp:TextBox>

        <asp:Label ID="lblBOMRev" runat="server" Text="BOM Revision" CssClass="lblinput"></asp:Label>
        <asp:Button ID="btnShowBOMRevs" runat="server" Text="Show" title ="List all BOM Revisions that have been uploaded for this Product/Revision combination" />
        <asp:TextBox ID="txtBOMRev" runat="server" CssClass="txtinput userinput" title ="Revision of the Product BOM">01</asp:TextBox>
        <asp:Panel ID="divBrowse" runat="server">
                    <asp:Label ID="lblFileUpload" runat="server" Text="(Optional) You can upload a BOM for this Product Revision" CssClass="lblinput"></asp:Label>
                    <asp:FileUpload ID="filFileUpload" runat="server" CssClass="filControl" BackColor="Black" ForeColor="White" />
        </asp:Panel>

        <asp:Button ID="btnUpload" runat="server" Text="Upload BOM" CssClass="btninput" OnClick="btnUpload_Click"  BackColor="Lime" />
        <asp:Button ID="btnViewBOM" runat="server" Text="View BOM" CssClass ="btninput" BackColor="#00CC00" />


    </asp:Panel>
</asp:Content>
