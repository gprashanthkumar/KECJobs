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
    public class JobOpeningsController : Controller
    {
        private KECJobsDBContext db = new KECJobsDBContext();

        // GET: JobOpenings
        public ActionResult Index(int? Page, int grad = 0, string Search = "")
        {
            var strAppPath = System.Web.HttpRuntime.AppDomainAppVirtualPath.ToString();
            if (strAppPath == "/")
            {
                strAppPath = string.Empty;
            }
            strAppPath += ConfigurationManager.AppSettings["OpeningUploads"];
            ViewBag.UploadPath = strAppPath;

            Search = Convert.ToString(Search).Trim();
            var jobOpenings = db.JobOpenings.Include(j => j.tbl_Lookup_Experiences);
            int PageSize = 3;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["GridRows"]))
            {
                int.TryParse(ConfigurationManager.AppSettings["GridRows"], out PageSize);
            }
            int PageNumber = (Page ?? 1);
            if (grad == 1)
            {
                ViewBag.grad = 1;
                if (Search.Equals(string.Empty))
                {
                    Page = 1;
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.GraduateGroup == true).OrderBy(m => m.JobOpenID).ToPagedList(PageNumber, PageSize));
                }
                else
                {

                    //return View(jobOpenings.ToPagedList(PageNumber, PageSize));
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.GraduateGroup == true
                    && (
                    n.Company.Contains(Search)
                    || (n.jobPosition.Contains(Search))
                    || (n.tbl_Lookup_Experiences.ExperienceShort_Description.Contains(Search))
                    || (n.Keywords.Contains(Search))
                     || (n.ContactDetails.Contains(Search))
                      || (n.Locations.Contains(Search))
                      || (n.Qualification.Contains(Search))
                    )).OrderBy(m => m.JobOpenID).ToPagedList(PageNumber, PageSize));


                }
            }
            else
            {
                ViewBag.grad = 0;
                if (Search.Equals(string.Empty))
                {
                    Page = 1;
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.ExperienceGroup == true).OrderBy(m => m.JobOpenID).ToPagedList(PageNumber, PageSize));
                }
                else
                {

                    //return View(jobOpenings.ToPagedList(PageNumber, PageSize));
                    return View(jobOpenings.Where(n => n.tbl_Lookup_Experiences.ExperienceGroup == true
                   && (
                   n.Company.Contains(Search)
                   || (n.jobPosition.Contains(Search))
                   || (n.tbl_Lookup_Experiences.ExperienceShort_Description.Contains(Search))
                   || (n.Keywords.Contains(Search))
                    || (n.ContactDetails.Contains(Search))
                    || (n.Locations.Contains(Search))
                    || (n.Qualification.Contains(Search))
                   )).OrderBy(m => m.JobOpenID).ToPagedList(PageNumber, PageSize));


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
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isJobsEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }

            ViewBag.jobExperienceID = new SelectList(db.Lookup_Experiences, "ExperienceID", "ExperienceShort_Description");

            return View();
        }

        // POST: JobOpenings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobOpenID,jobExperienceID,JobID,Company,jobPosition,Qualification,Locations,ContactDetails,ValidFrom,ValidTo,Keywords,JobFile")] JobOpenings jobOpenings)
        {
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isJobsEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }


            var strAppPath = System.Web.HttpRuntime.AppDomainAppVirtualPath.ToString();
            if (strAppPath == "/")
            {
                strAppPath = string.Empty;
            }



            if (ModelState.IsValid)
            {
                db.JobOpenings.Add(jobOpenings);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            foreach (string upload in Request.Files)
            {
                //c:\users\narsa\documents\visual studio 2015\Projects\KECJobs\KECJobs\Registrations\Uploads\
                //byte[] fileData = new byte[Request.Files[upload].InputStream.Length];
                var x = Server.MapPath(strAppPath + ConfigurationManager.AppSettings["OpeningUploads"]) + "\\" + jobOpenings.JobOpenID.ToString() + "_" + Request.Files[upload].FileName;
                Request.Files[upload].SaveAs(x);
                jobOpenings.JobFile = jobOpenings.JobOpenID.ToString() + "_" + Request.Files[upload].FileName;
                db.SaveChanges();

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
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isJobsEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }


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
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isJobsEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }


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
            if (!(KECAuthenticate.isAuthenticated) || (KECAuthenticate.IsAdmin == false) && (KECAuthenticate.isJobsEditor == false))
            {
                return RedirectToAction("NotAuthorised", "Home");
            }


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

        public ActionResult Success()
        {
            return View();
        }

        public void PrintExcel(int grad = 0)
        {
            List<ExcelJobOpenings> x = new List<ExcelJobOpenings>();
            if (grad == 1)
            {
                x = (from n in db.JobOpenings.AsEnumerable() where n.tbl_Lookup_Experiences.GraduateGroup == true orderby n.JobOpenID select
                    new ExcelJobOpenings {
                        JobOpenID = n.JobOpenID,
                        jobExperience = n.tbl_Lookup_Experiences.ExperienceShort_Description,
                        JobID = n.JobID,
                        Company = n.Company,
                        jobPosition = n.jobPosition,
                        Qualification = n.Qualification,
                        Locations = n.Locations,
                        ContactDetails = n.ContactDetails,
                        ValidFrom = n.ValidFrom.Value.ToShortDateString(),
                        ValidTo= n.ValidTo.Value.ToShortDateString(),
                        Keywords=n.Keywords,
                        JobFile=n.JobFile
                     }
                     ).ToList();
            }

            else {
                x = (from n in db.JobOpenings.AsEnumerable() where n.tbl_Lookup_Experiences.ExperienceGroup == true orderby n.JobOpenID select
                     new ExcelJobOpenings
                     {
                         JobOpenID = n.JobOpenID,
                         jobExperience = n.tbl_Lookup_Experiences.ExperienceShort_Description,
                         JobID = n.JobID,
                         Company = n.Company,
                         jobPosition = n.jobPosition,
                         Qualification = n.Qualification,
                         Locations = n.Locations,
                         ContactDetails = n.ContactDetails,
                         ValidFrom = n.ValidFrom.Value.ToShortDateString(),
                         ValidTo = n.ValidTo.Value.ToShortDateString(),
                         Keywords = n.Keywords,
                         JobFile = n.JobFile
                     }
                     ).ToList(); 
            }
            
            var dt = KECJobs.Constants.ToDataTable(x);
            Constants.ExporttoExcel(dt);
        }
    }
}
