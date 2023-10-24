using FeedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using FeedAPI.Models;


namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedController : ControllerBase 
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }
        [HttpGet]
        public async Task<ActionResult<List<FeedInformation>>> GetFeedInformation()
        {
            var lists = await _feedService.GetFeedInformationAsync();
            return Ok(lists); 
        }



    }
}
