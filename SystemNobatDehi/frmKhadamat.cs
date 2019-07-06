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
using Stimulsoft.Report;

namespace Matab
{
    public partial class frmKhadamat : Form
    {
        public frmKhadamat()
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
            adp.SelectCommand.CommandText = "select * from Khadamat";
            adp.Fill(ds,"Khadamat");
            dgvKhadamat.DataSource = ds;
            dgvKhadamat.DataMember = "Khadamat";
            dgvKhadamat.Columns[0].Width = 50;
            dgvKhadamat.Columns[0].HeaderText = "کد خدمات";
            dgvKhadamat.Columns[1].HeaderText = "نام خدمات";
            dgvKhadamat.Columns[2].HeaderText = "تعرفه خدمات";
            dgvKhadamat.Columns[3].HeaderText = "توضیحات";
        }
        private void frmKhadamat_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="" | txtMablagh.Text=="")
            {
                errorProvider1.SetError(txtName,"نام خدمات یا تعرفه وارد نشده است");
                txtName.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Khadamat(NameKhadamat,Mablagh,Tozih) Values (@a,@b,@c)";
                cmd.Parameters.AddWithValue("@a",txtName.Text);
                cmd.Parameters.AddWithValue("@b",txtMablagh.Text);
                cmd.Parameters.AddWithValue("@c", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("ثبت انجام شد");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvKhadamat.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Khadamat where Id=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف انجام شد");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtName, "کد وارد نشده است");
                txtCode.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText="Update Khadamat set NameKhadamat='"+txtName.Text+"',Mablagh='"+txtMablagh.Text+"',Tozih='"+txtTozih.Text+"' where Id="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("ویرایش انجام شد");
            }
        }

        private void dgvKhadamat_MouseUp(object sender, MouseEventArgs e)
        {
            txtCode.Text = dgvKhadamat[0, dgvKhadamat.CurrentRow.Index].Value.ToString();
            txtName.Text = dgvKhadamat[1, dgvKhadamat.CurrentRow.Index].Value.ToString();
            txtMablagh.Text = dgvKhadamat[2, dgvKhadamat.CurrentRow.Index].Value.ToString();
            txtTozih.Text = dgvKhadamat[3, dgvKhadamat.CurrentRow.Index].Value.ToString();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptKhadamat.mrt");
            Report.Compile();
            Report.ShowWithRibbonGUI();
        }
    }
}
