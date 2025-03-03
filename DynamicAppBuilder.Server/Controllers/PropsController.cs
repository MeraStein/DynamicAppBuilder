using Microsoft.AspNetCore.Mvc;
using DynamicAppBuilder.Server.Data;
using DynamicAppBuilder.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

[Route("api/props")]
[ApiController]
public class PropsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PropsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveProp([FromBody] PropsInput propsInput )
    {
        try
        {
            if (propsInput == null)
            {
                return BadRequest("הנתונים שהתקבלו אינם תקינים");
            }

            Props props = await _context.Props.FirstOrDefaultAsync(p => p.name == propsInput.name);
            if (props == null)
            {
                props = new Props() { name = propsInput.name };
                _context.Props.Add(props);
                await _context.SaveChangesAsync();
            }
            else
            {
                var controlsToDelete = await _context.ControlProperties.Where(p => p.PropId == props.Id).ToListAsync();
                if (controlsToDelete.Any())
                {
                    _context.ControlProperties.RemoveRange(controlsToDelete);
                    await _context.SaveChangesAsync();
                }
            }

                List<ControlProperties> controlProperties = new List<ControlProperties>();
            foreach (ControlPropertiesInput cp in propsInput.ControlsProperties)
            {
                if(cp != null)
                    _context.ControlProperties.Add(new ControlProperties
                    {
                        coordinates = cp.coordinates,
                        Type = cp.Type,
                        Placeholder = cp.Placeholder,
                        defaultValue = cp.defaultValue,
                        Options = cp.Options,
                        PropId = props.Id
                    });
            }

            await _context.SaveChangesAsync();
            return Ok(new { PropsId = props.Id });
        } catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetPropByName(string name)
    {
        var prop = await _context.Props.FirstOrDefaultAsync(p => p.name == name);
        if (prop == null) return NotFound();

        var controlProperties = await _context.ControlProperties
        .Where(cp => cp.PropId == prop.Id)
        .ToListAsync();

        return Ok(controlProperties);
    }

    [HttpGet]
    public async Task<IActionResult> GetProps()
    {
        var props = await _context.Props.ToListAsync();  
        if (props == null) return NotFound();

        return Ok(props);
    }

}
