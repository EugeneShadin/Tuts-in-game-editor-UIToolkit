using UnityEngine.UIElements;

public abstract class AModalWindow<TModel> where TModel : AViewModel
{
    protected VisualElement Root { get; }

    private readonly VisualElement _windowContainer;
    protected VisualElement Window { get; }
    protected VisualElement Header { get; }
    protected VisualElement Body { get; }
    protected VisualElement Footer { get; }

    private Label _title;
    private VisualElement _minimizeIcon;

    protected TModel Model { get; private set; }

    protected AModalWindow(VisualElement rootVisualElement)
    {
        Root = rootVisualElement;

        _windowContainer = new VisualElement();
        _windowContainer.AddClass("modal-window-container", "modal-window-container-bg");

        Window = _windowContainer.AddElement().AddClass("modal-window");
        Header = Window.AddElement("header").AddClass("modal-window_header");
        _title = Header.AddLabel("Title");

        Header.AddButton(CloseClicked).AddClass(ButtonStyle.Danger)
            .AddElement().AddClass("x-mark-icon", "flex-grow");

        Body = Window.AddElement("body").AddClass("modal-window_body", "modal-window_content");
        Footer = Window.AddElement("footer").AddClass("modal-window_footer", "modal-window_content");
    }

    protected void SetTitle(string title)
    {
        _title.text = title;
    }

    private void CloseClicked()
    {
        Hide();
    }

    public void Show(TModel model)
    {
        Model = model;

        BeforeShow();
        Root.Add(_windowContainer);
    }

    protected virtual void BeforeShow() { }

    public void Hide()
    {
        BeforeHide();
        Root.Remove(_windowContainer);

        Model = null;
    }

    protected virtual void BeforeHide() { }

    protected void AddSeparator(VisualElement visualElement)
    {
        visualElement.AddElement("separator");
    }
}