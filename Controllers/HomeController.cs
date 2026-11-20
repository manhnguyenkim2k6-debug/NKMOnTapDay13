using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NguyenKimManh_241230790.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace NguyenKimManh_241230790.Controllers
{
    public class HomeController : Controller
    {
        private QlHangHoaContext _Context;
        public HomeController(QlHangHoaContext context)
        {
            _Context = context;
        }
        public async Task<IActionResult> KiemTraKetNoi()
        {
            var danhsach = await _Context.LoaiHangs.ToListAsync();
            return Json(danhsach.Select(x => new { x.MaLoai, x.TenLoai }));
        }
        public async Task<IActionResult> Index()
        {
            var loaiHang = await _Context.LoaiHangs.ToListAsync();
            ViewBag.Loaihang = loaiHang;
            var hangHoa = await _Context.HangHoas.Where(x => x.Gia >= 100).ToListAsync();
            return View(hangHoa);
        }
        public async Task<IActionResult> LocTheoLoai(int maLoai)
        {
            var hangHoa = await _Context.HangHoas
                .Where(x => x.MaLoai == maLoai && x.Gia >= 100)
                .ToListAsync();

            return PartialView("NguyenKimManh_DanhSachHangHoa", hangHoa);
        }
        [HttpGet]
        public async Task<IActionResult> ThemHangHoa()
        {
            ViewBag.LoaiHang = await _Context.LoaiHangs.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                await _Context.LoaiHangs.ToListAsync(),
                "MaLoai",
                "TenLoai"
            );

            return View(new ThemHangHoaViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemHangHoa(
    ThemHangHoaViewModel model)
        {
            if (model.MaLoai.HasValue &&
                !await _Context.LoaiHangs
                    .AnyAsync(x => x.MaLoai == model.MaLoai.Value))
            {
                ModelState.AddModelError(
                    nameof(model.MaLoai),
                    "Loại hàng không tồn tại");
            }

            if (ModelState.IsValid)
            {
                var hangHoa = new HangHoa
                {
                    MaLoai = model.MaLoai!.Value,
                    TenHang = model.TenHang,
                    Gia = model.Gia,
                    Anh = model.Anh
                };

                _Context.HangHoas.Add(hangHoa);

                await _Context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.LoaiHang = await _Context.LoaiHangs.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                await _Context.LoaiHangs.ToListAsync(),
                "MaLoai",
                "TenLoai",
                model.MaLoai
            );

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
