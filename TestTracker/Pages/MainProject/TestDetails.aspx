<%@ Page Title="Test tracker | Тест" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="TestDetails.aspx.cs" Inherits="TestTracker.Pages.MainProject.TestDetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource runat="server" ID="sdsUsers"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="sdsDetails"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="sdsTestStep"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsStatus" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsComments" runat="server"></asp:SqlDataSource>
    <div class="row mb-2 mt-2" style="padding-left: 35px; padding-right: 15px">
        <button runat="server" id="btBack" class="btn-back" onserverclick="btBack_Click" causesvalidation="false">Вернуться к тестам <i class="fas fa-reply"></i></button>
    </div>
    <div class="container-fluid">
        <div class="row mb-3" style="padding-right: 15px">
            <div class="col-lg-9" style="max-width: 800px; display: inherit">
                <button runat="server" id="btTestNameEdit" onserverclick="btTestNameEdit_Click" class="btn-step" title="Изменить" causesvalidation="false" display="Dynamic"><i class="fas fa-edit"></i></button>
                <button runat="server" id="btTestNameSave" onserverclick="btTestNameSave_Click" class="btn-step btn-step-save" title="Сохранить" visible="false" causesvalidation="false" display="Dynamic"><i class="fas fa-check-circle"></i></button>
                <asp:TextBox runat="server" ID="tbTestName" CssClass="test-name-disabled" MaxLength="150" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <asp:Label runat="server" ID="lblStatusID" Visible="false" display="Dynamic"></asp:Label>
            <div class="col-lg-9" style="padding-right: 30px">
                <div class="status-section" style="max-width: 600px">
                    <div class="form-group">
                        <label for="ddlStatus" class="section-title">Статус теста</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control"></asp:DropDownList>
                    </div>
                    <div class="row justify-content-end">
                        <asp:Button runat="server" ID="btStatusUpdate" CausesValidation="false" CssClass="btn btn-outline-success" Text="Обновить" OnClick="btStatusUpdate_Click" />
                    </div>
                </div>
                <div class="section-description" style="max-width: 600px">
                    <p class="section-title">Описание</p>
                    <div class="form-group" style="margin-bottom: 10px">
                        <asp:TextBox runat="server" ID="tbDescription" CssClass="form-control" placeholder="Описание" TextMode="MultiLine" MaxLength="200"
                            Style="max-height: 250px; min-height: 39px; background: #fff" Rows="3" ReadOnly="true"></asp:TextBox>
                        <br />
                        <asp:Label runat="server" ID="lblDecsError" Text="Введите описание теста" Visible="false" display="Dynamic" CssClass="Error"></asp:Label>
                    </div>
                    <div class="row justify-content-end">
                        <asp:Button runat="server" ID="btEditDesc" Text="Изменить" CssClass="btn btn-outline-success" OnClick="btEditDesc_Click" display="Dynamic" CausesValidation="false" />
                        <asp:Button runat="server" ID="btCancel" Text="Отменить" CssClass="btn btn btn-secondary" OnClick="btCancel_Click" display="Dynamic" Visible="false" CausesValidation="false" />
                        <asp:Button runat="server" ID="btSaveDesc" Text="Сохранить" CssClass="btn btn btn-success" OnClick="btSaveDesc_Click" Style="margin-left: 10px" display="Dynamic" Visible="false" CausesValidation="false" />
                    </div>
                    <div runat="server" id="dvResultSection" display="Dynamic" visible="false">
                        <p class="section-title">Результат</p>
                        <div class="form-group" style="margin-bottom: 10px">
                            <asp:TextBox runat="server" ID="tbResult" CssClass="form-control" placeholder="Результат теста" TextMode="MultiLine" MaxLength="500"
                                Style="max-height: 300px; min-height: 39px; background: #fff" Rows="3" ReadOnly="true"></asp:TextBox>
                            <br />
                            <asp:Label runat="server" ID="lblResultError" Text="Введите результат" Visible="false" display="Dynamic" CssClass="Error"></asp:Label>
                        </div>
                        <div class="row justify-content-end">
                            <asp:Button runat="server" ID="btEditResult" Text="Изменить" CssClass="btn btn-outline-success" display="Dynamic" CausesValidation="false" OnClick="btEditResult_Click" />
                            <asp:Button runat="server" ID="btCancelResult" Text="Отменить" CssClass="btn btn btn-secondary" display="Dynamic" Visible="false" CausesValidation="false" OnClick="btCancelResult_Click" />
                            <asp:Button runat="server" ID="btSaveResult" Text="Сохранить" CssClass="btn btn btn-success" Style="margin-left: 10px" display="Dynamic" Visible="false" CausesValidation="false" OnClick="btSaveResult_Click" />
                        </div>
                    </div>
                </div>
                <div class="section-tests">
                    <p class="section-title">Этапы тестирования</p>
                    <div class="add-section" style="margin-bottom: 20px">
                        <asp:TextBox runat="server" ID="tbAddStep" placeholder="Новый этап тестирования" MaxLength="200" Style="margin-bottom: 20px" Rows="3" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите этап тестирования" Display="Dynamic" ControlToValidate="tbAddStep"></asp:RequiredFieldValidator>
                        <div class="row justify-content-end">
                            <asp:Button runat="server" ID="btAddStep" Text="Добавить" ToolTip="Добавить новый этап тестирования" CssClass="btn btn-success" OnClick="btAddStep_Click" />
                        </div>
                    </div>
                    <asp:Repeater runat="server" ID="rpTestStep" OnItemDataBound="rpTestStep_ItemDataBound">
                        <ItemTemplate>
                            <div class="row test-step">
                                <asp:Label runat="server" ID="lbStepId" Text='<%#Eval("StepId") %>' Visible="false" display="Dynamic"></asp:Label>
                                <asp:Label runat="server" ID="lblStepNumber" Text='<%#Eval("StepNumber") %>' Visible="false" display="Dynamic"></asp:Label>
                                <div class="col-1" style="padding-left: unset">
                                    <div class="step-number">
                                        <asp:Label ID="lblRowNumber" runat="server" />
                                    </div>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox runat="server" ID="tbStep" CssClass="inactive-step" Text='<%#Eval("StepName") %>' MaxLength="180" ReadOnly="true"></asp:TextBox>
                                    <asp:Label runat="server" ID="lblStepError" Text="Заполните этап тестирования" CssClass="Error" Visible="false" display="Dynamic"></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right: unset; text-align: end">
                                    <button runat="server" id="btSaveStep" onserverclick="btSaveStep_Click" class="btn-step btn-step-save" title="Сохранить" causesvalidation="false" visible="false" display="Dynamic"><i class="fas fa-check-circle"></i></button>
                                    <button runat="server" id="btCancelStep" onserverclick="btCancelStep_Click" class="btn-step btn-step-cancel" title="Отмена" causesvalidation="false" visible="false" display="Dynamic"><i class="fas fa-times"></i></button>
                                    <button runat="server" id="btEdit" onserverclick="btEdit_Click" class="btn-step" title="Изменить" causesvalidation="false" display="Dynamic"><i class="fas fa-edit"></i></button>
                                    <button runat="server" id="btDelete" onserverclick="btDelete_Click" class="btn-step btn-step-del" style="margin-left: 10px" title="Удалить" causesvalidation="false"><i class="fas fa-trash-alt"></i></button>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="comment-section mt-4">
                    <p class="section-title">Комментарии или замечание</p>
                    <div class="addcomment-section mb-2" style="max-width: 600px">
                        <label for="exampleInputEmail1">Добавить комментарий или замечание</label>
                        <asp:TextBox class="form-control" runat="server" ID="tbComment" placeholder="Комментарий" Style="margin-bottom: 20px; max-height: 300px; min-height: 39px" MaxLength="500" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <div class="row justify-content-end">
                            <asp:Button runat="server" ID="btAddComment" Text="Добавить" CssClass="btn btn-outline-success mt-2" OnClick="btAddComment_Click" CausesValidation="false" />
                        </div>
                    </div>
                    <asp:Repeater runat="server" ID="rpComments">
                        <ItemTemplate>
                            <div class="d-flex justify-content-center row">
                                <div class="col-md-12">
                                    <div class="d-flex flex-column comment-section">
                                        <div class="bg-white p-2">
                                            <div class="d-flex flex-row user-info">
                                                <img class="rounded-circle" src="../../Content/img/hacker.png" width="40">
                                                <div class="d-flex flex-column justify-content-start ml-2"><span class="d-block font-weight-bold name"><%#Eval("User") %></span><span class="date text-black-50"><%#Eval("CommentDate") %></span></div>
                                            </div>
                                            <div class="mt-2">
                                                <p class="comment-text"><%#Eval("CommentContent") %></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="section-details">
                    <p class="section-title">Данные о тесте</p>
                    <asp:Repeater runat="server" ID="rpDetails">
                        <ItemTemplate>
                            <div class="row">
                                <p class="details-title">Статус:  <span class="details-value"><%#Eval("StatusName") %></span></p>
                            </div>
                            <div class="row">
                                <p class="details-title">Проект:  <span class="details-value"><%#Eval("ProjectName") %></span></p>
                            </div>
                            <div class="row">
                                <p class="details-title">Дата создания теста:  <span class="details-value"><%#Eval("TestDate") %></span></p>
                            </div>
                            <div class="row">
                                <p class="details-title">Номер в Jira:  <span class="details-value"><%#Eval("TestJiraNumber") %></span></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div>
                    <p class="section-title">Участники проекта</p>
                    <ul style="padding-left: unset">
                        <asp:Repeater runat="server" ID="rpUsers">
                            <ItemTemplate>
                                <li style="margin-bottom: 10px">
                                    <%#Eval("Пользователь") %>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
