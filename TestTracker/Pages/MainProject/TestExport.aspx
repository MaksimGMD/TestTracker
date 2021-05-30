<%@ Page Title="Test tracker | Экспорт отчёта" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="TestExport.aspx.cs" Inherits="TestTracker.Pages.MainProject.TestExport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource runat="server" ID="sdsTestsExport"></asp:SqlDataSource>
    <div class="row mb-2 mt-2" style="padding-left: 35px; padding-right: 15px">
            <button runat="server" id="btBack" class="btn-back" onserverclick="btBack_Click" causesvalidation="false">Вернуться к тестам <i class="fas fa-reply"></i></button>
        </div>
    <div class="container">
        <div class="row" style="padding-left: 15px; padding-right: 15px;">
            <p class="h4">Предварительный просмотр</p>
        </div>
        <div class="row project-title align-items-center" style="padding-left: 15px; padding-right: 15px;">
            <div class="col-lg-6 align-self-start mt-2 mb-2" style="text-align: start; padding: unset; margin-bottom: unset">
                <h5 style="margin-bottom: unset">
                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                    <small class="text-muted">
                        <asp:Label ID="lblProjectVersion" runat="server"></asp:Label></small>
                </h5>
            </div>
            <div class="col-lg-6 align-self-end mt-2 mb-2" style="text-align: end; padding: unset">
                <button type="button" class="btn btn-outline-success" title="Экспорт отчёта в Excel" causesvalidation="False" onserverclick="btExport_Click" id="btExport" runat="server" style="height: 50px; width: 200px"><i class="fas fa-file-download"></i>Скачать отчёт</button>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <label for="tbStart">Начало периода</label>
                    <asp:TextBox CssClass="form-control" ID="tbStart" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите дату начала периода" Display="Dynamic" ControlToValidate="tbStart"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="form-group">
                    <label for="tbEnd">Конец периода</label>
                    <asp:TextBox CssClass="form-control" ID="tbEnd" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите дату конца периода" Display="Dynamic" ControlToValidate="tbEnd"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row justify-content-end mb-3" style="padding-right: 15px; text-align: end">
            <button runat="server" id="btCancel" class="btn btn-secondary" onserverclick="btCancel_Click" title="Отменить фильтрацию" visible="false" display="Dynamic" causesvalidation="False">Отменить</button>
            <button runat="server" id="btFilter" class="btn btn-primary" onserverclick="btFilter_Click" title="Применить фильтр" style="margin-left: 20px">Применить</button>
        </div>
        <div class="row" style="padding-left: 15px; padding-right: 15px">
            <h6 style="color: #6c757d"><asp:Label runat="server" ID="lblTitleExample"></asp:Label>
            <small class="text-muted"><asp:Label runat="server" ID="lblVersionExample"></asp:Label></small>
            </h6>
        </div>
        <div style="overflow-x: auto; width: 100%; overflow-y: auto; max-height: 350px; padding-left: 15px; padding-right: 15px; margin-bottom: 10px">
            <asp:GridView ID="gvTestsExport" runat="server" CurrentSortDirection="ASC" AutoGenerateColumns="false" Style="vertical-align: middle">
                <Columns>
                    <asp:BoundField DataField="ID теста" HeaderText="ID теста" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="Название теста" HeaderText="Название теста" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Описание" HeaderText="Описание" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Сценарий тестирования" HeaderText="Сценарий тестирования" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Результат" HeaderText="Результат" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Статус" HeaderText="Статус" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Комментарий" HeaderText="Комментарий" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Номер задачи" HeaderText="Номер задачи" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Дата" HeaderText="Дата" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
