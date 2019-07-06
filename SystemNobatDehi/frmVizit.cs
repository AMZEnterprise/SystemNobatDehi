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
    public partial class frmVizit : Form
    {
        public frmVizit()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmVizit_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text=="" | txtNameKh.Text=="")
            {
                errorProvider1.SetError(txtCode,"شماره ویزیت وارد نشده است");
                txtCode.Focus();
                errorProvider1.SetError(txtNameKh, "نام خانوادگی بیمار وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Vizit (IdVizit,Tarikh,NameBemar,NameKh,NameBemeh,MablaghBemeh,NameKhadamat,MablaghKhadamat,Mablagh,Tozih) values (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)";
                cmd.Parameters.AddWithValue("@a", txtCode.Text);
                cmd.Parameters.AddWithValue("@b", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@c", txtName.Text);
                cmd.Parameters.AddWithValue("@d", txtNameKh.Text);
                cmd.Parameters.AddWithValue("@e", txtNameBemeh.Text);
                cmd.Parameters.AddWithValue("@f", txtMablaghBemeh.Text);
                cmd.Parameters.AddWithValue("@g", txtNameKhadamat.Text);
                cmd.Parameters.AddWithValue("@h", txtMablaghKhadamat.Text);
                cmd.Parameters.AddWithValue("@i", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@j", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ویزیت انجام شد");
                //*****************************************************
                txtCode.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtNameBemeh.Text = "";
                txtMablaghBemeh.Text = "";
                txtNameKhadamat.Text = "";
                txtMablaghKhadamat.Text = "";
                txtMablagh.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "" )
            {
                errorProvider1.SetError(txtCode, "شماره ویزیت وارد نشده است");
                txtCode.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "delete from Vizit where IdVizit="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("حذف انجام شد");
                //*****************************************************
                txtCode.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtNameBemeh.Text = "";
                txtMablaghBemeh.Text = "";
                txtNameKhadamat.Text = "";
                txtMablaghKhadamat.Text = "";
                txtMablagh.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "شماره ویزیت وارد نشده است");
                txtCode.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "update Vizit set Tarikh='" + mskTarikh.Text + "',NameBemar='" + txtName.Text + "',NameKh='" + txtNameKh.Text + "',NameBemeh='" + txtNameBemeh.Text + "',MablaghBemeh='" + txtMablaghBemeh.Text + "',NameKhadamat='" + txtNameKhadamat.Text + "',MablaghKhadamat='" + txtMablaghKhadamat.Text + "',Mablagh='" + txtMablagh.Text + "',Tozih='" + txtTozih.Text + "' where IdVizit="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ویرایش انجام شد");
                //*****************************************************
                txtCode.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtNameBemeh.Text = "";
                txtMablaghBemeh.Text = "";
                txtNameKhadamat.Text = "";
                txtMablaghKhadamat.Text = "";
                txtMablagh.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnVizit_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Vizit where IdVizit=@S";
                cmd.Parameters.AddWithValue("@S", txtCode.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCode.Text = dr["IdVizit"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    txtName.Text = dr["NameBemar"].ToString();
                    txtNameKh.Text = dr["NameKh"].ToString();
                    txtNameBemeh.Text = dr["NameBemeh"].ToString();
                    txtMablaghBemeh.Text = dr["MablaghBemeh"].ToString();
                    txtNameKhadamat.Text = dr["NameKhadamat"].ToString();
                    txtMablaghKhadamat.Text = dr["MablaghKhadamat"].ToString();
                    txtMablagh.Text = dr["Mablagh"].ToString();
                    txtTozih.Text = dr["Tozih"].ToString();

                }
                else
                {
                    txtCode.Text = "";
                    MessageBox.Show("ویزیتی با این کد وجود ندارد");
                }
                con.Close();
            }
        }

        private void btnBemeh_Click(object sender, EventArgs e)
        {
            if (txtCodeBemeh.Text != "")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Bemeh where Id=@S";
                cmd.Parameters.AddWithValue("@S", txtCodeBemeh.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNameBemeh.Text = dr["NameBemeh"].ToString();
                    txtMablaghBemeh.Text = dr["Mablagh"].ToString();
                }
                else
                {
                    txtCodeBemeh.Text = "";
                    MessageBox.Show("بیمه ای با این کد وجود ندارد");
                }
                con.Close();
            }
        }

        private void btnKhadamat_Click(object sender, EventArgs e)
        {
            if (txtCodeKhadamat.Text != "")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Khadamat where Id=@S";
                cmd.Parameters.AddWithValue("@S", txtCodeKhadamat.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNameKhadamat.Text = dr["NameKhadamat"].ToString();
                    txtMablaghKhadamat.Text = dr["Mablagh"].ToString();
                }
                else
                {
                    txtCodeKhadamat.Text = "";
                    MessageBox.Show("خدماتی  با این کد وجود ندارد");
                }
                con.Close();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int a, b, sum=0;

            a = Convert.ToInt32(txtMablaghBemeh.Text);
            b = Convert.ToInt32(txtMablaghKhadamat.Text);
            sum = a + b;
            txtMablagh.Text = sum.ToString();
        }
    }
}
