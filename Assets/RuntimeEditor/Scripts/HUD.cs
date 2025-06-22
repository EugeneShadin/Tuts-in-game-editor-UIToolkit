using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;
    private UIContextMenu _contextMenu;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();

        var root = _uiDocument.rootVisualElement;
        // Allow the parent to intercept input events.
        root.pickingMode = PickingMode.Position;
        root.RegisterCallback<PointerDownEvent>(RootPointerDownHandler);

        _contextMenu = new UIContextMenu(root);
    }


    private void RootPointerDownHandler(PointerDownEvent evt)
    {
        //Check if the right mouse button was pressed.
        if (evt.button != 1)
        {
            return;
        }

        _contextMenu.Clear();
        _contextMenu.AddButton("Menu Item 1", EmptyAction, "email-icon");
        _contextMenu.AddButton("Menu Item 2", EmptyAction, "file-icon");
        _contextMenu.AddButton("Menu Item 3", EmptyAction, "settings-icon");
        _contextMenu.AddSeparator();

        // UIElemenets.Button.SetEnabled is similar to UI.Button.interactable
        _contextMenu.AddButton("Menu Item 4", EmptyAction).SetEnabled(false);
        _contextMenu.AddButton("Menu Item 5", EmptyAction).SetEnabled(false);
        _contextMenu.AddSeparator();
        _contextMenu.AddButton("Menu Item 6", EmptyAction);

        _contextMenu.SetPosition(evt.position);
        _contextMenu.Show();

        evt.StopPropagation();
    }

    private void EmptyAction()
    {
    }
}
