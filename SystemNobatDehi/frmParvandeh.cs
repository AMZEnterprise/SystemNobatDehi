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
    public partial class frmParvandeh : Form
    {
        public frmParvandeh()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private void frmParvandeh_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text=="")
            {
                errorProvider1.SetError(txtCode,"شماره پرونده وارد نشده است");
                txtCode.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Parvandeh (CodeP,Tarikh,NameBemar,NameKh,TarikhTavalod,Jensiyat,Tarikh1,Tarikh2,BemariF,BemariG,NameBemeh,MablaghBemeh,Tozih)values (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m)";
                cmd.Parameters.AddWithValue("@a", txtCode.Text);
                cmd.Parameters.AddWithValue("@b", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@c", txtName.Text);
                cmd.Parameters.AddWithValue("@d", txtNameKh.Text);
                cmd.Parameters.AddWithValue("@e", mskTarikhTavalod.Text);
                cmd.Parameters.AddWithValue("@f", cmbj.Text);
                cmd.Parameters.AddWithValue("@g", mskTarikh1.Text);
                cmd.Parameters.AddWithValue("@h", mskTarikh2.Text);
                cmd.Parameters.AddWithValue("@i", txtGabli.Text);
                cmd.Parameters.AddWithValue("@j", txtFeli.Text);
                cmd.Parameters.AddWithValue("@k", txtNameBemeh.Text);
                cmd.Parameters.AddWithValue("@l", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@m", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ثبت پرونده انجام شد");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "شماره پرونده وارد نشده است");
                txtCode.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "delete from Parvandeh where CodeP="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(" حذف انجام شد");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                errorProvider1.SetError(txtCode, "شماره پرونده وارد نشده است");
                txtCode.Focus();
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Update Parvandeh set Tarikh='" + mskTarikh.Text + "',NameBemar='" + txtName.Text + "',NameKh='" + txtNameKh.Text + "',TarikhTavalod='" + mskTarikhTavalod.Text + "', Jensiyat='" + cmbj.Text + "',Tarikh1='" + mskTarikh1.Text + "',Tarikh2='" + mskTarikh2.Text + "',NameBemeh='" + txtNameBemeh.Text + "',MablaghBemeh='" + txtMablagh.Text + "',BemariG='" + txtGabli.Text + "',BemariF='" + txtFeli.Text + "', Tozih='" + txtTozih.Text + "' where CodeP=" + txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(" ویرایش انجام شد");
            }
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                SqlDataReader dr;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Parvandeh where CodeP=@S";
                cmd.Parameters.AddWithValue("@S", txtCode.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCode.Text = dr["CodeP"].ToString();
                    mskTarikh.Text = dr["Tarikh"].ToString();
                    txtName.Text = dr["NameBemar"].ToString();
                    txtNameKh.Text = dr["NameKh"].ToString();
                    mskTarikhTavalod.Text = dr["TarikhTavalod"].ToString();
                    cmbj.Text = dr["Jensiyat"].ToString();
                    mskTarikh1.Text = dr["Tarikh1"].ToString();
                    mskTarikh2.Text = dr["Tarikh2"].ToString();
                    txtGabli.Text = dr["BemariG"].ToString();
                    txtFeli.Text = dr["BemariF"].ToString();
                    txtNameBemeh.Text = dr["NameBemeh"].ToString();
                    txtMablagh.Text = dr["MablaghBemeh"].ToString();
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
                    txtMablagh.Text = dr["Mablagh"].ToString();
                }
                else
                {
                    txtCodeBemeh.Text = "";
                    MessageBox.Show("بیمه ای با این کد وجود ندارد");
                }
                con.Close();
            }
        }
    }
}
