using DemoRestApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoRestApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        static private List<ClientDto> clients = new List<ClientDto>
        {
            new ClientDto
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                Phone = "123456789",
                Address = "123 Main St, Cityville",
                CompanyName = "Doe Enterprises"
            },
            new ClientDto
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "janesmith@",
                Phone = "987654321",
                Address = "456 Elm St, Townsville",
                CompanyName = "Smith & Co."
            }
        };

        // GET api/v1/client
        [HttpGet]
        public ActionResult<List<ClientDto>> GetAllClients()
        {
            return Ok(clients);
        }

        // GET api/v1/client/1
        [HttpGet("{id}")]
        public ActionResult<ClientDto> GetClientById(int id)
        {
            var client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // POST api/v1/client
        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientDto client)
        {
            if (client == null || string.IsNullOrEmpty(client.Name))
            {
                return BadRequest("Invalid client data.");
            }
            client.Id = clients.Max(c => c.Id) + 1;
            clients.Add(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        // PUT api/v1/client/1
        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] ClientDto updatedClient)
        {
            if (updatedClient == null || string.IsNullOrEmpty(updatedClient.Name))
            {
                return BadRequest("Invalid client data.");
            }
            var client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            client.Name = updatedClient.Name;
            client.Email = updatedClient.Email;
            client.Phone = updatedClient.Phone;
            client.Address = updatedClient.Address;
            client.CompanyName = updatedClient.CompanyName;
            return NoContent();
        }

        // DELETE api/v1/client/1
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            clients.Remove(client);
            return NoContent();
        }
    }
}
