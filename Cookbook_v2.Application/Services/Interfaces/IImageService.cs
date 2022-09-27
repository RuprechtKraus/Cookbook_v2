namespace Cookbook_v2.Application.Services.Interfaces
{
    public interface IImageService
    {
        /// <returns>Имя сохраненного изображения</returns>
        Task<string> CreateAndSaveImageFromBase64( string base64 );
        Task<string> EncodeImageToBase64( string imageName );
        void DeleteImage( string imageName );

        /// <summary>
        /// Принимает имя изображения рецепта и возвращает путь к этому изображению. 
        /// Путь к папке с изображениями рецептов задается в appsettings.json
        /// </summary>
        /// <returns>Путь к изображению</returns>
        string GetRecipeImagePath( string imageName );
    }
}
