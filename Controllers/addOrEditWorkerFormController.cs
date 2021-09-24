using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DB;

namespace Test.Controllers
{
    public class addOrEditWorkerFormController : Controller
    {
        private int idWorkerMaxCount = 0;
        private static string editoraddRoute;
        Worker selectedWorker = new Worker();
        public IActionResult Index(string titleinfo, string editoradd, int id, string FirstName, string SecondName, string Patronymic, DateTime DateRecruitment, Positions Position, string Company)
        {
            //формирование формы редактирования/добавления
            ViewBag.Title = titleinfo;
            editoraddRoute = editoradd;
            if (id == 0)
            {
                //если добавление нового работника (id==0), то заполнение формы подсказками
                selectedWorker.FirstName = "Введите имя";
                selectedWorker.SecondName = "Введите фамилию";
                selectedWorker.Patronymic = "Введите отчество";
                selectedWorker.DateRecruitment = DateTime.Now ;
                selectedWorker.Position = Positions.Разработчик;
                selectedWorker.Company = "Введите команию";
            }
            else
            {
                //если редактирование информации о работнике (id!=0), то заполнение полей полученными из БД информацией
                selectedWorker.Id = id;
                selectedWorker.FirstName = FirstName;
                selectedWorker.SecondName = SecondName;
                selectedWorker.Patronymic = Patronymic;
                selectedWorker.DateRecruitment = DateRecruitment;
                selectedWorker.Position = Position;
                selectedWorker.Company = Company;
            }
            return View(selectedWorker); 
        }

        [HttpPost]
        public RedirectToRouteResult Route(int id,string FirstName,string SecondName,string Patronymic,string DateRecruitment,string Position,string Company)
        {
            //определение выбора пользователя (добавление/редактирование)
            if (editoraddRoute == "add")
            {
                idWorkerMaxCount++;
                id = idWorkerMaxCount;
                DBWorker dbWorkerCrud = new DBWorker();
                dbWorkerCrud.CreateWorker(id, FirstName, SecondName, Patronymic, DateRecruitment, Position, Company);
                return RedirectToRoute(new { controller = "Worker", action = "Index" });
            }
            else
            {
                DBWorker dbWorkerCrud = new DBWorker();
                dbWorkerCrud.ChangeWorker(id, FirstName, SecondName, Patronymic, DateRecruitment, Position, Company);
                return RedirectToRoute(new { controller = "Worker", action = "Index" });
            }
           
        }
       
    }
}
