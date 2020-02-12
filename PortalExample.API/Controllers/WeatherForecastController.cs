using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalExample.API.Data;
using PortalExample.API.Models;

namespace PortalExample.API.Controllers
{
    //http://localhost:5000/api/WeatherForecast po takim adresem nasłuchuje kestrel
    [ApiController]
    [Route("api/[controller]")]
    public class ValueController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        //synchroniczny przychodzi żadanie request to wątek jest blokowany dopóki połączenie z bazą nie zostanie połączone i nie zostnaną pobrae
        //nie będzie wstanie obsłużyć innych żadań // lepiej już pomyśleć oskalowalności, bo możemy mieć tyciące zapytań od użytkowników i wtedy może być problem z pobraniem
        //asychnicziny wątek nie jest blokowanyc tylko tworzony nowy  wątek 
        private readonly ILogger<ValueController> _logger;
        private readonly DataContext _context;

        public ValueController(ILogger<ValueController> logger, DataContext context)
        {
            _context = context;

            _logger = logger;
        }

        // [HttpGet]
        // public IActionResult GetValues()
        // {
        //     var values = _context.Values.ToList();
        //     return Ok(values);
        // }
        [HttpGet]
        public async Task<IActionResult> GetValues()// Task reprezentuję asynchronicznośc
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id, [FromBody] Value value)
        {
            var data = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            data.Name = value.Name;
            
            await _context.SaveChangesAsync();
            return Ok(value);
        }
    }
}
