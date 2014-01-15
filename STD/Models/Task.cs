using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using STD.Models;
using System.ComponentModel.DataAnnotations;

namespace STD.Models
{
    public class Task
    {
        public Task()
        {
            this.Finished = false;
        }
        public int Id { get; set; }

        public string UserName { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        
        [Required]
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate {get;set;}
        
        public bool Finished { get; set; }
    }


    public class TaskDBContext : DbContext
    {
        public DbSet<Task> Tasks {get;set;}
    }
}
