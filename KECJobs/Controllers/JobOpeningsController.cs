using System;
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
    public class JobOpeningsController : Controller
    {
        private KECJobsDBContext db = new KECJobsDBContext();

        // GET: JobOpenings
        public ActionResult Index(int? Page , int grad = 0,string Search  = "")
        {
            Search = Convert.ToString(Search).Trim();
            var jobOpenings = db.JobOpenings.Include(j => j.tbl_Lookup_Experiences);
            int PageSize = 3;
            int PageNumber = (Page ?? 1);
            if (grad == 1)
            {
                ViewBag.grad = 1;
                if (Search.Equals(string.Empty))
                {
                    Page = 1;
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.GraduateGroup == true).OrderBy(m => m.JobOpenID).ToPagedList(PageNumber, PageSize));
                }
                else {
                    
                    //return View(jobOpenings.ToPagedList(PageNumber, PageSize));
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.GraduateGroup == true
                    && (
                    n.Company.Contains(Search)
                    || (n.jobPosition.Contains(Search))
                    || (n.tbl_Lookup_Experiences.ExperienceShort_Description.Contains(Search))
                    || (n.Keywords.Contains(Search))
                     || (n.ContactDetails.Contains(Search))
                      || (n.Locations.Contains(Search))
                      ||(n.Qualification.Contains(Search))
                    )).OrderBy(m=>m.JobOpenID).ToPagedList(PageNumber, PageSize));

                    
                }
            }
            else {
                ViewBag.grad = 0;
                if (Search.Equals(string.Empty))
                {
                    Page = 1;
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.ExperienceGroup == true).OrderBy(m => m.JobOpenID).ToPagedList(PageNumber, PageSize));
                }
                else {
                   
                    //return View(jobOpenings.ToPagedList(PageNumber, PageSize));
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.ExperienceGroup == true
                   && (
                   n.Company.Contains(Search)
                   || (n.jobPosition.Contains(Search))
                   || (n.tbl_Lookup_Experiences.ExperienceShort_Description.Contains(Search))
                   || (n.Keywords.Contains(Search))
                    || (n.ContactDetails.Contains(Search))
                    || (n.Locations.Contains(Search))
                    ||(n.Qualification.Contains(Search))
                   )).OrderBy(m=>m.JobOpenID).ToPagedList(PageNumber,PageSize));

                   
                }
            }
        
        }

        // GET: JobOpenings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpenings jobOpenings = db.JobOpenings.Find(id);
            if (jobOpenings == null)
            {
                return HttpNotFound();
            }
            return View(jobOpenings);
        }

        // GET: JobOpenings/Create
        public ActionResult Create()
        {
            ViewBag.jobExperienceID = new SelectList(db.Lookup_Experiences, "ExperienceID", "ExperienceShort_Description");
          
            return View();
        }

        // POST: JobOpenings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobOpenID,jobExperienceID,JobID,Company,jobPosition,Qualification,Locations,ContactDetails,ValidFrom,ValidTo,Keywords")] JobOpenings jobOpenings)
        {
            if (ModelState.IsValid)
            {
                db.JobOpenings.Add(jobOpenings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jobExperienceID = new SelectList(db.Lookup_Experiences, "ExperienceID", "ExperienceShort_Description", jobOpenings.jobExperienceID);
            return View(jobOpenings);
        }

        // GET: JobOpenings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpenings jobOpenings = db.JobOpenings.Find(id);
            if (jobOpenings == null)
            {
                return HttpNotFound();
            }
            ViewBag.jobExperienceID = new SelectList(db.Lookup_Experiences, "ExperienceID", "ExperienceShort_Description", jobOpenings.jobExperienceID);
            return View(jobOpenings);
        }

        // POST: JobOpenings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobOpenID,jobExperienceID,JobID,Company,jobPosition,Qualification,Locations,ContactDetails,ValidFrom,ValidTo,Keywords")] JobOpenings jobOpenings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobOpenings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jobExperienceID = new SelectList(db.Lookup_Experiences, "ExperienceID", "ExperienceShort_Description", jobOpenings.jobExperienceID);
            return View(jobOpenings);
        }

        // GET: JobOpenings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpenings jobOpenings = db.JobOpenings.Find(id);
            if (jobOpenings == null)
            {
                return HttpNotFound();
            }
            return View(jobOpenings);
        }

        // POST: JobOpenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobOpenings jobOpenings = db.JobOpenings.Find(id);
            db.JobOpenings.Remove(jobOpenings);
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
