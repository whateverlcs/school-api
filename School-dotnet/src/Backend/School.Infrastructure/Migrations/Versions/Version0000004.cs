using FluentMigrator;

namespace School.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.RENAME_TABLE_ACADEMY, "Rename table academy to academys")]
    public class Version0000004 : VersionBase
    {
        public override void Up()
        {
            Rename.Table("Academy").To("Academys");
        }
    }
}