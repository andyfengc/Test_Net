<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Webform.Temp.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            name:
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="name is required" ControlToValidate="txtName"></asp:RequiredFieldValidator>
        </div>
        <div>
            department:
            <asp:ListBox ID="lstDepartment" runat="server"></asp:ListBox>
        </div>
        <div>
            <input type="button" value="Submit" id="btnSubmit" runat="server" onserverclick="btnSave_Click" />
        </div>
        <div>
            You saved:
            <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
