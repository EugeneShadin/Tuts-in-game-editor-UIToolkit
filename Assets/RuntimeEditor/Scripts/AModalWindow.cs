using UnityEngine;
using UnityEngine.UIElements;

public abstract class AModalWindow<TModel> where TModel : AViewModel
{
    protected VisualElement Root { get; }

    private readonly VisualElement _windowContainer;
    protected VisualElement Window { get; }
    protected VisualElement Header { get; }
    protected VisualElement Body { get; }
    protected VisualElement Footer { get; }

    private bool _isMinimized;
    private Vector3 _positionOffset;

    private Label _title;
    private Button _minimizeButton;
    private Button _maximizeButton;
    private VisualElement _minimizeIcon;
    private DragManipulator _dragManipulator;

    protected TModel Model { get; private set; }

    protected AModalWindow(VisualElement rootVisualElement)
    {
        Root = rootVisualElement;

        _windowContainer = new VisualElement();
        _windowContainer.AddClass("modal-window-container", "modal-window-container-bg");

        Window = _windowContainer.AddElement().AddClass("modal-window");
        Header = Window.AddElement("header").AddClass("modal-window_header");
        _title = Header.AddLabel("Title");

        _dragManipulator = new DragManipulator(Header);
        Window.AddManipulator(_dragManipulator);

        //The Minimize button for the window.
        _minimizeButton = Header.AddButton(MinimizeClicked).AddClass("button-secondary");
        _minimizeButton.AddElement().AddClass("minimize-icon", "flex-grow");

        //The Maximize button for the window. It's hidden by default.
        _maximizeButton = Header.AddButton(MaximizeClicked).AddClass("button-success", "display-none");
        _maximizeButton.AddElement().AddClass("maximize-icon", "flex-grow");

        Header.AddButton(CloseClicked).AddClass(ButtonStyle.Danger)
            .AddElement().AddClass("x-mark-icon", "flex-grow");

        Body = Window.AddElement("body").AddClass("modal-window_body", "modal-window_content");
        Footer = Window.AddElement("footer").AddClass("modal-window_footer", "modal-window_content");
    }

    protected void SetTitle(string title)
    {
        _title.text = title;
    }

    private void MinimizeClicked()
    {
        // This flag helps determine whether the window was closed in a minimized state.
        _isMinimized = true;

        // Calculates offset from the window centre — header remains in place when minimized.
        _positionOffset = Vector3.up * (Window.worldBound.height - Header.worldBound.height) / -2;
        Window.transform.position += _positionOffset;

        //Disables event capture — similar to disabling Raycast Target in Unity UI.
        _windowContainer.pickingMode = PickingMode.Ignore;
        //Disables the vignette.
        _windowContainer.EnableInClassList("modal-window-container-bg", false);

        //Enables the maximize button.
        _maximizeButton.EnableInClassList("display-none", false);

        //It hides the minimize button and the window body.
        _minimizeButton.AddClass("display-none");
        Body.AddClass("display-none");
        Footer.AddClass("display-none");
    }

    private void MaximizeClicked()
    {
        /*
        * UI Toolkit doesn’t have a SetAsLastSibling method.
        * The only way to bring a window to the top of the hierarchy is to re-add it.
        */

        Root.Remove(_windowContainer);
        Root.Add(_windowContainer);

        MaximizeProcess();
        Window.schedule.Execute(_dragManipulator.ClampWindowPosition).ExecuteLater(2);
    }

    private void MaximizeProcess()
    {
        if (!_isMinimized)
        {
            return;
        }

        _isMinimized = false;

        //Restores the window's position to the centre.
        Window.transform.position -= _positionOffset;
        _positionOffset = Vector3.zero;

        //Enables event capture.
        _windowContainer.pickingMode = PickingMode.Position;
        //Enables the vignette.
        _windowContainer.AddClass("modal-window-container-bg");

        //It hides the maximize button.
        _maximizeButton.EnableInClassList("display-none", true);

        //Shows previously hidden elements.
        _minimizeButton.RemoveClass("display-none");
        Body.RemoveClass("display-none");
        Footer.RemoveClass("display-none");
    }

    private void CloseClicked()
    {
        Hide();
    }

    public void Show(TModel model)
    {
        Model = model;

        MaximizeProcess();
        Window.transform.position = Vector3.zero;

        BeforeShow();
        Root.Add(_windowContainer);
        AfterShow();
    }

    protected virtual void BeforeShow() { }

    protected virtual void AfterShow() { }

    public void Hide()
    {
        BeforeHide();
        Root.Remove(_windowContainer);
        AfterHide();

        Model = null;
    }

    protected virtual void BeforeHide() { }

    protected virtual void AfterHide() { }

    protected void AddSeparator(VisualElement visualElement)
    {
        visualElement.AddElement("separator");
    }
}