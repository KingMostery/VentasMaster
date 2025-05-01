using MasterVentas.Views;
using System.Windows;
using System.Windows.Controls;



namespace MasterVentas.Views
{
    public partial class ConfiguracionesControl : UserControl
    {
        public ConfiguracionesControl()
        {
            InitializeComponent();
        }


        private void CrearUsuario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Cargar el UserControl de creación de usuario
            var crearUsuarioControl = new CreateUserControl();

            // Opcional: Abrir en una ventana nueva
            var ventana = new Window
            {
                Title = "Nuevo Usuario",
                Content = crearUsuarioControl,
                Height = 400,
                Width = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            ventana.ShowDialog();
        }
      

        private void EditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí abrirías el formulario para editar usuario");
        }

        private void EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí abrirías el formulario para eliminar usuario");
        }

        private void CrearCategoria_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí abrirías el formulario para crear categoría");
        }
    }
}
