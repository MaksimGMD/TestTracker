<%@ Page Title="Test tracker | Проекты" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="TestTracker.Pages.MainProject.Projects" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource ID="sdsProject" runat="server"></asp:SqlDataSource>
    <div class="projects-wrapper container">
        <div class="projects-title row">
            <p class="h4">Проекты</p>
        </div>
        <asp:Repeater ID="rpProjects" runat="server">
            <ItemTemplate>
                <asp:LinkButton class="projects-card-link" runat="server" ID="btTests" OnClick="btTests_Click">
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ProjectId") %>' Style="display: none" />
                    <div class="card projects-card">
                        <div class="row align-items-center justify-content-between">
                            <div class="projects-card-section col-md-5">
                                <p><%#Eval("ProjectName") %></p>
                            </div>
                            <div class="projects-card-section col-md-4">
                                <p>v<%#Eval("ProjectVersion") %></p>
                            </div>
                            <div class="projects-card-section col-md-3">
                                <p>Тестов: <%#Eval("TesCount") %></p>
                            </div>
                        </div>
                    </div>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
