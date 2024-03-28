using FeedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using FeedAPI.Models;
using Microsoft.AspNetCore.SignalR;
using FeedAPI.VMs;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedController : ControllerBase 
    {
        private readonly IFeedService _feedService;
        private readonly IHubContext<ChatHub> _hubContext;

        public FeedController(IFeedService feedService, IHubContext<ChatHub> hubContext)
        {
            _feedService = feedService;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<FeedInformation>>> GetFeedInformation()
        {
            var lists = await _feedService.GetFeedInformationAsync();
            await _hubContext.Clients.All.SendAsync("FeedChange", lists);
            return Ok(lists); 
        }

        [HttpPost]
        public async Task<ActionResult<List<Feed>>> CreatFeed(Feed list)
        {
            await _feedService.CreatFeedAsync(list);
            var lists = await _feedService.GetFeedInformationAsync();
            await _hubContext.Clients.All.SendAsync("FeedChange", lists);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<FeedInformation>>> DeleteFeed(long id)
        {
            await _feedService.DeleteFeedAsync(id);
            var lists = await _feedService.GetFeedInformationAsync();
            await _hubContext.Clients.All.SendAsync("FeedChange", lists);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<FeedInformation>>> UpdateFeed(long id, FeedInformation list)
        {
            if (id != list.Id)
                return BadRequest();
            await _feedService.UpdataFeedAsync(list);
            var lists = await _feedService.GetFeedInformationAsync();
            await _hubContext.Clients.All.SendAsync("FeedChange", lists);
            return Ok();
        }

    }
}
