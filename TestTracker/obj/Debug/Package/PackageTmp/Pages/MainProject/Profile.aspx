<%@ Page Title="Test tracker | Профиль" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="TestTracker.Pages.MainProject.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource runat="server" ID="sdsUser"></asp:SqlDataSource>
    <div class="container">
        <asp:Repeater runat="server" ID="rpUser">
            <ItemTemplate>
                <div class="page-content page-container" id="page-content">
                    <div class="padding">
                        <div class="row container d-flex justify-content-center">
                            <div class="col-xl-9 col-md-12">
                                <div class="card user-card-full">
                                    <div class="row m-l-0 m-r-0">
                                        <div class="col-sm-4 bg-c-lite-green user-profile">
                                            <div class="card-block text-center text-white">
                                                <div class="m-b-25">
                                                    <img src="https://img.icons8.com/bubbles/100/000000/user.png" class="img-radius" alt="User-Profile-Image">
                                                </div>
                                                <h6 class="f-w-600"><%#Eval("User") %></h6>
                                                <p><%#Eval("RoleName") %></p>
                                                <i class=" mdi mdi-square-edit-outline feather icon-edit m-t-10 f-16"></i>
                                            </div>
                                        </div>
                                        <div class="col-sm-8">
                                            <div class="card-block">
                                                <h6 class="m-b-20 p-b-5 b-b-default f-w-600">Информация</h6>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <p class="m-b-10 f-w-600">Email</p>
                                                        <h6 class="text-muted f-w-400"><%#Eval("UserEmail") %></h6>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <p class="m-b-10 f-w-600">Логин</p>
                                                        <h6 class="text-muted f-w-400"><%#Eval("UserLogin") %></h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
