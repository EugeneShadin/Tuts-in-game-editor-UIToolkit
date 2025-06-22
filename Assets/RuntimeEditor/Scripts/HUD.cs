using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    private DialogModalWindow _dialogWindow;
    private CreateEnemyModalWindow _createEnemyWindow;


    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;

        _dialogWindow = new DialogModalWindow(root);
        _createEnemyWindow = new CreateEnemyModalWindow(root);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _dialogWindow.Show(new DialogModalWindowModel("Test Title", "Test Message", ResultOfDialog));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _createEnemyWindow.Show(new CreateEnemyModalWindowModal(EnemyCreated));
        }
    }

    private void ResultOfDialog(bool state)
    {
        Debug.Log("DialogModalWindow Result = "+state);
    }

    private void EnemyCreated(EnemyData data)
    {
        Debug.Log($"[New Enemy]" +
                  $"\nName : {data.Name}" +
                  $"\nType : {data.Type}" +
                  $"\nHealth : {data.Health}" +
                  $"\nDamage : {data.Damage}");
    }
}
