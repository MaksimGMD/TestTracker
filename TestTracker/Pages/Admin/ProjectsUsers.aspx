<%@ Page Title="Назначение на проект |  Администрирование" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProjectsUsers.aspx.cs" Inherits="TestTracker.Pages.Admin.ProjectsUsers" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsProjects" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsUsers" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProjectsUsers" runat="server"></asp:SqlDataSource>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <div class="container">
        <center>
            <p class="h2">Назначение на проект</p>
        </center>
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <div class="form-group">
                        <label for="ddlRole">Пользователи</label>
                        <asp:DropDownList ID="ddlUser" runat="server" class="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="form-group">
                    <div class="form-group">
                        <label for="ddlRole">Проект</label>
                        <asp:DropDownList ID="ddlProject" runat="server" class="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn btn-dark" ToolTip="Добавить новую запись" OnClick="btInsert_Click" />
        <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" Visible="false" OnClick="btUpdate_Click" />
    </div>
    <div class="grid-system mt-5">
        <div class="row mb-2">
            <div class="col">
                <div class="form-inline">
                    <asp:TextBox ID="tbSearch" CssClass="form-control" runat="server" placeholder="Поиск" type="text" TextMode="Search" Width="200px"></asp:TextBox>
                    <button runat="server" id="btSearch" class="btn-search" title="Поиск" causesvalidation="False" onserverclick="btSearch_Click">
                        <i class="fa fa-search"></i>
                    </button>
                    <button runat="server" id="btFilter" class="btn btn-dark" onserverclick="btFilter_Click"
                        title="Фильтр" causesvalidation="False" style="height: 36px!important; margin: 0 0 0 1%; color: #e1f1ff;">
                        Фильтр</button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="btn btn-dark"
                        ToolTip="Отменить поиск и фильтрацию" CausesValidation="False" Style="margin: 0 0 0 1%; color: #e1f1ff;" Visible="false" OnClick="btCancel_Click" />
                </div>
            </div>
        </div>
        <div id="SelectedMessage" class="form-inline mt-2" runat="server" visible="false" display="Dynamic">
            <button id="btCancelSelected" runat="server" onserverclick="btCancelSelected_Click"
                style="background: none; padding: 0; margin-right: 5px; border: none">
                <i class="fa fa-times" title="Отменить выбор строки"></i>
            </button>
            <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
        </div>
        <div class="table" style="overflow-x: auto; width: 100%">
            <asp:GridView ID="gvProjectsUsers" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvProjectsUsers_RowDataBound" OnRowDeleting="gvProjectsUsers_RowDeleting" OnSelectedIndexChanged="gvProjectsUsers_SelectedIndexChanged" OnSorting="gvProjectsUsers_Sorting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-icon.png" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
