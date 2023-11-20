using FeedAPI.Models;
using FeedAPI.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FeedAPI.Services
{


    public interface IStockService
    {
        Task<List<Stock>> GetStocksAsync();
        Task CreatStockAsync (StockInformation list);    // 新增
        Task DeleteStockAsync (int id);  // 刪除
        Task UpdateStockAsync (StockInformation list);  // 編輯

    }
    public class StockService : IStockService
    {
        private readonly FeedingContext _context;
        public StockService(FeedingContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetStocksAsync()
        {
            return await _context.StockInformation.Where(e => e.IsDeleted != true)
            .Select( e => new Stock
            {
                Id = e.Id,
                UpdateTime =e.UpdateTime,
                FinishAmount = e.FinishAmount,
                Weight = e.Weight, 
                IsDeleted = e.IsDeleted,
                StockName = e.StockName,
                Feed = e.FeedInformation.Where(s=>s.IsDeleted!=true).ToList(),
            })
            .ToListAsync();
        }
        public async Task DeleteStockAsync (int id)
        {
            var lists = await _context.StockInformation.FindAsync(id);
            if (lists != null)
            {
                lists.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreatStockAsync (StockInformation list)
        {
            _context.StockInformation.Add(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(StockInformation list)
        {
            _context.StockInformation.Update(list);
            await _context.SaveChangesAsync();
        }
    }
}
