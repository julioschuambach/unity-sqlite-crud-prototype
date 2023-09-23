using System.Text;
using System;
using System.Collections.Generic;

public class UsersContext
{
    private readonly DatabaseContext _databaseContext;
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

    public UsersContext()
    {
        _databaseContext = new DatabaseContext();
    }

    public void CreateTable()
    {
        using (var connection = _databaseContext.GetConnection())
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
}