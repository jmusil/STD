using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STD.Models;
using STD.Infrastructure;
using STD.Controllers;

namespace STD.Controllers
{

    public class TaskController : Controller
    {
        private TaskDBContext db = new TaskDBContext();
        private ITaskRepository _repo;
        
        
        public TaskController(ITaskRepository repo)
        {
            _repo = repo;
        }

        //
        // GET: /Task/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Header = "All Tasks";
                return View(_repo.GetTasks().Where(x => x.UserName == User.Identity.Name).OrderBy(x => x.Finished));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Task/Today

        public ActionResult Today()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Header = "Today's Tasks";
                return View("Index", _repo.GetTasks().Where(t => t.DueDate == DateTime.Today).Where(x => x.UserName == User.Identity.Name).OrderBy(x => x.Finished));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Task/Week

        public ActionResult Week()
        {
            if (User.Identity.IsAuthenticated)
            {
                DateTime now = DateTime.Now;
                DayOfWeek startDayOfWeek = DayOfWeek.Monday;
                int diff = now.DayOfWeek - startDayOfWeek;
                if (diff < 0)
                {
                    diff += 7;
                }
                DateTime startOfWeek = now.AddDays(-1 * diff).Date;
                DateTime endOfWeek = startOfWeek.AddDays(7);
                ViewBag.Header = "This Week's Tasks";

                return View("Index", _repo.GetTasks().Where(t => t.DueDate >= startOfWeek && t.DueDate <= endOfWeek).Where(x => x.UserName == User.Identity.Name).OrderBy(x => x.Finished));   
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Task/Week

        public ActionResult Month()
        {
            if (User.Identity.IsAuthenticated)
            {
                DateTime now = DateTime.Now;
                ViewBag.Header = "This Month's Tasks";
                return View("Index", _repo.GetTasks().Where(t => t.DueDate.Month == now.Month).Where(x => x.UserName == User.Identity.Name).OrderBy(x => x.Finished));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id = 0)
        {
            Task task = _repo.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // GET: /Task/Create

        public ActionResult CreatePartial()
        {
            return PartialView();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePartial(Task task)
        {
            if (ModelState.IsValid)
            {
                task.UserName = User.Identity.Name;
                _repo.InsertTask(task);
                return Json("passed");
            }
            return Json("failed");
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Task task = _repo.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return PartialView(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                task.UserName = User.Identity.Name;
                _repo.EditTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = _repo.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteTaskById(id);
            return Json("passed");
        }

        //
        // GET: /Task/Finish/5
        public ActionResult Finish(int id = 0)
        {
            Task task = _repo.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return PartialView(task);
        }

        //
        // POST: /Task/Finish/5
        [HttpPost, ActionName("Finish")]
        public ActionResult FinishConfirmed(int id)
        {
            Task task = _repo.GetTaskById(id);
            if (User.Identity.IsAuthenticated && task.UserName == User.Identity.Name)
            {
                _repo.FinishTaskById(id);
                return Json("passed");
            }
            else
            {
                return Json("failed");
            }
        }

        //
        // GET: /Task/Finish/5
        public ActionResult Reopen(int id = 0)
        {
            Task task = _repo.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return PartialView(task);
        }

        //
        // POST: /Task/Finish/5
        [HttpPost, ActionName("Reopen")]
        public ActionResult ReopenConfirmed(int id)
        {
            Task task = _repo.GetTaskById(id);
            if (User.Identity.IsAuthenticated && task.UserName == User.Identity.Name)
            {
                _repo.ReopenTaskById(id);
                return Json("passed");
            }
            else
            {
                return Json("failed");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        
        public PartialViewResult ActiveTasksPartial()
        {
            return PartialView(db.Tasks.Where(t => t.Finished == false).Where(t => t.UserName == User.Identity.Name).Count());
        }

        public PartialViewResult CreateButtonPartial()
        {
            return PartialView();
        }
    }
}