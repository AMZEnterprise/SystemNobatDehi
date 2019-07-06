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
    public partial class frmListVizit : Form
    {
        public frmListVizit()
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
            adp.SelectCommand.CommandText = "select * from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND  '" + mskTarikh2.Text + "'";
            adp.Fill(ds,"Vizit");
            dgvVizit.DataSource = ds;
            dgvVizit.DataMember = "Vizit";

            dgvVizit.Columns[0].HeaderText = "ردیف";
            dgvVizit.Columns[1].HeaderText = "شماره ویزیت";
            dgvVizit.Columns[2].HeaderText = "تاریخ ویزیت";
            dgvVizit.Columns[3].HeaderText = "نام بیمار";
            dgvVizit.Columns[4].HeaderText = "نام خانوادگی";
            dgvVizit.Columns[5].HeaderText = "نام بیمه";
            dgvVizit.Columns[6].HeaderText = "تعرفه بیمه";
            dgvVizit.Columns[7].HeaderText = "نام خدمات";
            dgvVizit.Columns[8].HeaderText = "تعرفه خدمات";
            dgvVizit.Columns[9].HeaderText = "مبلغ کل";
            dgvVizit.Columns[10].HeaderText = "دستور پزشک";
        }
        private void frmListVizit_Load(object sender, EventArgs e)
        {
            Display();

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
          
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

        }

        private void txtNameKh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Vizit where NameKh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameKh.Text + "%");
            adp.Fill(ds, "Vizit");
            dgvVizit.DataSource = ds;
            dgvVizit.DataMember = "Vizit";
        }

        private void mskTarikh1_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void mskTarikh2_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptDaramad.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["strTarikh2"] = mskTarikh2.Text;
            Report["Daramad"] = Daramad.Text;
            Report.ShowWithRibbonGUI();
        }

        private void BtnCalcSalary_Click(object sender, EventArgs e)
        {
            Display();
            int sum = 0;
            for (int i = 0; i < dgvVizit.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dgvVizit.Rows[i].Cells[9].Value);
                Daramad.Text = sum.ToString("###,###,###,###");
            }
        }
    }
}
