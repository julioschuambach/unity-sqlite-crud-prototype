using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

public class DatabaseManager
{
    private readonly string _databaseUri;
    private List<string> UsersTableFields
    {
        get
        {
            return new()
            {
                "[Id] BLOB PRIMARY KEY NOT NULL UNIQUE",
                "[Name] STRING NOT NULL",
                "[Email] STRING NOT NULL UNIQUE",
                "[CreatedDate] DATETIME NOT NULL",
                "[LastUpdatedDate] DATETIME NOT NULL"
            };
        } 
    }

    public DatabaseManager()
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

    public void CreateUsersTable()
    {
        using (var connection = GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                StringBuilder sb = new();
                sb.AppendLine("CREATE TABLE IF NOT EXISTS");
                sb.AppendLine("[Users]");
                sb.AppendLine("(" + string.Join(", ", UsersTableFields) + ")");

                command.CommandText = sb.ToString();

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public SqliteConnection GetConnection()
        => new(_databaseUri);
}