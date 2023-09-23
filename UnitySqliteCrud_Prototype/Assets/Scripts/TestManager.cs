using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for tests during development.
/// </summary>
public class TestManager : MonoBehaviour
{
    private readonly DatabaseContext _databaseContext;
    private readonly UsersContext _usersContext;

    public Text consolePanelText;

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

    public void ListUsers()
    {
        List<User> users = _usersContext.GetAllUsers();

        users.ForEach(user => consolePanelText.text += user.ToString() + Environment.NewLine);
    }
}