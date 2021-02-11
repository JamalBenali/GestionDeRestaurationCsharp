using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Restaurant
{
    public partial class LoginServeur : Form
    {
        SqlConnection cnx = new SqlConnection(@"Server=.\SQLEXPRESS;DataBase=RESTAURANT;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataReader rd;
        public LoginServeur()
        {
            InitializeComponent();
            label4.Hide();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }




        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void class11_Click(object sender, EventArgs e)
        {
            ServeurMain f3 = new ServeurMain();
            cmd = new SqlCommand("select login,password from Serveur where login='" + textBox1.Text + "' and password='" + textBox2.Text + "'", cnx);
            cnx.Open();
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                
                f3.Text = textBox1.Text;
                this.Hide();
                f3.ShowDialog();
            }
            else { label4.Text = "login ou mot de passe incorrecte"; label4.Show(); }
            cnx.Close();

        }

        private void class12_Click(object sender, EventArgs e)
        {
            Form1 F1 = new Form1();
            this.Hide();
            F1.ShowDialog();
            this.Close();
        }

        private void class11_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }

        private void class12_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
