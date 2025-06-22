using UnityEngine.UIElements;

public class UIToolbar
{
    private const string HORIZONTAL = "toolbar-horizontal";
    private const string VERTICAL = "toolbar-vertical";

    private const string PRIMARY = "toolbar-primary";
    private const string SECONDARY = "toolbar-secondary";

    private readonly UIToolbarGroup _rootGroup;
    private readonly string _orientation;

    public UIToolbar(VisualElement rootVisualElement, bool vertical = false, bool primary = true)
    {
        _orientation = vertical ? VERTICAL : HORIZONTAL;
        var color = primary ? PRIMARY : SECONDARY;

        _rootGroup = new UIToolbarGroup(rootVisualElement, null
            , _orientation, color);
    }

    public UIToolbarGroup AddGroup(string groupName = null)
    {
        return new UIToolbarGroup(_rootGroup.Root, groupName, _orientation);
    }
}
