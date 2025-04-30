using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MasterVentas.Controllers;
using MasterVentas.Views;

namespace MasterVentas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ingresar_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Aquí puedes hacer la validación, por ejemplo:
            if (username == "admin" && password == "123456")
            {
                MessageBox.Show("Ingreso exitoso");
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Aquí puedes poner lógica si necesitas reaccionar a cambios de texto
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string user = UsernameTextBox.Text.Trim();  // Obtener el usuario
            string pass = PasswordBox.Password.Trim(); // Obtener la contraseña

            var ctrl = new UsuarioController();
            var usuario = ctrl.ValidarUsuario(user, pass); // Validar en la base de datos

            if (usuario != null)
            {
                var dashboard = new DashboardWindow(user, usuario.Rol);
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
