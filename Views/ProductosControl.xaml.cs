using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterVentas.Controllers;
using MasterVentas.Model;
using System.Windows;
using MasterVentas.Util;

namespace MasterVentas.Views
{
    public partial class ProductosControl
    {

        public ProductosControl()
        {
            InitializeComponent();
        }


        //private void CrearProducto_Click3(object sender, RoutedEventArgs e)
        //{
            
        //    var productosController = new ProductosController();
        //    // Crear un producto con valores quemados por ahora
        //    var nuevoProducto = new Producto
        //    {
        //        Nombre = "Martillo",
        //        Descripcion = "Herramienta de golpeo",
        //        Precio = 15000,
        //        CategoriaId = "12ds213d213",
        //        Activo = true
        //    };


        //    bool creado = productosController.InsertarProducto(nuevoProducto);
            

        //    if (!creado) 
        //    {
        //        MessageBox.Show("Ya existe un producto con ese nombre");
        //    }

        //    else
        //    {
        //        MessageBox.Show("Producto creado con éxito");
              
                
        //    }

        //}

        private void CrearProducto_Click(object sender, RoutedEventArgs e)
        {

            var productosController = new ProductosController();
            // Ahora usamos el nombre de la categoria, no un ObjectId
            var nuevoProducto = new Producto
            {
                Nombre = "Martillo 2",
                Descripcion = "Herramienta de golpeo 2",
                Precio = 15000m,
                Categoria = "Herramientas",
                Activo = true
            };

            bool creado = productosController.InsertarProducto(nuevoProducto);

            if (!creado)
            {
                MessageBox.Show("Ya existe un producto con ese nombre");
            }
            else
            {
                MessageBox.Show("Producto creado con éxito");
                // Si quieres, aquí podrías ya pintar la imagen del código de barras:
                ImgCodigoBarras.Source = CodeBarGenerador.CrearCodigo128(nuevoProducto.CodigoBarras);
            }
        }
    }
}
