using FeedAPI.Models;
using FeedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IHubContext<ChatHub> _hubContext;
        public ClientController(IClientService clientService, IHubContext<ChatHub> hubContext)
        {
            _clientService = clientService;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<ClientInformation>>> GetClientInformatio()
        {
            var lists = await _clientService.GetClientInformationAsync();
            return Ok(lists);
        }
        [HttpPost]
        public async Task<ActionResult<List<ClientInformation>>> CreatClient(ClientInformation list)
        {
            await _clientService.CreatClientAsync(list);
            var lists = await _clientService.GetClientInformationAsync();
            await _hubContext.Clients.All.SendAsync("ClientChange", lists);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ClientInformation>>> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            var lists = await _clientService.GetClientInformationAsync(); 
            await _hubContext.Clients.All.SendAsync("ClientChange", lists);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<ClientInformation>>> UpdateClient(int id , ClientInformation list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }
            await _clientService.UpdateClientAsync(list);
            var lists = await _clientService.GetClientInformationAsync();
            await _hubContext.Clients.All.SendAsync("ClientChange", lists);
            return Ok();
        }
    }


}