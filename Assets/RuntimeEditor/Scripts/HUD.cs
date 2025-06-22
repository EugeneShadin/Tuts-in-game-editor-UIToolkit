using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    void Start()
    {
        //Get the UIDocument component from the GameObject.
        _uiDocument = GetComponent<UIDocument>();
        //Access the root VisualElement.
        var root = _uiDocument.rootVisualElement;
        
        //Create the toolbar and add a few buttons.
        var toolbar = new UIToolbar(root);

        var group = toolbar.AddGroup("left");
        toolbar.AddButton(group, "Btn1", Button1Clicked);
        toolbar.AddButton(group, "Btn2", Button2Clicked);
        toolbar.AddButton(group, "Btn3", Button3Clicked);
    }

    private void Button1Clicked() => Debug.Log("Button1Clicked");
    private void Button2Clicked() => Debug.Log("Button2Clicked");
    private void Button3Clicked() => Debug.Log("Button3Clicked");
}
