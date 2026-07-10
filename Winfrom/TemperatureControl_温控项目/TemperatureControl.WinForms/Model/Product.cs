using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Product")]
    public partial class Product
    {
           public Product(){


           }
           /// <summary>
           /// Desc:产品Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ProductId {get;set;}

           /// <summary>
           /// Desc:产品编码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ProductNo {get;set;}

           /// <summary>
           /// Desc:产品名称
           /// Default:format(getdate(),''yyyyMMdd'')+RIGHT(''00000000'' + CAST(ProductId AS VARCHAR(8)), 8)
           /// Nullable:False
           /// </summary>           
           public string ProductName {get;set;}

           /// <summary>
           /// Desc:产品适应的最小温度
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? MinTemperature {get;set;}

           /// <summary>
           /// Desc:产品适应的最大温度
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? MaxTemperature {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:状态（0正常，1禁用）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public short Status {get;set;}

           /// <summary>
           /// Desc:创建者Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:最后一次修改者Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? LastUpdateUserId {get;set;}

           /// <summary>
           /// Desc:最后一次修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastUpdateTime {get;set;}

    }
}
