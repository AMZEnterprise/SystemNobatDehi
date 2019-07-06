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
    public partial class frmAmalkard : Form
    {
        public frmAmalkard()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmAmalkard_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

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
            //************************************
            //SqlCommand sqlcmd = new SqlCommand("select count(*) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            //con.Open();
            //lblTedad.Text = "" + Convert.ToString((int)sqlcmd.ExecuteScalar()) + "";
            //con.Close();
            ////***********************************
            //SqlCommand sqlcmd1 = new SqlCommand("select sum(MablaghBemeh) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            //con.Open();
            //lblBemeh.Text = "" + Convert.ToString((int)sqlcmd1.ExecuteScalar()) + "";
            //con.Close();
            ////***********************************
            //SqlCommand sqlcmd2 = new SqlCommand("select sum(MablaghKhadamat) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            //con.Open();
            //lblKhadamat.Text = "" + Convert.ToString((int)sqlcmd2.ExecuteScalar()) + "";
            //con.Close();
            ////***********************************
            //SqlCommand sqlcmd3 = new SqlCommand("select sum(Mablagh) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            //con.Open();
            //lblMablagh.Text = "" + Convert.ToString((int)sqlcmd3.ExecuteScalar()) + "";
            //con.Close();
            ////***********************************
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand("select count(*) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            con.Open();
            lblTedad.Text = "" + Convert.ToString((int)sqlcmd.ExecuteScalar()) + "";
            con.Close();
            
            SqlCommand sqlcmd1 = new SqlCommand("select sum(MablaghBemeh) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            con.Open();
            lblBemeh.Text = "" + Convert.ToString((int)sqlcmd1.ExecuteScalar()) + "";
            con.Close();
            
            SqlCommand sqlcmd2 = new SqlCommand("select sum(MablaghKhadamat) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            con.Open();
            lblKhadamat.Text = "" + Convert.ToString((int)sqlcmd2.ExecuteScalar()) + "";
            con.Close();

            SqlCommand sqlcmd3 = new SqlCommand("select sum(Mablagh) from Vizit where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'", con);
            con.Open();
            lblMablagh.Text = "" + Convert.ToString((int)sqlcmd3.ExecuteScalar()) + "";
            con.Close();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptAmalkard.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskTarikh1.Text;
            Report["strTarikh2"] = mskTarikh2.Text;
            Report["Tedad"] = lblTedad.Text;
            Report["Bemeh"] = lblBemeh.Text;
            Report["Khadamat"] = lblKhadamat.Text;
            Report["MablaghKol"] = lblMablagh.Text;

            Report["NameMatab"] = lblName.Text;
            Report["Tel"] = lblTel.Text;

            Report.ShowWithRibbonGUI();
        }
    }
}
