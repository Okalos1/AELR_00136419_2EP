using System;
using System.Windows.Forms;

namespace HugoApp
{
    public partial class frmUsuario : Form
    {
        private Usuario usuario;

        public frmUsuario(Usuario pusuario)
        {
            InitializeComponent();
            usuario = pusuario;
        }
        
        private void poblarControles()
        {
            // Actualizar ComboBox
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "idproduct";
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = ProductoDAO.getLista();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            {
                if (textBox6.Text.Equals(""))
                {
                    MessageBox.Show($"No puedes dejar campos vacios!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    try
                    {
                        Conexion.realizarAccion($"INSERT INTO ADDRESS(iduser, address) " +
                                                $"VALUES({usuario.iduser}, '{textBox6.Text}')");

                        MessageBox.Show($"Dirección agregada!",
                            "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmUsuario_Load(sender,e);


                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Ha ocurrido un problema");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    string nonQuery = $"delete from ADDRESS "+
                                      $"where idaddress='{textBox1.Text}';";
                
                
                    Conexion.realizarAccion(nonQuery);
                
                
                
                    MessageBox.Show("Dirección eliminada!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                    
                    frmUsuario_Load(sender,e);
                

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
                
            }
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = Conexion.realizarConsulta($"SELECT ad.idAddress, ad.address FROM ADDRESS ad WHERE idUser = {usuario.iduser}");

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Ha ocurrido un problema");  
            }
            
            try
            {
                var dt = Conexion.realizarConsulta($"SELECT ao.idOrder, ao.createDate, " +
                                                   $"pr.name, au.fullname, ad.address FROM APPORDER ao, " +
                                                   $"ADDRESS ad, PRODUCT pr, APPUSER au WHERE ao.idProduct" +
                                                   $" = pr.idProduct AND ao.idAddress = ad.idAddress AND " +
                                                   $"ad.idUser = au.idUser AND au.idUser = {usuario.iduser};");

                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Ha ocurrido un problema");  
            }
            
            poblarControles();
            
        }

        private void frmAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("") ||
                textBox4.Text.Equals(""))
            {
                MessageBox.Show($"No puedes dejar campos vacios!",
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                try
                {
                    Conexion.realizarAccion($"INSERT INTO APPORDER(createDate, idProduct, idAddress) VALUES('{DateTime.Today.ToString("d")}', {comboBox1.SelectedValue.ToString()}, {textBox4.Text});");

                    MessageBox.Show($"Pedido agregado!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmUsuario_Load(sender,e);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Ha ocurrido un problema");
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
                    string nonQuery = $"delete from APPORDER "+
                                      $"where idorder='{textBox5.Text}';";
                
                
                    Conexion.realizarAccion(nonQuery);
                
                
                
                    MessageBox.Show("Pedido eliminado!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    frmUsuario_Load(sender,e);

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error!",
                        "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
                
            }
        }
    }
}