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
    public partial class ClienMain : Form
    {
        SqlConnection cnx = new SqlConnection(@"Server=.\SQLEXPRESS;DataBase=Restaurant;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader rd;
        DataSet ds = new DataSet();
        public ClienMain()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            da = new SqlDataAdapter("select numtablee from tablee where numtablee not in(select numtablee from reservation)", cnx);
            da.Fill(ds, "numtab");
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            for (int i = 0; i < ds.Tables["numtab"].Rows.Count; i++)
                comboBox1.Items.Add(ds.Tables["numtab"].Rows[i]["numtablee"].ToString());
            da = new SqlDataAdapter("select codePlat from plate", cnx);
            da.Fill(ds, "numplat");
            cb = new SqlCommandBuilder(da);
            for (int i = 0; i < ds.Tables["numplat"].Rows.Count; i++)
                comboBox2.Items.Add(ds.Tables["numplat"].Rows[i]["codePlat"].ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    String a;
                    cmd = new SqlCommand("select IDSer from serveur where actif='+'", cnx);
                    cnx.Open();
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    a = rd["IDSer"].ToString­();
                    rd.Close();


                    cmd = new SqlCommand("insert into reservation values (getdate()," + Convert.ToInt32(comb­oBox1.SelectedItem) + "," + Convert.ToInt32(a) + ")", cnx);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("select max(CodeRes) from reservation", cnx);
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    this.Text = rd[0].ToString();
                    rd.Close();
                    this.comboBox1.Refre­sh();
                    MessageBox.Show("votre table a bien été reservé :) !!", "ok!!", MessageBo­xButtons.OK, MessageB­oxIcon.Information);
                }
                catch { MessageBox.Show("ser­veur non desponible !! Attendez svp, et veuillez ressayer plus tard!!", "Attendez svp!!", MessageBoxBut­tons.OK, MessageBoxIc­on.Information); }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.M­essage);
            }
            finally
            {
                cnx.Close();
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("inserer_qteRes", cnx);
                cmd.CommandType = CommandType.StoredPr­ocedure;
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("a", SqlDbType.Int);
                param[0].Value = int.Parse(this.Text);
                param[1] = new SqlParameter("b", SqlDbType.Int);
                param[1].Value = Convert.ToInt32(comb­oBox2.SelectedItem);
                param[2] = new SqlParameter("c", SqlDbType.Int);
                param[2].Value = Convert.ToInt32(text­Box2.Text);
                cmd.Parameters.AddRa­nge(param);
                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
                MessageBox.Show("vous avez bien demandez un plat, demandez un autre si vous voulez !!", "message", MessageBoxButtons.OK­, MessageBoxIcon.Infor­mation);
            }
            catch { MessageBox.Show("veuillez entrer tous les champs necessaires!!", "Erreur", MessageBoxButtons.OK­, MessageBoxIcon.Error­); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(602, 301);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select p.codePlat,p.nomPlate,p.prixPlate,c.libellecat from plate p,categorie c where c.codecat=p.codecat", cnx);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(602, 301);
            cmd = new SqlCommand("table_disponible", cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            cnx.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da;
            da = new SqlDataAdapter("select * from T", cnx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd = new SqlCommand("Drop table T",cnx);
            cmd.ExecuteNonQuery();
            cnx.Close();
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
         try   
            {
                dataGridView1.Size = new Size(602, 301);
                cmd = new SqlCommand("Facture", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("CodR", SqlDbType.Int);
                param[0].Value = int.Parse(this.Text);
                cmd.Parameters.AddRa­nge(param);
                cnx.Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select * from Ta", cnx);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                cmd = new SqlCommand("Drop table Ta", cnx);
                cmd.ExecuteNonQuery();
                cnx.Close();
            }catch { MessageBox.Show("Il faut d'abord Reserver un plats","Error",MessageBoxButtons.OK,MessageBoxIcon.Information); }
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Size = new Size(602, 113);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select sum(Qte*PrixPlate) as prixTotal from QteReservee Q,Plate P where Q.codePlat=P.codePlat and Q.CodeRes='" + Convert.ToInt32(this.Text) + "'", cnx);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("votre reservation n'est pas encore prise !!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("annuler_res", cnx);
                cmd.CommandType = CommandType.StoredPr­ocedure;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("a", SqlDbType.Int);
                p[0].Value = Convert.ToInt32(this.Text.ToString());
                cmd.Parameters.AddRa­nge(p);
                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
                MessageBox.Show("Votre Reservation a été annulé !!", "Attention", MessageBoxButtons.OK­, MessageBoxIcon.Infor­mation);
            }
            catch
            {
                MessageBox.Show("Votre Reservation n'est pas encore prise !!", "Attention", MessageBoxButtons.OK­, MessageBoxIcon.Warni­ng);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form1 F1 = new Form1();
            this.Hide();
            F1.ShowDialog();
            this.Close();
        }
    }
}
