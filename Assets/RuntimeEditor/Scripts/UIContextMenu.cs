using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIContextMenu
{
    private readonly VisualElement _rootVisualElement;

    private readonly VisualElement _contextMenuRoot;
    private readonly VisualElement _contextMenuContainer;

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

        _contextMenuContainer = new VisualElement();
        _contextMenuContainer.AddToClassList("context-menu_container");
        _contextMenuContainer.AddToClassList("context-menu_back");

        /*
         * Subscribe to events so clicking inside the menu doesn’t close it.
         * The event is stopped to avoid triggering _contextMenuRoot logic.
         */
        _contextMenuContainer.RegisterCallback<PointerDownEvent>(evt => evt.StopPropagation());
        _contextMenuRoot.Add(_contextMenuContainer);
    }

    public void SetPosition(Vector2 position)
    {
        _contextMenuContainer.transform.position = position;
    }

    public Button AddButton(string text, Action callback, string icon = null)
    {
        // Simple button creation logic
        var button = new Button(()=>
        {
            // Wraps a target event and adds logic to hide the menu after execution.
            Hide();
            callback();
        });

        button.text = text;

        if (!string.IsNullOrEmpty(icon))
        {
            var iconElement = new VisualElement();
            iconElement.AddToClassList("context-menu_icon");
            iconElement.AddToClassList(icon);


            button.Add(iconElement);
        }

        _contextMenuContainer.Add(button);
        return button;
    }

    public void AddSeparator()
    {
        //Creates a separator — make sure to assign it a name.
        var separator = new VisualElement();
        separator.name = "separator";


        _contextMenuContainer.Add(separator);
    }

    // Handle for clicks outside the menu.
    private void ContextMenuBackClicked(PointerDownEvent evt)
    {
        evt.StopPropagation();
        Hide();
    }

    public void Clear()
    {
        // This method allows you to remove previously added buttons and separators.
        _contextMenuContainer.Clear();
    }

    public void Show()
    {
        _rootVisualElement.Add(_contextMenuRoot);
        _contextMenuContainer.schedule.Execute(ScheduleClampToScreen).ExecuteLater(2);
    }

    public void Hide()
    {
        /*
         * To hide the menu, remove it from its parent container.
         * This is a cleaner approach — elements are removed from the hierarchy along with event bindings.
         */
        _rootVisualElement.Remove(_contextMenuRoot);
    }

    private void ScheduleClampToScreen()
    {
        var menuBound = _contextMenuContainer.worldBound;
        var rootBound = _contextMenuRoot.worldBound;

        var menuMin = menuBound.min;
        var menuMax = menuBound.max;

        var rootMin = rootBound.min;
        var rootMax = rootBound.max;

        var menuPos = menuMin;

        if (menuMin.x < rootMin.x)
        {
            menuPos.x += rootMin.x - menuMin.x;
        }
        else if (menuMax.x > rootMax.x)
        {
            menuPos.x += rootMax.x - menuMax.x;
        }

        if (menuMin.y < rootMin.y)
        {
            menuPos.y += rootMin.y - menuMin.y;
        }
        if (menuMax.y > rootMax.y)
        {
            menuPos.y += rootMax.y - menuMax.y;
        }

        SetPosition(menuPos);
    }
}