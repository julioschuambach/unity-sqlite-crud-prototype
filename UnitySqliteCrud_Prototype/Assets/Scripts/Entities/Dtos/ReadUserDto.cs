using Mono.Data.Sqlite;
using System;

public class ReadUserDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }

    public ReadUserDto(Guid id, string name, string email, DateTime createdDate, DateTime lastUpdatedDate)
    {
        Id = id;
        Name = name;
        Email = email;
        CreatedDate = createdDate;
        LastUpdatedDate = lastUpdatedDate;
    }

    public ReadUserDto(SqliteDataReader reader)
    {
        Id = new Guid((string)reader["Id"]);
        Name = (string)reader["Name"];
        Email = (string)reader["Email"];
        CreatedDate = (DateTime)reader["CreatedDate"];
        LastUpdatedDate = (DateTime)reader["LastUpdatedDate"];
    }
}