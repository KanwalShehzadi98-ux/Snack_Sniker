﻿using Dblayer;
using Snack_Sniker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snack_Sniker.Controllers
{
    public class StockController : Controller
    {
        PizzaAndDrinkDbEntities1 Db = new PizzaAndDrinkDbEntities1();
        // GET: Stock
        public ActionResult StockItemCategory(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var stockcategories = new CRU_StockItemCategoryMV(id);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockcategories.VisibleStatusID);
            return View(stockcategories);
        }
        [HttpPost]
        public ActionResult StockItemCategory(CRU_StockItemCategoryMV stockcategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            stockcategory.CreatedBy_UserID = userid;
            if (ModelState.IsValid)
            {
                if (stockcategory.StockItemCategoryID == 0)
                {
                    var checkexist = Db.StockItemCategoryTables.Where(s => s.StockItemCategory.Trim().ToUpper() == stockcategory.StockItemCategory.Trim().ToUpper()).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newcategory = new StockItemCategoryTable();
                        newcategory.StockItemCategory = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stockcategory.StockItemCategory.ToLower());
                        newcategory.VisibleStatusID = stockcategory.VisibleStatusID;
                        newcategory.CreatedBy_UserID = userid;
                        Db.StockItemCategoryTables.Add(newcategory);
                        Db.SaveChanges();
                        return RedirectToAction("StockItemCategory", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockItemCategory", "Already Registered");
                    }
                }
            }
            else
            {
                var checkexist = Db.StockItemCategoryTables.Where(s =>
                s.StockItemCategory.Trim().ToUpper() == stockcategory.StockItemCategory.Trim().ToUpper()
                && s.StockItemCategoryID != stockcategory.StockItemCategoryID).FirstOrDefault();
                if (checkexist == null)
                {
                    var editcategory = Db.StockItemCategoryTables.Find(stockcategory.StockItemCategoryID);
                    editcategory.StockItemCategory = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stockcategory.StockItemCategory.ToLower());
                    editcategory.VisibleStatusID = stockcategory.VisibleStatusID;
                    editcategory.CreatedBy_UserID = userid;
                    Db.Entry(editcategory).State = System.Data.Entity.EntityState.Modified;
                    Db.SaveChanges();
                    return RedirectToAction("StockItemCategory", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("StockItemCategory", "Already Registered");
                }
            }
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockcategory.VisibleStatusID);
            return View(stockcategory);
        }

        public ActionResult OrderType(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var ordertype = new CRU_OrderTypeMV(id);
            return View(ordertype);
        }
        [HttpPost]
        public ActionResult OrderType(CRU_OrderTypeMV orderTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                if (orderTypeMV.OrderTypeID == 0)
                {
                    var checkexist = Db.OrderTypeTables.Where(o => o.OrderType.Trim().ToUpper() == orderTypeMV.OrderType.Trim().ToLower()).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newordertype = new OrderTypeTable();
                        newordertype.OrderType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(orderTypeMV.OrderType.ToLower());
                        Db.OrderTypeTables.Add(newordertype);
                        Db.SaveChanges();
                        return RedirectToAction("OrderType", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("OrderType", "Already Exist!");
                    }
                }
                else
                {
                    var checkexist = Db.OrderTypeTables.Where(o => o.OrderType.Trim().ToUpper() == orderTypeMV.OrderType.Trim().ToLower() && o.OrderTypeID != orderTypeMV.OrderTypeID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var editordertype = Db.OrderTypeTables.Find(orderTypeMV.OrderTypeID);
                        editordertype.OrderType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(orderTypeMV.OrderType.ToLower());
                        Db.Entry(editordertype).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction("OrderType", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("OrderType", "Already Exist!");
                    }
                }
            }
            return View(orderTypeMV);
        }

        public ActionResult StockMenuCategory(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var menucategories = new CRU_StockMenuCategoryMV(id);
            return View(menucategories);
        }

        [HttpPost]
        public ActionResult StockMenuCategory(CRU_StockMenuCategoryMV menucategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            menucategory.CreatedBy_UserID = userid;
            if (ModelState.IsValid)
            {
                if (menucategory.StockMenuCategoryID == 0)
                {
                    var checkexist = Db.StockMenuCategoryTables.Where(s => s.StockMenuCategory.Trim().ToUpper() == menucategory.StockMenuCategory.Trim().ToUpper()).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newcategory = new StockMenuCategoryTable();
                        newcategory.StockMenuCategory = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(menucategory.StockMenuCategory.ToLower());
                        newcategory.CreatedBy_UserID = userid;
                        Db.StockMenuCategoryTables.Add(newcategory);
                        Db.SaveChanges();
                        return RedirectToAction("StockMenuCategory", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockMenuCategory", "Already Registered");
                    }
                }
            }
            else
            {
                var checkexist = Db.StockMenuCategoryTables.Where(s =>
                s.StockMenuCategory.Trim().ToUpper() == menucategory.StockMenuCategory.Trim().ToUpper()
                && s.StockMenuCategoryID != menucategory.StockMenuCategoryID).FirstOrDefault();
                if (checkexist == null)
                {
                    var editcategory = Db.StockMenuCategoryTables.Find(menucategory.StockMenuCategoryID);
                    editcategory.StockMenuCategory = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(menucategory.StockMenuCategory.ToLower());
                    editcategory.CreatedBy_UserID = userid;
                    Db.Entry(editcategory).State = System.Data.Entity.EntityState.Modified;
                    Db.SaveChanges();
                    return RedirectToAction("StockMenuCategory", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("StockMenuCategory", "Already Registered");
                }
            }
            return View(menucategory);
        }
        public ActionResult StockItem(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var stockitem = new CRU_StockItemMV(id);
            ViewBag.StockItemCategoryID=new SelectList(Db.StockItemCategoryTables.ToList(), "StockItemCategoryID", "StockItemCategory",stockitem.StockItemCategoryID);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockitem.VisibleStatusID);
            ViewBag.OrderTypeID = new SelectList(Db.OrderTypeTables.ToList(), "OrderTypeID", "OrderType", stockitem.OrderTypeID);
            return View(stockitem);
        }

        [HttpPost]
        public ActionResult StockItem(CRU_StockItemMV cru_StockItemMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            cru_StockItemMV.CreatedBy_UserID = userid;
            cru_StockItemMV.RegisterDate = DateTime.Now;
            if(ModelState.IsValid)
            {
                if (cru_StockItemMV.StockItemID == 0)
                {
                    var checkexit = Db.StockItemTables.Where(
                        s => s.StockItemTitle.Trim().ToUpper() == cru_StockItemMV.StockItemTitle.Trim().ToUpper() 
                        && s.StockItemCategoryID == cru_StockItemMV.StockItemCategoryID 
                        && s.OrderTypeID == cru_StockItemMV.OrderTypeID
                        && s.ItemSize== cru_StockItemMV.ItemSize).FirstOrDefault();
                    if (checkexit == null) 
                    {
                        var newitem = new StockItemTable();
                        newitem.StockItemCategoryID = cru_StockItemMV.StockItemCategoryID;
                        newitem.ItemPhotoPath = @"~/Content/StockItemPhoto/default.png";
                        newitem.StockItemTitle = cru_StockItemMV.StockItemTitle;
                        newitem.ItemSize = cru_StockItemMV.ItemSize;
                        newitem.UnitPrice = cru_StockItemMV.UnitPrice;
                        newitem.VisibleStatusID = cru_StockItemMV.VisibleStatusID;
                        newitem.OrderTypeID = cru_StockItemMV.OrderTypeID;
                        newitem.CreatedBy_UserID = cru_StockItemMV.CreatedBy_UserID;
                        newitem.RegisterDate = DateTime.Now;
                        Db.StockItemTables.Add(newitem);
                        Db.SaveChanges();
                        if (cru_StockItemMV.PhotoPath != null)
                        {
                            var folder = "~/Content/StockItemPhoto";
                            var photoname = string.Format("{0}.jpg", newitem.StockItemID);
                            var response = HelperClass.FileUpload.UploadPhoto(cru_StockItemMV.PhotoPath, folder, photoname);
                            if (response)
                            {
                                var photo = string.Format("{0}/{1}", folder, photoname);
                                newitem.ItemPhotoPath = photo;
                                Db.Entry(newitem).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
                        }
                        return RedirectToAction("StockItem", new { id = 0 });
                    }
                }
                else
                {
                    ModelState.AddModelError("StockItemTitle","Already Exist!");
                }
            }
            else
            {
                if (cru_StockItemMV.StockItemID == 0)
                {
                    var checkexit = Db.StockItemTables.Where(
                        s => s.StockItemTitle.Trim().ToUpper() == cru_StockItemMV.StockItemTitle.Trim().ToUpper()
                        && s.StockItemCategoryID == cru_StockItemMV.StockItemCategoryID
                        && s.OrderTypeID == cru_StockItemMV.OrderTypeID
                        && s.ItemSize == cru_StockItemMV.ItemSize && s.StockItemID != cru_StockItemMV.StockItemID).FirstOrDefault();
                    if (checkexit == null)
                    {
                        var edititem = Db.StockItemTables.Find(cru_StockItemMV.StockItemID);
                        edititem.StockItemCategoryID = cru_StockItemMV.StockItemCategoryID;
                        edititem.StockItemTitle = cru_StockItemMV.StockItemTitle;
                        edititem.ItemSize = cru_StockItemMV.ItemSize;
                        edititem.UnitPrice = cru_StockItemMV.UnitPrice;
                        edititem.VisibleStatusID = cru_StockItemMV.VisibleStatusID;
                        edititem.OrderTypeID = cru_StockItemMV.OrderTypeID;
                        Db.Entry(edititem).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        if (cru_StockItemMV.PhotoPath != null)
                        {
                            var folder = "~/Content/StockItemPhoto";
                            var photoname = string.Format("{0}.jpg", edititem.StockItemID);
                            var response = HelperClass.FileUpload.UploadPhoto(cru_StockItemMV.PhotoPath, folder, photoname);
                            if (response)
                            {
                                var photo = string.Format("{0}/{1}", folder, photoname);
                                edititem.ItemPhotoPath = photo;
                                Db.Entry(edititem).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
                        }
                        return RedirectToAction("StockItem", new { id = 0 });
                    }
                }
                else
                {
                    ModelState.AddModelError("StockItemTitle", "Already Exist!");
                }
            }
            ViewBag.StockItemCategoryID = new SelectList(Db.StockItemCategoryTables.ToList(), "StockItemCategoryID", "StockItemCategory", cru_StockItemMV.StockItemCategoryID);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", cru_StockItemMV.VisibleStatusID);
            ViewBag.OrderTypeID = new SelectList(Db.OrderTypeTables.ToList(), "OrderTypeID", "OrderType", cru_StockItemMV.OrderTypeID);
            return View(cru_StockItemMV);
        }

        public ActionResult StockItemIngredient(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var list = new CRU_StockItemIngredientMV(id);
            return View(list);
        }
        [HttpPost]
        public ActionResult StockItemIngredient(CRU_StockItemIngredientMV cRU_StockItemIngredientMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);

            if (ModelState.IsValid)
            {
                if (cRU_StockItemIngredientMV.StockItemID > 0)
                {
                    var checkexist = Db.StockItemIngredientTables.Where(
                        s => s.StockItemIngredientTitle.Trim().ToUpper() == cRU_StockItemIngredientMV.StockItemIngredientTitle.Trim().ToUpper()
                        && s.StockItemID == cRU_StockItemIngredientMV.StockItemID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newingredient = new StockItemIngredientTable();
                        newingredient.StockItemID = cRU_StockItemIngredientMV.StockItemID;
                        newingredient.StockItemIngredientPhotoPath = @"~/Content/StockIngredientPhoto/default.png";
                        newingredient.StockItemIngredientTitle = cRU_StockItemIngredientMV.StockItemIngredientTitle;
                        newingredient.CreatedBy_UserID = userid;
                        Db.StockItemIngredientTables.Add(newingredient);
                        Db.SaveChanges();
                        if (cRU_StockItemIngredientMV.PhotoPath != null)
                        {
                            var folder = "~/Content/StockIngredientPhoto";
                            var photoname = string.Format("{0}.jpg", newingredient.StockItemIngredientID);
                            var response = HelperClass.FileUpload.UploadPhoto(cRU_StockItemIngredientMV.PhotoPath, folder, photoname);
                            if (response)
                            {
                                var photo = string.Format("{0}/{1}", folder, photoname);
                                newingredient.StockItemIngredientPhotoPath = photo;
                                Db.Entry(newingredient).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
                        }
                        return RedirectToAction("StockItemIngredient", new { id = cRU_StockItemIngredientMV.StockItemID });
                    }
                    else
                    {
                        ModelState.AddModelError("StockItemIngredientTitle", "Already Exist!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Close and Select Item First!");
                }
            }
            var list = new CRU_StockItemIngredientMV(cRU_StockItemIngredientMV.StockItemID);
            list.StockItemIngredientTitle = cRU_StockItemIngredientMV.StockItemIngredientTitle;
            list.StockItemID = cRU_StockItemIngredientMV.StockItemID;
            list.PhotoPath = cRU_StockItemIngredientMV.PhotoPath;
            return View(list);
        }

        public ActionResult DeleteIngredient(int? id)
        {
            var ingredient = Db.StockItemIngredientTables.Find(id);
            Db.Entry(ingredient).State = System.Data.Entity.EntityState.Deleted;
            Db.SaveChanges();
            return RedirectToAction("StockItemIngredient", new { id = ingredient.StockItemID });
        }

        public ActionResult StockMenu(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var stockmenuitem = new CRU_StockMenuItemMV(id);
            ViewBag.StockMenuCategoryID = new SelectList(Db.StockMenuCategoryTables.ToList(), "StockMenuCategoryID", "StockMenuCategory", stockmenuitem.StockMenuCategoryID);
            ViewBag.StockItemID = new SelectList(Db.StockItemTables.ToList(), "StockItemID", "StockItemTitle", stockmenuitem.StockItemID);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockmenuitem.VisibleStatusID);
            return View(stockmenuitem);
        }

        [HttpPost]
        public ActionResult StockMenu(CRU_StockMenuItemMV cru_StockMenuItemMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            if (ModelState.IsValid)
            {
                if (cru_StockMenuItemMV.StockMenuItemID == 0)
                {
                    var checkexist = Db.StockMenuItemTables.Where(
                        s => s.StockMenuCategoryID == cru_StockMenuItemMV.StockMenuCategoryID
                        && s.StockItemID == cru_StockMenuItemMV.StockItemID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newitem = new StockMenuItemTable();
                        newitem.StockMenuCategoryID = cru_StockMenuItemMV.StockMenuCategoryID;
                        newitem.StockItemID = cru_StockMenuItemMV.StockItemID;
                        newitem.VisibleStatusID = cru_StockMenuItemMV.VisibleStatusID;
                        newitem.CreatedBy_UserID = userid;
                        Db.StockMenuItemTables.Add(newitem);
                        Db.SaveChanges();
                        return RedirectToAction("StockMenu", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockItemID", "Already Exist!");
                    }
                }
                else
                {
                    var checkexist = Db.StockMenuItemTables.Where(
                        s => s.StockMenuCategoryID == cru_StockMenuItemMV.StockMenuCategoryID
                        && s.StockItemID == cru_StockMenuItemMV.StockItemID
                        && s.StockMenuItemID != cru_StockMenuItemMV.StockMenuItemID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var edititem = Db.StockMenuItemTables.Find(cru_StockMenuItemMV.StockMenuItemID);
                        edititem.StockMenuCategoryID = cru_StockMenuItemMV.StockMenuCategoryID;
                        edititem.StockItemID = cru_StockMenuItemMV.StockItemID;
                        edititem.VisibleStatusID = cru_StockMenuItemMV.VisibleStatusID;
                        edititem.CreatedBy_UserID = userid;
                        Db.Entry(edititem).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction("StockMenu", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockItemID", "Already Exist!");
                    }
                }
            }
            ViewBag.StockMenuCategoryID = new SelectList(Db.StockMenuCategoryTables.ToList(), "StockMenuCategoryID", "StockMenuCategory", cru_StockMenuItemMV.StockMenuCategoryID);
            ViewBag.StockItemID = new SelectList(Db.StockItemTables.ToList(), "StockItemID", "StockItemTitle", cru_StockMenuItemMV.StockItemID);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", cru_StockMenuItemMV.VisibleStatusID);
            return View(cru_StockMenuItemMV);
        }
        [HttpGet]
        public ActionResult StockDeal(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var stockdeals = new CRU_StockDealMV(id);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockdeals.VisibleStatusID);
            return View(stockdeals);
        }

        [HttpPost]
        public ActionResult StockDeal(CRU_StockDealMV cru_StockDealMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            if (ModelState.IsValid)
            {
                if (cru_StockDealMV.StockDealID == 0)
                {
                    var checkexist = Db.StockDealTables.Where(
                        s => s.StockDealTitle == cru_StockDealMV.StockDealTitle).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newdeal = new StockDealTable();
                        newdeal.StockDealTitle = cru_StockDealMV.StockDealTitle;
                        newdeal.DealPrice = cru_StockDealMV.DealPrice;
                        newdeal.VisibleStatusID = cru_StockDealMV.VisibleStatusID;
                        newdeal.StockDealStartDate = (DateTime)cru_StockDealMV.StockDealStartDate;
                        newdeal.Discount = (double)cru_StockDealMV.Discount;
                        newdeal.StockDealEndDate = (DateTime)cru_StockDealMV.StockDealEndDate;
                        newdeal.StockDealRegisterDate = cru_StockDealMV.StockDealRegisterDate;
                        Db.StockDealTables.Add(newdeal);
                        Db.SaveChanges();
                        if (cru_StockDealMV.PhotoPath != null)
                        {
                            var folder = "~/Content/Deals";
                            var photoname = string.Format("{0}.jpg", newdeal.StockDealID);
                            var response = HelperClass.FileUpload.UploadPhoto(cru_StockDealMV.PhotoPath, folder, photoname);
                            if (response)
                            {
                                var photo = string.Format("{0}/{1}", folder, photoname);
                                newdeal.DealPhoto = photo;
                                Db.Entry(newdeal).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
                        }
                        return RedirectToAction("StockDeal", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockDealTitle", "Already Exist!");
                    }
                }
                else
                {
                    var checkexist = Db.StockDealTables.Where(
                        s => s.StockDealTitle == cru_StockDealMV.StockDealTitle
                        && s.StockDealID != cru_StockDealMV.StockDealID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var editdeal = Db.StockDealTables.Find(cru_StockDealMV.StockDealID);
                        editdeal.StockDealTitle = cru_StockDealMV.StockDealTitle;
                        editdeal.DealPrice = cru_StockDealMV.DealPrice;
                        editdeal.VisibleStatusID = cru_StockDealMV.VisibleStatusID;
                        editdeal.StockDealStartDate = (DateTime)cru_StockDealMV.StockDealStartDate;
                        editdeal.Discount = (double)cru_StockDealMV.Discount;
                        editdeal.StockDealEndDate = (DateTime)cru_StockDealMV.StockDealEndDate;
                        Db.Entry(editdeal).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                       // if (cru_StockDealMV.PhotoPath != null)
                       // {
                            var folder = "~/Content/Deals";
                            var photoname = string.Format("{0}.jpg", editdeal.StockDealID);
                            var response = HelperClass.FileUpload.UploadPhoto(cru_StockDealMV.PhotoPath, folder, photoname);
                            if (response)
                            {
                                var photo = string.Format("{0}/{1}", folder, photoname);
                                editdeal.DealPhoto = photo;
                                Db.Entry(editdeal).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
                       // }
                        return RedirectToAction("Deals", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockDealTitle", "Already Exist!");
                    }
                }
            }
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", cru_StockDealMV.VisibleStatusID);
            return View(cru_StockDealMV);
        }

        [HttpGet]
        public ActionResult StockDealItem(int dealid, int stockdealdetailid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var stockdealdetails = new CRU_StockDealDetailMV(dealid, stockdealdetailid);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockdealdetails.VisibleStatusID);
            ViewBag.StockItemID = new SelectList(Db.StockItemTables.ToList(), "StockItemID", "StockItemTitle", stockdealdetails.StockItemID);
            return View(stockdealdetails);
        }

        [HttpPost]
        public ActionResult StockDealItem(CRU_StockDealDetailMV cru_StockDealDetailMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);

            if (ModelState.IsValid)
            {
                if (cru_StockDealDetailMV.StockDealDetailID == 0)
                {
                    var checkexist = Db.StockDealDetailTables.Where(
                        s => s.StockItemID == cru_StockDealDetailMV.StockItemID
                        && s.VisibleStatusID == 1 && s.StockDealID == cru_StockDealDetailMV.StockDealID).FirstOrDefault();

                    if (checkexist == null)
                    {
                        var newitem = new StockDealDetailTable();
                        newitem.StockDealID = cru_StockDealDetailMV.StockDealID;
                        newitem.StockItemID = cru_StockDealDetailMV.StockItemID;
                        newitem.Discount = cru_StockDealDetailMV.Discount;
                        newitem.Quantity = cru_StockDealDetailMV.Quantity;
                        newitem.VisibleStatusID = cru_StockDealDetailMV.VisibleStatusID;
                        Db.StockDealDetailTables.Add(newitem);
                        Db.SaveChanges();
                        return RedirectToAction("StockDealItem", new { dealid = cru_StockDealDetailMV.StockDealID, stockdealdetailid = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockItemID", "Already Exist!");
                    }
                }
                else
                {
                    var checkexist = Db.StockDealDetailTables.Where(
                       s => s.StockItemID == cru_StockDealDetailMV.StockItemID
                       && s.VisibleStatusID == 1 && s.StockDealID == cru_StockDealDetailMV.StockDealID
                       && s.StockDealDetailID != cru_StockDealDetailMV.StockDealDetailID).FirstOrDefault();

                    if (checkexist == null)
                    {
                        var edititem = Db.StockDealDetailTables.Find(cru_StockDealDetailMV.StockDealDetailID);
                        edititem.StockDealID = cru_StockDealDetailMV.StockDealID;
                        edititem.StockItemID = cru_StockDealDetailMV.StockItemID;
                        edititem.Discount = cru_StockDealDetailMV.Discount;
                        edititem.Quantity = cru_StockDealDetailMV.Quantity;
                        edititem.VisibleStatusID = cru_StockDealDetailMV.VisibleStatusID;
                        Db.Entry(edititem).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction("StockDealItem", new { dealid = cru_StockDealDetailMV.StockDealID, stockdealdetailid = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("StockItemID", "Already Exist!");
                    }
                }
            }

            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", cru_StockDealDetailMV.VisibleStatusID);
            ViewBag.StockItemID = new SelectList(Db.StockItemTables.ToList(), "StockItemID", "StockItemTitle", cru_StockDealDetailMV.StockItemID);
            return View(cru_StockDealDetailMV);
        }
    }
}

