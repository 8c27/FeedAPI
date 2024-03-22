using FeedAPI.Models;
using FeedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using System.Formats.Asn1;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController: ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IHubContext<ChatHub> _hubContext;

        public DeliveryController(IDeliveryService deliveryService, IHubContext<ChatHub> hubContext)
        {
            _deliveryService = deliveryService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeliveryAddress>>> GetData()
        {
            var lists = await _deliveryService.GetDataAsync();
            return Ok(lists);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryAddress>> CreatData(DeliveryAddress list)
        {
            await _deliveryService.CreatDataAsync(list);
            var lists = await _deliveryService.GetDataAsync();
            await _hubContext.Clients.All.SendAsync("DeliveryChange", lists);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeliveryAddress>> DeleteData(int id)
        {
            await _deliveryService.DeleteDataAsync(id);
            var lists = await _deliveryService.GetDataAsync();
            await _hubContext.Clients.All.SendAsync("DeliveryChange", lists);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryAddress>> EditData(int id, DeliveryAddress list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }
            await _deliveryService.EditDataAsync(list);
            var lists = await _deliveryService.GetDataAsync();
            await _hubContext.Clients.All.SendAsync("DeliveryChange", lists);
            return Ok();
        }
    }
}
