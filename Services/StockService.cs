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
        Task DeleteStockAsync (long id);  // 刪除
        Task UpdateStockAsync (StockInformation list);  // 編輯
        Task EditStockAmountAsync(StockInformation list, int quantity);  // 庫存數量調整
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
                ClientId = e.ClientId,
                ClientName = e.Client.Name,
                Material = e.Material,
                Size = e.Size,
                Pcs = e.Pcs,
                Cost = e.Cost,
                Raise = e.Raise,
                Class = e.Class,
                Peel1 = e.Peel1,
                Peel2 = e.Peel2,
                Typing = e.Typing,
                Chamfer = e.Chamfer,
                Hole1 = e.Hole1,
                Hole2 = e.Hole2,
                Ditch = e.Ditch,
                Taper = e.Taper,
                Ear = e.Ear,
                Special = e.Special,
                Mm = e.Mm,
                Place = e.Place,
                Project = e.Project,
                Omi = e.Omi,
                Feed = e.FeedInformation.Select(e => new FeedInformation
                {
                    Id = e.Id,
                    CreationTime = e.CreationTime,
                    ClientId = e.ClientId,
                    Quantity = e.Quantity,
                    Description = e.Description,
                    Machine = e.Machine,
                    FeedNumber = e.FeedNumber,
                    IsDeleted = e.IsDeleted,
                    StockId = e.StockId,
                    Status = e.Status,
                    Weight = e.Weight,
                    Raw = e.Raw,
                }).Where(s=>s.Status!=true).ToList(),
            })
            .ToListAsync();
        }
        public async Task DeleteStockAsync (long id)
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

        public async Task EditStockAmountAsync(StockInformation list, int quantity)
        {
            var stock = _context.StockInformation.Where(e => e.StockName == list.StockName).FirstOrDefault();
            stock.FinishAmount = stock.FinishAmount + quantity;
            await _context.SaveChangesAsync();
        }
    }
}
