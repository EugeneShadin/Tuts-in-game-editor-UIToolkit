using System;
using UnityEngine.UIElements;

public class UIToolbar
{
    // This is the VisualElement we added in UI Builder.
    private VisualElement _rootPanel;

    // The toolbar class receives the parent VisualElement.
    public UIToolbar(VisualElement rootVisualElement)
    {
        //We create a new VisualElement and assign the style selectors.
        _rootPanel = new VisualElement();
        _rootPanel.AddToClassList("toolbar-horizontal");
        _rootPanel.AddToClassList("toolbar-primary");


        //Add the toolbar to the parent container so it appears on the screen.
        rootVisualElement.Add(_rootPanel);
    }


    public void AddButton(VisualElement group, string text, Action click)
    {
        //Create the button.
        var button = new Button(click);
        //Assign the style selector.
        button.AddToClassList("toolbar-button");
        button.text = text;


        //Add the button to the VisualElement
        group.Add(button);
    }


    //Method for adding a button to the toolbar:
    public void AddButton(string text, Action click)
    {
        AddButton(_rootPanel, text, click);
    }

    //Method for adding a group to the toolbar:
    public VisualElement AddGroup(string groupName = null)
    {
        var group = new VisualElement();
        group.name = groupName;
        group.AddToClassList("toolbar-horizontal");


        _rootPanel.Add(group);
        return group;
    }
}
