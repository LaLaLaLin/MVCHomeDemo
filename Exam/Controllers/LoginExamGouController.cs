﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Models;

namespace Exam.Controllers
{
    public class LoginExamGouController : Controller
    {
        ExamSysEntities1 ef = new ExamSysEntities1();
        // GET: LoginExamGou
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dian(string LoginDeng, string LognPwd)
        {
            if(ef.Teachers.FirstOrDefault(x=>x.Password==LognPwd && x.UserName == LoginDeng) != null)
            {
                Session["User"] = ef.Teachers.FirstOrDefault(x => x.Password == LognPwd && x.UserName == LoginDeng);
                return Content("成功");
            }
            return Content("账号或者密码错误");
        }
    }
}