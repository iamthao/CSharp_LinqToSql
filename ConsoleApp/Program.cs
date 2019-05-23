using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GradeAction.Show();
        New:
            Console.WriteLine("Go 1 action: add, edit, delete");
            Console.WriteLine("Go exit de thoat");
            var action = Console.ReadLine();
            int id;
            switch (action)
            {
                case "add":
                    id = GradeAction.Add();
                    Console.WriteLine("Add Item Success ID = {0}", id);
                    break;
                case "edit":
                    Console.WriteLine("Nhap ID can edit");
                    id = Convert.ToInt32(Console.ReadLine());
                    GradeAction.Edit(id);
                    break;
                case "delete":
                    Console.WriteLine("Nhap ID can delete");
                    id = Convert.ToInt32(Console.ReadLine());
                    GradeAction.Delete(id);
                    break;
                case "none":
                    break;
            }

            if (action == "add" || action == "edit" || action == "delete")
            {
                GradeAction.Show();
                Console.WriteLine("Ban co muon tiep tuc (y/n)?:");
                var yesNo = Console.ReadLine();
                switch (yesNo)
                {
                    case "y":
                        goto New;
                    case "n":
                        break;
                }
            }

            Console.WriteLine("Nhap phim bat ky de dong");
            Console.ReadLine();
        }
    }

    public class Test
    {
        public int id { get; set; }
        public int name { get; set; }
    }

    public static class GradeAction
    {
        public static void Show()
        {
            using (ConnectDbDataContext db = new ConnectDbDataContext())
            {
                var data = db.Grades.ToList();
                foreach (var item in data)
                {
                    Console.WriteLine("ID: {0} - Name: {1}", item.id, item.name);
                }
            }
        }
        public static int Add()
        {
            using (ConnectDbDataContext db = new ConnectDbDataContext())
            {
                var item = new Grade();
                item.name = String.Format("Lop {0}", GenerateTime());
                db.Grades.InsertOnSubmit(item);
                db.SubmitChanges();
                return item.id;
            }
        }

        public static void Edit(int id)
        {
            using (ConnectDbDataContext db = new ConnectDbDataContext())
            {
                var item = db.Grades.FirstOrDefault(o => o.id == id);
                if (item != null)
                {
                    Console.WriteLine("nhap name moi:");
                    item.name = Console.ReadLine();
                    db.SubmitChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (ConnectDbDataContext db = new ConnectDbDataContext())
            {
                var item = db.Grades.FirstOrDefault(o => o.id == id);
                if (item != null)
                {
                    db.Grades.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
            }
        }

        public static string GenerateTime()
        {
            var now = DateTime.Now;
            return String.Format("{0}{1}{2}{3}{4}{5}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        }
    }
}

