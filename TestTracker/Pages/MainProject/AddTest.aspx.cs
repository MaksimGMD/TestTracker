using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TestTracker.Pages.MainProject
{
    public partial class AddTest : System.Web.UI.Page
    {
        string ProjectId;
        string QRSteps = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectId = Request.QueryString["ProjectID"];
            QRSteps = DBConnection.qrTestSteps;
            if (!IsPostBack)
            {
                if (ProjectId != null)
                {
                    rpTestStepFill(QRSteps);
                }
                else
                {
                    Response.Redirect("Projects.aspx");
                }
            }
            if (ProjectId != null)
            {
                if (DBConnection.TestAddValid == true)
                {
                    rpTestStepFill(QRSteps);
                }
                else
                {
                    Response.Redirect("Projects.aspx");
                }
            }
        }
        //Заполнение списка этапов тестировани
        protected void rpTestStepFill(string qr)
        {
            sdsTestStep.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTestStep.SelectCommand = qr + " where IdTest = '" + DBConnection.LastId + "' order by StepNumber Asc";
            sdsTestStep.DataSourceMode = SqlDataSourceMode.DataReader;
            rpTestStep.DataSource = sdsTestStep;
            rpTestStep.DataBind();
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
                    procedures.StepUpdate(Convert.ToInt32(StepID), Number.ToString(), Convert.ToString(Step), Convert.ToInt32(DBConnection.LastId), true);
                    ((TextBox)item.FindControl("tbStep")).CssClass = "inactive-step";
                    ((TextBox)item.FindControl("tbStep")).ReadOnly = true;
                    ((HtmlButton)item.FindControl("btSaveStep")).Visible = false;
                    ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
                    ((HtmlButton)item.FindControl("btEdit")).Visible = true;
                    ((HtmlButton)item.FindControl("btCancelStep")).Visible = false;
                    ((Label)item.FindControl("lblStepError")).Visible = false;
                    dvStepSection.Visible = true;
                    btToTest.Visible = true;
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
            dvStepSection.Visible = true;
            btToTest.Visible = true;
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
                procedure.StepDelete(Convert.ToInt32(ID), Convert.ToInt32(DBConnection.LastId));
                rpTestStepFill(QRSteps);
                tbAddStep.Text = string.Empty;
                dvStepSection.Visible = true;
                btToTest.Visible = true;
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
        //Добавить этап тестирования
        protected void btAddStep_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                if (tbAddStep.Text == "")
                {
                    lblError.Visible = true;
                }
                else
                {
                    lblError.Visible = false;
                    procedures.StepUserInsert(tbAddStep.Text, Convert.ToInt32(DBConnection.LastId));
                    rpTestStepFill(QRSteps);
                    tbAddStep.Text = string.Empty;
                    dvStepSection.Visible = true;
                    btToTest.Visible = true;
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
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
            dvStepSection.Visible = true;
            btToTest.Visible = true;

        }
        //Сохранить данные о тесте
        protected void btSave_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            DBConnection connection = new DBConnection();
            try
            {
                procedures.TestInsert(tbTestName.Text, tbDescription.Text, DateTime.Now.ToString("dd.MM.yyyy"), string.Empty, tbJira.Text, false, 4, Convert.ToInt32(ProjectId), String.Empty);
                connection.GetLastId(ProjectId);
                rpTestStepFill(QRSteps);
                dvStepSection.Visible = true;
                btToTest.Visible = true;
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }

        //Открыть созданный тест
        protected void btToTest_Click(object sender, EventArgs e)
        {
            DBConnection.TestAddValid = false;
            Response.Redirect("TestDetails.aspx?ID=" + DBConnection.LastId + "");
        }

        //Возвращение к тестам
        protected void btBack_Click(object sender, EventArgs e)
        {
            //Перейти на страницу Tests.aspx с ProjectId в зашифрованном виде
            DBConnection.LastId = "0";
            DBConnection.TestAddValid = false;
            Response.Redirect("Tests.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
    }
}