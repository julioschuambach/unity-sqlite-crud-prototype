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
                "[Id] STRING PRIMARY KEY NOT NULL UNIQUE",
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

    public List<User> GetAllUsers()
    {
        List<User> users = new();

        using (var connection = _databaseContext.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [Id], [Name], [Email], [CreatedDate], [LastUpdatedDate] FROM [Users]";

                try
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(
                                new User(
                                    new ReadUserDto(reader)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        return users;
    }

    public void Insert(User user)
    {
        using (var connection = _databaseContext.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO [Users] ([Id], [Name], [Email], [CreatedDate], [LastUpdatedDate]) VALUES (@Id, @Name, @Email, @CreatedDate, @LastUpdatedDate)";
                command.Parameters.AddWithValue("@Id", user.Id.ToString());
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                command.Parameters.AddWithValue("@LastUpdatedDate", user.LastUpdatedDate);

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

    public User? Select(Guid id)
    {
        ReadUserDto readDto = new();

        using (var connection = _databaseContext.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [Id], [Name], [Email], [CreatedDate], [LastUpdatedDate] FROM [Users] WHERE [Id] = @Id";
                command.Parameters.AddWithValue("@Id", id.ToString());

                try
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readDto.Fill(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        return readDto.Id == Guid.Empty ? null : new User(readDto);
    }

    public User Update(Guid id, string name, string email)
    {
        using (var connection = _databaseContext.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE [Users] SET [Name] = @Name, [Email] = @Email, [LastUpdatedDate] = @LastUpdatedDate WHERE [Id] = @Id";
                command.Parameters.AddWithValue("@Id", id.ToString());
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now);

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

        return Select(id);
    }

    public void Delete(Guid id)
    {
        using (var connection = _databaseContext.GetConnection())
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM [Users] WHERE [Id] = @Id";
                command.Parameters.AddWithValue("@Id", id.ToString());

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