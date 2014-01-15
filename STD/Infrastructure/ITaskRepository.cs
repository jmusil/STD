using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STD.Models;

namespace STD.Infrastructure
{
    public interface ITaskRepository
    {

        IEnumerable<Task> GetTasks();
        Task GetTaskById(int id);
        void FinishTaskById(int id);
        void ReopenTaskById(int id);
        void InsertTask(Task task);
        void EditTask(Task task);
        void DeleteTaskById(int id);


    }
}
