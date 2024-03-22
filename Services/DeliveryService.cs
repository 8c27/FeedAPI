using FeedAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FeedAPI.Services
{
    public interface IDeliveryService
    {
        Task<List<DeliveryAddress>> GetDataAsync();
        Task CreatDataAsync(DeliveryAddress list);
        Task DeleteDataAsync(int id);
        Task EditDataAsync(DeliveryAddress list);
    }
    public class DeliveryService : IDeliveryService
    {
        private readonly FeedingContext _context;
        public DeliveryService(FeedingContext context)
        {
            _context = context;
        }
        public async Task<List<DeliveryAddress>> GetDataAsync()
        {
            return await _context.DeliveryAddress.Where(e => e.Status != true).ToListAsync();
        }

        public async Task CreatDataAsync(DeliveryAddress list)
        {
            _context.DeliveryAddress.Add(list);
             await _context.SaveChangesAsync();
        }
        public async Task DeleteDataAsync(int id)
        {
            var list = await _context.DeliveryAddress.FindAsync(id);
            if (list != null)
            {
                list.Status = true;
                await _context.SaveChangesAsync();  
            }
        }

        public async Task EditDataAsync(DeliveryAddress list)
        {
            _context.DeliveryAddress.Update(list);
            await _context.SaveChangesAsync();  
        }
    }
}
