<%@ Page Title="Test tracker | Отправка письма" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="SharePage.aspx.cs" Inherits="TestTracker.Pages.MainProject.SharePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource runat="server" ID="sdsTestsExport"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="sdsUsers"></asp:SqlDataSource>
    <div class="container">
        <div class="row">
            <div class="col">
                <div class="mail-form" style="margin-bottom: 30px">
                    <div class="row">
                        <div class="col">
                            <div class="alert alert-success" role="alert" id="alert" runat="server" visible="false" display="Dynamic">
                                Ваше сообщение успешно отправленно!
                            </div>
                        </div>
                    </div>
                    <div class="row project-title align-items-center" style="padding-left: 15px; padding-right: 15px;">
                        <div class="col-lg-6 align-self-start mt-2 mb-2" style="text-align: start; padding: unset; margin-bottom: unset">
                            <p class="h4">Письмо</p>
                        </div>
                        <div class="col-lg-6 align-self-end mt-2 mb-2" style="text-align: end; padding: unset">
                            <asp:Button runat="server" ID="Button1" Text="Отправить" CssClass="btn btn-success" OnClick="btSent_Click" CausesValidation="false" Style="height: 50px; width: 130px" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <div class="form-group">
                                <label for="exampleInputPassword1">Собщение</label>
                                <asp:TextBox CssClass="form-control" ID="tbMessage" placeholder="Введите сообщение" runat="server" TextMode="MultiLine" Style="max-height: 300px; min-height: 39px" MaxLength="500"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="padding-left: 15px; padding-right: 15px">
                        <label style="color: #212529; font-size: 1rem; font-weight: 400; text-align: left">Кому</label>
                        <br />
                        <asp:CheckBoxList ID="cbUsers" runat="server" CssClass="users-section" OnSelectedIndexChanged="cbUsers_SelectedIndexChanged">
                        </asp:CheckBoxList>
                        <br />
                        <asp:Label ID="lblError" runat="server" Text="Выберите получателя письма" Visible="false" Display="Dynamic" CssClass="Error"></asp:Label>
                    </div>
                    <hr />
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
                        <h6 style="color: #6c757d">
                            <asp:Label runat="server" ID="lblTitleExample"></asp:Label>
                            <small class="text-muted">
                                <asp:Label runat="server" ID="lblVersionExample"></asp:Label></small>
                        </h6>
                    </div>
                    <div style="overflow-x: auto; width: 100%; overflow-y: auto; max-height: 300px; padding-left: 15px; padding-right: 15px; margin-bottom: 10px; font-size: 10px">
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
            </div>
        </div>
    </div>
</asp:Content>
