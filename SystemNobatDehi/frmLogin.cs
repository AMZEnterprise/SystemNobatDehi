using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Matab
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();

        
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (String.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "نام کاربری را وارد کنید");
            }
            else if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "لطفا رمز عبور را وارد کنید");
            }
            else if (cmbAccess.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbAccess, "لطفا سطح دسترسی را مشخص کنید");
            }
            else
            {
                string struser, Search;

                if (cmbAccess.SelectedItem == "مدیر")
                {
                    struser = "admin";
                    clc_variable.stru = "مدیر";
                }

                else
                {
                    clc_variable.stru = "کاربر عادی";
                    struser = "user";
                }
                Search = "Select Id from Karbar where Access='" + struser + "' and UserName='" + txtUserName.Text + "' And Password='" + txtPassword.Text + "' ";
                SqlDataAdapter da = new SqlDataAdapter(Search, con);
                da.Fill(ds, "Karbar");
                if (ds.Tables["karbar"].Rows.Count > 0)
                {
                    this.Hide();
                    new frmMain().ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("کاربری با این مشخصات وجود ندارد");
                }
                con.Close();
            }
        }
    }
}
