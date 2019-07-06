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
    public partial class frmMonshi : Form
    {
        public frmMonshi()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();


        private void frmMonshi_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMablagh.Text=="")
            {
                errorProvider1.SetError(txtMablagh,"مبلغ حقوق وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Monshi(NameMonshi,NameKh,Tel,Mablagh,Tarikh,Tozih) Values(@a,@b,@c,@d,@e,@f)";
                cmd.Parameters.AddWithValue("@a",txtName.Text);
                cmd.Parameters.AddWithValue("@b",txtNameKh.Text);
                cmd.Parameters.AddWithValue("@c",txtTel.Text);
                cmd.Parameters.AddWithValue("@d",txtMablagh.Text);
                cmd.Parameters.AddWithValue("@e",mskTarikh.Text);
                cmd.Parameters.AddWithValue("@f",txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات ثبت انجام شد");
                //****************************************
                txtId.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtTel.Text = "";
                txtMablagh.Text = "";
                mskTarikh.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                errorProvider1.SetError(txtId, "کد وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Monshi where Id="+  txtId.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات حذف انجام شد");

                //****************************************
                txtId.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtTel.Text = "";
                txtMablagh.Text = "";
                mskTarikh.Text = "";
                txtTozih.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                errorProvider1.SetError(txtId, "کد وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Update Monshi set NameMonshi='"+txtName.Text+"',NameKh='"+txtNameKh.Text+"',Tel='"+txtTel.Text+"',Mablagh='"+txtMablagh.Text+"',Tarikh='"+mskTarikh.Text+"',Tozih='"+txtTozih.Text+"' where Id="+ txtId.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات ویرایش انجام شد");

                //****************************************
                txtId.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtTel.Text = "";
                txtMablagh.Text = "";
                mskTarikh.Text = "";
                txtTozih.Text = "";
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtId.Text !="")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Monshi where Id=@S";
                cmd.Parameters.AddWithValue("@s",txtId.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtId.Text = dr["id"].ToString();
                    txtName.Text = dr["NameMonshi"].ToString();
                    txtNameKh.Text = dr["NameKh"].ToString();
                    txtTel.Text = dr["Tel"].ToString();
                    txtMablagh.Text = dr["Mablagh"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    txtTozih.Text = dr["Tozih"].ToString();

                }
                else
                {
                    txtId.Text = "";
                    MessageBox.Show("منشی با این کد وجود ندارد");
                }
                con.Close();
            }
        }
    }
}
