using Application.DTO.sells;
using Application.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Sells : ISells
    {
        private readonly AppDbContext _db;
        public Sells(AppDbContext db) 
        {
            _db = db;
        }
        public async Task<SellsDto> LoadSells(int start, int length)
        {
            var data =await _db.Database.SqlQuery<SellsList>($@"SELECT [Id],[Name],[Description]FROM  [sells]")
                .Skip(start)
                .Take(length)
                .ToListAsync();
            var total =await _db.sells.CountAsync();
            var filtertotal = data.Count();
            var records = new SellsDto
            { 
            data = data,
            recordsFiltered = filtertotal,
            recordsTotal = filtertotal,  
            };
            return records;
            
        }
    }
}
