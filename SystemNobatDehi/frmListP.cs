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
    public partial class frmListP : Form
    {
        public frmListP()
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
            adp.SelectCommand.CommandText = "select * from Parvandeh";
            adp.Fill(ds, "Parvandeh");
            dgvParvandeh.DataSource = ds;
            dgvParvandeh.DataMember = "Parvandeh";

            dgvParvandeh.Columns[0].HeaderText = "ردیف";
            dgvParvandeh.Columns[1].HeaderText = "شماره پرونده";
            dgvParvandeh.Columns[2].HeaderText = "تاریخ تشکیل پرونده";
            dgvParvandeh.Columns[3].HeaderText = "نام بیمار";
            dgvParvandeh.Columns[4].HeaderText = "نام خانوادگی";
            dgvParvandeh.Columns[5].HeaderText = "تاریخ تولد";
            dgvParvandeh.Columns[6].HeaderText = "جنسیت";
            dgvParvandeh.Columns[7].HeaderText = "تاریخ مراجعه فعلی";
            dgvParvandeh.Columns[8].HeaderText = "تاریخ مراجعه بعدی";
            dgvParvandeh.Columns[9].HeaderText = "بیماری فعلی";
            dgvParvandeh.Columns[10].HeaderText = "بیماری قبلی";

            dgvParvandeh.Columns[11].HeaderText = "نام بیمه";
            dgvParvandeh.Columns[12].HeaderText = "تعرفه بیمه";
            dgvParvandeh.Columns[13].HeaderText = "دستور پزشک";
        }

        private void frmListP_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();
        }

        private void txtNameKh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Parvandeh where NameKh like '%'+ @S +'%'";
            adp.SelectCommand.Parameters.AddWithValue("@S",txtNameKh.Text + "%");
            adp.Fill(ds, "Parvandeh");
            dgvParvandeh.DataSource = ds;
            dgvParvandeh.DataMember = "Parvandeh";
        }

        private void mskTarikh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Parvandeh where Tarikh2 like '%'+ @S +'%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", mskTarikh.Text + "%");
            adp.Fill(ds, "Parvandeh");
            dgvParvandeh.DataSource = ds;
            dgvParvandeh.DataMember = "Parvandeh";
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptParvandeh.mrt");
            Report.Compile();
            Report["strTarikh"] = mskTarikh.Text;
            Report.ShowWithRibbonGUI();
        }
    }
}
