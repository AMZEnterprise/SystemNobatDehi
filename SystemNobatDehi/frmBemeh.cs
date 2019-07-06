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
using DevExpress.Export.Xl;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace Matab
{
    public partial class frmBemeh : Form
    {
        public frmBemeh()
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
            adp.SelectCommand.CommandText = "select * from Bemeh";
            adp.Fill(ds, "Bemeh");
            dgvBemeh.DataSource = ds;
            dgvBemeh.DataMember = "Bemeh";
            dgvBemeh.Columns[0].Width = 50;
            dgvBemeh.Columns[0].HeaderText = "کد بیمه";
            dgvBemeh.Columns[1].HeaderText = "نام بیمه";
            dgvBemeh.Columns[2].HeaderText = "تعرفه بیمه";
            dgvBemeh.Columns[3].HeaderText = "توضیحات";
        }

        private void frmBemeh_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="" | txtMablagh.Text=="")
            {
                errorProvider1.SetError(txtName,"نام بیمه وارد نشده است");
                errorProvider1.SetError(txtMablagh, "تعرفه بیمه وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Bemeh(NameBemeh,Mablagh,Tozih) Values(@a,@b,@c)";
                cmd.Parameters.AddWithValue("@a",txtName.Text);
                cmd.Parameters.AddWithValue("@b", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@c",txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("ثبت با موفقیت انجام شد");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtMablagh.Text == "")
            {
                errorProvider1.SetError(txtMablagh, "تعرفه بیمه وارد نشده است");
            }
            else
            {
                int x = Convert.ToInt32(dgvBemeh.SelectedCells[0].Value);
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Bemeh where Id=@N";
                cmd.Parameters.AddWithValue("@N",x);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("حذف با موفقیت انجام شد");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtMablagh.Text == "")
            {
                errorProvider1.SetError(txtMablagh, "تعرفه بیمه وارد نشده است");
            }
            else
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Update Bemeh set NameBemeh='"+txtName.Text+"',Mablagh='"+txtMablagh.Text+"',Tozih='"+txtTozih.Text+"' where Id="+ txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("ویرایش با موفقیت انجام شد");
            }
        }

        private void dgvBemeh_MouseUp(object sender, MouseEventArgs e)
        {
            txtCode.Text = dgvBemeh[0, dgvBemeh.CurrentRow.Index].Value.ToString() ;
            txtName.Text = dgvBemeh[1, dgvBemeh.CurrentRow.Index].Value.ToString();
            txtMablagh.Text = dgvBemeh[2, dgvBemeh.CurrentRow.Index].Value.ToString();
            txtTozih.Text = dgvBemeh[3, dgvBemeh.CurrentRow.Index].Value.ToString();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(con.ConnectionString);
            StiReport Report = new StiReport();
            //Report.Dictionary.Databases.Clear();
            //Report.Dictionary.Databases.Add(
            //    new Stimulsoft.Report.Dictionary.StiOleDbDatabase("NameInReport", con.ConnectionString));
            Report.Load("Report/rptBemeh.mrt");
            Report.Compile();
            Report.ShowWithRibbonGUI();


            //cmd = new SqlCommand("ReportBemeh",con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //con.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            //List<string> Id = new List<string>();
            //List<string> NameBemeh = new List<string>();
            //List<string> Mablagh = new List<string>();
            //List<string> Tozih = new List<string>();
            //while (reader.Read())
            //{
            //    Id.Add(reader["Id"].ToString());
            //    NameBemeh.Add(reader["NameBemeh"].ToString());
            //    Mablagh.Add(reader["Mablagh"].ToString());
            //    Tozih.Add(reader["Tozih"].ToString());
            //}

            //List<string> output = new List<string>();
            //for (int i = 0; i <= Id.Count; i++)
            //{
            //    output.Add(Id[i] + ";" + NameBemeh[i] + ";" + Mablagh[i] + ";" + Tozih[i]);
            //}

            //string data = string.Empty;
            //foreach (var item in output)
            //{
            //    data += item + "\n";
            //}

            //MessageBox.Show(data);

            //con.Close();

        }

    }
}
