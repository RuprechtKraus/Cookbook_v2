using Microsoft.Extensions.Configuration;

namespace Cookbook_v2.Toolkit.Extensions
{
    public static class AppsettingsExtensions
    {
        /// <summary>
        /// Сокращение для GetSection("MigrationAssemblies")[name]
        /// </summary>
        /// <param name="configuration">Конфигурация</param>
        /// <param name="name">Ключ сборки для миграций</param>
        /// <returns>Строка целевой сборки</returns>
        public static string GetMigrationAssembly( this IConfiguration configuration, string name )
        {
            return configuration?.GetSection( "MigrationAssemblies" )?[ name ];
        }
    }
}
