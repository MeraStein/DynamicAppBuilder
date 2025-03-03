using DynamicAppBuilder.Client.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;


public class PropsService
{
    private readonly HttpClient _httpClient;
    private readonly Globals _globals;
    private readonly ControlMediator _mediator;
    private readonly Handles _handels;


    public PropsService(HttpClient httpClient, Globals globals, ControlMediator mediator, Handles handels)
    {
        _httpClient = httpClient;
        _globals = globals;
        _mediator = mediator;
        _handels = handels;
    }

    public async Task SavePropsAsync(Props props)
    {
        var json = JsonSerializer.Serialize(props);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/props/save", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to save design");
        }

        GetPropsAsync();
    }

    public async Task GetPropsAsync()
    {
        var response = await _httpClient.GetAsync("api/props");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch props");
        }

        var responseBody = await response.Content.ReadAsStringAsync();

        _globals.HistoryNames = JsonSerializer.Deserialize<List<Props>>(responseBody);
        _mediator.NotifyStateChanged();
    }

    public async Task GetPropsByNameAsync(string name)
    {
        var encodedName = Uri.EscapeDataString(name);
        var response = await _httpClient.GetAsync($"api/props/{encodedName}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch props");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var jsonNode = JsonNode.Parse(responseBody);

        try
        {
            if (jsonNode is JsonArray propsArray)
            {
                _globals.Props = new List<ControlProperties>();

                foreach (var item in propsArray)
                {
                    if (item != null)
                    {
                        _globals.Props.Add(new ControlProperties
                        {
                            coordinates = new Coordinates
                            {
                                X = item["coordinates"]?["x"]?.GetValue<double>() ?? 0,
                                Y = item["coordinates"]?["y"]?.GetValue<double>() ?? 0
                            },
                            Type = item["type"]?.ToString(),
                            Placeholder = item["placeholder"]?.ToString(),
                            defaultValue = item["defaultValue"]?.ToString(),
                            Options = item["type"]?.ToString() != "DropDown" ? null : item["options"]?.AsArray()?.Select(option => option.ToString()).ToList() ?? new List<string>(),
                        });
                    }
                }
                _globals.AppName = name;
                _handels.editState();
            }
            else
                Console.WriteLine("jsonNode is not JsonArray");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public bool isNameinHistory()
    {
        foreach(Props props in _globals.HistoryNames)
            if (props.name.Equals(_globals.AppName))
                return true;

        return false;
    }
}
