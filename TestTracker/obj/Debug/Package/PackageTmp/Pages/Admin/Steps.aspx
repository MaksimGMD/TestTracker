<%@ Page Title="Этапы тестирования | Администрирование" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Steps.aspx.cs" Inherits="TestTracker.Pages.Admin.Steps" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsTest" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsSteps" runat="server"></asp:SqlDataSource>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <div class="container">
        <center>
            <p class="h2">Этапы тестирования</p>
        </center>
        <div class="row">
            <div class="col-lg-4">
                <div class="form-group">
                        <label for="ddlTest">Тест</label>
                        <asp:DropDownList ID="ddlTest" runat="server" class="form-control" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"></asp:DropDownList>
                    </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="tbName">Этап тестирования</label>
                    <asp:TextBox ID="tbName" runat="server" placeholder="Этап тестирования" class="form-control" MaxLength="200" TextMode="MultiLine" style="max-height: 300px; min-height: 39px" OnTextChanged="tbName_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите содержание этапа тестирования" Display="Dynamic" ControlToValidate="tbName"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="form-group">
                    <label for="tbNumber">Номер этапа</label>
                    <asp:TextBox ID="tbNumber" runat="server" placeholder="Номер этапа" class="form-control" TextMode="Number" min="0" max="999" OnTextChanged="tbNumber_TextChanged" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер этапа" Display="Dynamic" ControlToValidate="tbNumber"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn btn-dark" ToolTip="Добавить новую запись" OnClick="btInsert_Click" />
        <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" Visible="false" OnClick="btUpdate_Click" />
    </div>
    <div class="grid-system mt-4">
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
        <div id="SelectedMessage" class="form-inline" runat="server" visible="false" display="Dynamic">
            <button id="btCanselSelected" runat="server" CausesValidation="False" onserverclick="btCanselSelected_Click"
                style="background: none; padding: 0; margin-right: 5px; border: none">
                <i class="fa fa-times" title="Отменить выбор строки"></i>
            </button>
            <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
        </div>
        <div class="table" style="overflow-x: auto; width: 100%">
           <asp:GridView ID="gvSteps" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px" 
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvSteps_RowDataBound" OnRowDeleting="gvSteps_RowDeleting" OnSelectedIndexChanged="gvSteps_SelectedIndexChanged" OnSorting="gvSteps_Sorting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-icon.png" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" CausesValidation="false"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
