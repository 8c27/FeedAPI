using FeedAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FeedAPI.Services
{
    public interface IClientService
    {
        Task<List<ClientInformation>> GetClientInformationAsync(); // 取全部 Client資料
        Task CreatClientAsync(ClientInformation list);
        Task DeleteClientAsync(long id );
        Task UpdateClientAsync(ClientInformation list);
       
    }
    public class ClientService : IClientService
    {
        private readonly FeedingContext _context;
        public ClientService(FeedingContext context)
        {
            _context = context;
        }
        public async Task<List<ClientInformation>> GetClientInformationAsync()
        {
            return await _context.ClientInformation.Where(e => e.IsDeleted != true).ToListAsync();
        }
        public async Task CreatClientAsync(ClientInformation list)
        {
            _context.ClientInformation.Add(list);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteClientAsync(long id)
        {
            var list = await _context.ClientInformation.FindAsync(id);
            if (list != null)
            {
                list.IsDeleted = true;
                await _context.SaveChangesAsync();  
            }
        }
        public async Task UpdateClientAsync(ClientInformation list)
        {
            _context.ClientInformation.Update(list);
            await _context.SaveChangesAsync();
        }
    }
}
