using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeniorProjectECS.DAL;
using SeniorProjectECS.Models;

namespace SeniorProjectECS.Controllers
{
    public class StaffMembersController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: StaffMembers
        public ActionResult Index()
        {
            var staffMembers = db.StaffMembers.Include(s => s.Center);
            return View(staffMembers.ToList());
        }

        // GET: StaffMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            return View(staffMember);
        }

        // GET: StaffMembers/Create
        public ActionResult Create()
        {
            ViewBag.CenterID = new SelectList(db.Centers, "CenterID", "Name");
            return View();
        }

        // POST: StaffMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffMemberID,Name,Email,DateOfHire,Position,CenterID,DirectorCredentials,DCExpiration,CDAInProgress,CDAType,CDAExpiration,CDARenewalProcess,Degree,Comments,Goal,MidYear,EndYear,GoalMet,TAndAApp,AppApp,ClassCompleted,ClassPaid,RequiredHours,HoursEarned,Notes,TermDate")] StaffMember staffMember)
        {
            if (ModelState.IsValid)
            {
                db.StaffMembers.Add(staffMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CenterID = new SelectList(db.Centers, "CenterID", "Name", staffMember.CenterID);
            return View(staffMember);
        }

        // GET: StaffMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.CenterID = new SelectList(db.Centers, "CenterID", "Name", staffMember.CenterID);
            return View(staffMember);
        }

        // POST: StaffMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffMemberID,Name,Email,DateOfHire,Position,CenterID,DirectorCredentials,DCExpiration,CDAInProgress,CDAType,CDAExpiration,CDARenewalProcess,Degree,Comments,Goal,MidYear,EndYear,GoalMet,TAndAApp,AppApp,ClassCompleted,ClassPaid,RequiredHours,HoursEarned,Notes,TermDate")] StaffMember staffMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CenterID = new SelectList(db.Centers, "CenterID", "Name", staffMember.CenterID);
            return View(staffMember);
        }

        // GET: StaffMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            return View(staffMember);
        }

        // POST: StaffMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffMember staffMember = db.StaffMembers.Find(id);
            db.StaffMembers.Remove(staffMember);
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
