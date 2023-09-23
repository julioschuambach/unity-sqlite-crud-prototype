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
        User user = new(userNameInputField.text, userEmailInputField.text);

        StringBuilder sb = new();
        sb.AppendLine("<b>CREATE new user with:</b>");
        sb.AppendLine("Id: " + user.Id);
        sb.AppendLine("Name: " + user.Name);
        sb.AppendLine("Email: " + user.Email);
        sb.AppendLine("CreatedDate: " + user.CreatedDate);
        sb.AppendLine("LastUpdatedDate: " + user.LastUpdatedDate);

        consolePanelText.text = sb.ToString();

        CloseCrudPanel();
    }

    public void ReadUser()
    {
        StringBuilder sb = new();
        sb.AppendLine("<b>READ user with:</b>");
        sb.AppendLine("Id: " + userIdInputField.text);

        consolePanelText.text = sb.ToString();

        CloseCrudPanel();
    }

    public void UpdateUser()
    {
        StringBuilder sb = new();
        sb.AppendLine("<b>UPDATE user with:</b>");
        sb.AppendLine("Id: " + userIdInputField.text);
        sb.AppendLine("<b>SET</b>");
        sb.AppendLine("Name: " + userNameInputField.text);
        sb.AppendLine("Email: " + userEmailInputField.text);
        sb.AppendLine("LastUpdatedDate: " + DateTime.Now);

        consolePanelText.text = sb.ToString();

        CloseCrudPanel();
    }

    public void DeleteUser()
    {
        StringBuilder sb = new();
        sb.AppendLine("<b>DELETE user with:</b>");
        sb.AppendLine("Id: " + userIdInputField.text);

        consolePanelText.text = sb.ToString();

        CloseCrudPanel();
    }

    private void ClearInputFields()
    {
        userIdInputField.text = "";
        userNameInputField.text = "";
        userEmailInputField.text = "";
    }
}