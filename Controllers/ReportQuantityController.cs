using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using WebAPI.Models.Response;

namespace WebAPI.Controllers
{
    public class ReportQuantityController : ApiController
    {
        private DBModel db = new DBModel();

        [ResponseType(typeof(ReportResponse))]
        public IHttpActionResult GetReport(DateTime datefrom, DateTime dateto)
        {

            var result = (from a in db.ChiTietHoaDons
                          join b in db.MonAns on a.MonAn_ID equals b.MonAn_ID
                          join c in db.HoaDons on a.HoaDon_ID equals c.HoaDon_ID
                          where c.HoaDon_ThoiGianRa >= datefrom && c.HoaDon_ThoiGianRa <= dateto && c.HoaDon_TrangThai == "Đã hoàn thành"
                          group new { b, a } by b.MonAn_Ten into x
                          orderby x.Sum(c => c.a.ChiTietHD_SoLuong) descending
                          select new
                          {
                              MonAn_Ten = x.Key,
                              MonAn_SoLuong = x.Sum(c => c.a.ChiTietHD_SoLuong)
                          }).ToList();

            return Ok(result);
        }


    }
}