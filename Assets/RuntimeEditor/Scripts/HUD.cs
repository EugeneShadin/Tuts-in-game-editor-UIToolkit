using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;

        CreateHorizontalPanel1(root);

        //Create a container for vertical toolbars
        var centerElement = new VisualElement();
        centerElement.AddToClassList("center-container");
        root.Add(centerElement);

        CreateVerticalPanel1(centerElement);
        CreateVerticalPanel2(centerElement);

        CreateHorizontalPanel2(root);
    }

    private void CreateHorizontalPanel1(VisualElement root)
    {
        var topToolbar = new UIToolbar(root);

        var group = topToolbar.AddGroup("left");
        group.AddButton("Btn1", EmptyAction, true);
        group.AddButton("Btn2", EmptyAction);
        group.AddButton("Btn3", EmptyAction, wide:true);

        group = topToolbar.AddGroup("right");
        group.AddButton("Btn3", EmptyAction, true, true);
    }

    private void CreateVerticalPanel1(VisualElement root)
    {
        var leftToolbar = new UIToolbar(root, true, false);
        var group = leftToolbar.AddGroup("top");
        group.AddButton("Btn1", EmptyAction);
        group.AddButton("Btn2", EmptyAction);
        group.AddButton("Btn3", EmptyAction, true);
    }

    private void CreateVerticalPanel2(VisualElement root)
    {
        var rightToolbar = new UIToolbar(root, true, false);
        var group = rightToolbar.AddGroup("top");
        group.AddButton("Btn1", EmptyAction);
        group.AddButton("Btn2", EmptyAction);

        group = rightToolbar.AddGroup("middle");
        group.AddButton("Btn1", EmptyAction, true);
        group.AddButton("Btn2", EmptyAction);

        group = rightToolbar.AddGroup("bottom");
        group.AddButton("Btn1", EmptyAction, true);
    }

    private void CreateHorizontalPanel2(VisualElement root)
    {
        var bottomToolbar = new UIToolbar(root);
        bottomToolbar.AddGroup("left");

        var group = bottomToolbar.AddGroup("center");
        group.AddButton("Btn1", EmptyAction, true);
        group.AddButton("Btn2", EmptyAction, true);
        group.AddButton("Btn3", EmptyAction, true);

        bottomToolbar.AddGroup("right");
    }

    private void EmptyAction()
    {
    }
}
