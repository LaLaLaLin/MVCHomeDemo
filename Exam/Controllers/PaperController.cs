﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Models;
using Exam.Models.ViewModels;
using System.Data.Entity;
using Exam.Filter;

namespace Exam.Controllers
{
    /// <summary>
    /// 考卷管理
    /// </summary>
    public class PaperController : Controller
    {
        ExamSysEntities1 ef = new ExamSysEntities1();
        // GET: Paper
        [LoginHou]
        public ActionResult Index()
        {
            List<Class> GradeListSum = ef.Classes.ToList();
            ViewData["GradeList"] = GradeListSum;
            return View();
        }

        public ActionResult Yu(string bian)
        {
            ViewData["Bian"] = bian;
            return View();
        }

        [HttpPost]
        public ActionResult List(string page)
        {
            List<Grade> Gradli = new List<Grade>();
            foreach (var item in ef.Grades.Where(x => x.Shan == false).ToList())
            {
                Gradli.Add(new Grade()
                {
                    GradeName = item.GradeName
                });
            }
            Gradli.Insert(0, new Grade() { GradeName = "全部" });
            int ye = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);
            List<PaperS> li = Show();
            int CountYe = li.Count % 10 > 0 ? (li.Count / 10) + 1 : li.Count / 10;
            var Tili = new { Li = li.Skip((ye - 1) * 10).Take(10), Ye = CountYe, Hang = page, list1 = Gradli };
            return Json(Tili, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Select(string page, string GradeID)
        {
            int ye = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);
            List<PaperS> li = Show();
            List<PaperS> pagedList = null;
            if (!string.IsNullOrEmpty(GradeID))
            {
                pagedList = li.Where(x => x.GradeIDName == GradeID).ToList();
            }
            int CountYe = pagedList.Count % 10 > 0 ? (pagedList.Count / 10) + 1 : pagedList.Count / 10;
            var Tili = new { Li = pagedList.Skip((ye - 1) * 10).Take(10).ToList(), Ye = CountYe, Hang = ye };
            return Json(Tili, JsonRequestBehavior.AllowGet);
        }
        public List<PaperS> Show()
        {
            List<PaperS> li = new List<PaperS>();
            foreach (var item in ef.Papers.ToList())
            {
                li.Add(new PaperS()
                {
                    CreateTime = item.CreateTime.ToString(),
                    CreatorID = item.CreatorID,
                    Duration = item.Duration,
                    IsOpen = item.IsOpen == false ? 0 : 1,
                    PaperID = item.PaperID,
                    PaperName = item.PaperName,
                    QuestionCount = item.QuestionCount,
                    RuleID = item.RuleID.ToString(),
                    typeID = item.typeID.ToString(),
                    GradeIDName = ef.Grades.FirstOrDefault(x => x.GradeID == item.GradeID).GradeName,
                    ClassList = item.ClassList,
                });
            }
            return li;
        }

        [HttpPost]
        public ActionResult Update(string id, string Shi)
        {
            int ID = Convert.ToInt32(id);
            bool flag = Shi == "0" ? true : false;
            Paper te = ef.Papers.FirstOrDefault(x => x.PaperID == ID);
            te.IsOpen = flag;
            ef.Entry(te).State = EntityState.Modified;
            if (ef.SaveChanges() > 0)
            {
                return Content("成功");
            }
            return Content("失败");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                ef.Configuration.LazyLoadingEnabled = false;
                ef.Configuration.ProxyCreationEnabled = false;
                int ID = Convert.ToInt32(id);
                Paper te = ef.Papers.FirstOrDefault(x => x.PaperID == ID);

                ef.Papers.Remove(te);
                if (ef.SaveChanges() > 0)
                {
                    return Content("删除成功");
                }
                return Content("失败！");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

        }



        /// <summary>
        /// 返回 策略类型 年级信息  考试类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowListJie()
        {
            List<string> GradeLi = new List<string>();
            List<string> TypeLi = new List<string>();
            List<string> CeLve = new List<string>();
            List<string> GradeListSum = new List<string>();
            foreach (var item in ef.Grades.ToList())
            {
                GradeLi.Add(item.GradeName);
            }
            foreach (var item in ef.PaperTypes.ToList())
            {
                TypeLi.Add(item.TypeName);
            }

            foreach (var item in ef.PaperRules.ToList())
            {
                CeLve.Add(item.RuleName);
            }
            foreach (var item in ef.Classes)
            {
                GradeListSum.Add(item.ClassName);
            }
            CeLve.Add("自定义策略");
            var Sum = new { Grad = GradeLi, Ty = TypeLi, CL = CeLve, GradeLi = GradeListSum };
            return Json(Sum, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ADDPaper(PaperS te)
        {
            try
            {
                string TypeName = te.typeID.ToString();
                string RuleName = te.RuleID.ToString();
                List<Paper> PapA = ef.Papers.Where(x => x.Shan == false).ToList();
                Paper ta = new Paper();
                ta.CreateTime = DateTime.Now;
                ta.PaperName = te.ClassList + "(1)";
                if (PapA.Count == 0)
                    ta.PaperName = te.ClassList + "(1)";
                else
                    ta.PaperName = te.ClassList + "-" + PapA[PapA.Count - 1].PaperID;
                //foreach (var item in PapA)
                //{
                //    if (ta.PaperName == item.PaperName)
                //    {
                //        item.PaperName.Substring(item.PaperName.IndexOf('(')+1);
                //    }
                //}
                ta.GradeID = ef.Grades.FirstOrDefault(x => x.GradeName == te.GradeIDName).GradeID;
                ta.typeID = ef.PaperTypes.FirstOrDefault(x => x.TypeName == TypeName).TypeID;
                ta.Duration = te.Duration;
                ta.IsOpen = te.IsOpen == 0 ? false : true;
                ta.ClassList = te.ClassList;
                ta.RuleID = ef.PaperRules.FirstOrDefault(x => x.RuleName == RuleName && x.Shan == false).RuleID;
                ta.CreatorID = 1;

                ta.QuestionCount = 0; ;
                ta.QuestionCount = 0; ;
                ef.Entry(ta).State = EntityState.Added;
                ef.SaveChanges();
                List<Paper> PapeID = ef.Papers.ToList();

                //List<RuleDetail> list = ef.RuleDetails.Where(x => x.RuleID == ef.PaperRules.FirstOrDefault(a => a.RuleName == RuleName && a.Shan == false).RuleID).ToList();
                //for (int i = 0; i < list.Count; i++)
                    //获取根据策略名称的抽屉策略的详细 获得
                 List<Question> ListNum = ef.Questions.Where(x => x.BookID == ef.RuleDetails.FirstOrDefault(a => a.RuleID == ef.PaperRules.FirstOrDefault(b => b.RuleName == RuleName).RuleID).BookID).ToList();
                //策略名称的 使用策略的章节数量
                for (int i = 0; i < 50; i++)
                {
                    Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + i);
                    int num = r.Next(ListNum[0].QuestionID, ListNum[ListNum.Count - 1].QuestionID);
                    PaperDetail Xiang = new PaperDetail();
                    Xiang.QuestionID = num;
                    Xiang.PaperID = PapeID[PapeID.Count - 1].PaperID;
                    ef.Entry(Xiang).State = EntityState.Added;
                    ef.SaveChanges();

                }
                return Content("添加成功");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

    }
}