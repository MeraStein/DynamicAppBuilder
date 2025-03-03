using DynamicAppBuilder.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json.Nodes;

public class Handles
{
    private readonly Globals _globals;
    private readonly BuildWares _buildWares;
    private readonly ControlMediator _mediator;
    private readonly IJSRuntime _jsRuntime;

    public Handles(Globals globals, BuildWares buildWares, ControlMediator mediator, IJSRuntime jsRuntime)
    {
        _globals = globals;
        _buildWares = buildWares;
        _mediator = mediator;
        _jsRuntime = jsRuntime;
    }


    public void HandleDrop(string elementId, Coordinates coordinates)
    {
        _globals.InDrop = true;

        string placeholder = "";
        string defaultValue = "";
        List<String> options = [];

        switch (elementId)
        {
            case "Button":
                placeholder = null;
                defaultValue = elementId;
                options = null;
                break;
            case "TextInput":
                placeholder = "Enter Text...";
                defaultValue = "default";
                options = null;
                break;
            case "NumberInput":
                placeholder = "Enter Number...";
                defaultValue = "0";
                options = null;
                break;
            case "DropDown":
                placeholder = null;
                defaultValue = "Add Options...";
                options = [];
                break;
            case "CheckBox":
                placeholder = null;
                defaultValue = "false";
                options = null;
                break;
            case "DatePicker":
                placeholder = null;
                defaultValue = "24-03-2000";
                options = null;
                break;
            default:
                Console.WriteLine($"Unknown element: {elementId}");
                break;
        }
        addWares(coordinates, elementId, placeholder, defaultValue, options);

        _globals.InDrop = false;
    }

    public void addWares(Coordinates coordinates, string type, string placeholder, string defaultValue, List<string> options)
    {
        switch (type)
        {
            case "Button":
                _buildWares.AddButton(coordinates, type, placeholder, defaultValue, options);
                break;
            case "TextInput":
                _buildWares.AddTextInput(coordinates, type, placeholder, defaultValue, options);
                break;
            case "NumberInput":
                _buildWares.AddNumberInput(coordinates, type, placeholder, defaultValue, options);
                break;
            case "DropDown":
                _buildWares.AddDropdown(coordinates, type, placeholder, defaultValue, options);
                break;
            case "CheckBox":
                _buildWares.AddCheckBox(coordinates, type, placeholder, defaultValue, options);
                break;
            case "DatePicker":
                _buildWares.AddDatePicker(coordinates, type, placeholder, defaultValue, options);
                break;
            default:
                Console.WriteLine($"Unknown element: {type}");
                break;
        }
    }

    public void HandleChange(ChangeEventArgs e, string fieldName, string type)
    {
        foreach (var field in _globals.SelectedControl.GetType().GetProperties())
        {
            if (field.Name == fieldName)
            {
                string convertedValue = e.Value.ToString();
                //switch (type)
                //{
                //    case "NumberInput":
                //    case "TextInput":
                //    case "Button":
                //    case "DropDown":
                //        convertedValue = e.Value?.ToString();
                //        break;
                //    case "CheckBox":
                //        convertedValue = (e.Value?.ToString() == "on").ToString();
                //        break;
                //    case "DatePicker":
                //        convertedValue = e.Value?.ToString();
                //        break;
                //}
                field.SetValue(_globals.SelectedControl, convertedValue);
            }
        }
        _mediator.NotifyStateChanged();
    }

    public void setInEdit(bool val)
    {
        _globals.InEdit = val;
    }

    public void editState()
    {
        setInEdit(true);
        _globals.Controls = new List<RenderFragment>();

        foreach (ControlProperties prop in _globals.Props)
        {
            if (prop != null)
                addWares(prop.coordinates, prop.Type, prop.Placeholder, prop.defaultValue, prop.Options);
            else
                _globals.Controls.Add(null);
        }

        _globals.SelectedControl = null;
        _mediator.NotifyStateChanged();
    }

    public void runState()
    {
        setInEdit(false);
        _globals.Controls = new List<RenderFragment>();

        foreach (ControlProperties prop in _globals.Props)
        {
            if (prop != null)
                addWares(prop.coordinates, prop.Type, prop.Placeholder, prop.defaultValue, prop.Options);
        }
        _mediator.NotifyStateChanged();
    }

    public void OnDragStartTI(DragEventArgs e)
    {
        _globals.IsNew = true;
        _globals.DraggedControlId = "TextInput";
        Console.WriteLine("Dragging " + _globals.DraggedControlId);
    }

    public void OnDragStartNI(DragEventArgs e)
    {
        _globals.IsNew = true;
        _globals.DraggedControlId = "NumberInput";
        Console.WriteLine("Dragging " + _globals.DraggedControlId);
    }

    public void OnDragStartB(DragEventArgs e)
    {
        _globals.IsNew = true;
        _globals.DraggedControlId = "Button";
        Console.WriteLine("Dragging " + _globals.DraggedControlId);
    }

    public void OnDragStartD(DragEventArgs e)
    {
        _globals.IsNew = true;
        _globals.DraggedControlId = "DropDown";
        Console.WriteLine("Dragging " + _globals.DraggedControlId);
    }

    public void OnDragStartCB(DragEventArgs e)
    {
        _globals.IsNew = true;
        _globals.DraggedControlId = "CheckBox";
        Console.WriteLine("Dragging " + _globals.DraggedControlId);
    }

    public void OnDragStartDT(DragEventArgs e)
    {
        _globals.IsNew = true;
        _globals.DraggedControlId = "DatePicker";
        Console.WriteLine("Dragging " + _globals.DraggedControlId);
    }

    public async Task OnDrop(DragEventArgs e)
    {
        var coordinates = await _jsRuntime.InvokeAsync<Coordinates>("getCoordinates", e);
        HandleDrop(_globals.DraggedControlId, coordinates);
        _mediator.NotifyStateChanged();
    }

    public async Task OnDragOver(DragEventArgs e)
    {
        await _jsRuntime.InvokeVoidAsync("preventDefaultDrag");
    }

    public void RemoveControl(int index, string type)
    {
        _mediator.RemoveControl(index, type);
        _mediator.NotifyStateChanged();
    }

    public void ClearCanvas()
    {
        _globals.Controls = new List<RenderFragment>();
        _globals.Props = new List<ControlProperties>();
        _globals.DraggedControlId = "";
        _globals.SelectedControl = null;
        _mediator.NotifyStateChanged();
    }

    public void SelectControl(int index)
    {
        _mediator.SelectControl(index);
        _mediator.NotifyStateChanged();
    }

    public void UpdateValue(int index, string newValue)
    {
        _globals.Props[index].defaultValue = newValue;
        _mediator.NotifyStateChanged();
    }

    public void AddOptionInput()
    {
        _globals.isAddingOption = true;
        _mediator.NotifyStateChanged();
    }

    public void SaveOption()
    {
        if (!string.IsNullOrEmpty(_globals.NewOption) && _globals.SelectedControl != null)
            _globals.SelectedControl.Options.Add(_globals.NewOption);

        _globals.NewOption = string.Empty;
        _globals.isAddingOption = false;
        _mediator.NotifyStateChanged();
    }

    public void CancelAdding()
    {
        _globals.NewOption = string.Empty;
        _globals.isAddingOption = false;
        _mediator.NotifyStateChanged();
    }
}