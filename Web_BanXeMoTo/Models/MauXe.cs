﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Web_BanXeMoTo.Models
{
    public partial class MauXe
    {
        public MauXe()
        {
            DatLiches = new HashSet<DatLich>();
            Xes = new HashSet<Xe>();
        }

        [Display(Name = "ID mẫu xe")]
        [Required(ErrorMessage = "Không được bỏ trống, không được trùng lặp")]
        public string Idmau { get; set; }

        [Display(Name = "Khuyến mãi (%)")]
        public string Idkm { get; set; }

        [Display(Name = "Hãng xe")]
        public string Idhang { get; set; }

        [Display(Name = "Tên xe")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string TenXe { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public decimal GiaBan { get; set; }

        [Display(Name = "Hình 1")]
        public string HinhAnh1 { get; set; }

        [Display(Name = "Hình 2")]
        public string HinhAnh2 { get; set; }

        [Display(Name = "Hình 3")]
        public string HinhAnh3 { get; set; }

        [Display(Name = "Bảo hành")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int BaoHanh { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public virtual Hang IdhangNavigation { get; set; }
        public virtual KhuyenMai IdkmNavigation { get; set; }
        public virtual ICollection<DatLich> DatLiches { get; set; }
        public virtual ICollection<Xe> Xes { get; set; }
        [NotMapped]
        public IFormFile UploadHinh1 { get; set; }
        [NotMapped]
        public IFormFile UploadHinh2 { get; set; }
        [NotMapped]
        public IFormFile UploadHinh3 { get; set; }


    }
}
