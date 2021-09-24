using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.DB;

namespace Test.Controllers
{
    public class CompanyController : Controller
    {
        List<Company> allCompaniesList = new List<Company>();
        public IActionResult Index()
        {
            ViewBag.LabelSizeName = "Company Size";         
            DBCompany dbCompanyCompaniesTable = new DBCompany();
            dbCompanyCompaniesTable.GetAllCompanies(allCompaniesList);
            return View(allCompaniesList);
        }
        public RedirectToRouteResult Delete(Worker item)
        {
            //Удалить значение по id
            DBCompany dbCompanyCrud = new DBCompany();
            dbCompanyCrud.DeleteCompany(item.Id);
            return RedirectToRoute(new
            {
                action = "Index",
                titleinfo = "Удаление информации о компании"
            });
        }

        public RedirectToRouteResult Add(int id)
        {
            //Добавить значение по id
            return RedirectToRoute(new
            {
                controller = "addOrEditCompanyForm",
                action = "Index",
                titleinfo = "Добавление информации о компании",
                editoradd = "add"
            });
        }

        public RedirectToRouteResult Edit(Company item)
        {
            //Редактировать значение по id
            return RedirectToRoute(new
            {
                controller = "addOrEditCompanyForm",
                action = "Index",
                titleinfo = "Редактирование информации о компании",
                editoradd = "edit",
                Id = item.Id,
                Name = item.Name,
                OrgLegalFormName = item.OrgLegalFormName,
                SizeOfCompany = item.SizeOfCompany,
               
            });
        }

    }
}
