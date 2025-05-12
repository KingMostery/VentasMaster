using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MasterVentas.Controllers;
using MasterVentas.Model;
using MasterVentas.Util;

namespace MasterVentas.Views
{
    public partial class ProductosControl : UserControl
    {
        private readonly ProductosController _productosController = new ProductosController();
        private List<Producto> todosLosProductos;
        private ICollectionView vistaFiltrada;

        public ProductosControl()
        {
            InitializeComponent();
            CargarProductos();
            ConfigurarFiltros();
        }

        private void CargarProductos()
        {
            // Cargar todos los productos
            todosLosProductos = _productosController.ObtenerTodos();

            // Configurar la vista filtrada
            vistaFiltrada = CollectionViewSource.GetDefaultView(todosLosProductos);
            vistaFiltrada.Filter = FiltrarProductos;

            // Asignar ItemsSource
            ProductosDataGrid.ItemsSource = vistaFiltrada;
        }

        private void ConfigurarFiltros()
        {
            // Suscribir a eventos de cambio de filtro
            NombreTextBox.TextChanged += (s, e) => vistaFiltrada?.Refresh();
            PrecioTextBox.TextChanged += (s, e) => vistaFiltrada?.Refresh();
            CategoriaComboBox.SelectionChanged += (s, e) => vistaFiltrada?.Refresh();
        }

        private bool FiltrarProductos(object item)
        {
            if (item is Producto producto)
            {
                var filtroNombre = NombreTextBox.Text?.Trim().ToLower() ?? string.Empty;
                var filtroPrecio = PrecioTextBox.Text?.Trim() ?? string.Empty;
                var filtroCategoria = (CategoriaComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? string.Empty;

                bool coincideNombre = string.IsNullOrWhiteSpace(filtroNombre) ||
                                     producto.Nombre.ToLower().Contains(filtroNombre);

                bool coincidePrecio = string.IsNullOrWhiteSpace(filtroPrecio) ||
                                     producto.Precio.ToString().Contains(filtroPrecio);

                bool coincideCategoria = string.IsNullOrWhiteSpace(filtroCategoria) ||
                                        producto.Categoria == filtroCategoria;

                return coincideNombre && coincidePrecio && coincideCategoria;
            }
            return false;
        }

        private void LimpiarFiltros_Click(object sender, RoutedEventArgs e)
        {
            NombreTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
            CategoriaComboBox.SelectedIndex = -1;
            vistaFiltrada?.Refresh();
        }

        private void CrearProducto_Click(object sender, RoutedEventArgs e)
        {
            var nuevoProducto = new Producto
            {
                Nombre = "Martillo 2",
                Descripcion = "Herramienta de golpeo 2",
                Precio = 15000m,
                Categoria = "Herramientas",
                Activo = true
            };

            bool creado = _productosController.InsertarProducto(nuevoProducto);
            if (!creado)
            {
                MessageBox.Show("Ya existe un producto con ese nombre");
                return;
            }

            MessageBox.Show("Producto creado con éxito");
            ImgCodigoBarras.Source = CodeBarGenerador.CrearCodigo128(nuevoProducto.CodigoBarras);

            // Recargar y reconfigurar filtros
            CargarProductos();
        }
    }
}