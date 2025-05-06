using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MasterVentas.ModelViews.User;
using MasterVentas.Controllers;

namespace MasterVentas.Views
{
    public partial class EditUserControl : UserControl
    {
        private readonly UsuarioController _controller = new UsuarioController();
        private List<Usuario> _usuarios;

        public EditUserControl()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            // Cargar todos los usuarios desde MongoDB
            _usuarios = _controller.GetAllUsuarios();
            CmbUsuarios.ItemsSource = _usuarios;
            CmbUsuarios.DisplayMemberPath = "Username";
        }

        private void CmbUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbUsuarios.SelectedItem is Usuario seleccionado)
            {
               
                TxtUsernameFull.Text = seleccionado.Nombre;

                foreach (ComboBoxItem item in CmbRol.Items)
                {
                    if (item.Content.ToString().Equals(seleccionado.Rol, StringComparison.OrdinalIgnoreCase))
                    {
                        CmbRol.SelectedItem = item;
                        break;
                    }
                }
                ChkActivo.IsChecked = seleccionado.Activo;
            }
        }

        private void BtnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            var usuarioSeleccionado = CmbUsuarios.SelectedItem as Usuario;
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Selecciona un usuario primero.");
                return;
            }

            string nombre = TxtUsernameFull.Text;
            string rol = (CmbRol.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string nuevaContraseña = TxtPassword.Password;
            bool estaActivo = ChkActivo.IsChecked == true;


            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            usuarioSeleccionado.Nombre = nombre;
            usuarioSeleccionado.Rol = rol;
            usuarioSeleccionado.Password = nuevaContraseña;
            usuarioSeleccionado.Activo = estaActivo;

            bool actualizado = _controller.UpdateUsuario(usuarioSeleccionado);

            if (actualizado)
            {
                MessageBox.Show("Usuario actualizado exitosamente.");
                Window.GetWindow(this)?.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario.");
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
    }
}
