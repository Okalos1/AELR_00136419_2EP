using System;
using System.Windows.Forms;

namespace HugoApp
{
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }

        private void poblarControles()
        {
            // Actualizar ComboBox
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "password";
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = UsuarioDAO.getLista();
        }

        private void frmInicioSesion_Load(object sender, EventArgs e)
        {
            poblarControles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                frmCambiarContra unaVentana = new frmCambiarContra();
                unaVentana.ShowDialog();
                poblarControles();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.Equals(textBox1.Text))
            {
                Usuario u = (Usuario) comboBox1.SelectedItem;
                MessageBox.Show("¡Bienvenido!", 
                    "HUGO APP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (u.userType.Equals(true))
                {
                    var ventana = new frmAdmin();
                    ventana.Show();
                    this.Hide();
                }
                else if(u.userType.Equals(false))
                {
                    var ventana = new frmUsuario(u);
                    ventana.Show();
                    this.Hide();
                }

            }
            else
                MessageBox.Show("¡Contraseña incorrecta!", "HUGO APP",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }
    }
}