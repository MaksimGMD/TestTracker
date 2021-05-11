using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestTracker.Pages.Admin
{
    public partial class Сomments : System.Web.UI.Page
    {
        private string QR = "";
        private bool IsModified;
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrComment;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlTestFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsComments.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsComments.SelectCommand = qr;
            sdsComments.DataSourceMode = SqlDataSourceMode.DataReader;
            gvComments.DataSource = sdsComments;
            gvComments.DataBind();
        }
        //Заполнение списка должностей
        private void ddlTestFill()
        {
            sdsTests.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTests.SelectCommand = DBConnection.qrTests;
            sdsTests.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlTest.DataSource = sdsTests;
            ddlTest.DataTextField = "Название теста";
            ddlTest.DataValueField = "ID";
            ddlTest.DataBind();
        }
        //Очистка полей
        private void Cleaner()
        {
            SelectedMessage.Visible = false;
            DBConnection.IdRecord = 0;
            ddlTest.SelectedIndex = 0;
            tbCommnet.Text = string.Empty;
            IsModified = false;
        }
        //Добавление данных
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            DBConnection connection = new DBConnection();
            try
            {
                int UserId = connection.GetUserId(HttpContext.Current.User.Identity.Name.ToString());
                procedures.CommentInsert(tbCommnet.Text, DateTime.Now.ToString("dd.MM.yyyy"), UserId, Convert.ToInt32(ddlTest.SelectedValue.ToString()));
                    Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlTestFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Обвноление данных
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBConnection.IdRecord != 0)
                {
                    DataProcedures procedures = new DataProcedures();
                    procedures.CommentUpdate(DBConnection.IdRecord, tbCommnet.Text, DateTime.Now.ToString("dd.MM.yyyy"), Convert.ToInt32(ddlTest.SelectedValue.ToString()), IsModified);
                    Response.Redirect(Request.Url.AbsoluteUri);
                    gvFill(QR);
                    Cleaner();
                    ddlTestFill();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись запись :(')", true);
            }
        }

        protected void gvComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[4].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvComments, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvComments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvComments.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvComments.Rows[Index].Cells[1].Text.ToString());
                int TestId = Convert.ToInt32(gvComments.Rows[Index].Cells[6].Text.ToString());
                procedure.CommentDelete(DBConnection.IdRecord, TestId);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlTestFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }

        protected void gvComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvComments.Rows)
            {
                if (row.RowIndex == gvComments.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvComments.SelectedRow.RowIndex;
            GridViewRow rows = gvComments.SelectedRow;
            DBConnection.IdRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbCommnet.Text = rows.Cells[2].Text.ToString();
            ddlTest.SelectedValue = rows.Cells[6].Text.ToString();
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.IdRecord;
            btUpdate.Visible = true;
        }

        protected void gvComments_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[CommentId]";
                    break;
                case ("Комментарий"):
                    e.SortExpression = "[CommentContent]";
                    break;
                case ("Дата комментария"):
                    e.SortExpression = "[CommentDate]";
                    break;
                case ("Пользователь"):
                    e.SortExpression = "[Пользователь]";
                    break;
                case ("Тест"):
                    e.SortExpression = "[TestName]";
                    break;
            }
            sortGridView(gvComments, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }
        //Очистка поиска
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            gvFill(QR);
            ddlTestFill();
        }
        //Отменить выбор
        protected void btCanselSelected_Click(object sender, EventArgs e)
        {
            Cleaner();
            btUpdate.Visible = false;
            DBConnection.IdRecord = 0;
            SelectedMessage.Visible = false;
            gvFill(QR);
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvComments.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                    row.Cells[2].Text.Equals(tbSearch.Text) ||
                    row.Cells[3].Text.Equals(tbSearch.Text) ||
                    row.Cells[5].Text.Equals(tbSearch.Text) ||
                    row.Cells[7].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [CommentId] like '%" + tbSearch.Text + "%' or [CommentContent] like '%" + tbSearch.Text + "%' or [CommentDate] like '%" + tbSearch.Text + "%' or (CONCAT_WS(' ',[UserSurname],[UserName])) like '%" + tbSearch.Text + "%' or" +
                    "[TestName] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void tbCommnet_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }
    }
}