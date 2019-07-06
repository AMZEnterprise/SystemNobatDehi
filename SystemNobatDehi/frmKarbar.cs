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
    public partial class frmKarbar : Form
    {
        public frmKarbar()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Karbar";
            adp.Fill(ds,"Karbar");
            dgvKarbar.DataSource = ds;
            dgvKarbar.DataMember = "Karbar";

            dgvKarbar.Columns[0].HeaderText = "ردیف";
            dgvKarbar.Columns[1].HeaderText = "نام کاربری";
            dgvKarbar.Columns[2].HeaderText = "کلمه عبور";
            dgvKarbar.Columns[3].HeaderText = "سطح دسترسی";
        }

        private void frmKarbar_Load(object sender, EventArgs e)
        {
            Display();
        }

        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Insert into Karbar(UserName,Password,Access) values (@a,@b,@c)";
            cmd.Parameters.AddWithValue("@a", txtUserName.Text);
            cmd.Parameters.AddWithValue("@b", txtPassword.Text);
            cmd.Parameters.AddWithValue("@c", cmbAccess.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("ثبت کاربر انجام شد");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvKarbar.SelectedCells[0].Value);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "delete from Karbar where Id=@N";
            cmd.Parameters.AddWithValue("@N", x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف کاربر انجام شد");
        }
    }
}
