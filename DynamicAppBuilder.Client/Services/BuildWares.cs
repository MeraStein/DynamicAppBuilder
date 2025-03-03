using DynamicAppBuilder.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

public class BuildWares
{
    private readonly Globals _globals;
    private readonly ControlMediator _mediator;

    public BuildWares(Globals globals, ControlMediator mediator)
    {
        _globals = globals;
        _mediator = mediator;
    }

    public void AddButton(Coordinates coordinates, string type, string placeholder, string defaultValue, List<String> options)
    {
        if (coordinates == null)
            return;

        int index = _globals.Controls.Count();
        _globals.Controls.Add(builder =>
        {
            builder.OpenElement(0, "button");
            if (_globals.InEdit)
                builder.AddAttribute(1, "draggable", "true");
            builder.AddAttribute(2, "ondrag", EventCallback.Factory.Create<DragEventArgs>(this, _ => { _mediator.RemoveControl(index, type); _globals.IsNew = false; }));
            builder.AddAttribute(3, "onpointerdown", EventCallback.Factory.Create(this, () => _mediator.SelectControl(index)));
            if (!_globals.InEdit)
                builder.AddAttribute(4, "onmousedown", EventCallback.Factory.Create(this, () => { btnClicked(); }));
            builder.AddAttribute(5, "style", $"top: {coordinates.Y}px; left: {coordinates.X}px; width: 100%");
            builder.AddAttribute(6, "class", _globals.InEdit ? "control" : "control buttonInRun");
            builder.AddContent(7, _globals.InEdit ? type : defaultValue);
            builder.CloseElement();
        });

        addToProps(coordinates, type, placeholder, defaultValue, options);

        _mediator.SelectControl(index);
        _globals.IsNew = true;
    }

