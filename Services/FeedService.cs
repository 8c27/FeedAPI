using Microsoft.EntityFrameworkCore;
using FeedAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedAPI.Models;

namespace FeedAPI.Services
{
    public interface IFeedService
    {
        Task<List<FeedInformation>> GetFeedInformationAsync(); // 取的全部 FeedInformation 資料
    }
    public class FeedService : IFeedService
    {
        private readonly FeedingContext _context;
        public FeedService(FeedingContext context)
        {
           // 與資料庫連結必要步驟
            _context = context;
        }

        public async Task<List<FeedInformation>> GetFeedInformationAsync()
        {
            // 取未被刪除的資料 | IsDeleted = true 為刪除
            return await _context.FeedInformation.Where(e => e.IsDeleted != true).ToListAsync();
        }
    }
}
