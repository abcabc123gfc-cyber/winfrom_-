using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
     interface   IUser
    {
          string Name { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
          string Residence { get; set; }
          char Sex { get; set; }
          DateTime Birth { get; set; }
          string BirthAddress { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
          string Political { get; set; }
          DateTime Party { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
          string Ethnicity { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
          double Weight { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
          double Height { get; set; }
        /// <summary>
        /// 外语水平
        /// </summary>
          string Language { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
          string Contact { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
          string Description { get; set; }
        /// <summary>
        ///  图片文件路径
        /// </summary>
        string PictureFIlePath { get; set; }
    }
}
