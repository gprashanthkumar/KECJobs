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
using System.Configuration;

namespace KECJobs.Controllers
{
    public class SkillDevelopmentsController : Controller
    {
        private KECJobsDBContext db = new KECJobsDBContext();

        // GET: SkillDevelopments
        public ActionResult Index(int? Page, string Search = "")
        {
            Search = Convert.ToString(Search).Trim();
            var skillDevelopments = db.SkillDevelopments.Include(s => s.tbl_lookup_costs).Include(s => s.tbl_Lookup_Duration);
            int PageSize = 3;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["GridRows"]))
            {
                int.TryParse(ConfigurationManager.AppSettings["GridRows"], out PageSize);
            }
            int PageNumber = (Page ?? 1);

            if (Search.Equals(string.Empty))
            {

                return View(skillDevelopments.OrderBy(n=>n.SkillDevID).ToPagedList(PageNumber, PageSize));
            }
            else
            {
              
                return View(skillDevelopments.Where(n => n.SkillDevID== n.SkillDevID
                   && (
                   n.OrgName.Contains(Search)
                   || (n.TrainingCources.Contains(Search))
                   || (n.Criteria.Contains(Search))
                    || (n.Locations.Contains(Search))
                     || (n.OtherInfo.Contains(Search))
                     || (n.ContactDetails.Contains(Search))
                     ||(n.tbl_lookup_costs.CostDesc.Contains(Search))
                     ||(n.tbl_Lookup_Duration.DurationDesc.Contains(Search))
                   )).OrderBy(m => m.SkillDevID).ToPagedList(PageNumber, PageSize));
            }

            return View(skillDevelopments.OrderBy(n => n.SkillDevID).ToPagedList(PageNumber, PageSize));
        }

        // GET: SkillDevelopments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillDevelopment skillDevelopment = db.SkillDevelopments.Find(id);
            if (skillDevelopment == null)
            {
                return HttpNotFound();
            }
            return View(skillDevelopment);
        }

        // GET: SkillDevelopments/Create
        public ActionResult Create()
        {

            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isSkillDevelopmentEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }

            ViewBag.CostId = new SelectList(db.Lookup_Costs1, "Costid", "CostDesc");
            ViewBag.DurationId = new SelectList(db.Lookup_Duration, "DurationId", "DurationDesc");
            return View();
        }

        // POST: SkillDevelopments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillDevID,OrgName,TrainingCources,DurationId,CostId,Criteria,Locations,OtherInfo,ContactDetails")] SkillDevelopment skillDevelopment)
        {
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isSkillDevelopmentEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }

            if (ModelState.IsValid)
            {
                db.SkillDevelopments.Add(skillDevelopment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CostId = new SelectList(db.Lookup_Costs1, "Costid", "CostDesc", skillDevelopment.CostId);
            ViewBag.DurationId = new SelectList(db.Lookup_Duration, "DurationId", "DurationDesc", skillDevelopment.DurationId);
            return View(skillDevelopment);
        }

        // GET: SkillDevelopments/Edit/5
        public ActionResult Edit(int? id)
        {

            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isSkillDevelopmentEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillDevelopment skillDevelopment = db.SkillDevelopments.Find(id);
            if (skillDevelopment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostId = new SelectList(db.Lookup_Costs1, "Costid", "CostDesc", skillDevelopment.CostId);
            ViewBag.DurationId = new SelectList(db.Lookup_Duration, "DurationId", "DurationDesc", skillDevelopment.DurationId);
            return View(skillDevelopment);
        }

        // POST: SkillDevelopments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkillDevID,OrgName,TrainingCources,DurationId,CostId,Criteria,Locations,OtherInfo,ContactDetails")] SkillDevelopment skillDevelopment)
        {

            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isSkillDevelopmentEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(skillDevelopment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CostId = new SelectList(db.Lookup_Costs1, "Costid", "CostDesc", skillDevelopment.CostId);
            ViewBag.DurationId = new SelectList(db.Lookup_Duration, "DurationId", "DurationDesc", skillDevelopment.DurationId);
            return View(skillDevelopment);
        }

        // GET: SkillDevelopments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isSkillDevelopmentEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillDevelopment skillDevelopment = db.SkillDevelopments.Find(id);
            if (skillDevelopment == null)
            {
                return HttpNotFound();
            }
            return View(skillDevelopment);
        }

        // POST: SkillDevelopments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isSkillDevelopmentEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }

            SkillDevelopment skillDevelopment = db.SkillDevelopments.Find(id);
            db.SkillDevelopments.Remove(skillDevelopment);
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
        public ActionResult Success()
        {
            return View();
        }
    }
}
