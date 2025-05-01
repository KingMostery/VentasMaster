using MasterVentas.Views;
using System.Windows;



namespace MasterVentas.Views
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow(string usuario, string rol)
            : this()  // Llama al constructor sin parámetros para InitializeComponent
        {
           
            LblUsuario.Text = $"{usuario} ({rol})";
        }
        private void CerrarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public DashboardWindow()
        {
            InitializeComponent();
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            // Al hacer clic, cargamos el UserControl de Configuraciones
            ModuleHost.Content = new ConfiguracionesControl();
        }
    }
}

