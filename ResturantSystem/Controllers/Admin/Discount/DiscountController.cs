﻿using ResturantSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResturantSystem.Controllers.Admin.Discount
{
    public class DiscountController : Controller
    {
        ResturantsystemEntities db;
        // GET: Discount
        public DiscountController()
        {
            db = new ResturantsystemEntities();
        }
        public ActionResult Index()
        {
            var discount = db.DiscountTables.ToList();
            return View(discount);
        }
        public ActionResult AddDiscount()
        {
            var Categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(Categories, "FoodCategory", "FoodCategory");
            var productTables = db.ProductTables.ToList();
            ViewBag.productTables = new SelectList(productTables, "ProductName", "ProductName");
            return View();

        }

        [HttpPost]
        public ActionResult AddDiscount(DiscountTable recordsave)
        {
            db.DiscountTables.Add(recordsave);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditDiscount(int Id)
        {
            var Categories = db.Categories.ToList();
            ViewData["Categories"] = new SelectList(Categories, "FoodCategory", "FoodCategory");
            var productTables = db.ProductTables.ToList();
            ViewData["productTables"] = new SelectList(productTables, "ProductName", "ProductName");
            DiscountTable data = db.DiscountTables.Find(Id);
            return View(data);

        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditDiscountData(DiscountTable update)
        {

            // TODO: Add update logic here

            db.Entry(update).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDiscount(int Id)
        {
            DiscountTable data = db.DiscountTables.Find(Id);
            return View(data);
        }
        public ActionResult DeleteDiscountData(int Id)
        {
            DiscountTable data = db.DiscountTables.Find(Id);
            db.DiscountTables.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //uta bata yo action call hunxa esma chai hamle FoodCategory anusar product data leko xam 
        public JsonResult GetProductByCategoryId(string FoodCategory)
        {
            List<ProductTable> ProductTable = db.ProductTables.Where(x => x.FoodCategory == FoodCategory).ToList();

            if (ProductTable == null)
            {
                return Json(new
                {
                    Success = false,
                },
                JsonRequestBehavior.AllowGet);
            }
            return Json(ProductTable, JsonRequestBehavior.AllowGet); // esma aako data Json format ma return hunxa
        }
    }
}