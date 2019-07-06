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
    public partial class frmListNobat : Form
    {
        public frmListNobat()
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
            adp.SelectCommand.CommandText = "select * from Nobat";
            adp.Fill(ds, "Nobat");
            dgvNobat.DataSource = ds;
            dgvNobat.DataMember = "Nobat";

            dgvNobat.Columns[0].HeaderText = "ردیف";
            dgvNobat.Columns[1].HeaderText = "شماره نوبت";
            dgvNobat.Columns[2].HeaderText = "تاریخ";
            dgvNobat.Columns[3].HeaderText = "نام ";
            dgvNobat.Columns[4].HeaderText = "نام خانوادگی";
            dgvNobat.Columns[5].HeaderText = "تلفن تماس";
            dgvNobat.Columns[6].HeaderText = "توضیحات";

            dgvNobat.Columns[6].Width = 150;
        }

        private void frmListNobat_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void txtNameKh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Nobat where NameKh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameKh.Text + "%");
            adp.Fill(ds, "Nobat");
            dgvNobat.DataSource = ds;
            dgvNobat.DataMember = "Nobat";
        }

        private void txtNobat_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Nobat where Nobat like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNobat.Text + "%");
            adp.Fill(ds, "Nobat");
            dgvNobat.DataSource = ds;
            dgvNobat.DataMember = "Nobat";
        }

        private void mskTarikh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Nobat where Tarikh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", mskTarikh.Text + "%");
            adp.Fill(ds, "Nobat");
            dgvNobat.DataSource = ds;
            dgvNobat.DataMember = "Nobat";
        }

       

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptNobat.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh.Text;
            Report["strTarikh2"] = mskTarikh.Text;
            Report.ShowWithRibbonGUI();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvNobat.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Nobat where Id=@N";
            cmd.Parameters.AddWithValue("@N", x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("حذف انجام شد");
            Display();
        }
    }
}
