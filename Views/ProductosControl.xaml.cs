using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using MasterVentas.Controllers;
using MasterVentas.Model;
using MasterVentas.Util;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using MigraDoc.DocumentObjectModel.Tables;


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

        private void ExportarProductos_Click(object sender, RoutedEventArgs e)
        {
            var productos = _productosController.ObtenerTodos().ToList();
            var documento = new Document();
            var seccion = documento.AddSection();

            Table tabla = null;
            int contador = 0;

            foreach (var p in productos)
            {
                // Crear una nueva tabla cada 10 productos
                if (contador % 10 == 0)
                {
                    if (contador > 0)
                    {
                        // Salto de página y nueva sección
                        seccion.AddPageBreak();
                        seccion = documento.AddSection();
                    }

                    tabla = seccion.AddTable();
                    tabla.Borders.Width = 0.75;

                    tabla.AddColumn("4cm"); // Nombre
                    tabla.AddColumn("3cm"); // Precio
                    tabla.AddColumn("3cm"); // Categoría
                    tabla.AddColumn("6cm"); // Código de barras

                    // Encabezados
                    var encabezado = tabla.AddRow();
                    encabezado.Shading.Color = Colors.LightGray;
                    encabezado.Cells[0].AddParagraph("Nombre");
                    encabezado.Cells[1].AddParagraph("Precio");
                    encabezado.Cells[2].AddParagraph("Categoría");
                    encabezado.Cells[3].AddParagraph("Código de Barras");
                }

                // Agregar fila de producto
                var fila = tabla.AddRow();
                fila.Cells[0].AddParagraph(p.Nombre);
                fila.Cells[1].AddParagraph(p.Precio.ToString("C"));
               

                // Generar código de barras como imagen en base64
                var barcodeBitmap = MasterVentas.Util.CodeBarGenerador.CrearCodigo128(p.Id.ToString());
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(barcodeBitmap));
                using (var ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    ms.Position = 0;
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    var image = fila.Cells[3].AddImage($"base64:{base64}");
                    image.Width = "5cm";
                    image.LockAspectRatio = true;
                }

                contador++;
            }

            // Renderizar y guardar
            var renderer = new PdfDocumentRenderer(true) { Document = documento };
            renderer.RenderDocument();

            var rutaPDF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "productos_exportados.pdf");
            renderer.PdfDocument.Save(rutaPDF);

            MessageBox.Show($"PDF generado con hasta 10 productos por página.\nUbicación:\n{rutaPDF}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        //private void ExportarProductos_Click(object sender, RoutedEventArgs e)
        //{
        //    var productos = _productosController.ObtenerTodos().ToList();
        //    var documento = new Document();
        //    var seccion = documento.AddSection();

        //    foreach (var p in productos)
        //    {
        //        // Crear tabla con 3 columnas
        //        var tabla = seccion.AddTable();
        //        tabla.Borders.Width = 0.75;

        //        // Definir columnas
        //        tabla.AddColumn("5cm"); // Nombre
        //        tabla.AddColumn("3cm"); // Precio
        //        tabla.AddColumn("4cm"); // Categoría

        //        // Agregar fila de encabezado
        //        var filaEncabezado = tabla.AddRow();
        //        filaEncabezado.Shading.Color = Colors.LightGray;
        //        filaEncabezado.Cells[0].AddParagraph("Nombre");
        //        filaEncabezado.Cells[1].AddParagraph("Precio");


        //        // Agregar fila de datos
        //        var filaDatos = tabla.AddRow();
        //        filaDatos.Cells[0].AddParagraph(p.Nombre);
        //        filaDatos.Cells[1].AddParagraph(p.Precio.ToString("C"));


        //        seccion.AddParagraph(); // espacio

        //        // Generar el código de barras
        //        var barcodeBitmap = MasterVentas.Util.CodeBarGenerador.CrearCodigo128(p.Id.ToString());

        //        // Convertir a base64
        //        var encoder = new PngBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(barcodeBitmap));
        //        using (var ms = new MemoryStream())
        //        {
        //            encoder.Save(ms);
        //            ms.Position = 0;
        //            var imageBase64 = Convert.ToBase64String(ms.ToArray());

        //            // Insertar imagen debajo de la tabla
        //            var image = seccion.AddImage($"base64:{imageBase64}");
        //            image.Width = "8cm";
        //            image.LockAspectRatio = true;
        //            image.Left = ShapePosition.Center;
        //        }

        //        // Salto de página por producto
        //        seccion.AddPageBreak();
        //    }

        //    // Renderizar y guardar
        //    var renderer = new PdfDocumentRenderer(true) { Document = documento };
        //    renderer.RenderDocument();

        //    var rutaPDF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "productos_exportados.pdf");
        //    renderer.PdfDocument.Save(rutaPDF);

        //    MessageBox.Show($"PDF generado con códigos de barras en:\n{rutaPDF}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        //}
        //private void ExportarProductos_Click(object sender, RoutedEventArgs e)
        //{
        //    var productos = _productosController.ObtenerTodos().ToList();
        //    var documento = new Document();
        //    var seccion = documento.AddSection();

        //    // Se crea el renderer de PDF
        //    var renderer = new PdfDocumentRenderer(true) { Document = documento };
        //    renderer.RenderDocument();
        //    var pdfDocument = renderer.PdfDocument;

        //    // Recorremos todos los productos
        //    foreach (var p in productos)
        //    {
        //        // Añadir detalles del producto
        //        seccion.AddParagraph($"Producto: {p.Nombre}", "Heading1");
        //        seccion.AddParagraph($"Precio: {p.Precio}");

        //        // Generar el código de barras
        //        var barcodeBitmap = MasterVentas.Util.CodeBarGenerador.CrearCodigo128(p.Id.ToString());

        //        // Convertir BitmapSource a System.Drawing.Image
        //        var encoder = new PngBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(barcodeBitmap));
        //        using (var ms = new MemoryStream())
        //        {
        //            encoder.Save(ms);
        //            ms.Position = 0;  // Asegurarte de que la posición del MemoryStream esté al principio

        //            // Crear la imagen a partir del MemoryStream
        //            var image = XImage.FromStream(ms);  // Pasa el MemoryStream directamente

        //            // Dibujar la imagen en el PDF
        //            var page = pdfDocument.AddPage();
        //            var gfx = XGraphics.FromPdfPage(page);
        //            gfx.DrawImage(image, 50, 50, 200, 60); // Ajusta la posición y tamaño según necesites
        //        }

        //        // Agregar espacio y página nueva por producto
        //        seccion.AddParagraph();
        //        seccion.AddPageBreak();
        //    }

        //    // Guardar el archivo PDF
        //    var rutaPDF = "productos_exportados.pdf";
        //    pdfDocument.Save(rutaPDF);

        //    // Mostrar mensaje al usuario
        //    MessageBox.Show("PDF generado con códigos de barras.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        //}




    }
}