using FluentMigrator;

namespace School.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_ACADEMY, "Create table to save the academys created")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        CreateTable("Academy")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Address").AsString(255).NotNullable()
            .WithColumn("State").AsString(255).NotNullable()
            .WithColumn("City").AsString(255).NotNullable();
    }
}