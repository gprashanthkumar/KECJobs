﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KECJobs.Models;
using PagedList;

namespace KECJobs.Controllers
{
    public class CompanyReferencesController : Controller
    {
        private KECJobsDBContext db = new KECJobsDBContext();

        // GET: CompanyReferences
        public ActionResult Index(int? Page, string Search = "")
        {
            Search = Convert.ToString(Search).Trim();
            var References = db.CompanyReferences.ToList();
            //return View(db.CompanyReferences.ToList());
            int PageSize = 3;
            int PageNumber = (Page ?? 1);
            if (Search.Equals(string.Empty))
            {

                return View(References.OrderBy(m => m.ReferorID).ToPagedList(PageNumber, PageSize));
            }
            else
            {
                return View(References.Where(n => n.ReferorID == n.ReferorID
                   && (
                   n.Contact.Contains(Search)
                   || (n.CompanyName.Contains(Search))
                   || (n.MailID.Contains(Search))
                   )).OrderBy(m => m.ReferorID).ToPagedList(PageNumber, PageSize)); 
            }
            return View(References.OrderBy(m => m.ReferorID).ToPagedList(PageNumber, PageSize));
        }

        // GET: CompanyReferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyReferences companyReferences = db.CompanyReferences.Find(id);
            if (companyReferences == null)
            {
                return HttpNotFound();
            }
            return View(companyReferences);
        }

        // GET: CompanyReferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyReferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReferorID,Contact,CompanyName,MailID")] CompanyReferences companyReferences)
        {
            if (ModelState.IsValid)
            {
                db.CompanyReferences.Add(companyReferences);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyReferences);
        }

        // GET: CompanyReferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyReferences companyReferences = db.CompanyReferences.Find(id);
            if (companyReferences == null)
            {
                return HttpNotFound();
            }
            return View(companyReferences);
        }

        // POST: CompanyReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReferorID,Contact,CompanyName,MailID")] CompanyReferences companyReferences)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyReferences).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyReferences);
        }

        // GET: CompanyReferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyReferences companyReferences = db.CompanyReferences.Find(id);
            if (companyReferences == null)
            {
                return HttpNotFound();
            }
            return View(companyReferences);
        }

        // POST: CompanyReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyReferences companyReferences = db.CompanyReferences.Find(id);
            db.CompanyReferences.Remove(companyReferences);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
