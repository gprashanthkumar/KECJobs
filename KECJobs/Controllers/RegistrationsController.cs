using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KECJobs.Models;
using System.Configuration;
using PagedList;


namespace KECJobs.Controllers
{
    public class RegistrationsController : Controller
    {
        private KECJobsDBContext db = new KECJobsDBContext();

        // GET: Registrations
        public ActionResult Index(int? Page, string Search = "")
        {
            Search = Convert.ToString(Search).Trim();
            var registrations = db.Registrations.Include(r => r.tbl_lookup_PositionType).Include(r => r.tbl_Lookup_Qualifications).Include(r=>r.tbl_Lookup_MaritalStatus);
            int PageSize = 3;
            int PageNumber = (Page ?? 1);

            if (Search.Equals(string.Empty))
            {

                return View(registrations.OrderBy(n =>n.RegistrationID).ToPagedList(PageNumber, PageSize));
            }
            else
            {

                return View(registrations.Where(n => n.RegistrationID == n.RegistrationID
                   && (
                   n.Surname.Contains(Search)
                   || (n.Name.Contains(Search))
                   || (n.FatherName.Contains(Search))
                    || (n.tbl_Lookup_MaritalStatus.maritalstatus.Contains(Search))
                     //|| (n.DateofBirth.Contains(Search))
                     || (n.Gender.Contains(Search))
                     ||(n.AreaOfIntrest.Contains(Search))
                     ||(n.EmailAddress.Contains(Search))
                     ||(n.ContactNumber.Contains(Search))
                     ||(n.Address.Contains(Search))
                     ||(n.tbl_lookup_PositionType.PositionName.Contains(Search))
                     ||(n.tbl_Lookup_Qualifications.QualificationName.Contains(Search))
                   )).OrderBy(m => m.RegistrationID).ToPagedList(PageNumber, PageSize));
            }
            return View(registrations.OrderBy(n => n.RegistrationID).ToPagedList(PageNumber, PageSize));
           
        }


        public ActionResult Success()
        {
            return View();
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Register()
        {
            ViewBag.PositiontypeID = new SelectList(db.tbl_lookup_PositionType, "PositiontypeId", "PositionName");
            ViewBag.QualificationID = new SelectList(db.tbl_Lookup_Qualifications, "QualificationId", "QualificationName");
            ViewBag.maritalstatusid = new SelectList(db.tbl_Lookup_MaritalStatus, "maritalstatusid", "maritalstatus");

            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "RegistrationID,Surname,Name,FatherName,MaritalStatusId,DateofBirth,Gender,QualificationID,PositiontypeID,AreaOfIntrest,EmailAddress,ContactNumber,Address,ResumeFilePath")] Registration registration)
        {
            var strAppPath = System.Web.HttpRuntime.AppDomainAppVirtualPath.ToString();
            if (strAppPath == "/") {
                strAppPath = string.Empty;
            }

            if (ModelState.IsValid)
            {

               
                db.Registrations.Add(registration);
                db.SaveChanges();

                foreach (string upload in Request.Files)
                {
                    //c:\users\narsa\documents\visual studio 2015\Projects\KECJobs\KECJobs\Registrations\Uploads\
                    //byte[] fileData = new byte[Request.Files[upload].InputStream.Length];
                    var x = Server.MapPath(strAppPath + ConfigurationManager.AppSettings["RegistrationUploads"]) + "\\" + registration.RegistrationID.ToString() + "_" + Request.Files[upload].FileName;
                    Request.Files[upload].SaveAs(x);
                    registration.ResumeFilePath = registration.RegistrationID.ToString() + "_" + Request.Files[upload].FileName;
                    db.SaveChanges();

                }
                //return RedirectToAction("Index");
                return RedirectToAction("Success");
            }

            ViewBag.PositiontypeID = new SelectList(db.tbl_lookup_PositionType, "PositiontypeId", "PositionName", registration.PositiontypeID);
            ViewBag.QualificationID = new SelectList(db.tbl_Lookup_Qualifications, "QualificationId", "QualificationName", registration.QualificationID);
            ViewBag.maritalstatusid = new SelectList(db.tbl_Lookup_MaritalStatus, "maritalstatusid", "maritalstatus",registration.MaritalStatusId);

            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositiontypeID = new SelectList(db.tbl_lookup_PositionType, "PositiontypeId", "PositionName", registration.PositiontypeID);
            ViewBag.QualificationID = new SelectList(db.tbl_Lookup_Qualifications, "QualificationId", "QualificationName", registration.QualificationID);
            ViewBag.maritalstatusid = new SelectList(db.tbl_Lookup_MaritalStatus, "maritalstatusid", "maritalstatus",registration.MaritalStatusId);

            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationID,Surname,Name,FatherName,MaritalStatusId,DateofBirth,Gender,QualificationID,PositiontypeID,AreaOfIntrest,EmailAddress,ContactNumber,Address,ResumeFilePath")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositiontypeID = new SelectList(db.tbl_lookup_PositionType, "PositiontypeId", "PositionName", registration.PositiontypeID);
            ViewBag.QualificationID = new SelectList(db.tbl_Lookup_Qualifications, "QualificationId", "QualificationName", registration.QualificationID);
            ViewBag.maritalstatusid = new SelectList(db.tbl_Lookup_MaritalStatus, "maritalstatusid", "maritalstatus", registration.MaritalStatusId);

            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
