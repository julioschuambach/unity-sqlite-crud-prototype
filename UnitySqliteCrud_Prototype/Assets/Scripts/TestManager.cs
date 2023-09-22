using UnityEngine;

/// <summary>
/// Class for tests during development.
/// </summary>
public class TestManager : MonoBehaviour
{
    private readonly DatabaseManager _databaseManager;

    public TestManager()
    {
        _databaseManager = new DatabaseManager();
    }

    void Start()
    {
        _databaseManager.CreateDatabase();
        _databaseManager.CreateUsersTable();
    }
}