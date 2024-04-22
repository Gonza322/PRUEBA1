using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PRUEBA1
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-D91HGVG;Initial Catalog=TEST;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    Regex regex = new Regex(@"^\d+$");

                    if (!regex.IsMatch(textBox2.Text))
                    {
                        throw new Exception("Solo se acepta IdOrder numérico");
                    }


                    connection.Open();
                    SqlCommand command = new SqlCommand("SearchOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", textBox2.Text);

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    sqlDataAdapter.Fill(dataSet);

                    panel1.Controls.Clear();
                    panel2.Controls.Clear();



                    if (dataSet.Tables.Count == 0)
                    {
                        throw new Exception("IdOrder Inexistente");
                    }

                    for (int i = 0; i < dataSet.Tables.Count; i++)
                    {
                        DataGridView dataGridView = new DataGridView();
                        dataGridView.DataSource = dataSet.Tables[i];
                        dataGridView.Dock = DockStyle.Fill;
                        if (i == 0)
                        {
                            panel1.Controls.Add(dataGridView);
                        }
                        else
                        {
                            panel2.Controls.Add(dataGridView);
                        }


                    }


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }
    }
}
