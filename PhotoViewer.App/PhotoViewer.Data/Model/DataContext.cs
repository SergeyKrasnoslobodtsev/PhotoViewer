using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;

namespace Data.Model
{
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, ContextMigrationConfiguration>(true));
        }

        internal sealed class ContextMigrationConfiguration : DbMigrationsConfiguration<DataContext>
        {
            public ContextMigrationConfiguration() {
                AutomaticMigrationsEnabled = true;
                AutomaticMigrationDataLossAllowed = true;
                SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
            }
        }
        public DbSet<Photo> Photos { get; set; }
    }
}
