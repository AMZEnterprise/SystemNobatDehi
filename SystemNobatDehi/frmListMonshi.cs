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
    public partial class frmListMonshi : Form
    {
        public frmListMonshi()
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
            adp.SelectCommand.CommandText = "select * from Monshi";
            adp.Fill(ds,"Monshi");
            dgvMonshi.DataSource = ds;
            dgvMonshi.DataMember = "Monshi";

            dgvMonshi.Columns[0].HeaderText = "کد";
            dgvMonshi.Columns[1].HeaderText = "نام";
            dgvMonshi.Columns[2].HeaderText = "نام خانوادگی";
            dgvMonshi.Columns[3].HeaderText = "تلفن تماس";
            dgvMonshi.Columns[4].HeaderText = "مبلغ حقوق";
            dgvMonshi.Columns[5].HeaderText = "تاریخ پرداخت";
            dgvMonshi.Columns[6].HeaderText = "توضیحات";

            dgvMonshi.Columns[6].Width = 150;
        }


        private void frmListMonshi_Load(object sender, EventArgs e)
        {

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString()  + p.GetMonth(DateTime.Now).ToString("0#")  + p.GetDayOfMonth(DateTime.Now).ToString("0#");

            Display();
        }

        private void txtNameKh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Monshi where NameKh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S",txtNameKh.Text + "%");
            adp.Fill(ds, "Monshi");
            dgvMonshi.DataSource = ds;
            dgvMonshi.DataMember = "Monshi";
        }

        private void mskTarikh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Monshi where Tarikh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", mskTarikh.Text + "%");
            adp.Fill(ds, "Monshi");
            dgvMonshi.DataSource = ds;
            dgvMonshi.DataMember = "Monshi";
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "insert into Pardakht (Id,NameKh,Mablagh,Tarikh,Tozih)Values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a", dgvMonshi.CurrentRow.Cells[0].Value);
            cmd.Parameters.AddWithValue("@b", dgvMonshi.CurrentRow.Cells[2].Value);
            cmd.Parameters.AddWithValue("@c", dgvMonshi.CurrentRow.Cells[4].Value);
            cmd.Parameters.AddWithValue("@d", dgvMonshi.CurrentRow.Cells[5].Value);
            cmd.Parameters.AddWithValue("@e", dgvMonshi.CurrentRow.Cells[6].Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("حقوق منشی مورد نظر پرداخت شد");
        }
    }
}
