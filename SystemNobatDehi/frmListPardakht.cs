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
    public partial class frmListPardakht : Form
    {
        public frmListPardakht()
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
            adp.SelectCommand.CommandText = "select * from Pardakht";
            adp.Fill(ds, "Pardakht");
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "Pardakht";

            dgvPardakht.Columns[0].HeaderText = "ردیف";
            dgvPardakht.Columns[1].HeaderText = "کد";
            dgvPardakht.Columns[2].HeaderText = "نام خانوادگی";
            dgvPardakht.Columns[3].HeaderText = "مبلغ حقوق";
            dgvPardakht.Columns[4].HeaderText = "تاریخ پرداخت";
            dgvPardakht.Columns[5].HeaderText = "توضیحات";

            dgvPardakht.Columns[5].Width = 150;
        }

        private void frmListPardakht_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            System.Globalization.PersianCalendar p1 = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p1.GetYear(DateTime.Now).ToString() + p1.GetMonth(DateTime.Now).ToString("0#") + p1.GetDayOfMonth(DateTime.Now).ToString("0#");

            System.Globalization.PersianCalendar p2 = new System.Globalization.PersianCalendar();
            mskTarikh2.Text = p2.GetYear(DateTime.Now).ToString() + p2.GetMonth(DateTime.Now).ToString("0#") + p2.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();
        }

        private void txtNameKh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Pardakht where NameKh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameKh.Text + "%");
            adp.Fill(ds, "Pardakht");
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "Pardakht";
        }

        private void mskTarikh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Pardakht where Tarikh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", mskTarikh.Text + "%");
            adp.Fill(ds, "Pardakht");
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "Pardakht";
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptPardakht.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["strTarikh2"] = mskTarikh2.Text;
            Report.ShowWithRibbonGUI();
        }
    }
}
