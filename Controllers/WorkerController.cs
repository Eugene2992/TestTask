using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DB;
using Test.Models;

namespace Test.Controllers
{
    public class WorkerController : Controller
    {
        //загрузка,формирование и отображение списка рабочих из БД в виде таблицы
        List<Worker> allWorkersList = new List<Worker>();
        public IActionResult Index()
        {
            DBWorker dbWorkerWorkersTable = new DBWorker();
            dbWorkerWorkersTable.GetAllWorkers(allWorkersList);
            return View(allWorkersList);
        }


        public RedirectToRouteResult Delete(Worker item)
        {
            //Удалить значение по id (по нажатию кнопки)
            DBWorker dbWorkerCrud = new DBWorker();
            dbWorkerCrud.DeleteWorker(item.Id);
            return RedirectToRoute(new 
            { 
                action = "Index", 
                titleinfo = "Удаление информации о работнике" 
            });
        }

        public RedirectToRouteResult Add()
        {
            //Добавить значение (по нажатию кнопки)
            return RedirectToRoute(new 
            { 
                controller = "addOrEditWorkerForm", 
                action = "Index", 
                titleinfo = "Добавление информации о работнике", 
                editoradd = "add"
            });
        }

        public RedirectToRouteResult Edit(Worker item)
        {
            //Редактировать значение по id (по нажатию кнопки)
            return RedirectToRoute(new
            {
                controller = "addOrEditWorkerForm",
                action = "Index",
                titleinfo = "Редактирование информации о работнике",
                editoradd = "edit",
                Id= item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Patronymic = item.Patronymic,
                DateRecruitment = item.DateRecruitment,
                Position = item.Position,
                Company = item.Company
            });
        }



    }

}
