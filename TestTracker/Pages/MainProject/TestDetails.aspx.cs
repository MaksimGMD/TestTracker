using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace TestTracker.Pages.MainProject
{
    public partial class TestDetails : System.Web.UI.Page
    {
        string TestId;
        string ProjectId;
        string QRUsers = ""; //Для списка участников
        string QRdetails = ""; //Подробности о тесте
        string QRSteps = "";
        string QRComments = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TestId = Request.QueryString["ID"];
            QRUsers = DBConnection.qrUsersTest;
            QRdetails = DBConnection.qrTestDetails;
            QRSteps = DBConnection.qrTestSteps;
            QRComments = DBConnection.qrTestComment;
            if (!IsPostBack)
            {
                if (TestId != null)
                {
                    getProjectId();
                    GetStatusId();
                    TestNameFill();
                    rpUsersFill(QRUsers);
                    rpDetailsFill(QRdetails);
                    DescriptionFill();
                    ResultFill();
                    rpTestStepFill(QRSteps);
                    ddlStatusFill();
                    rpCommentFill(QRComments);
                    if(ddlStatus.SelectedValue == "4")
                    {
                        dvResultSection.Visible = false;
                    }
                    else
                    {
                        dvResultSection.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("Projects.aspx");
                }
            }
        }

        //Получает id проекта по TestId
        protected void getProjectId()
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select[IdProject] from[Test] where [TestId] = '" + TestId + "'";
            try
            {
                DBConnection.connection.Open();
                ProjectId = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                Response.Redirect("Projects.aspx");
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //Устанавливает название проекта
        protected void TestNameFill()
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [TestName] from [Test] where [TestId] = '" + TestId + "'";
            try
            {
                DBConnection.connection.Open();
                lblTestName.Text = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                Response.Redirect("Projects.aspx");
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //Заполнение данными списка участников
        private void rpUsersFill(string qr)
        {
            sdsUsers.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsUsers.SelectCommand = qr + "where [IdProject] = '" + ProjectId + "'";
            sdsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            rpUsers.DataSource = sdsUsers;
            rpUsers.DataBind();
        }
        //Заполнение данными списка участников
        private void rpDetailsFill(string qr)
        {
            sdsDetails.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsDetails.SelectCommand = qr + "where [TestId] = '" + TestId + "'";
            sdsDetails.DataSourceMode = SqlDataSourceMode.DataReader;
            rpDetails.DataSource = sdsDetails;
            rpDetails.DataBind();
        }
        //Заполнение поля с описанием
        private void DescriptionFill()
        {

            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [TestDescription] from [Test] where [TestId] = '" + TestId + "'";
            try
            {
                DBConnection.connection.Open();
                tbDescription.Text = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                tbDescription.Text = "";
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //Заполнение поля с результатом
        private void ResultFill()
        {

            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [TestResult] from [Test] where [TestId] = '" + TestId + "'";
            try
            {
                DBConnection.connection.Open();
                tbResult.Text = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                tbResult.Text = "";
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //Получает и устанавливает id статуса
        private void GetStatusId()
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [IdStatus] from [Test] where [TestId] = '" + TestId + "'";
            try
            {
                DBConnection.connection.Open();
                lblStatusID.Text = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                lblStatusID.Text = "";
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //Заполнение списка статусов
        private void ddlStatusFill()
        {
            sdsStatus.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsStatus.SelectCommand = DBConnection.qrStatus;
            sdsStatus.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlStatus.DataSource = sdsStatus;
            ddlStatus.DataTextField = "Статус";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = lblStatusID.Text.ToString();
        }
        //Заполнение списка этапов тестировани
        protected void rpTestStepFill(string qr)
        {
            sdsTestStep.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTestStep.SelectCommand = qr + " where IdTest = '" + TestId + "' order by StepNumber Asc";
            sdsTestStep.DataSourceMode = SqlDataSourceMode.DataReader;
            rpTestStep.DataSource = sdsTestStep;
            rpTestStep.DataBind();
        }
        //Заполнение комментарием
        protected void rpCommentFill(string qr)
        {
            sdsComments.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsComments.SelectCommand = qr + " where IdTest = '" + TestId + "'";
            sdsComments.DataSourceMode = SqlDataSourceMode.DataReader;
            rpComments.DataSource = sdsComments;
            rpComments.DataBind();
        }
        //Изменить описание
        protected void btEditDesc_Click(object sender, EventArgs e)
        {
            tbDescription.ReadOnly = false;
            btCancel.Visible = true;
            btSaveDesc.Visible = true;
            btEditDesc.Visible = false;
        }
        //Сохранить изменения в описании
        protected void btSaveDesc_Click(object sender, EventArgs e)
        {
            if (tbDescription.Text != "")
            {
                getProjectId();
                DataProcedures procedures = new DataProcedures();
                procedures.TestDescriptionUpdate(Convert.ToInt32(TestId), Convert.ToInt32(ProjectId), tbDescription.Text);
                DescriptionFill();
                tbDescription.ReadOnly = true;
                btCancel.Visible = false;
                btSaveDesc.Visible = false;
                btEditDesc.Visible = true;
                DescriptionFill();
                lblDecsError.Visible = false;
            }
            else
                lblDecsError.Visible = true;
        }
        //Отмена сохранения описания
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbDescription.ReadOnly = true;
            btCancel.Visible = false;
            btSaveDesc.Visible = false;
            btEditDesc.Visible = true;
            DescriptionFill();
            lblDecsError.Visible = false;
        }
        //Изменить этап тестирования
        protected void btEdit_Click(object sender, EventArgs e)
        {
            var btn = (HtmlButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            ((TextBox)item.FindControl("tbStep")).CssClass = "form-control";
            ((TextBox)item.FindControl("tbStep")).ReadOnly = false;
            ((HtmlButton)item.FindControl("btSaveStep")).Visible = true;
            ((HtmlButton)item.FindControl("btCancelStep")).Visible = true;
            ((HtmlButton)item.FindControl("btEdit")).Visible = false;

        }
        //Сохранить этап тестирования
        protected void btSaveStep_Click(object sender, EventArgs e)
        {
            var btn = (HtmlButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            var StepID = ((Label)item.FindControl("lbStepId")).Text;
            var Number = ((Label)item.FindControl("lblStepNumber")).Text;
            var Step = ((TextBox)item.FindControl("tbStep")).Text;
            DataProcedures procedures = new DataProcedures();
            if (((TextBox)item.FindControl("tbStep")).Text != "")
            {
                ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
                try
                {
                    procedures.StepUpdate(Convert.ToInt32(StepID), Number.ToString(), Convert.ToString(Step), Convert.ToInt32(TestId), true);
                    Response.Redirect(Request.Url.AbsoluteUri);
                    ((TextBox)item.FindControl("tbStep")).CssClass = "inactive-step";
                    ((TextBox)item.FindControl("tbStep")).ReadOnly = true;
                    ((HtmlButton)item.FindControl("btSaveStep")).Visible = false;
                    ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
                    ((HtmlButton)item.FindControl("btEdit")).Visible = true;
                    ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
                    ((Label)item.FindControl("lblStepError")).Visible = false;
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись :(')", true);
                }
            }
            else
            {
                ((Label)item.FindControl("lblStepError")).Visible = true;
            }
        }
        //Отмена изменения этапа
        protected void btCancelStep_Click(object sender, EventArgs e)
        {
            var btn = (HtmlButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            ((TextBox)item.FindControl("tbStep")).CssClass = "inactive-step";
            ((TextBox)item.FindControl("tbStep")).ReadOnly = true;
            ((HtmlButton)item.FindControl("btSaveStep")).Visible = false;
            ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
            ((HtmlButton)item.FindControl("btEdit")).Visible = true;
            ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
            rpTestStepFill(QRSteps);
            ((Label)item.FindControl("lblStepError")).Visible = false;
        }
        //Удалить этап тестирования
        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (HtmlButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                var ID = ((Label)item.FindControl("lbStepId")).Text;
                DataProcedures procedure = new DataProcedures();
                procedure.StepDelete(Convert.ToInt32(ID), Convert.ToInt32(TestId));
                Response.Redirect(Request.Url.AbsoluteUri);
                rpTestStepFill(QRSteps);
                tbAddStep.Text = string.Empty;
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }

        protected void rpTestStep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                (e.Item.FindControl("lblRowNumber") as Label).Text = (e.Item.ItemIndex + 1).ToString();
            }
        }
        //Добавление нового этапа 
        protected void btAddStep_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                procedures.StepUserInsert(tbAddStep.Text, Convert.ToInt32(TestId));
                Response.Redirect(Request.Url.AbsoluteUri);
                rpTestStepFill(QRSteps);
                tbAddStep.Text = string.Empty;
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Обновление статуса теста
        protected void btStatusUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataProcedures procedures = new DataProcedures();
                getProjectId();
                procedures.TestStatusUpdate(Convert.ToInt32(TestId), Convert.ToInt32(ProjectId), Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                rpDetailsFill(QRdetails);
                if(ddlStatus.SelectedValue != "4")
                {
                    dvResultSection.Visible = true;
                }
                else
                {
                    dvResultSection.Visible = false;
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись :(')", true);
            }
        }
        //Добавление комментария
        protected void btAddComment_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            DBConnection connection = new DBConnection();
            try
            {
                if (tbComment.Text != string.Empty)
                {
                    int UserId = connection.GetUserId(HttpContext.Current.User.Identity.Name.ToString());
                    procedures.CommentInsert(tbComment.Text, DateTime.Now.ToString("dd.MM.yyyy"), UserId, Convert.ToInt32(TestId));
                    Response.Redirect(Request.Url.AbsoluteUri);
                    rpCommentFill(QRComments);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Возвращение к тестам
        protected void btBack_Click(object sender, EventArgs e)
        {
            getProjectId();
            //Перейти на страницу Tests.aspx с ProjectId в зашифрованном виде
            Response.Redirect("Tests.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
        //Изменить результат
        protected void btEditResult_Click(object sender, EventArgs e)
        {
            tbResult.ReadOnly = false;
            btCancelResult.Visible = true;
            btSaveResult.Visible = true;
            btEditResult.Visible = false;

        }
        //Отменить изменение результата
        protected void btCancelResult_Click(object sender, EventArgs e)
        {

            tbResult.ReadOnly = true;
            btCancelResult.Visible = false;
            btSaveResult.Visible = false;
            btEditResult.Visible = true;
            ResultFill();
            lblResultError.Visible = false;
        }
        //Сохранить изменения результата
        protected void btSaveResult_Click(object sender, EventArgs e)
        {
            if (tbResult.Text != "")
            {
                getProjectId();
                DataProcedures procedures = new DataProcedures();
                procedures.TestResultUpdate(Convert.ToInt32(TestId), Convert.ToInt32(ProjectId), tbResult.Text);
                ResultFill();
                tbResult.ReadOnly = true;
                btCancelResult.Visible = false;
                btSaveResult.Visible = false;
                btEditResult.Visible = true;
                ResultFill();
                lblResultError.Visible = false;
            }
            else
                lblResultError.Visible = true;
        }
    }
}