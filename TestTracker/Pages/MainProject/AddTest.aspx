<%@ Page Title="Test tracker | Добавление теста" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="AddTest.aspx.cs" Inherits="TestTracker.Pages.MainProject.AddTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <asp:SqlDataSource runat="server" ID="sdsTestStep"></asp:SqlDataSource>
    <div class="container">
        <div class="row mb-3 justify-content-around">
            <div class="col-8 p-0">
                <p class="h4">
                    Новый тест
                </p>
            </div>
            <div class="col-4 p-0" style="text-align:end">
                <button id="btToTest" runat="server" class="btn btn-outline-secondary" onserverclick="btToTest_Click" Display="Dynamic" visible="false" title="Открыть созданный тест" causesvalidation="false">Открыть тест <i class="fas fa-share"></i></button>
            </div>
        </div>
        <div>
            <div class="row mb-4">
                <label for="tbTestName">Название теста</label>
                <asp:TextBox class="form-control" ID="tbTestName" runat="server" aria-describedby="emailHelp" placeholder="Название теста"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Error" ErrorMessage="Введите название теста" Display="Dynamic" ControlToValidate="tbDescription"></asp:RequiredFieldValidator>
            </div>
            <div class="row mb-4">
                <label for="tbTestName">Описание теста</label>
                <asp:TextBox runat="server" ID="tbDescription" CssClass="form-control" placeholder="Описание" TextMode="MultiLine" MaxLength="200"
                    Style="max-height: 250px; min-height: 39px; background: #fff" Rows="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите описание теста" Display="Dynamic" ControlToValidate="tbDescription"></asp:RequiredFieldValidator>
            </div>
            <div class="row mb-4">
                <label for="tbJira">Номер задачи</label>
                <asp:TextBox runat="server" ID="tbJira" CssClass="form-control" placeholder="Номер задачи" TextMode="Number" min="1" max="9999" type="number" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите номер задачи" Display="Dynamic" ControlToValidate="tbJira"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row justify-content-end mt-4">
            <asp:Button runat="server" ID="btSave" Text="Сохранить" CssClass="btn btn-success" Style="height: 50px; width: 140px" OnClick="btSave_Click" />
        </div>
        <div class="section-tests" runat="server" id="dvStepSection" visible="false" display="Dynamic">
            <p class="section-title">Этапы тестирования</p>
            <div class="row">
                <asp:TextBox runat="server" ID="tbAddStep" placeholder="Новый этап тестирования" MaxLength="200" Style="margin-bottom: 20px" Rows="3" CssClass="form-control"></asp:TextBox>
                <asp:Label runat="server" ID="lblError" Text="Введите этап тестирования" CssClass="Error" Visible="false" display="Dynamic"></asp:Label>
            </div>
            <div class="row justify-content-end mb-4">
                <asp:Button runat="server" ID="btAddStep" Text="Добавить" ToolTip="Добавить новый этап тестирования" CssClass="btn btn-success" OnClick="btAddStep_Click" CausesValidation="false" />
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
    </div>
</asp:Content>
