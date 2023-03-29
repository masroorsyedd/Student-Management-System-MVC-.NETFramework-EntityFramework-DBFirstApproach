using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementSystem_ASP.NET_Framework_MVC.Controllers
{
    public class HomeController : Controller
    {
        MVC_StudentContext studentContext = new MVC_StudentContext();
        public ActionResult Index()
        {
            var studentList = studentContext.Students.ToList();
            return View(studentList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                studentContext.Students.Add(student);
                studentContext.SaveChanges();
                ViewBag.Message = "Data Inserted Successfully";
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = studentContext.Students.Where(x => x.StudentId == id).FirstOrDefault();   
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Student studentModel)
        {
            var student = studentContext.Students.Where(x => x.StudentId == studentModel.StudentId).FirstOrDefault();
            if(student != null)
            {
                student.StudentName = studentModel.StudentName;
                student.StudentCity = studentModel.StudentCity;
                student.StudentFees = studentModel.StudentFees;
                studentContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var student = studentContext.Students.Where(x => x.StudentId == id).FirstOrDefault();
            return View(student);
        }

        public ActionResult Delete(int id)
        {
            var student = studentContext.Students.Where(x => x.StudentId == id).FirstOrDefault();
            studentContext.Students.Remove(student);
            studentContext.SaveChanges();
            ViewBag.Message = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}