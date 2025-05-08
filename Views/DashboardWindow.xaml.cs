using MasterVentas.Views;
using System.Windows;



namespace MasterVentas.Views
{
    public partial class DashboardWindow : Window
    {
        private string _rol;

        public DashboardWindow(string usuario, string rol)
        {
            _rol = rol;

            InitializeComponent();

            LblUsuario.Text = $"{usuario} ({rol})";

            ConfigurarInterfazPorRol();
        }

        private void ConfigurarInterfazPorRol()
        {
            if (_rol != "Administrador")
            {
                BtnConfig.Visibility = Visibility.Collapsed;
            }
        }

        private void CerrarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            ModuleHost.Content = new ConfiguracionesControl();
        }
    }
}

