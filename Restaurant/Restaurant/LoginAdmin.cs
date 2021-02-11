using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Restaurant
{
    public partial class LoginAdmin : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=ZAKARIA-PC\SQLEXPRESS; DataBase=Restaurant; Integrated Security=true");
        SqlCommand cmd;
        SqlDataReader dr;
        public LoginAdmin()
        {
            InitializeComponent();
        }

        private void LoginAdmin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminMain AM = new AdminMain();
            cmd = new SqlCommand("Select username,password From logini where username = '" + usertxt.Text + "' and password = '" + passtxt.Text + "'", cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.Hide();
                AM.ShowDialog();
                this.Close();
            }
            else { label3.Text = "login ou mot de passe incorrecte"; label3.Show(); }
            cn.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 F1 = new Form1();
            this.Hide();
            F1.ShowDialog();
            this.Close();
        }

        private void usertxt_TextChanged(object sender, EventArgs e)
        {
            label3.Hide();
        }
    }
}
