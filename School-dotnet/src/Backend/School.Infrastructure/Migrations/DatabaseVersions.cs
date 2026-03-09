namespace School.Infrastructure.Migrations;

public abstract class DatabaseVersions
{
    public const int TABLE_USER = 1;
    public const int TABLE_REFRESH_TOKEN = 2;
    public const int TABLE_ACADEMY = 3;
    public const int RENAME_TABLE_ACADEMY = 4;
    public const int TABLE_STUDENT = 5;
}