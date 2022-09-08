using Cookbook_v2.Toolkit.Exceptions;

namespace Cookbook_v2.Application.Services
{
    public static class ImageService
    {
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

        public static void DeleteImage( string path )
        {
            if ( File.Exists( path ) )
            {
                File.Delete( path );
            }
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
