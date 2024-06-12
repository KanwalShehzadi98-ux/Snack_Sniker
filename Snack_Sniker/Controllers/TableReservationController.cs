using Dblayer;
using Snack_Sniker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snack_Sniker.Controllers
{
    public class TableReservationController : Controller
    {
        PizzaAndDrinkDbEntities1 Db = new PizzaAndDrinkDbEntities1();

        public ActionResult BookingTables(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            var list = new CRU_TableReservationMV(usertypeid, userid, id);
            ViewBag.BookingStatusID = new SelectList(Db.BookingStatusTables.ToList(), "BookingStatusID", "BookingStatus", "0");
            return View(list);
        }

        [HttpPost]
        public ActionResult BookingTables(CRU_TableReservationMV cRU_TableReservationMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            if (ModelState.IsValid)
            {
                var date = Convert.ToDateTime(cRU_TableReservationMV.ReservationDate).Date.ToString("yyyy/MM/dd");
                var time = cRU_TableReservationMV.ReservationTime;
              //  var reservationdatetime = Convert.ToDateTime(date + " " + time);
                var reservation = Db.BookingTblTables.Find(cRU_TableReservationMV.BookingTableID);
                reservation.ProcessBy_UserID = userid;
                reservation.BookingStatusID = cRU_TableReservationMV.BookingStatusID;
                reservation.Description = cRU_TableReservationMV.Description;
                Db.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("BookingTables", new { id = 0 });
            }
            return View(cRU_TableReservationMV);
        }

        public ActionResult CancelBooking(int bookingtableid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            if (ModelState.IsValid)
            {
                var reservation = Db.BookingTblTables.Find(bookingtableid);
                reservation.Description = "Canceled By User";
                reservation.BookingStatusID = 4;
                reservation.ProcessBy_UserID = userid;
                Db.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("BookingTables", new { id = 0 });
            }
            return View();
        }


        [HttpPost]
        public ActionResult BookTable(CRU_TableReservationMV cRU_TableReservationMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            if (ModelState.IsValid)
            {
                var date = Convert.ToDateTime(cRU_TableReservationMV.ReservationDate).Date.ToString("yyyy/MM/dd");
                var time = cRU_TableReservationMV.ReservationTime;
                var reservationdatetime = Convert.ToDateTime(date + " " + time);
                var reservation = new BookingTblTable();
                reservation.BookingUserID = userid;
                reservation.FullName = cRU_TableReservationMV.FullName;
                reservation.EmailAddress = cRU_TableReservationMV.EmailAddress;
                reservation.MobileNo = cRU_TableReservationMV.MobileNo;
                reservation.BookingDate = DateTime.Now;
                reservation.ReservationDteTime = reservationdatetime;
                reservation.NoOfPersons = (int)cRU_TableReservationMV.NoOfPersons;
                reservation.ProcessBy_UserID = 0;
                reservation.BookingStatusID = 1;
                reservation.Description = string.Empty;
                Db.BookingTblTables.Add(reservation);
                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(cRU_TableReservationMV);
        }
    }
}
