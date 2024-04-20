using DACS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS.MyViewComponent
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public SliderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var slider = _context.HINHANHQUANGCAO.ToList();
            return View(slider);
        }
    }
}
