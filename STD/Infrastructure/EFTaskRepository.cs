using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STD.Infrastructure;
using STD.Models;
using System.Data;

namespace STD.Infrastructure
{
    public class EFTaskRepository : ITaskRepository
    {
        public TaskDBContext db = new TaskDBContext();
        
        public IEnumerable<Task> GetTasks()
        {
            return db.Tasks.ToList();
        }

        public Task GetTaskById(int id)
        {
            return db.Tasks.Find(id);
        }

        public void InsertTask(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void EditTask(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteTaskById(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
        }

        public void FinishTaskById(int id)
        {
            Task task = db.Tasks.Find(id);
            task.Finished = true;
            db.SaveChanges();
        }
        
        public void ReopenTaskById(int id)
        {
            Task task = db.Tasks.Find(id);
            task.Finished = false;
            db.SaveChanges();
        }
    }
}