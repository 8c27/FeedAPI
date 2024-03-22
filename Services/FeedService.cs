﻿using Microsoft.EntityFrameworkCore;
using FeedAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedAPI.Models;
using FeedAPI.VMs;

namespace FeedAPI.Services
{
    public interface IFeedService
    {
        Task<List<Feed>> GetFeedInformationAsync(); // 取的全部 FeedInformation 資料
        Task CreatFeedAsync(Feed list); // 創建
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

        public async Task<List<Feed>> GetFeedInformationAsync()
        {
            // 取未被刪除的資料 | IsDeleted = true 為刪除
            return await _context.FeedInformation.Where(e => e.IsDeleted != true).OrderByDescending(e=>e.CreationTime)
                .Select(e=> new Feed
                {
                    Id= e.Id,
                    CreationTime = e.CreationTime,
                    ClientId= e.ClientId,
                    ClientName=e.Client.Name,    
                    Quantity = e.Quantity,           
                    Description = e.Description,   
                    Machine = e.Machine,
                    FeedNumber = e.FeedNumber,             
                    IsDeleted = e.IsDeleted,
                    StockId = e.StockId,
                    StockName = e.Stock.StockName,
                    Status = e.Status,
                    Weight = e.Weight,
                    Raw = e.Raw,
                }
                
                ).ToListAsync();
        }
      
        public async Task CreatFeedAsync(Feed e)
        {
            var client = _context.ClientInformation.Where(s=>s.Id==e.ClientId).FirstOrDefault();
            var number = client.Number;
            var feed_number = _context.FeedInformation.Where(s=>s.ClientId==e.ClientId).OrderByDescending(s =>s.Id).Select(s=>s.FeedNumber).FirstOrDefault();
            var ROCyear = DateTime.Now;

            if ( feed_number!=null)
            {              
                var yearmonth = feed_number.Substring(feed_number.Length - 9, 6);
                if (ROCyear.Year.ToString() + ROCyear.Month.ToString().PadLeft(2, '0')== yearmonth)
                {
                    var last = Int32.Parse(feed_number.Substring(feed_number.Length - 3));
                    feed_number = number + ROCyear.Year.ToString() + ROCyear.Month.ToString() + (last + 1).ToString().PadLeft(3, '0'); ;
                }
                else
                {
                    var last = "001";
                    feed_number = number + ROCyear.Year.ToString() + ROCyear.Month.ToString().PadLeft(2, '0') + last;
                }
                
            }
            else
            {
                var last = "001";
                feed_number = number+ROCyear.Year.ToString() + ROCyear.Month.ToString().PadLeft(2, '0') + last;
            }
         
            e.FeedNumber = feed_number;

            var feed = new FeedInformation
            {
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
            };
            // 新增資料
            _context.FeedInformation.Add(feed);
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
            var stock = _context.StockInformation.Where(e => e.Id == list.StockId).FirstOrDefault();
            var oldlist = _context.FeedInformation.AsNoTracking().Where(e => e.Id == list.Id).FirstOrDefault();
            if (list.Status == true)
            {
                if(oldlist.Status == false)
                {
                    stock.FinishAmount = (int?)(stock.FinishAmount - list.Quantity < 0 ? 0 : stock.FinishAmount - list.Quantity);
                }
            }
            else
            {
                if (oldlist.Status == true)
                {
                    stock.FinishAmount = (int?)(stock.FinishAmount + list.Quantity);
                }
            }
             _context.FeedInformation.Update(list);
             await _context.SaveChangesAsync();
            
        }
    }
}
