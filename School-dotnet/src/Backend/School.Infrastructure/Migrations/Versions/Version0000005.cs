using FluentMigrator;

namespace School.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_STUDENT, "Create table to save the students created")]
    public class Version0000005 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Students")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Surname").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable()
            .WithColumn("Schooling").AsInt32().NotNullable()
            .WithColumn("AcademyId").AsInt64().NotNullable().ForeignKey("FK_Student_Academy_Id", "Academys", "Id");
        }
    }
}