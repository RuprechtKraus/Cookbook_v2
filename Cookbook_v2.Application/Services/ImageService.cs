using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Application.Settings;
using Cookbook_v2.Toolkit.Exceptions;
using Microsoft.Extensions.Options;

namespace Cookbook_v2.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly ImagesSettings _imagesSettings;

        public ImageService( IOptions<ImagesSettings> imagesSettings )
        {
            _imagesSettings = imagesSettings.Value;
        }

        public async Task<string> CreateAndSaveImageFromBase64( string base64 )
        {
            // Удаление base64 хедера с mime информацией
            base64 = base64[ ( base64.IndexOf( "," ) + 1 ).. ];

            byte[] bytes = Convert.FromBase64String( base64 );
            string imageFormat = GetImageFormat( base64 );
            string imageName = Path.ChangeExtension(
                Path.GetRandomFileName(), imageFormat );
            string imagePath = GetRecipeImagePath( imageName ).ToLower();

            using var fs = new FileStream( imagePath, FileMode.Create );
            await fs.WriteAsync( bytes );
            fs.Close();

            return imageName;
        }

        public void DeleteImage( string imageName )
        {
            string path = GetRecipeImagePath( imageName );
            if ( File.Exists( path ) )
            {
                File.Delete( path );
            }
        }

        public string GetRecipeImagePath( string imageName )
        {
            return Path.Combine( _imagesSettings.RecipeImagesDirectory, imageName );
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
