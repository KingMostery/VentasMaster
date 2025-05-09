// Helper: CodeBarGenerador.cs
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;

namespace MasterVentas.Util
{
    /// <summary>
    /// Generador y renderizador de códigos de barras Code 128.
    /// </summary>
    public static class CodeBarGenerador
    {
        /// <summary>
        /// Genera un identificador único (GUID sin guiones) para usar como contenido del código de barras.
        /// </summary>
        public static string GenerarCodigo() => Guid.NewGuid().ToString("N");

        /// <summary>
        /// Crea un BitmapSource con un código de barras Code 128 a partir de la cadena de entrada.
        /// </summary>
        /// <param name="content">Texto a convertir en código de barras.</param>
        /// <param name="width">Ancho de la imagen en píxeles.</param>
        /// <param name="height">Alto de la imagen en píxeles.</param>
        /// <returns>BitmapSource listo para asignar a un control Image en WPF.</returns>
        public static BitmapSource CrearCodigo128(string content, int width = 300, int height = 100)
        {
            // Genera los datos de píxeles usando ZXing
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 10
                }
            };
            var pixelData = writer.Write(content);

            // Crea un WriteableBitmap con el formato apropiado (Bgra32)
            var bitmap = new WriteableBitmap(
                pixelData.Width,
                pixelData.Height,
                96, // DPI X
                96, // DPI Y
                PixelFormats.Bgra32,
                null);

            // Copia los datos de píxeles en el WriteableBitmap
            bitmap.WritePixels(
                new Int32Rect(0, 0, pixelData.Width, pixelData.Height),
                pixelData.Pixels,
                pixelData.Width * (bitmap.Format.BitsPerPixel / 8),
                0);

            return bitmap;
        }
    }
}
