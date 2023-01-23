﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Admin_panel
{
    public partial class machines : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=smarthome; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        DataTable dataTable = new DataTable();
        int currRowIndex;
        public machines()
        {

            InitializeComponent();
            button1.Enabled = false;
            
            fillcombo();
        }
        void fillcombo()
        {

            string query = "select * from zones ;";
            MySqlConnection conDataBase = new MySqlConnection(parametres);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string aid = myReader.GetString("id");
                    comboBox1.Items.Add(aid);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            comboBox1.Text = row.Cells[1].Value.ToString();
            comboBox2.Text = row.Cells[3].Value.ToString();

            button1.Enabled = true;
            

        }

        private void machines_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "INSERT INTO machine (id, nom,status1 , zone) VALUES (@id, @nom,@status1,@zone )";
                cmd.Parameters.AddWithValue("@id", "null");
                cmd.Parameters.AddWithValue("@nom", comboBox2.Text);
                cmd.Parameters.AddWithValue("@status1", 0);
                cmd.Parameters.AddWithValue("@zone", comboBox1.Text);
            
                cmd.ExecuteNonQuery();
                maconnexion.Close();


            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            dataGridView2.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select id, nom , status1,zone from machine";
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            int i;
            String[] myArray = new String[4];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                dataGridView2.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3]);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }
            else if (dialogClose == DialogResult.Cancel)
            {
                //do something else
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView2.CurrentCell.RowIndex;

            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer cette zone", "Supprimer une zone", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                dataGridView2.Rows.RemoveAt(rowIndex);
                button1.Enabled = false;
                
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "DELETE FROM machine WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                maconnexion.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zones a = new zones();
            this.Hide();
            a.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            this.Hide();
            a.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            appartements a = new appartements();
            this.Hide();
            a.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            user a = new user();
            this.Hide();
            a.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
