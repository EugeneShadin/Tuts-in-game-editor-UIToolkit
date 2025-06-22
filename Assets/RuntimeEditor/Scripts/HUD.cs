using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;
    private UIContextMenu _contextMenu;
    private UIMenuContainer _container;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;

        root.pickingMode = PickingMode.Position;
        root.RegisterCallback<PointerDownEvent>(RootPointerDownHandler);

        _contextMenu = new UIContextMenu(root);

        _container = new UIMenuContainer();
        _container.AddButton("Menu Item 1", EmptyAction, true, ButtonIcon.FILE);
        _container.AddButton("Menu Item 2", EmptyAction);
        _container.AddButton("Menu Item 3", EmptyAction, false, ButtonIcon.SETTINGS);
        _container.AddSeparator();

        var subMenuContainer = new UIMenuContainer();
        subMenuContainer.AddButton("Menu Item 1", EmptyAction, false, ButtonIcon.EMAIL);
        subMenuContainer.AddButton("Menu Item 2", EmptyAction);
        subMenuContainer.AddSeparator();

        var subMenuContainer2 = new UIMenuContainer();
        subMenuContainer2.AddButton("Menu Item 1", EmptyAction, false, ButtonIcon.FILE);
        subMenuContainer2.AddButton("Menu Item 2", EmptyAction);

        subMenuContainer.AddSubMenu("SubMenu 3", subMenuContainer2);

        var subMenuContainer3 = new UIMenuContainer();
        subMenuContainer3.AddButton("Menu Item 1", EmptyAction);
        subMenuContainer3.AddSeparator();
        subMenuContainer3.AddButton("Menu Item 2", EmptyAction);
        subMenuContainer3.AddButton("Menu Item 3", EmptyAction, icon:ButtonIcon.SETTINGS);

        _container.AddSubMenu("SubMenu 1", subMenuContainer);
        _container.AddSubMenu("SubMenu 2", subMenuContainer3);

        _container.AddSeparator();
        _container.AddButton("Menu Item 6", EmptyAction);
    }

    private void RootPointerDownHandler(PointerDownEvent evt)
    {
        //right-click
        if (evt.button != 1)
        {
            return;
        }

        _contextMenu.Show(_container, evt.position);
        evt.StopPropagation();
    }

    private void EmptyAction()
    {
    }
}
