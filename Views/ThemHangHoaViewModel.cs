using System.ComponentModel.DataAnnotations;

namespace NguyenKimManh_241230790.Models
{
    public class ThemHangHoaViewModel
    {
        [Display(Name = "Mã Loại")]
        [Required(ErrorMessage = "Vui lòng chọn loại hàng")]
        public int? MaLoai { get; set; }

        [Display(Name = "Tên Hàng")]
        [Required(ErrorMessage = "Vui lòng nhập tên hàng")]
        public string TenHang { get; set; } = string.Empty;

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(100, 5000,
            ErrorMessage = "Giá phải từ 100 đến 5000")]
        public decimal? Gia { get; set; }

        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Vui lòng nhập tên ảnh")]
        [RegularExpression(
            @"(?i)^.+\.(jpg|png|gif|tiff)$",
            ErrorMessage = "Ảnh phải có đuôi .jpg, .png, .gif hoặc .tiff")]
        public string Anh { get; set; } = string.Empty;
    }
}
