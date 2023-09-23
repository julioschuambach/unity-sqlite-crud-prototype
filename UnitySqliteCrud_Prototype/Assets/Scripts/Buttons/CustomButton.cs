using UnityEngine;
using UnityEngine.EventSystems;

public enum CrudOperation { Create, Read, Update, Delete }

public class CustomButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private CrudOperation operation;

    public void OnPointerDown(PointerEventData eventData)
    {
        TestManager.instance.OpenCrudPanel(operation);
    }
}
