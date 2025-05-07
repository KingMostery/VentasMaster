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
using System.Windows.Documents;
using MasterVentas.Model.ModelViews.Categorias;
namespace MasterVentas.Views
{
    public partial class CreateCategoriasControl : UserControl
    {

        private readonly CategoriasController _controller = new CategoriasController();

        public CreateCategoriasControl()
        {
            InitializeComponent();
        }

        private async void BtnCrearCategoria_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtCategoriaName.Text;
            string descripcion = TxtCategoriaDescripcion.Text;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre de la categoría no puede estar vacío.");
                return;
            }

            var categoriaController = new CategoriasController();
            var nuevaCategoria = new Categorias
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Activo = true
            };

            bool resultado = await Task.Run(() => categoriaController.InsertCategoria(nuevaCategoria));
            if (resultado)
            {
                MessageBox.Show("Categoría creada exitosamente.");
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Ya existe una categoría con ese nombre.");
            }


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtiene la ventana que contiene este UserControl y la cierra
            Window.GetWindow(this)?.Close();
        }
    }
}
