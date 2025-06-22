using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMenuContainer
{
    private const int DELAY = 2;

    private VisualElement _contextMenuRoot;
    private readonly VisualElement _contextMenuContainer;

    private int _depth = 0;
    private Action<int, UIMenuContainer, VisualElement> _showSubMenuAction;

    public UIMenuContainer()
    {
        _contextMenuContainer = new VisualElement();
        _contextMenuContainer.AddToClassList("context-menu_container");
        _contextMenuContainer.AddToClassList("context-menu_back");
        _contextMenuContainer.RegisterCallback<PointerDownEvent>(evt => evt.StopPropagation());
    }

    private void ClampInsideScreenSchedule(VisualElement pivotElement = null)
    {
        var menuBound = _contextMenuContainer.worldBound;
        var rootBound = _contextMenuRoot.worldBound;

        var menuMin = menuBound.min;
        var menuMax = menuBound.max;

        var rootMin = rootBound.min;
        var rootMax = rootBound.max;

        var menuPos = menuMin;

        if (pivotElement != null)
        {
            var pivotBound = pivotElement.worldBound;
            if (menuMin.x < rootMin.x)
            {
                menuPos.x = pivotBound.max.x;
            }
            else if (menuMax.x > rootMax.x)
            {
                menuPos.x = pivotBound.min.x - menuBound.size.x;
            }
        }
        else
        {
            if (menuMin.x < rootMin.x)
            {
                menuPos.x += rootMin.x - menuMin.x;
            }
            else if (menuMax.x > rootMax.x)
            {
                menuPos.x += (rootMax.x - menuMax.x);
            }
        }

        if (menuMin.y < rootMin.y)
        {
            menuPos.y += rootMin.y - menuMin.y;
        }
        if (menuMax.y > rootMax.y)
        {
            menuPos.y += (rootMax.y - menuMax.y);
        }

        SetPosition(menuPos);
    }

    public void Clear()
    {
        _contextMenuContainer.Clear();
    }

    public void SetDepth(int depth)
    {
        _depth = depth;
    }

    public void SetPosition(Vector2 position)
    {
        _contextMenuContainer.transform.position = position;
    }

    public void SetSubMenuCallback(Action<int, UIMenuContainer, VisualElement> showSubMenuAction)
    {
        _showSubMenuAction = showSubMenuAction;
    }

    public void AddSeparator()
    {
        var separator = new VisualElement();
        separator.name = "separator";

        _contextMenuContainer.Add(separator);
    }

    public void AddButton(string text, Action action, bool enabled = true, string icon = null)
    {
        var button = new Button(action);
        button.SetEnabled(enabled);
        button.text = text;

        AddIcon(button, icon);

        _contextMenuContainer.Add(button);
    }

    public void AddSubMenu(string text, UIMenuContainer menuContainer, bool enabled = true, string icon = null)
    {
        var button = new Button();
        button.RegisterCallback<ClickEvent>(evt =>
        {
            _showSubMenuAction(_depth, menuContainer, evt.target as VisualElement);
        });

        button.text = text;
        button.SetEnabled(enabled);

        AddIcon(button, icon, true);
        AddArrow(button);

        _contextMenuContainer.Add(button);
    }

    private void AddIcon(VisualElement root, string icon, bool addedAnyway = false)
    {
        if (string.IsNullOrEmpty(icon))
        {
            root.Add(addedAnyway ? new VisualElement() : null);
            return;
        }

        var iconElement = new VisualElement();
        iconElement.AddToClassList("context-menu_icon");
        iconElement.AddToClassList(icon);

        root.Add(iconElement);
    }

    private void AddArrow(VisualElement root)
    {
        var arrow = new VisualElement();
        arrow.AddToClassList("context-menu_arrow");
        root.Add(arrow);
    }

    public void Show(VisualElement root, VisualElement pivotElement = null)
    {
        if (pivotElement != null)
        {
            var pivotBound = pivotElement.worldBound;
            SetPosition(new Vector2(pivotBound.max.x, pivotBound.min.y));
        }

        _contextMenuRoot = root;
        _contextMenuRoot.Add(_contextMenuContainer);

        _contextMenuContainer.schedule.Execute(()=>
            {
                ClampInsideScreenSchedule(pivotElement);
            })
            .ExecuteLater(DELAY);
    }

    public void Hide()
    {
        _contextMenuRoot.Remove(_contextMenuContainer);
    }
}