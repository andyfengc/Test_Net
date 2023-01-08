<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Webform.Temp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <asp:Literal ID="lbl1" runat="server">Hello world</asp:Literal>
            <div class="panel panel-default">
                <div class="panel-heading">Server control</div>
                <div class="panel-body">
                    <asp:Button ID="btnChange" runat="server" Text="Change me" OnClick="btnChange_Click" />
                    <div>
                        <asp:TextBox ID="txt1" runat="server" OnTextChanged="txt1_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Vertical"  RepeatLayout="UnorderedList" OnSelectedIndexChanged="btnChange_Click" AutoPostBack="true">
                        <asp:ListItem Text="value1"></asp:ListItem>
                        <asp:ListItem Text="value2"></asp:ListItem>
                        <asp:ListItem Text="value3"></asp:ListItem>
                        <asp:ListItem Text="value4"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>

            </div>
            <div class="panel panel-default">
                <div class="panel-heading">Html control</div>
                <div class="panel-body">
                    <a href="#" class="btn btn-primary" id="btnHtmlChange" runat="server" onserverclick="btnHtmlChange_Click">html change</a>
                    <input type="text" value="" id="txt2" />
                </div>
            </div>
        </form>
    </div>


    <script src="../Scripts/jquery-1.12.3.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script>
        $(function () {
            $("#txt2").blur(function () {
                alert("blur!");
            })
        })

    </script>
</body>
</html>
