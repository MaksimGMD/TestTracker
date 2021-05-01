using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption_Library;

namespace TestTracker.Pages.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrUsers;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlRoleFill();
            }
        }
        //Заполнение таблицы данными
        private void gvFill(string qr)
        {
            sdsUsers.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsUsers.SelectCommand = qr;
            sdsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            gvUsers.DataSource = sdsUsers;
            gvUsers.DataBind();
        }
        //Вывод списка ролей
        private void ddlRoleFill()
        {
            sdsRole.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsRole.SelectCommand = DBConnection.qrRole;
            sdsRole.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlRole.DataSource = sdsRole;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataBind();
        }
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Сортировка данных
        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[UserId]";
                    break;
                case ("Фамилия"):
                    e.SortExpression = "[UserSurname]";
                    break;
                case ("Имя"):
                    e.SortExpression = "[UserName]";
                    break;
                case ("Отчество"):
                    e.SortExpression = "[UserMiddleName]";
                    break;
                case ("Логин"):
                    e.SortExpression = "[UserLogin]";
                    break;
                case ("Почта"):
                    e.SortExpression = "[UserEmail]";
                    break;
                case ("Роль"):
                    e.SortExpression = "[RoleName]";
                    break;
            }
            sortGridView(gvUsers, e, out sortDirection, out strField);
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
        //Удаление записи
        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvUsers.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvUsers.Rows[Index].Cells[1].Text.ToString());
                procedure.UserDelete(DBConnection.IdRecord);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlRoleFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }
        //Выбор записи
        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvUsers.Rows)
            {
                if (row.RowIndex == gvUsers.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvUsers.SelectedRow.RowIndex;
            GridViewRow rows = gvUsers.SelectedRow;
            DBConnection.IdRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbSurname.Text = rows.Cells[2].Text.ToString();
            tbName.Text = rows.Cells[3].Text.ToString();
            tbMiddleName.Text = rows.Cells[4].Text.ToString();
            tbLogin.Text = rows.Cells[5].Text.ToString();
            tbMail.Text = rows.Cells[7].Text.ToString();
            ddlRole.SelectedValue = rows.Cells[8].Text.ToString();
            SelectedMessage.Visible = true;
            cbPasswordChange.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.IdRecord;
            btUpdate.Visible = true;

        }
        //Очитска полей
        protected void Cleaner()
        {
            SelectedMessage.Visible = false;
            DBConnection.IdRecord = 0;
            ddlRole.SelectedIndex = 0;
            tbMail.Text = string.Empty;
            tbLogin.Text = string.Empty;
            tbMiddleName.Text = string.Empty;
            tbName.Text = string.Empty;
            tbPassword.Text = string.Empty;
            tbSurname.Text = string.Empty;
            cbPasswordChange.Visible = false;
            lblLoginCheck.Visible = false;
            lblMailCheck.Visible = false;
        }
        //Добавить запись
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            DBConnection connection = new DBConnection();
            try
            {
                if (connection.LoginCheck(tbLogin.Text) > 0)
                {
                    lblLoginCheck.Visible = true;
                }
                else
                {
                    if (connection.MailCheck(tbMail.Text) > 0)
                    {
                        lblMailCheck.Visible = true;
                    }
                    else
                    {
                        procedures.UserInsert(tbName.Text, tbMiddleName.Text, tbSurname.Text, tbLogin.Text, tbPassword.Text, tbMail.Text, 
                            Convert.ToInt32(ddlRole.SelectedValue.ToString()));
                        gvFill(QR);
                        ddlRoleFill();
                        Cleaner();
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Обновить запись
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow rows = gvUsers.SelectedRow;
            DataProcedures procedure = new DataProcedures();
            switch (cbPasswordChange.Checked)
            {
                case (true):
                    RequiredFieldValidator7.IsValid = true;
                    RegularExpressionValidator2.IsValid = true;
                    procedure.UserUpdate(DBConnection.IdRecord, tbName.Text, tbMiddleName.Text, tbSurname.Text, tbLogin.Text, tbMail.Text, 
                        Convert.ToInt32(ddlRole.SelectedValue.ToString()));
                    procedure.UserPasswordUpdate(DBConnection.IdRecord, tbPassword.Text);
                    break;
                case (false):
                    procedure.UserUpdate(DBConnection.IdRecord, tbName.Text, tbMiddleName.Text, tbSurname.Text, tbLogin.Text, tbMail.Text,
                        Convert.ToInt32(ddlRole.SelectedValue.ToString()));
                    break;
            }
            Cleaner();
            gvFill(QR);
            btUpdate.Visible = false;
            SelectedMessage.Visible = false;
            cbPasswordChange.Visible = false;
            DBConnection.IdRecord = 0;
        }
        //Отмена поиска и фльтрации
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            gvFill(QR);
            ddlRoleFill();
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [UserId] like '%" + tbSearch.Text + "%' or [UserSurname] like '%" + tbSearch.Text + "%' or [UserName] like '%" + tbSearch.Text + "%' " +
                    "or [UserMiddleName] like '%" + tbSearch.Text + "%' or [UserLogin] like '%" + tbSearch.Text + "%' or [UserEmail] like '%" + tbSearch.Text + "%' " +
                    "or [RoleName] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvUsers.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                    row.Cells[2].Text.Equals(tbSearch.Text) ||
                    row.Cells[3].Text.Equals(tbSearch.Text) ||
                    row.Cells[4].Text.Equals(tbSearch.Text) ||
                    row.Cells[5].Text.Equals(tbSearch.Text) ||
                    row.Cells[7].Text.Equals(tbSearch.Text) ||
                    row.Cells[9].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
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
    }
}