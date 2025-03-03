
public class ControlMediator
{
    private readonly Globals _globals;
    public event Action StateChanged;

    public void RegisterStateChanged(Action onStateChanged)
    {
        StateChanged = onStateChanged;
    }

    public ControlMediator(Globals globals)
    {
        _globals = globals;
    }


    public void SelectControl(int index)
    {
        _globals.SelectedControl = _globals.Props[index];
        NotifyStateChanged();
    }

    public void RemoveControl(int index, String type)
    {
        _globals.DraggedControlId = type;
        SelectControl(index);
        _globals.Controls[index] = null;
        _globals.Props[index] = null;
        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        StateChanged?.Invoke();
    }
}