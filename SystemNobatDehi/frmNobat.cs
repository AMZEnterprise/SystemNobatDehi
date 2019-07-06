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
    public partial class frmNobat : Form
    {
        public frmNobat()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmNobat_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNobat.Text=="" | mskTarikh.Text=="")
            {
                errorProvider1.SetError(txtNobat,"شماره نوبت ویا تاریخ وارد نشده است"); 
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Nobat(Nobat,Tarikh,NameBemar,NameKh,Tel,Tozih) Values (@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a", txtNobat.Text);
                cmd.Parameters.AddWithValue("@b", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@c", txtName.Text);
                cmd.Parameters.AddWithValue("@d", txtNameKh.Text);
                cmd.Parameters.AddWithValue("@e", txtTel.Text);
                cmd.Parameters.AddWithValue("@f", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("رزرو وقت انجام شد");
                //************************************
                txtNobat.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtTel.Text = "";
                txtTozih.Text = "";

            }
     
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtNobat.Text == "")
            {
                errorProvider1.SetError(txtNobat, "شماره نوبت وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Nobat where Nobat=" + txtNobat.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("حذف وقت رزرو شده انجام شد");

                //************************************
                txtNobat.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtTel.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtNobat.Text == "")
            {
                errorProvider1.SetError(txtNobat, "شماره نوبت وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Update Nobat set Tarikh='"+mskTarikh.Text+"', NameBemar='"+txtName.Text+"',NameKh='"+txtNameKh.Text+"',Tel='"+txtTel.Text+"',Tozih='"+txtTozih.Text+"' where Nobat="+ txtNobat.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ویرایش وقت رزرو شده انجام شد");

                //************************************
                txtNobat.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtTel.Text = "";
                txtTozih.Text = "";
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtNobat.Text !="")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Nobat where Nobat=@S";
                cmd.Parameters.AddWithValue("@S",txtNobat.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNobat.Text = dr["Nobat"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    txtName.Text = dr["NameBemar"].ToString();
                    txtNameKh.Text = dr["NameKh"].ToString();
                    txtTel.Text = dr["Tel"].ToString();
                    txtTozih.Text = dr["Tozih"].ToString();

                }
                else
                {
                    txtNobat.Text = "";
                    txtNobat.Focus();
                    MessageBox.Show("نوبتی با این شماره وجود ندارد");
                }
                con.Close();
            }
        }
    }
}
