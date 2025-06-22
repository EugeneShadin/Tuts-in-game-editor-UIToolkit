using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;

    }

    private void EmptyAction()
    {
    }
}
