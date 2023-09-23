using System;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }

    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        CreatedDate = DateTime.Now;
        LastUpdatedDate = DateTime.Now;
    }

    public User(ReadUserDto readDto)
    {
        Id = readDto.Id;
        Name = readDto.Name;
        Email = readDto.Email;
        CreatedDate = readDto.CreatedDate;
        LastUpdatedDate = readDto.LastUpdatedDate;
    }

    public override string ToString()
        => $"<b>Id:</b> {Id}" + 
           Environment.NewLine + 
           $"<b>Name:</b> {Name}" +
           Environment.NewLine + 
           $"<b>E-mail:</b> {Email}" +
           Environment.NewLine;
}
