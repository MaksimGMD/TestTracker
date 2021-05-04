<%@ Page Title="Тесты | Администрирование" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Tests.aspx.cs" Inherits="TestTracker.Pages.Admin.Tests" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsProjects" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsStatus" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsTests" runat="server"></asp:SqlDataSource>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <div class="container">
        <center>
            <p class="h2">Тесты</p>
        </center>
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="tbTestName">Название теста</label>
                    <asp:TextBox ID="tbTestName" runat="server" placeholder="Название теста" class="form-control" MaxLength="150" OnTextChanged="tbTestName_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите название теста" Display="Dynamic" ControlToValidate="tbTestName"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="tbDescription">Описание теста</label>
                    <asp:TextBox ID="tbDescription" runat="server" placeholder="Описание теста" class="form-control" MaxLength="200" TextMode="MultiLine" style="max-height: 300px; min-height: 39px" OnTextChanged="tbDescription_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите описание теста" Display="Dynamic" ControlToValidate="tbDescription"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="tbNumber">Номер в Jira</label>
                    <asp:TextBox ID="tbNumber" runat="server" placeholder="Номер в Jira" class="form-control" MaxLength="20" OnTextChanged="tbNumber_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер" Display="Dynamic" ControlToValidate="tbNumber"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                    <div class="form-group">
                        <label for="ddlProject">Проект</label>
                        <asp:DropDownList ID="ddlProject" runat="server" class="form-control" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <div class="form-group">
                        <label for="ddlStatus">Статус теста</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="tbResult">Результат</label>
                    <asp:TextBox ID="tbResult" runat="server" placeholder="Результат" class="form-control" TextMode="MultiLine" style="max-height: 300px; min-height: 39px" OnTextChanged="tbResult_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <asp:CheckBox ID="cbDelete" runat="server" />
                <label style="font-size: 16px; font-weight: 400">Пометить для удаления</label>
            </div>
        </div>
        <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn btn-dark" ToolTip="Добавить новую запись" OnClick="btInsert_Click" />
        <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" Visible="false" OnClick="btUpdate_Click" />
    <asp:Label ID="test" runat="server"></asp:Label>
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
                        title="Фильтр" causesvalidation="false" style="height: 36px!important; margin: 0 0 0 1%; color: #e1f1ff;">
                        Фильтр</button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="btn btn-dark"
                        ToolTip="Отменить поиск и фильтрацию" CausesValidation="False" Style="margin: 0 0 0 1%; color: #e1f1ff;" Visible="false" OnClick="btCancel_Click" />
                </div>
            </div>
        </div>
        <div id="SelectedMessage" class="form-inline mt-2" runat="server" visible="false" display="Dynamic">
            <button id="btCanselSelected" runat="server" onserverclick="btCanselSelected_Click"
                style="background: none; padding: 0; margin-right: 5px; border: none">
                <i class="fa fa-times" title="Отменить выбор строки"></i>
            </button>
            <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
        </div>
        <div class="table" style="overflow-x: auto; width: 100%; text-align: center">
            <asp:GridView ID="gvTests" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvTests_RowDataBound" OnRowDeleting="gvTests_RowDeleting" OnSelectedIndexChanged="gvTests_SelectedIndexChanged" OnSorting="gvTests_Sorting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-icon.png" CausesValidation="False" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
