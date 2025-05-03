using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterVentas.Controllers;
using System.Windows.Controls;
using System.Windows;
using MasterVentas.ModelViews.User;
using System.Windows.Media;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace MasterVentas.Views
{
    public partial class CreateUserControl : UserControl
    {
        private readonly UsuarioController _controller = new UsuarioController();

        public CreateUserControl()
        {
            InitializeComponent();
        }

        private async void BtnCrearUsuario_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text;
            string usernamefull = TxtUsernameFull.Text;
            string password = TxtPassword.Password;
            string rol = (CmbRol.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(username) || username.Contains(" "))
            {
                MessageBox.Show("El nombre de usuario no puede contener espacios.");
                return;
            }

            var usernamePattern = @"^[a-zA-Z0-9.-]+$";
            if (!Regex.IsMatch(username, usernamePattern))
            {
                MessageBox.Show("El nombre de usuario solo puede contener letras, números, puntos y guiones.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            var usuarioController = new UsuarioController();
            var nuevoUsuario = new Usuario
            {
                Username = username,
                Password = password,
                Nombre = usernamefull,
                Rol = rol,
                Activo = true
            };

            // Intenta insertar
            bool creado = usuarioController.InsertUsuario(nuevoUsuario);

            if (!creado)
            {
                MessageBox.Show("Ya existe un usuario con ese nombre.");
                return;
            }

            MessageBox.Show("Usuario creado exitosamente");

            await Task.Delay(TimeSpan.FromSeconds(1));
            Window.GetWindow(this)?.Close();
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtiene la ventana que contiene este UserControl y la cierra
            Window.GetWindow(this)?.Close();
        }

       
    }
}
