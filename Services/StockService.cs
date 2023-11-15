using FeedAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace FeedAPI.Services
{


    public interface IStockService
    {
        Task<List<Stock>> GetStocksAsync();
        Task CreatStockAsync (Stock list);    // 新增
        Task DeleteStockAsync (int id);  // 刪除
        Task UpdateStockAsync (Stock list);  // 編輯

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
            return await _context.Stock.Where(e => e.IsDeleted != true).ToListAsync();
        }
        public async Task DeleteStockAsync (int id)
        {
            var lists = await _context.Stock.FindAsync(id);
            if (lists != null)
            {
                lists.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreatStockAsync (Stock list)
        {
            _context.Stock.Add(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(Stock list)
        {
            _context.Stock.Update(list);
            await _context.SaveChangesAsync();
        }
    }
}
