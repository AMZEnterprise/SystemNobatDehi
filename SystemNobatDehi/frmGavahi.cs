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
    public partial class frmGavahi : Form
    {
        public frmGavahi()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmGavahi_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select NameMatab,Tel from Setting";
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            lblName.Text = dr["NameMatab"].ToString();
            lblTel.Text = dr["Tel"].ToString();

            con.Close();
        
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptGavahi.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["NameBemar"] = txtName.Text;
            Report["Bemari"] = txtBemari.Text;
            Report["Tedad"] = txtTedad.Text;
            Report["Makan"] = txtMakan.Text;

            Report["NameMatab"] = lblName.Text;
            Report["Tel"] = lblTel.Text;

            Report.ShowWithRibbonGUI();
        }
    }
}
