﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExamSysEntities1 : DbContext
    {
        public ExamSysEntities1()
            : base("name=ExamSysEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperDetail> PaperDetails { get; set; }
        public virtual DbSet<PaperRule> PaperRules { get; set; }
        public virtual DbSet<PaperType> PaperTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RuleDetail> RuleDetails { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StuQueRecord> StuQueRecords { get; set; }
        public virtual DbSet<StuQueRecordDetail> StuQueRecordDetails { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TextBook> TextBooks { get; set; }
        public virtual DbSet<JinRuKaiShi> JinRuKaiShis { get; set; }
    }
}
