<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="TestTracker.Pages.MainProject.Projects" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource ID="sdsProject" runat="server"></asp:SqlDataSource>
    <div class="projects-wrapper container">
        <div class="projects-title row">
            <h2>Проекты</h2>
        </div>
        <%--<div class="projects-add-section row">
            <asp:Button runat="server" ID="btbAdd" Text="Добавить проект" CssClass="btn btn-primary" />
        </div>--%>
        <asp:Repeater ID="rpProjects" runat="server">
            <ItemTemplate>
                <a href="#" class="projects-card-link">
                    <div class="card projects-card">
                        <div class="row align-items-center justify-content-between">
                            <div class="projects-name-section col-md-5">
                                <p><%#Eval("ProjectName") %></p>
                            </div>
                            <div class="projects-version-section col-md-4">
                                <p>v<%#Eval("ProjectVersion") %></p>
                            </div>
                            <div class="projects-statistic-section col-md-3">
                                <p>Тестов: <%#Eval("TesCount") %></p>
                            </div>
                        </div>
                    </div>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
