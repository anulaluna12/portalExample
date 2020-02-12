using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ILogger<ValueController> _logger;
        private readonly DataContext _context;

        public ValueController(ILogger<ValueController> logger, DataContext context)
        {
            _context = context;

            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetValues()
        {
            var values = _context.Values.ToList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
            var value = _context.Values.FirstOrDefault(x => x.Id == id);
            return Ok(value);
        }
        [HttpPost]
        public IActionResult AddValue([FromBody] Value value)
        {
            _context.Values.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }
        [HttpPut("{id}")]
        public IActionResult EditValue(int id, [FromBody] Value value)
        {
            var data = _context.Values.FirstOrDefault(x => x.Id == id);
            data.Name = value.Name;

            _context.SaveChanges();
            return Ok(value);
        }
    }
}
