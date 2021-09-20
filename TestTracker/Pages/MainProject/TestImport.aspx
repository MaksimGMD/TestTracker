<%@ Page Title="Test tracker | Импорт отчёта" Language="C#" MasterPageFile="~/Pages/MainProject/Main.Master" AutoEventWireup="true" CodeBehind="TestImport.aspx.cs" Inherits="TestTracker.Pages.MainProject.TestImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMainContent" runat="server">
    <div class="container py-3">
        <div class="card">
            <div class="card-header bg-primary text-uppercase text-white">
                <h5>Import Excel File</h5>
            </div>
            <div class="card-body">
                <button style="margin-bottom: 10px;" type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                    <i class="fa fa-plus-circle"></i>Import Excel  
                </button>
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Import Excel File</h4>
                                <button type="button" class="close" data-dismiss="modal">×</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Choose excel file</label>
                                            <div class="input-group">
                                                <div class="custom-file">
                                                    <asp:FileUpload ID="FileUpload1" CssClass="custom-file-input" runat="server" />
                                                    <label class="custom-file-label"></label>
                                                </div>
                                                <label id="filename"></label>
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-outline-primary" Text="Upload" OnClick="btnUpload_Click" />
                                                </div>
                                            </div>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
