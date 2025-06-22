using System;
using UnityEngine.UIElements;

public class DialogModalWindowModel : AViewModel
{
    public string Title { get; }
    public string Message { get; }

    public Action<bool> ResultCallback { get; }

    public string OkText { get; }
    public string CancelText { get; }

    public bool Result { get; private set; }

    public DialogModalWindowModel(string title, string message, Action<bool> resultCallbackCallback, string okText = "OK", string cancelText = "Cancel")
    {
        Title = title;
        Message = message;

        ResultCallback = resultCallbackCallback;

        OkText = okText;
        CancelText = cancelText;
    }

    public void SendResult()
    {
        ResultCallback?.Invoke(Result);
    }

    public void SetResult(bool result)
    {
        Result = result;
    }
}

public class DialogModalWindow : AModalWindow<DialogModalWindowModel>
{
    private readonly Label _messageLabel;
    private readonly Button _okButton;
    private readonly Button _cancelButton;

    public DialogModalWindow(VisualElement rootVisualElement) : base(rootVisualElement)
    {
        Window.AddClass("modal-window-compact");

        _messageLabel = Body.AddLabel();
        _messageLabel.AddClass("text-center");

        _okButton = Footer.AddButton(OkClicked).AddClass(ButtonStyle.Success);
        _cancelButton = Footer.AddButton(Hide).AddClass(ButtonStyle.Danger, "display-none");
    }

    private void OkClicked()
    {
        Model.SetResult(true);
        Hide();
    }

    protected override void BeforeShow()
    {
        SetTitle(Model.Title);
        _messageLabel.text = Model.Message;
        _okButton.text = Model.OkText;
        _cancelButton.text = Model.CancelText;
        _cancelButton.EnableInClassList("display-none", string.IsNullOrEmpty(Model.CancelText));
    }

    protected override void BeforeHide()
    {
        Model.SendResult();
    }
}