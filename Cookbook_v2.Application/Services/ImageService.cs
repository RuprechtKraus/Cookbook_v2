using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using Cookbook_v2.Toolkit.Exceptions;

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
        public static async Task<string> CreateAndSaveImageFromBase64( string base64, string path )
        {
            // Удаление base64 хедера с mime информацией
            base64 = base64[ ( base64.IndexOf( "," ) + 1 ).. ];

            byte[] bytes = Convert.FromBase64String( base64 );
            string imageFormat = GetImageFormat( base64 );
            string imageName = Path.ChangeExtension(
                Path.GetRandomFileName(), imageFormat ).ToLower();
            string imagePath = Path.Combine( path, imageName );

            using var fs = new FileStream( imagePath, FileMode.Create );
            await fs.WriteAsync( bytes );
            fs.Close();

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

        public static string GetImageFormat( string base64 )
        {
            string data = base64[ ..5 ];
            return data.ToUpper() switch
            {
                "IVBOR" => "png",
                "/9J/4" => "jpg",
                _ => throw new ImageFormatException( "Invalid image format" ),
            };
        }
    }
}
