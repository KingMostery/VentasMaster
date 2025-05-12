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
using MasterVentas.Model;
using MasterVentas.Util;
using System.ComponentModel;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using MigraDoc.DocumentObjectModel.Tables;

namespace MasterVentas.Views
{
    public partial class CreateProductoControl : UserControl
    {

        private readonly ProductosController _controller = new ProductosController();

        public CreateProductoControl()
        {
            InitializeComponent();
            Loaded += LoadCategorias;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtiene la ventana que contiene este UserControl y la cierra
            Window.GetWindow(this)?.Close();
        }

        private async void LoadCategorias(object sender, RoutedEventArgs e)
        {
            
            var categoriasController = new CategoriasController();
            var categorias = categoriasController.GetCategorias();
            CmbCategoria.ItemsSource = categorias;
        }

        private async void BtnCrearProducto_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtNombre.Text;
            string descripcion = TxtDescripcion.Text;
            string precioTexto = TxtPrecio.Text;
            string categoriaId = CmbCategoria.SelectedValue?.ToString();


            if (!decimal.TryParse(precioTexto, out decimal precio))
            {
                MessageBox.Show("El precio ingresado no es válido.");
                return;
            }

            var nuevoProducto = new Producto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Categoria = categoriaId,
                Activo = true
            };

            bool creado = _controller.InsertarProducto(nuevoProducto);

            if (!creado)
            {
                MessageBox.Show("Ya existe un producto con ese nombre");
                return;
            }

            MessageBox.Show("Producto creado con éxito");
            ImgCodigoBarras.Source = CodeBarGenerador.CrearCodigo128(nuevoProducto.CodigoBarras);
        }


    }
}
