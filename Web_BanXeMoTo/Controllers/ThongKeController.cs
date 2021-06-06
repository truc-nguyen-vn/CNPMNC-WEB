﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_BanXeMoTo.Models;
using X.PagedList;

namespace Web_BanXeMoTo.Controllers
{
    [Authorize(Roles = "admin")]
    public class ThongKeController : Controller
    {
        private readonly QLMTContext database;

        public ThongKeController(QLMTContext db)
        {
            database = db;
        }
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //A ViewBag property provides the view with the current sort order, because this must be included in 
            //  the paging links in order to keep the sort order the same while paging
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            var ModelList = database.HoaDons.Where(x=>x.TrangThai==TrangThaiHoaDon.DaThanhToan).ToList();

            //ViewBag.CurrentFilter, provides the view with the current filter string.
            //he search string is changed when a value is entered in the text box and the submit button is pressed. In that case, the searchString parameter is not null.
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            using (var context = new QLMTContext())
            {
                var model = from s in context.HoaDons
                            select s;
                //Search and match data, if search string is not null or empty
                if (!String.IsNullOrEmpty(searchString))
                {
                    int.TryParse(searchString.Substring(5, 2), out int month);
                    int.TryParse(searchString.Substring(0, 4), out int year);
                    //int month = searchString.Substring(5, 2);
                    //int year = searchString.Substring(0, 4);


                    model = model.Where(s => s.NgayDat.Month.Equals(month) && s.NgayDat.Year.Equals(year));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        ModelList = model.OrderByDescending(s => s.NgayDat).ToList();
                        break;

                    default:
                        ModelList = model.OrderBy(s => s.NgayDat).ToList();
                        break;
                }

            }
            //indicates the size of list
            int pageSize = 10;
            //set page to one is there is no value, ??  is called the null-coalescing operator.
            int pageNumber = (page ?? 1);
            //return the Model data with paged
            var modelv = new HoaDonViewModel
            {
                ListHoaDon = ModelList.ToPagedList(pageNumber, pageSize),
                ListChiTietHd = database.ChiTietHds.ToArray(),
                ListKhachHang = database.KhachHangs.ToArray()
            };
            return View(modelv);
        }
    }
}
