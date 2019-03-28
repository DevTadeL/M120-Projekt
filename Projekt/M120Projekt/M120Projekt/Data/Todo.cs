using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace M120Projekt.Data
{
    public class Todo
    {
        #region Datenbankschicht
        [Key]
        public Int64 TodoID { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public Boolean Done { get; set; }
        public DateTime Deadline { get; set; }
        public String Description { get; set; }
        public String Asignee { get; set; }
        public String Place { get; set; }
        public Int64 Position { get; set; }
        public String TelNumber { get; set; }
        [Required]
        public Int64 Priority { get; set; }

        #endregion
        #region Applikationsschicht
        public Todo() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
            public static IEnumerable<Data.Todo> GetAll()
        {
            return (from record in Data.Global.context.Todo select record);
        }
        public static Data.Todo GetById(Int64 todoID)
        {
            return (from record in Data.Global.context.Todo where record.TodoID == todoID select record).FirstOrDefault();
        }
        public static IEnumerable<Data.Todo> GetByTitle(String suchbegriff)
        {
            return (from record in Data.Global.context.Todo where record.Title == suchbegriff select record);
        }
        public static IEnumerable<Data.Todo> GetByTitleLike(String suchbegriff)
        {
            return (from record in Data.Global.context.Todo where record.Title.Contains(suchbegriff) select record);
        }
        public Int64 Create()
        {
            if (this.Title == null || this.Title == "") this.Title = "leer";
            if (this.Created == null) this.Created = DateTime.MinValue;
            if (this.Deadline == null) this.Deadline = DateTime.MinValue;
            Data.Global.context.Todo.Add(this);
            Data.Global.context.SaveChanges();
            return this.TodoID;
        }
        public Int64 Update()
        {
            Data.Global.context.Entry(this).State = System.Data.Entity.EntityState.Modified;
            Data.Global.context.SaveChanges();
            return this.TodoID;
        }
        public void Delete()
        {
            Data.Global.context.Entry(this).State = System.Data.Entity.EntityState.Deleted;
            Data.Global.context.SaveChanges();
        }
        public static void DeleteAll(){
            Data.Global.context.Database.ExecuteSqlCommand("DELETE FROM dbo.Todo");
            //TodoID-counter resetten.
            Data.Global.context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Todo', RESEED, 0)");
        }
        public override string ToString()
        {
            return TodoID.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
