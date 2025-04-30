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

        public DashboardWindow()
        {
            InitializeComponent();
        }
    }
}

