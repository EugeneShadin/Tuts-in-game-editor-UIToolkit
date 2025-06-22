using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public static class VisualElementExtension
{
    public static VisualElement AddElement(this VisualElement visualElement, string name = null)
    {
        var element = new VisualElement();
        element.name = name;
        visualElement.Add(element);

        return element;
    }

    private static void AddToClassList(VisualElement visualElement, params string[] classes)
    {
    }

    public static T AddClass<T>(this T element, params string[] classes) where T : VisualElement
    {
        foreach (var @class in classes)
        {
            element.AddToClassList(@class);
        }

        return element;
    }

    public static T RemoveClass<T>(this T element, params string[] classes) where T : VisualElement
    {
        foreach (var @class in classes)
        {
            element.RemoveFromClassList(@class);
        }

        return element;
    }

    public static Label AddLabel(this VisualElement visualElement, string text = null, string name = null)
    {
        var label = new Label(text);
        label.text = text;
        label.name = name;
        visualElement.Add(label);

        return label;
    }

    public static Button AddButton(this VisualElement visualElement, Action onClick, string text = null,
        string name = null)
    {
        var button = new Button(onClick);
        button.text = text;
        button.name = name;
        visualElement.Add(button);

        return button;
    }

    public static TextField AddField(this VisualElement visualElement, string label = null, string name = null)
    {
        var textField = new TextField(label);
        textField.name = name;
        visualElement.Add(textField);

        return textField;
    }

    public static IntegerField AddIntField(this VisualElement visualElement, string label = null, string name = null)
    {
        var integerField = new IntegerField(label);
        integerField.name = name;
        visualElement.Add(integerField);

        return integerField;
    }

    public static EnumField AddEnumField<TEnum>(this VisualElement visualElement, TEnum value, string label = null, string name = null)
    where TEnum : Enum
    {
        var addEnumField = new EnumField(label, value);
        addEnumField.name = name;

        visualElement.Add(addEnumField);
        return addEnumField;
    }
}