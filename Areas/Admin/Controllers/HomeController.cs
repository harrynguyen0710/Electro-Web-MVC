﻿using DACS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int year = 2024;
            var ordersIYear = _context.DONHANG.Where(dh => dh.NgayLapDonHang.Year == year);

            var totalRevenue = await _context.DONHANG.SumAsync(dh => dh.TongGiaTriDonHang);

            var totalOrders = await _context.DONHANG.Where(d => d.NgayLapDonHang.Year == year).CountAsync();

            ViewBag.TotalRevenue = totalRevenue.ToString("N0") + " VND";
            ViewBag.TotalOrder = totalOrders;
            ViewBag.Year = 2024; 
            return View();
        }

        public IActionResult ProductsView()
        {
            return View();  
        }
        public IActionResult DeviceView()
        {
            return View();
        }
        public async Task<JsonResult> GetMonthlyRevenueData(int year)
        {
            var monthlyData = await _context.DONHANG
                .Where(d => d.NgayLapDonHang.Year == year)
                .GroupBy(d => d.NgayLapDonHang.Month)
                .Select(g => new {
                    Month = g.Key,
                    Total = g.Sum(d => d.TongGiaTriDonHang)
                })
                .OrderBy(x => x.Month)
                .ToListAsync();

            return Json(monthlyData);
        }

    }
}
