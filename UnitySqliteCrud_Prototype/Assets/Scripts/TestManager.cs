using UnityEngine;

/// <summary>
/// Class for tests during development.
/// </summary>
public class TestManager : MonoBehaviour
{
    private readonly DatabaseContext _databaseContext;
    private readonly UsersContext _usersContext;

    public TestManager()
    {
        _databaseContext = new DatabaseContext();
        _usersContext = new UsersContext();
    }

    void Start()
    {
        _databaseContext.CreateDatabase();
        _usersContext.CreateTable();
    }
}