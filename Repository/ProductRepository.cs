﻿using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACS.Repository
{
    public class ProductRepository<T> : IProductRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Set<T>()
                .Include("BoNho")
                .Include("MauSac")
                .Include("Ram")
                .Include("ThuongHieu")
                .Include("HinhAnh")
                .Include("LoaiSanPham")
                .Include("BinhLuan")
                .Include("BinhLuan.Customer")
                .FirstOrDefaultAsync(t => ((SanPham)(object)t).MaSanPham == id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetSanPhamWithImg()
        {
            return await _context.Set<T>().Include("HinhAnh").ToListAsync();
        }

        public async Task<List<T>> SearchProductsAsync(string keyword)
        {
            try
            {
                // Thực hiện tìm kiếm sản phẩm theo từ khóa (ví dụ: Tên sản phẩm chứa từ khóa)
                return await _context.Set<T>()
                                     .Where(p => ((SanPham)(object)p).TenSanPham.Contains(keyword))
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching products: {ex.Message}");
            }
        }


        // Các phương thức khác được triển khai tương tự

        public Task<List<T>> GetSanPhamByBranch(string branch)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetSanPhamByColor(string color)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetSanPhamByType(string type)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(T[] entities)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