    public void AddTextInput(Coordinates coordinates, string type, string placeholder, string defaultValue, List<String> options)
    {
        if (coordinates == null)
            return;

        int index = _globals.Controls.Count();
        _globals.Controls.Add(builder => {
            builder.OpenElement(0, "input");
            if (_globals.InEdit)
                builder.AddAttribute(1, "draggable", "true");
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "ondrag", EventCallback.Factory.Create<DragEventArgs>(this, _ => { _mediator.RemoveControl(index, type); _globals.IsNew = false; }));
            builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create(this, () => _mediator.SelectControl(index)));
            builder.AddAttribute(5, "placeholder", _globals.InEdit ? type : placeholder);
            builder.AddAttribute(6, "style", $"top: {coordinates.Y}px; left: {coordinates.X}px;");
            builder.AddAttribute(7, "class", "control");
            if (_globals.InEdit)
                builder.AddAttribute(8, "disabled");
            builder.CloseElement();
        });

        addToProps(coordinates, type, placeholder, defaultValue, options);

        _mediator.SelectControl(index);
        _globals.IsNew = true;
    }

    public void AddNumberInput(Coordinates coordinates, string type, string placeholder, string defaultValue, List<String> options)
    {
        if (coordinates == null)
            return;

        int index = _globals.Controls.Count();
        _globals.Controls.Add(builder => {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "number");
            if (_globals.InEdit)
                builder.AddAttribute(2, "draggable", "true");
            builder.AddAttribute(3, "ondrag", EventCallback.Factory.Create<DragEventArgs>(this, _ => { _mediator.RemoveControl(index, type); _globals.IsNew = false; }));
            builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create(this, () => _mediator.SelectControl(index)));
            builder.AddAttribute(5, "placeholder", _globals.InEdit ? type : placeholder);
            builder.AddAttribute(7, "style", $"top: {coordinates.Y}px; left: {coordinates.X}px;");
            builder.AddAttribute(8, "class", "control");
            if (_globals.InEdit)
                builder.AddAttribute(9, "disabled");
            builder.CloseElement();
        });

        addToProps(coordinates, type, placeholder, defaultValue, options);

        _mediator.SelectControl(index);
        _globals.IsNew = true;
    }

    public void AddDropdown(Coordinates coordinates, string type, string placeholder, string defaultValue, List<String> options)
    {
        if (coordinates == null)
            return;

        int index = _globals.Controls.Count();
        _globals.Controls.Add(builder =>
        {
            builder.OpenElement(0, "div");
            if (_globals.InEdit)
                builder.AddAttribute(1, "draggable", "true");
            builder.AddAttribute(2, "ondrag", EventCallback.Factory.Create<DragEventArgs>(this, _ => { _mediator.RemoveControl(index, "DropDown"); _globals.IsNew = false; }));
            builder.AddAttribute(3, "style", $"top: {coordinates.Y}px; left: {coordinates.X}px; width: fit-content; display: inline-block;");
            builder.AddAttribute(4, "class", "control");

            builder.OpenElement(5, "select");
            builder.AddAttribute(7, "onpointerdown", EventCallback.Factory.Create(this, () => _mediator.SelectControl(index)));
            builder.AddAttribute(8, "ondragstart", "event.stopPropagation()");
            if (_globals.InEdit)
                builder.AddAttribute(9, "disabled", "true");
            else if (options.Count > 0)
            {
                int idx = 0;
                foreach (var opt in options)
                {
                    builder.OpenElement(idx, "option");
                    builder.AddAttribute(idx + 1, "value", opt);
                    builder.AddContent(idx + 2, opt);
                    builder.CloseElement();
                    idx += 3;
                }
            }

            builder.CloseElement();
            builder.CloseElement();
        });

        addToProps(coordinates, type, placeholder, defaultValue, options);

        _mediator.SelectControl(index);
        _globals.IsNew = true;
    }

    public void AddCheckBox(Coordinates coordinates, string type, string placeholder, string defaultValue, List<String> options)
    {
        if (coordinates == null)
            return;

        int index = _globals.Controls.Count();
        _globals.Controls.Add(builder =>
        {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "checkbox");
            if (_globals.InEdit)
                builder.AddAttribute(2, "draggable", "true");
            if (_globals.InEdit)
            if (_globals.InEdit)
            builder.AddAttribute(3, "ondrag", EventCallback.Factory.Create<DragEventArgs>(this, _ => { _mediator.RemoveControl(index, "CheckBox"); _globals.IsNew = false; }));
            builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create(this, () => _mediator.SelectControl(index)));
            builder.AddAttribute(5, "style", $"top: {coordinates.Y}px; left: {coordinates.X}px; width: 100%;");
            builder.AddAttribute(6, "class", "control");
            Console.WriteLine(defaultValue);
            if (_globals.InEdit)
                builder.AddAttribute(7, "disabled");
            builder.AddAttribute(7, "checked", defaultValue.ToLower() == "true");
            builder.CloseElement();
        });

        addToProps(coordinates, type, placeholder, defaultValue, options);

        _mediator.SelectControl(index);
        _globals.IsNew = true;
    }

    public void AddDatePicker(Coordinates coordinates, string type, string placeholder, string defaultValue, List<String> options)
    {
        if (coordinates == null)
            return;

        int index = _globals.Controls.Count();
        _globals.Controls.Add(builder =>
        {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "date");
            if (_globals.InEdit)
                builder.AddAttribute(2, "draggable", "true");
            builder.AddAttribute(3, "ondrag", EventCallback.Factory.Create<DragEventArgs>(this, _ => { _mediator.RemoveControl(index, "DatePicker"); _globals.IsNew = false; }));
            builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create(this, () => _mediator.SelectControl(index)));
            builder.AddAttribute(5, "style", $"top: {coordinates.Y}px; left: {coordinates.X}px;");
            builder.AddAttribute(7, "class", "control");
            if (_globals.InEdit)
                builder.AddAttribute(8, "disabled");
            else if (defaultValue != null && DateTime.TryParse(defaultValue, out DateTime date))
            {
                string formattedDate = date.ToString("yyyy-MM-dd");
                builder.AddAttribute(8, "value", formattedDate);
            }

            builder.CloseElement();
        });

        addToProps(coordinates, type, placeholder, defaultValue, options);

        _mediator.SelectControl(index);
        _globals.IsNew = true;
    }



    public void addToProps(Coordinates coords, string type, string placeholder, string defaultvalue, List<String> options)
    {
        if (!_globals.InDrop)
            return;

        var newControl = new ControlProperties();
        if (_globals.IsNew)
        {
            newControl = new ControlProperties
            {
                coordinates = coords,
                Type = type,
                Placeholder = placeholder,
                defaultValue = defaultvalue,
                Options = options,
            };
        }
        else
        {
            newControl = _globals.SelectedControl;
            newControl.coordinates = coords;
        }
        _globals.Props.Add(newControl);
    }


    public void btnClicked()
    {

    }
}