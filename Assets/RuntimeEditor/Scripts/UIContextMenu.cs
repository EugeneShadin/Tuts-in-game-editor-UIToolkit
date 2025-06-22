using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIContextMenu
{
    private readonly VisualElement _rootVisualElement;
    private readonly Stack<UIMenuContainer> _containers = new();

    private readonly VisualElement _contextMenuRoot;

    public UIContextMenu(VisualElement root)
    {
        _rootVisualElement = root;

        _contextMenuRoot = new VisualElement();
        _contextMenuRoot.AddToClassList("context-menu_root");

        /*
         * Subscribe to events to hide the menu when clicked outside.
         * The event is stopped to prevent clicks from affecting buttons underneath the menu.
         */
        _contextMenuRoot.RegisterCallback<PointerDownEvent>(ContextMenuBackClicked);
    }

    // Handle for clicks outside the menu.
    private void ContextMenuBackClicked(PointerDownEvent evt)
    {
        evt.StopPropagation();
        Hide();
    }

    public void Clear()
    {
        _containers.Clear();
        _contextMenuRoot.Clear();
    }

    public void Show(UIMenuContainer container, Vector2 position)
    {
        container.SetDepth(_containers.Count);
        container.SetSubMenuCallback(ShowSubMenu);
        container.SetPosition(position);
        container.Show(_contextMenuRoot);

        _containers.Push(container);
        _rootVisualElement.Add(_contextMenuRoot);
    }

    private void ShowSubMenu(int depth, UIMenuContainer container, VisualElement pivotElement)
    {
        if (depth < _containers.Count - 1)
        {
            var count = _containers.Count - depth - 1;
            for (var i = 0; i < count; i++)
            {
                var subMenu = _containers.Pop();
                subMenu.Hide();
            }
        }

        container.SetDepth(_containers.Count);
        container.SetSubMenuCallback(ShowSubMenu);
        container.Show(_contextMenuRoot, pivotElement);

        _containers.Push(container);
    }

    public void Hide()
    {
        /*
         * To hide the menu, remove it from its parent container.
         * This is a cleaner approach — elements are removed from the hierarchy along with event bindings.
         */
        _rootVisualElement.Remove(_contextMenuRoot);
        Clear();
    }
}