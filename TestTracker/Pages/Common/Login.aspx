<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TestTracker.Pages.Common.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="../../Content/img/logo.svg" type="image/x-icon" />
    <link rel="stylesheet" href="../../Content/bootstrap-grid.min.css" />
    <link rel="stylesheet" href="../../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../../Content/css/Styles.css" />
    <title>Авторизация</title>
</head>
<body>

    <div class="container" style="padding: 7em 0;">
        <div class="row justify-content-center">
            <div class="col">
                <div class="login-icon">
                    <a href="Main.aspx">
                        <img src="../../Content/img/logo.svg" alt="Test tracker" /></a>
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col" style="max-width: 600px">
                <form id="LoginForm" runat="server">
                    <div class="alert alert-danger" role="alert" id="alert" runat="server" visible="false">
                        Введен неверный логин или пароль!
                    </div>
                    <div class="card login-form">
                        <h4>Авторизация</h4>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph" />
                        <div class="form-group">
                            <asp:Label runat="server" for="exampleInputEmail1">Логин или адрес электронной почты</asp:Label>
                            <asp:TextBox runat="server" class="form-control" ID="tbLogin" placeholder="Введите логин или почту" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" for="exampleInputEmail1">Пароль</asp:Label>
                            <asp:TextBox runat="server" type="password" class="form-control" ID="tbPassword" placeholder="Введите пароль" required></asp:TextBox>
                        </div>
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" id="chCookie" runat="server" required>
                            <label class="form-check-label" for="exampleCheck1">Использовать Cookie</label>
                        </div>

                        <asp:Button ID="btEnter" runat="server" CssClass="btn btn-primary" Text="Войти" OnClick="btEnter_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
