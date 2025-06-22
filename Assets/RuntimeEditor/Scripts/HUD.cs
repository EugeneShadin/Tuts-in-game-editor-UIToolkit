using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;
        // Allow the parent to intercept input events.
        root.pickingMode = PickingMode.Position;
        root.RegisterCallback<PointerDownEvent>(RootPointerDownHandler);
    }

    private void RootPointerDownHandler(PointerDownEvent evt)
    {
        //Check if the right mouse button was pressed.
        if (evt.button != 1)
        {
            return;
        }

        evt.StopPropagation();
    }

    private void EmptyAction()
    {
    }
}
