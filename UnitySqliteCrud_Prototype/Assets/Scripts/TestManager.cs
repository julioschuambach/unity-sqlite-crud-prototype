using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Class for tests during development.
/// </summary>
public class TestManager : MonoBehaviour
{
    public static TestManager instance;

    [Header("Contexts")]
    private readonly DatabaseContext _databaseContext;
    private readonly UsersContext _usersContext;

    [Header("Main Screen")]
    public Text consolePanelText;

    [Header("CRUD Panel")]
    public GameObject crudMasterPanel;
    public Text crudPanelTitle;
    public InputField userIdInputField;
    public InputField userNameInputField;
    public InputField userEmailInputField;
    public Button submitButton;

    public TestManager()
    {
        _databaseContext = new DatabaseContext();
        _usersContext = new UsersContext();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CloseCrudPanel();

        _databaseContext.CreateDatabase();
        _usersContext.CreateTable();
    }

    public void ListUsers()
    {
        List<User> users = _usersContext.GetAllUsers();

        ClearConsolePanel();
        users.ForEach(user => consolePanelText.text += user.ToString() + Environment.NewLine);
    }

    public void OpenCrudPanel(CrudOperation operation)
    {
        ToggleCrudPanel(true);

        switch (operation)
        {
            case CrudOperation.Create:
                UpdateCrudPanel("Create");
                UpdateCrudPanelInputFieldsInteractability(userId: false, userName: true, userEmail: true);
                UpdateSubmitButtonListeners(CreateUser);
                break;

            case CrudOperation.Read:
                UpdateCrudPanel("Read");
                UpdateCrudPanelInputFieldsInteractability(userId: true, userName: false, userEmail: false);
                UpdateSubmitButtonListeners(ReadUser);
                break;

            case CrudOperation.Update:
                UpdateCrudPanel("Update");
                UpdateCrudPanelInputFieldsInteractability(userId: true, userName: true, userEmail: true);
                UpdateSubmitButtonListeners(UpdateUser);
                break;

            case CrudOperation.Delete:
                UpdateCrudPanel("Delete");
                UpdateCrudPanelInputFieldsInteractability(userId: true, userName: false, userEmail: false);
                UpdateSubmitButtonListeners(DeleteUser);
                break;
        }
    }

    private void UpdateSubmitButtonListeners(UnityAction action)
    {
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(action);
    }

    public void CloseCrudPanel()
    {
        ToggleCrudPanel(false);
        ClearInputFields();
    }

    private void ToggleCrudPanel(bool value)
        => crudMasterPanel.SetActive(value);

    private void UpdateCrudPanel(string operation)
    {
        crudPanelTitle.text = operation;
    }

    private void UpdateCrudPanelInputFieldsInteractability(bool userId, bool userName, bool userEmail)
    {
        userIdInputField.interactable = userId;
        userNameInputField.interactable = userName;
        userEmailInputField.interactable = userEmail;
    }

    public void CreateUser()
    {
        StringBuilder sb = new();

        try
        {
            User user = new(userNameInputField.text, userEmailInputField.text);

            _usersContext.Insert(user);

            sb.AppendLine("<b>Created user:</b>");
            sb.AppendUser(user);
        }
        catch (Exception ex)
        {
            sb.AppendLine("Failed!");
            sb.AppendLine("Error: " + ex.Message);
        }
        finally
        {
            CloseCrudPanel();
            UpdateConsolePanel(sb.ToString());
        }
    }

    public void ReadUser()
    {
        StringBuilder sb = new();

        try
        {
            User? user = GetUserById(new Guid(userIdInputField.text));

            sb.AppendLine("<b>User:</b>");

            if (user == null)
                sb.AppendLine("Not found a User with Id: " + userIdInputField.text);
            else
                sb.AppendUser(user);
        }
        catch (Exception ex)
        {
            sb.AppendLine("Failed!");
            sb.AppendLine("Error: " + ex.Message);
        }
        finally
        {
            CloseCrudPanel();
            UpdateConsolePanel(sb.ToString());
        }
    }

    public void UpdateUser()
    {
        StringBuilder sb = new();

        try
        {
            Guid id = new Guid(userIdInputField.text);
            User? user = GetUserById(id);

            if (user == null)
                sb.AppendLine("Not found a User with Id: " + userIdInputField.text);
            else
            {
                user = _usersContext.Update(id, userNameInputField.text, userEmailInputField.text);
                sb.AppendLine("<b>Updated User:</b>");
                sb.AppendUser(user);
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine("Failed!");
            sb.AppendLine("Error: " + ex.Message);
        }
        finally
        {
            CloseCrudPanel();
            UpdateConsolePanel(sb.ToString());
        }
    }

    public void DeleteUser()
    {
        StringBuilder sb = new();

        try
        {
            Guid id = new Guid(userIdInputField.text);
            User? user = GetUserById(id);

            if (user == null)
                sb.AppendLine("Not found a User with Id: " + userIdInputField.text);
            else
            {
                _usersContext.Delete(id);
                sb.AppendLine("<b>Deleted user:</b>");
                sb.AppendUser(user);
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine("Failed!");
            sb.AppendLine("Error: " + ex.Message);
        }
        finally
        {
            CloseCrudPanel();
            UpdateConsolePanel(sb.ToString());
        }
    }

    private User? GetUserById(Guid id)
        => _usersContext.Select(id);

    private void ClearInputFields()
    {
        userIdInputField.text = "";
        userNameInputField.text = "";
        userEmailInputField.text = "";
    }

    private void ClearConsolePanel()
    {
        consolePanelText.text = "";
    }

    private void UpdateConsolePanel(string text)
        => consolePanelText.text = text;
}
