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

    public override string ToString()
        => $"Id: {Id}, Name: {Name}, E-mail: {Email}.";
}