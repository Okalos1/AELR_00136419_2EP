using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Charts;
using CartesianChart = LiveCharts.WinForms.CartesianChart;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;

namespace HugoApp
{
    public partial class frmAdmin : Form
    {
        private CartesianChart graficocolumnas;
        public frmAdmin()
        {
            InitializeComponent();
            graficocolumnas = new CartesianChart();
            this.Controls.Add(graficocolumnas);
            graficocolumnas.Parent = tabControl1.TabPages[4];
        }

        private void configrafico()
        {
            // Posicion (ubicacion) y tamanio
            graficocolumnas.Top = 10;
            graficocolumnas.Left = 10;
            graficocolumnas.Width = graficocolumnas.Parent.Width - 20;
            graficocolumnas.Height = graficocolumnas.Parent.Height - 20;

            // Configuracion de series, ejes y leyendas
            graficocolumnas.Series = new SeriesCollection
            {
                new ColumnSeries() {Title = "Cantidad de pedidos por negocio", Values = new ChartValues<int>{}}
            };
            graficocolumnas.AxisX.Add(new Axis {Labels = new List<string>()});
            graficocolumnas.AxisX[0].Separator = new Separator(){Step = 1, IsEnabled = false};
            graficocolumnas.AxisX[0].LabelsRotation = 15;
            graficocolumnas.LegendLocation = LegendLocation.Top;

            // Poblado de datos
            foreach (NumPedidos unGrupo in ConsultaNumPedidos.getLista())
            {
                graficocolumnas.Series[0].Values.Add(unGrupo.CantPedidos);
                graficocolumnas.AxisX[0].Labels.Add(unGrupo.NombNegocio);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if (textBox2.Text.Equals("") ||
                textBox1.Text.Equals("") ||
                comboBox1.Text.Equals(""))
            {
                MessageBox.Show($"No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool encontrado = false;
                foreach (Usuario u in UsuarioDAO.getLista())
                {
                    if (u.username.Equals(textBox2.Text))
                    {
                        encontrado = true;
                    }
                }

                if (!encontrado)
                {
                    try
                    {
                        Conexion.realizarAccion($"INSERT INTO APPUSER(fullname, username, password, usertype) " +
                                                $"VALUES('{textBox1.Text}', '{textBox2.Text}', '{textBox2.Text}', " +
                                                $"{comboBox1.Text})");

                        MessageBox.Show($"Usuario agregado!",
                            "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        frmAdmin_Load(sender, e);

                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Ha ocurrido un problema");
                    }
                }
                else
                {
                    MessageBox.Show($"Ya existe ese nombre de usuario!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }


        private void frmAdmin_Load(object sender, EventArgs e)
        {
            configrafico();
            try
            {
                var dt = Conexion.realizarConsulta("SELECT * FROM APPUSER");

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Ha ocurrido un problema");  
            }
            
            try
            {
                var dt = Conexion.realizarConsulta("SELECT * FROM BUSINESS");

                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }

            try
            {
                var dt = Conexion.realizarConsulta("SELECT * FROM PRODUCT");

                dataGridView3.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
            
            try
            {
                var dt = Conexion.realizarConsulta("SELECT * FROM APPORDER");

                dataGridView4.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
            
            var usu = Conexion.realizarConsulta("SELECT username FROM APPUSER ");
            var usuCombo = new List<string>();

            foreach (DataRow dr in usu.Rows)
            {
                usuCombo.Add(dr[0].ToString());
            }
            
            comboBox2.DataSource = usuCombo;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            if (comboBox2.Text.Equals(""))
            {
                MessageBox.Show("No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    string nonQuery = $"delete from APPUSER "+
                                      $"where username='{comboBox2.Text}';";
                
                
                    Conexion.realizarAccion(nonQuery);
                
                
                
                    MessageBox.Show("Usuario eliminado!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                
                    frmAdmin_Load(sender,e);

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            
            
            if (textBox5.Text.Equals(""))
            {
                MessageBox.Show("No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    string nonQuery = $"delete from BUSINESS "+
                                      $"where idbusiness='{textBox5.Text}';";
                
                
                    Conexion.realizarAccion(nonQuery);
                
                
                
                    MessageBox.Show("Negocio eliminado!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                
                    frmAdmin_Load(sender,e);

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            
            if (textBox3.Text.Equals("") ||
                textBox4.Text.Equals(""))
            {
                MessageBox.Show($"No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                    try
                    {
                        Conexion.realizarAccion($"INSERT INTO BUSINESS(name, description) " +
                                                $"VALUES('{textBox4.Text}', '{textBox3.Text}')");

                        MessageBox.Show($"Negocio agregado!",
                            "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmAdmin_Load(sender, e);

                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Ha ocurrido un problema");
                    }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Equals("") ||
                textBox6.Text.Equals(""))
            {
                MessageBox.Show($"No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                try
                {
                    Conexion.realizarAccion($"INSERT INTO PRODUCT(idbusiness, name) " +
                                            $"VALUES('{textBox7.Text}', '{textBox6.Text}')");

                    MessageBox.Show($"Producto agregado!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmAdmin_Load(sender, e);

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Ha ocurrido un problema");
                }
            }   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox8.Text.Equals(""))
            {
                MessageBox.Show("No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    string nonQuery = $"delete from PRODUCT "+
                                      $"where idproduct='{textBox8.Text}';";
                
                
                    Conexion.realizarAccion(nonQuery);
                
                
                
                    MessageBox.Show("Producto eliminado!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                
                    frmAdmin_Load(sender,e);

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
                
            }        
        }
       


        private void frmAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}