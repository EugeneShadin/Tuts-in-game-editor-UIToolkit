using UnityEngine;
using UnityEngine.UIElements;

public class DragManipulator : Manipulator
{
    private Vector3 _offset;
    private bool _isPressed;

    private readonly VisualElement _interactionTarget;
    private VisualElement _eventSource;

    /*
     * The constructor takes an optional VisualElement target.
     * It's useful if you want to drag a window only when interacting with a specific element like a header.
     */
    public DragManipulator(VisualElement interactionTarget = null)
    {
        _interactionTarget = interactionTarget;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        // If the optional target is null, fallback to the default target.
        _eventSource = _interactionTarget ?? target;

        _eventSource.RegisterCallback<PointerDownEvent>(OnPointerDown);
        _eventSource.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        _eventSource.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        _eventSource.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        _eventSource.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
        _eventSource.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }


    private void OnPointerDown(PointerDownEvent e)
    {
        _isPressed = true;

        // It saves the offset between the element centre and the mouse and prevents the window from jumping to the cursor.
        _offset = target.transform.position - e.position;

        _eventSource.CapturePointer(e.pointerId);
        e.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent e)
    {
        if (!_isPressed || !_eventSource.HasPointerCapture(e.pointerId))
        {
            return;
        }

        target.transform.position = e.position + _offset;

        ClampWindowPosition();
        e.StopPropagation();
    }


    private void OnPointerUp(PointerUpEvent e)
    {
        if (!_isPressed || !_eventSource.HasPointerCapture(e.pointerId))
        {
            return;
        }

        _isPressed = false;
        _eventSource.ReleasePointer(e.pointerId);
        e.StopPropagation();
    }

    public void ClampWindowPosition()
    {
        // Gets the document bounds — target.panel.visualTree is similar to transform.root.
        var documentBound = target.panel.visualTree.worldBound;
        var targetBound = target.worldBound;

        // Verifies the element’s position inside the document
        var clampedX = Mathf.Clamp(targetBound.x, documentBound.x, documentBound.xMax - targetBound.width);
        var clampedY = Mathf.Clamp(targetBound.y, documentBound.y, documentBound.yMax - targetBound.height);

        target.transform.position += (Vector3)(new Vector2(clampedX, clampedY) - targetBound.position);
    }
}