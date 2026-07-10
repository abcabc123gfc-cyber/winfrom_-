using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("StoreArea")]
    public partial class StoreArea
    {
           public StoreArea(){


           }
           /// <summary>
           /// Desc:仓库区域Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int StoreAreaId {get;set;}

           /// <summary>
           /// Desc:仓库区域编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string StoreAreaNo {get;set;}

           /// <summary>
           /// Desc:仓库区域名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string StoreAreaName {get;set;}

           /// <summary>
           /// Desc:仓库Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int StoreId {get;set;}

           /// <summary>
           /// Desc:仓库区域当前温度
           /// Default:
           /// Nullable:False
           /// </summary>           
           public double Temperature {get;set;}

           /// <summary>
           /// Desc:仓库区域最小温度
           /// Default:
           /// Nullable:False
           /// </summary>           
           public double MinTemperature {get;set;}

           /// <summary>
           /// Desc:仓库区域最大温度
           /// Default:
           /// Nullable:False
           /// </summary>           
           public double MaxTemperature {get;set;}

           /// <summary>
           /// Desc:温度状态（0正常，1高预警，2低预警）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TemperatureStatus {get;set;}

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
