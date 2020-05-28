using System;
using System.Windows.Forms;

namespace HugoApp
{
    public partial class frmCambiarContra : Form
    {
        public frmCambiarContra()
        {
            InitializeComponent();
        }


        private void frmCambiarContra_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "password";
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = UsuarioDAO.getLista();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            bool actualIgual = comboBox1.SelectedValue.Equals(textBox1.Text);
            bool nuevaIgual = textBox2.Text.Equals(textBox3.Text);
            bool nuevaValida = textBox2.Text.Length > 0;

            if (actualIgual && nuevaIgual && nuevaValida)
            {
                try
                {
                    UsuarioDAO.actualizarContra(comboBox1.Text, textBox2.Text);
                    
                    MessageBox.Show("¡Contraseña actualizada exitosamente!", 
                        "HUGOAPP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show($"¡Contraseña no actualizada! Favor intente mas tarde.", 
                        "HUGOAPP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("¡¡Favor verifique que los datos sean correctos!", 
                "HUGOAPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}