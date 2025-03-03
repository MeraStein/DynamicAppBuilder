using Microsoft.AspNetCore.Components;
using DynamicAppBuilder.Client.Models;

public class Globals
{
    public string AppName = null;
    public List<RenderFragment> Controls = new List<RenderFragment>();
    public List<ControlProperties> Props = new List<ControlProperties>();
    public string DraggedControlId = "";
    public ControlProperties SelectedControl = null;
    public bool IsNew  = true;
    public bool isAddingOption  = false;
    public string NewOption;
    public bool InEdit = true;
    public bool InDrop = false;
    public List<Props> HistoryNames = new List<Props>();
    public string SelectedProp;
}
