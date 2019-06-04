using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120Projekt
{
    static class APIDemo
    {
        #region KlasseA
        // Create
        public static void DemoACreate()
        {
            Debug.Print("--- DemoACreate ---");
            // Todo 1
            Data.Todo todo1 = new Data.Todo();
            todo1.Title = "Einfen";
            todo1.Description = "Eier\nMilch\nTomaten\nKartoffeln";
            todo1.Priority = 2;
            todo1.Place = "Migros";
            todo1.Asignee = "Freundin";
            todo1.Created = DateTime.Today;
            Int64 todo1ID = todo1.Create();
            Debug.Print("Todo erstellt mit Id:" + todo1ID);
            //
            // Todo 2
            Data.Todo todo2 = new Data.Todo();
            todo2.Title = "Zahnarzt anrufen";
            todo2.Description = "Herrn Zahndudr anrufen um Termin zu vereinbaren.";
            todo2.Priority = 1;
            todo2.Asignee = "Herr Zahndudr";
            todo2.TelNumber = "0041 79 552 12 24";
            todo2.Deadline = DateTime.Today;
            todo2.Created = DateTime.Today;
            todo2.Done = true;
            Int64 todo2ID = todo2.Create();
            Debug.Print("Todo erstellt mit Id:" + todo2ID);
            //
            // Todo 3
            Data.Todo todo3 = new Data.Todo();
            todo3.Title = "Projekt 120";
            todo3.Description = "Arbeitsblätter A3, B1, B2, B3";
            todo3.Priority = 1;
            todo3.Asignee = "Herr Bernasconi";
            todo3.Deadline = DateTime.Today;
            todo3.Created = DateTime.Today;
            Int64 todo3ID = todo3.Create();
            Debug.Print("Todo erstellt mit Id:" + todo3ID);
        }
        public static void DemoACreateKurz()
        {
            Data.Todo todo4 = new Data.Todo { Title = "Todo leer", Done = true, Created = DateTime.Today };
            Int64 todo4ID = todo4.Create();
            Debug.Print("Todo erstellt mit Id:" + todo4ID);
        }

        // Read
        public static void DemoARead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.Todo todo in Data.Todo.GetAll())
            {
                Debug.Print("Todo Id:" + todo.TodoID + " Name:" + todo.Title);
            }
        }
        // Update
        public static void DemoAUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // Todo ändert Attribute
            Data.Todo todo1 = Data.Todo.GetById(1);
            todo1.Title = "Einkaufen";
            todo1.Update();
        }
        // Delete
        public static void DemoADelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Todo.DeleteAll();
            Debug.Print("Alle Todos gelöscht");
        }
        #endregion
    }
}
