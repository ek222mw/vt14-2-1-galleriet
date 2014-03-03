<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Gallerie_Labb2_Emil_k._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divWin" title="Gallerie">
        <asp:Label ID="Label1" runat="server" Text="Gallerie"></asp:Label><br />
        <asp:Image ID="BigImage" runat="server" Visible="false" /><br />
        <asp:FileUpload ID="chooseFileUpload" runat="server" />
        <asp:ImageButton ID="chooseImageButton" runat="server" AlternateText="Välj fil" />
        <asp:Button ID="uploadButton" runat="server" Text="Ladda upp" OnClick="uploadButton_Click" /><br />
        <div id="pic">
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound ="Repeater1_ItemDataBound" ItemType="System.String" SelectMethod="Repeater1_GetData">
            <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server"
                        ImageUrl='<%# "Thumbnail/"+ Item %>'
                        NavigateUrl='<%# "?name="+ Item %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Filändelsen är inte korrekt." ControlToValidate="chooseFileUpload" ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Jj][Pp][Ee][Gg])|.*\.([Pp][Nn][Gg]) $)|.*\.([Gg][Ii][Ff])"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ingen fil har valts" ControlToValidate="chooseFileUpload"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:PlaceHolder ID="successPlaceHolder" runat="server" Visible="false">
                <asp:Button ID="hideButton" runat="server" Text="Ta bort meddelande" CausesValidation="false" />
                <asp:Label ID="successLabel" runat="server"></asp:Label>
          </asp:PlaceHolder>
            </div>
        </div>
    </form>
</body>
</html>
