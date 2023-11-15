﻿using FeedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using FeedAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IHubContext<ChatHub> _hubContext;

        public StockController(IStockService stockService, IHubContext<ChatHub> hubContext)
        {
            _stockService = stockService;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetStock()
        {
            var lists = await _stockService.GetStocksAsync();
            return Ok(lists);
        }

        [HttpPost]
        public async Task<ActionResult<List<Stock>>> CreatFeed(Stock list)
        {
            await _stockService.CreatStockAsync(list);
            var lists = await _stockService.GetStocksAsync();
            await _hubContext.Clients.All.SendAsync("StockChange", lists);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Stock>>> DeleteFeed(int id)
        {
            await _stockService.DeleteStockAsync(id);
            var lists = await _stockService.GetStocksAsync();
            await _hubContext.Clients.All.SendAsync("StockChange", lists);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Stock>>> UpdateFeed(int id, Stock list)
        {
            if (id != list.Id)
                return BadRequest();
            await _stockService.UpdateStockAsync(list);
            var lists = await _stockService.GetStocksAsync();
            await _hubContext.Clients.All.SendAsync("StockChange", lists);
            return Ok();
        }

    }
}