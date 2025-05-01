using MasterVentas.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;



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

            // Crear la ventana sin chrome del sistema
            var ventana = new Window
            {
                Content = crearUsuarioControl,
                Height = 990,
                Width = 800,

                // Aquí están las claves para esconder bordes y barra de título:
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false,

                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Window.GetWindow(this) // para que sea modal de la ventana actual
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
