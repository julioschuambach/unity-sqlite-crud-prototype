using Mono.Data.Sqlite;
using System;

public class DatabaseContext
{
    private readonly string _databaseUri;

    public DatabaseContext()
    {
        _databaseUri = "URI = file:prototype.db";
    }

    public void CreateDatabase()
    {
        using (var connection = GetConnection())
        {
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public SqliteConnection GetConnection()
        => new(_databaseUri);
}