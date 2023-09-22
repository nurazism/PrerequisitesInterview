using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        Entities db = new Entities();
        // GET: Tasks
        public ActionResult Index()
        {
            var dataList = db.TaskTables.ToList();
            return View(dataList);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            var data = db.TaskTables.First(x => x.TaskId == id);
            return View(data);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create

        private int SetTaskId()
        {
            int id = 0;

            var countTask = db.TaskTables.Count();
            id = countTask + 1;

            var checkTaskData = db.TaskTables.FirstOrDefault(x => x.TaskId == id);
            while (checkTaskData != null)
            {
                checkTaskData = db.TaskTables.FirstOrDefault(x => x.TaskId == id);
                id++;
            }

            return id;
        }

        [HttpPost]
        public ActionResult Create(TaskModels model)
        {
            try
            {
                // TODO: Add insert logic here

                TaskTable newData = new TaskTable
                {
                    TaskId = SetTaskId(),
                    UserId = User.Identity.GetUserName(),
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    IsCompleted = model.IsCompleted,
                };
                db.TaskTables.Add(newData);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.TaskTables.First(x => x.TaskId == id);
            return View(data);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TaskModels model)
        {
            try
            {
                // TODO: Add update logic here

                var editData = db.TaskTables.FirstOrDefault(x => x.TaskId == id);
                editData.Title = model.Title;
                editData.Description = model.Description;
                editData.DueDate = model.DueDate;
                editData.IsCompleted = model.IsCompleted;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.TaskTables.First(x => x.TaskId == id);
            return View(data);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TaskModels model)
        {
            try
            {
                // TODO: Add delete logic here

                var deleteData = db.TaskTables.FirstOrDefault(x => x.TaskId==id);
                
                db.TaskTables.Remove(deleteData);
                db.SaveChanges();   

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
