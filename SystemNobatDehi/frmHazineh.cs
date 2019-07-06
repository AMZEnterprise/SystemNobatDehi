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
    public partial class frmHazineh : Form
    {
        public frmHazineh()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmHazineh_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMablagh.Text=="")
            {
                errorProvider1.SetError(txtMablagh,"مبلغ وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Hazineh(NameH,Mablagh,Tarikh,NameMonshi,Tozih) values (@a,@b,@c,@d,@e)";
                cmd.Parameters.AddWithValue("@a", txtNameH.Text);
                cmd.Parameters.AddWithValue("@b", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@c", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@d", txtName.Text);
                cmd.Parameters.AddWithValue("@e", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ثبت انجام شد");
                //******************************************
                txtCode.Text = "";
                txtNameH.Text = "";
                txtMablagh.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "کد وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Hazineh where Id="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("حذف انجام شد");
                //******************************************
                txtCode.Text = "";
                txtNameH.Text = "";
                txtMablagh.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "کد وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "update Hazineh set NameH='" + txtNameH.Text + "',Mablagh='" + txtMablagh.Text + "',Tarikh='" + mskTarikh.Text + "',NameMonshi='" + txtName.Text + "',Tozih='" + txtTozih.Text + "' where Id="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ویرایش انجام شد");
                //******************************************
                txtCode.Text = "";
                txtNameH.Text = "";
                txtMablagh.Text = "";
                mskTarikh.Text = "";
                txtName.Text = "";
                txtTozih.Text = "";
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Hazineh where Id=@S";
                cmd.Parameters.AddWithValue("@S", txtCode.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCode.Text = dr["Id"].ToString();
                    txtNameH.Text = dr["NameH"].ToString();
                    txtMablagh.Text = dr["Mablagh"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    txtName.Text = dr["NameMonshi"].ToString();
                    txtTozih.Text = dr["Tozih"].ToString();

                }
                else
                {
                    txtCode.Text = "";
                    MessageBox.Show("هزینه ای با این کد وجود ندارد");
                }
                con.Close();
            }
        }
    }
}
