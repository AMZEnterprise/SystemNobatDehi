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
    public partial class frmInfo : Form
    {
        public frmInfo()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Setting (NameMatab,NamePezeshk,Tel,Mobile,Address) Values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a",txtNameMatab.Text);
            cmd.Parameters.AddWithValue("@b",txtName.Text);
            cmd.Parameters.AddWithValue("@c",txtTel.Text);
            cmd.Parameters.AddWithValue("@d",txtMobile.Text);
            cmd.Parameters.AddWithValue("@e",txtAddress.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ثبت اطلاعات انجام شد");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "delete from Setting where Id ="+ txtId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("حذف اطلاعات انجام شد");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Update Setting set NameMatab='"+txtNameMatab.Text+"',NamePezeshk='"+txtName.Text+"',Tel='"+txtTel.Text+"',Mobile='"+txtMobile.Text+"',Address='"+txtAddress.Text+"' where Id="+ txtId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ویرایش اطلاعات انجام شد");
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtId.Text !="")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Setting where id=@S";
                cmd.Parameters.AddWithValue("@S",txtId.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtId.Text = dr["id"].ToString();
                    txtNameMatab.Text = dr["NameMatab"].ToString();
                    txtName.Text = dr["NamePezeshk"].ToString();
                    txtTel.Text = dr["Tel"].ToString();
                    txtMobile.Text = dr["Mobile"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                }
                else
                {
                    txtId.Text = "";

                }
                con.Close();
            }
        }
    }
}
