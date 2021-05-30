﻿<%@ Page Title="Test tracker | Обратная связь" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="FeedBack.aspx.cs" Inherits="TestTracker.Pages.MainProject.FeedBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <div class="mail-form" style="margin-bottom: 30px">
                    <div class="row">
                        <div class="col" style="padding: unset">
                            <div class="alert alert-success" role="alert" id="alert" runat="server" visible="false" display="Dynamic">
                                Ваше сообщение успешно отправленно!
                            </div>
                        </div>
                    </div>
                    <div class="row project-title align-items-center">
                        <div class="col-lg-6 align-self-start mt-2 mb-2" style="text-align: start; padding: unset; margin-bottom: unset">
                            <p class="h4">Письмо</p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="tbPhone">Тема сообщения</label>
                        <asp:DropDownList ID="ddlTittle" runat="server" CssClass="form-control">
                            <asp:ListItem>Идея</asp:ListItem>
                            <asp:ListItem>Проблема</asp:ListItem>
                            <asp:ListItem>Вопрос</asp:ListItem>
                            <asp:ListItem>Благодраность</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="tbMessage">Сообщение</label>
                        <asp:TextBox ID="tbMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Введите сообщение"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите сообщение" Display="Dynamic" ControlToValidate="tbMessage"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="tbName">Имя</label>
                        <asp:TextBox ID="tbName" CssClass="form-control" runat="server" placeholder="Введите имя"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите имя" Display="Dynamic" ControlToValidate="tbName"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="tbmail">Адрес почты</label>
                        <asp:TextBox ID="tbmail" runat="server" CssClass="form-control" placeholder="Введите адрес электронной почты" type="email" TextMode="Email"></asp:TextBox>
                    </div>
                    <div style="text-align:end">
                        <asp:Button ID="btSubmit" runat="server" Text="Отправить" CssClass="btn btn-success" OnClick="btSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
