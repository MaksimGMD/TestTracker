<%@ Page Title="Test tracker | Тесты" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="Tests.aspx.cs" Inherits="TestTracker.Pages.MainProject.Tests" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource runat="server" ID="sdsProgress"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="sdsTests"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="sdsTestsExport"></asp:SqlDataSource>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <div class="container-fluid">
        <div class="row">
            <p class="h4">Тесты</p>
        </div>
        <div class="row project-title align-items-center">
            <div class="col-lg-6 align-self-start mt-2 mb-2" style="text-align: start; padding: unset; margin-bottom: unset">
                <h5 style="margin-bottom: unset">
                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                    <small class="text-muted">
                        <asp:Label ID="lblProjectVersion" runat="server"></asp:Label></small>
                </h5>
            </div>
            <div class="col-lg-6 align-self-end mt-2 mb-2" style="text-align: end; padding: unset">
                <button class="btn btn-outline-primary" id="btShare" runat="server" onserverclick="btShare_Click" style="margin-right: 20px" title="Отправить письмо"><i class="fas fa-envelope"></i>Поделиться</button>
                <button type="button" class="btn btn-outline-primary" title="Экспорт отчёта в Excel" onserverclick="btExport_Click" id="btExport" runat="server"><i class="fas fa-file-download"></i>Экспорт</button>
            </div>
        </div>
        <div class="progress-section" runat="server" id="dvProgress" display="Dynamic">
            <asp:Repeater ID="rpProgress" runat="server">
                <ItemTemplate>
                    <p class="h6">Статус выполнения</p>
                    <div class="progress">
                        <div class="progress-bar bg-success" role="progressbar" style="width: <%#Eval("% Успешно") %>%"></div>
                        <div class="progress-bar bg-warning" role="progressbar" style="width: <%#Eval("% Замечание") %>%"></div>
                        <div class="progress-bar bg-danger" role="progressbar" style="width: <%#Eval("% Не успешно") %>%"></div>
                        <div class="progress-bar bg-secondary" role="progressbar" style="width: <%#Eval("% Не проводился") %>%"></div>
                    </div>
                    <ul>
                        <li>
                            <asp:Label runat="server" ID="lblSuccess" Text='<%#Eval("Успешно") %>' CssClass="text-success progress-count"></asp:Label>
                            <span>Успешно</span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="lblWarining" Text='<%#Eval("Замечание") %>' CssClass="text-warning progress-count"></asp:Label>
                            <span>Замечание</span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="lblDanger" Text='<%#Eval("Не успешно") %>' CssClass="text-danger progress-count"></asp:Label>
                            <span>Не успешно</span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="lblSecond" Text='<%#Eval("Не проводился") %>' CssClass="text-secondary progress-count"></asp:Label>
                            <span>Не проводился</span>
                        </li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="grid-section">
            <div class="grid-header">
                <div class="row" style="padding: 10px">
                    <div class="col-lg-5 mt-2">
                        <div class="input-group">
                            <asp:TextBox type="text" class="form-control" placeholder="Поиск" runat="server" ID="tbSearch"></asp:TextBox>
                            <div class="input-group-append">
                                <button class="btn btn-primary" type="button" runat="server" id="btSearch" title="Поиск" style="border-radius: 0 5px 5px 0" onserverclick="btSearch_Click">
                                    <i class="fa fa-search" style="margin: unset"></i>
                                </button>
                            </div>
                            <button class="btn btn-primary" type="button" id="btFilter" style="margin-left: 10px" title="Фильтр" runat="server" onserverclick="btFilter_Click">
                                <i class="fas fa-filter" style="margin: unset"></i>
                            </button>
                            <asp:Button runat="server" ID="btCancel" Text="Отмена" CssClass="btn btn-primary" Style="margin-left: 10px" Visible="false" display="Dynamic" title="Отменить поиск и фльтрацию" OnClick="btCancel_Click" />
                        </div>
                    </div>
                    <div class="col-lg-3 mt-2">
                        <div class="dropdown">
                            <a class="btn btn-primary dropdown-toggle" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="width: 100%">Фильтр по датам
                            </a>
                            <div class="dropdown-menu p-3" aria-labelledby="dropdownMenuLink" style="width: 100%">
                                <div>
                                    <div class="form-group" Style="margin-top: 10px">
                                        <label for="tbStartDate">Дата начала</label>
                                        <asp:TextBox runat="server" ID="tbStartDate" TextMode="Date" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" Style="margin-top: 10px">
                                        <label for="tbEndDate">Дата окончания</label>
                                        <asp:TextBox runat="server" ID="tbEndDate" TextMode="Date" class="form-control"></asp:TextBox>
                                    </div>
                                    <div style="text-align: end; margin-top: 10px">
                                         <asp:Button runat="server" ID="btDateFilterCancel" Text="Отменить" CssClass="btn btn-secondary mt-1" OnClick="btDateFilterCancel_Click" />
                                        <asp:Button runat="server" ID="btDateFilter" Text="Применить" CssClass="btn btn-primary mt-1" OnClick="btDateFilter_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 mt-2">
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Статус</asp:ListItem>
                            <asp:ListItem>Успешно</asp:ListItem>
                            <asp:ListItem>Замечание</asp:ListItem>
                            <asp:ListItem>Не успешно</asp:ListItem>
                            <asp:ListItem>Не проводился</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 align-self-end mt-2" style="text-align: end">
                        <button class="btn btn-success" id="btInsert" runat="server" onserverclick="btInsert_Click"><i class="fas fa-plus" title="Добавить тест"></i>Добавить</button>
                    </div>
                </div>
            </div>
            <div class="tab-content" style="overflow-x: auto; width: 100%; overflow-y: auto; max-height: 600px">
                <asp:GridView ID="gvTests" CssClass="table table-hover" runat="server" AllowSorting="true" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" GridLines="None" OnRowDataBound="gvTests_RowDataBound" OnSorting="gvTests_Sorting" OnRowDeleting="gvTests_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-icon.png" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID"
                            SortExpression="ID" />
                        <asp:HyperLinkField DataTextField="Название теста" HeaderText="Название теста" DataNavigateUrlFormatString="TestDetails.aspx?ID={0}" DataNavigateUrlFields="ID" SortExpression="Название теста" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
