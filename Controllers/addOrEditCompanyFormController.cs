using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DB;

namespace Test.Controllers
{
    public class addOrEditCompanyFormController : Controller
    {
        private int idCompanyMaxCount = 0;
        private static string editoraddRoute;
        Company selectedCompany = new Company();
        public IActionResult Index(string titleinfo, string editoradd, int Id, string Name, string OrgLegalFormName, int SizeOfCompany)
        {
            ViewBag.Title = titleinfo;
            editoraddRoute = editoradd;
            if (Id == 0)
            {
                selectedCompany.Name = "Введите имя";
                selectedCompany.OrgLegalFormName = "Введите правовую форму";
            }
            else
            {
                selectedCompany.Id = Id;
                selectedCompany.Name = Name;
                selectedCompany.OrgLegalFormName = OrgLegalFormName;
                selectedCompany.SizeOfCompany = SizeOfCompany;
            }
            return View(selectedCompany); 
        }

        [HttpPost]
        public RedirectToRouteResult Route(int Id,string Name,string OrgLegalFormName)
        {
            
            if (editoraddRoute == "add")
            {
                idCompanyMaxCount++;
                Id = idCompanyMaxCount;
                DBCompany dbCompanyCrud = new DBCompany();
                dbCompanyCrud.CreateCompany(Id, Name, OrgLegalFormName);
                return RedirectToRoute(new { controller = "Company", action = "Index" });
            }
            else
            {
                DBCompany dbCompanyCrud = new DBCompany();
                dbCompanyCrud.ChangeCompany(Id, Name, OrgLegalFormName);
                return RedirectToRoute(new { controller = "Company", action = "Index" });
            }
           
        }
       
    }
}
