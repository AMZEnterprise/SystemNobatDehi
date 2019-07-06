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
    public partial class frmListHazineh : Form
    {
        public frmListHazineh()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        void display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Hazineh where Tarikh Between '" + mskTarikh1.Text + "' AND  '" + mskTarikh2.Text + "'";
            adp.Fill(ds,"Hazineh");
            dgvHazineh.DataSource = ds;
            dgvHazineh.DataMember = "Hazineh";

            dgvHazineh.Columns[0].HeaderText = "کد هزینه";
            dgvHazineh.Columns[1].HeaderText = "نام هزینه";
            dgvHazineh.Columns[2].HeaderText = "مبلغ هزینه";
            dgvHazineh.Columns[3].HeaderText = "تاریخ هزینه";
            dgvHazineh.Columns[4].HeaderText = "نام شخص";
            dgvHazineh.Columns[5].HeaderText = "توضیحات";
            dgvHazineh.Columns[5].Width = 150;
        }

        private void frmListHazineh_Load(object sender, EventArgs e)
        {
            display();
            System.Globalization.PersianCalendar p1 = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p1.GetYear(DateTime.Now).ToString() + p1.GetMonth(DateTime.Now).ToString("0#") + p1.GetDayOfMonth(DateTime.Now).ToString("0#");

            System.Globalization.PersianCalendar p2 = new System.Globalization.PersianCalendar();
            mskTarikh2.Text = p2.GetYear(DateTime.Now).ToString() + p2.GetMonth(DateTime.Now).ToString("0#") + p2.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void mskTarikh1_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void mskTarikh2_TextChanged(object sender, EventArgs e)
        {
            display();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptHazineh.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["strTarikh2"] = mskTarikh2.Text;
            Report.ShowWithRibbonGUI();
        }
    }
}
