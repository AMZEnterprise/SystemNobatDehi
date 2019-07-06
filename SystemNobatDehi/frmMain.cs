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
    public partial class frmMain : Form
    {
        public frmMain()
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
            adp.SelectCommand.CommandText = "select * from Nobat";
            adp.Fill(ds,"Nobat");
            dgvNobat.DataSource = ds;
            dgvNobat.DataMember = "Nobat";

            dgvNobat.Columns[0].HeaderText = "ردیف";
            dgvNobat.Columns[1].HeaderText = "نوبت";
            dgvNobat.Columns[2].HeaderText = "تاریخ";
            dgvNobat.Columns[3].HeaderText = "نام ";
            dgvNobat.Columns[4].HeaderText = "نام خانوادگی";
            dgvNobat.Columns[5].HeaderText = "تلفن تماس";
            dgvNobat.Columns[6].HeaderText = "توضیحات";
            dgvNobat.Columns[1].Width = 50;
            dgvNobat.Columns[0].Width = 50;
            dgvNobat.Columns[6].Width = 150;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            display();

            lbluser.Text = clc_variable.stru;
            if (lbluser.Text=="کاربر عادی")
            {
                btnSetting.Enabled = false;
                buttonX1.Enabled = false;
            }

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            lblDate.Text = p.GetYear(DateTime.Now).ToString() + "/" + p.GetMonth(DateTime.Now).ToString("0#") + "/" + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    lblDay.Text="جمعه";
                    break;
                case DayOfWeek.Saturday:
                    lblDay.Text = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    lblDay.Text = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    lblDay.Text = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    lblDay.Text = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    lblDay.Text = "چهرشنبه";
                    break;
                case DayOfWeek.Thursday:
                    lblDay.Text = "پنج شنبه";
                    break;
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmKarbar().ShowDialog();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            new frmInfo().ShowDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            new frmMonshi().ShowDialog();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            new frmListMonshi().ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            new frmListPardakht().ShowDialog();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            new frmNobat().ShowDialog();
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            new frmListNobat().ShowDialog();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            new frmKhadamat().ShowDialog();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            new frmBemeh().ShowDialog();
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            new frmVizit().ShowDialog();
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            new frmListVizit().ShowDialog();
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            new frmParvandeh().ShowDialog();
        }

        private void buttonX12_Click(object sender, EventArgs e)
        {
            new frmListP().ShowDialog();
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            new frmAmalkard().ShowDialog();
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            new frmGavahi().ShowDialog();
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            new frmHazineh().ShowDialog();
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            new frmListHazineh().ShowDialog();
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

        private void Backup(string filename)
        {
            SqlConnection oconnection = null;
            try
            {
                string command = @"Backup DataBase [Matab] To Disk='" + filename + "'";
                this.Cursor = Cursors.WaitCursor;
                SqlCommand ocommand = null;
                oconnection = new SqlConnection("Data Source=.;Initial Catalog=Matab;Integrated Security=True");
                if (oconnection.State != ConnectionState.Open)
                    oconnection.Open();
                ocommand = new SqlCommand(command, oconnection);
                ocommand.ExecuteNonQuery();
                this.Cursor = Cursors.Default;
                MessageBox.Show("تهيه نسخه پشتيبان از اطلاعات با موفقيت انجام شد");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                oconnection.Close();
            }
        }


        private void Restore(string filename)
        {
            SqlConnection oconnection = null;
            try
            {
                string command = @"ALTER DATABASE [Matab] SET SINGLE_USER with ROLLBACK IMMEDIATE " + " USE master " + " RESTORE DATABASE [Matab] FROM DISK= N'" + filename + "'WITH RECOVERY, REPLACE";
                this.Cursor = Cursors.WaitCursor;
                SqlCommand ocommand = null;
                oconnection = new SqlConnection("Data Source=.;Initial Catalog=Matab;Integrated Security=True");
                if (oconnection.State != ConnectionState.Open)
                    oconnection.Open();
                ocommand = new SqlCommand(command, oconnection);
                ocommand.ExecuteNonQuery();
                this.Cursor = Cursors.Default;
                MessageBox.Show("بازيابي اطلاعات از  نسخه پشتيبان  با موفقيت انجام شد");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : ", ex.Message);
            }
            finally
            {
                oconnection.Close();
            }
            
        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
           
               SaveFileDialog SaveBackUp = new SaveFileDialog();
               string filename = string.Empty;
               SaveBackUp.OverwritePrompt = true;
               SaveBackUp.Filter = @"SQL Backup Files ALL Files (*.*) |*.*| (*.Bak)|*.Bak";
               SaveBackUp.DefaultExt = "Bak";
               SaveBackUp.FilterIndex = 1;
               SaveBackUp.FileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
               SaveBackUp.Title = "Backup SQL File";
               if (SaveBackUp.ShowDialog() == DialogResult.OK)
               {
                   filename = SaveBackUp.FileName;
                   Backup(filename);
               }

        }

        private void buttonX17_Click(object sender, EventArgs e)
        {
          
            string filename = string.Empty;
            OpenFileDialog OpenBackUp = new OpenFileDialog();
            OpenBackUp.Filter = @"SQL Backup Files ALL Files (*.*) |*.*| (*.Bak)|*.Bak";
            OpenBackUp.FilterIndex = 1;
            OpenBackUp.Filter = @"SQL Backup Files (*.*)|";

            OpenBackUp.FileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            if (OpenBackUp.ShowDialog() == DialogResult.OK)
            {
                filename = OpenBackUp.FileName;
                Restore(filename);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
