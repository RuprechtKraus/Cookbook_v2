using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;

namespace Cookbook_v2.Application.Services
{
    public static class ImageService
    {
        /// <summary>
        /// Создает изображение из формата base64 и сохраняет его по указанному пути
        /// </summary>
        /// <param name="base64">Закодированное изображение</param>
        /// <param name="path">Путь для сохранения изображения</param>
        /// <returns>Имя сохраненного изображения</returns>
        [SuppressMessage( "Interoperability", "CA1416:Проверка совместимости платформы" )]
        public static async Task<string> CreateAndSaveImageFromBase64( string base64, string path )
        {
            byte[] bytes = Convert.FromBase64String( base64 );
            using var ms = new MemoryStream( bytes );
            Image image = Image.FromStream( ms );
            ImageFormat imageFormat = image.RawFormat;

            string imageName = Path.ChangeExtension(
                Path.GetRandomFileName(), imageFormat.ToString() ).ToLower();
            string imagePath = Path.Combine( path, imageName );

            using var fs = new FileStream( imagePath, FileMode.Create );
            await fs.WriteAsync( bytes );

            fs.Close();
            ms.Close();
            return imageName;
        }

        /// <summary>
        /// Удаляет изображение по указанному пути
        /// </summary>
        /// <param name="path">Путь к изображению</param>
        /// <returns></returns>
        public static void DeleteImage( string path )
        {
            throw new NotImplementedException( "Method not implemented" );
        }
    }
}
