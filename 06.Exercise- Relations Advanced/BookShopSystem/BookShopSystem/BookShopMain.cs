namespace BookShopSystem
{
    using System.Data.Entity;
    using System.IO;
    using System.Reflection;
    using BookShopSystem.Data;
    using BookShopSystem.Migrations;

    class BookShopMain
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>());
            
        }
    }
}
