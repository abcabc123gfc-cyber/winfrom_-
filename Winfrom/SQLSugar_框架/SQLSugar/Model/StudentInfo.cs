using SqlSugar;
using System;

namespace SQLSugar.Model
{
    [SugarTable("Students")]
    public class StudentInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string StuName { get; set; }
        public int StuAge { get; set; }
        public int StuSex { get; set; }
        public DateTime StuBirthday { get; set; }
        public int Status { get; set; }
    }
}
