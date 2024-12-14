using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using PersonApi.Data;
using PersonApi.Models;
using System.Xml.Linq;

namespace PersonApi.Controllers
{
    [Route("api/customers")] // Ruta de acceso a este controlador
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ApplicationDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Name) || string.IsNullOrWhiteSpace(userDTO.PhoneNumber))
            {
                return BadRequest("Name and PhoneNumber are required");
            }

            // Mapeo del DTO al modelo de base de datos
            var user = new User
            {
                Name = userDTO.Name,
                PhoneNumber = userDTO.PhoneNumber
            };

            // Creación usuario en base de datos
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Simulación del envío de un mensaje a los logs
            var logMessage = $"Mensaje de bienvenida enviado a {user.Name} al número {user.PhoneNumber}";

            _logger.LogInformation(logMessage.ToString());

            return Ok(new { Message = "Usuario creado correctamente.", LogMessage = logMessage });
        }
    }
}
