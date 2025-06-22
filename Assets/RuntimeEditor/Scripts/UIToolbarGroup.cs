using System;
using UnityEngine.UIElements;

public class UIToolbarGroup
{
    private const string BUTTON_PRIMARY = "toolbar-button-primary";
    private const string BUTTON_SECONDARY = "toolbar-button-secondary";

    private const string BUTTON = "toolbar-button";
    private const string BUTTON_WIDE = "toolbar-button-wide";

    public VisualElement Root { get;}

    public UIToolbarGroup(VisualElement rootVisualElement, string name, params string[] styles)
    {
        Root = new VisualElement();
        Root.name = name;

        foreach (var style in styles)
        {
            Root.AddToClassList(style);
        }

        rootVisualElement.Add(Root);
    }

    public void AddButton(string text, Action click, bool primary = false, bool wide = false)
    {
        var button = new Button(click);
        button.AddToClassList(primary ? BUTTON_PRIMARY : BUTTON_SECONDARY);
        button.AddToClassList(wide ? BUTTON_WIDE : BUTTON);

        button.text = text;

        Root.Add(button);
    }
}