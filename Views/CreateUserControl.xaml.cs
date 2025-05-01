using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterVentas.Controllers;
using System.Windows.Controls;
using System.Windows;
using MasterVentas.ModelViews.User;

namespace MasterVentas.Views
{
    public partial class CreateUserControl : UserControl
    {
        private readonly UsuarioController _controller = new UsuarioController();

        public CreateUserControl()
        {
            InitializeComponent();
        }

        private void BtnCrearUsuario_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Password;
            string rol = (CmbRol.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            var usuarioController = new UsuarioController();
            var nuevoUsuario = new Usuario
            {
                Username = username,
                Password = password,
                Rol = rol
            };

            usuarioController.InsertUsuario(nuevoUsuario);
            MessageBox.Show("Usuario creado exitosamente");
        }
    }
}
