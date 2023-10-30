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
        Task CreatFeedAsync(FeedInformation list); // 創建
        Task DeleteFeedAsync(int id);  // 刪除 
        Task UpdataFeedAsync(FeedInformation list); // 編輯
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
            return await _context.FeedInformation.Where(e => e.IsDeleted != true).OrderByDescending(e=>e.CreationTime).ToListAsync();
        }
        public async Task CreatFeedAsync(FeedInformation list)
        {
            // 新增資料
            _context.FeedInformation.Add(list);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteFeedAsync(int id)
        {
            // 刪除資料---> 將IsDeleted欄位設為 true 
            var list = await _context.FeedInformation.FindAsync(id);
            if (list != null)
            {
                list.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdataFeedAsync(FeedInformation list)
        {
            // 更新資料
            _context.FeedInformation.Update(list);
            await _context.SaveChangesAsync();
        }
    }
}
