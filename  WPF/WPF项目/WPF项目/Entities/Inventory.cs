using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Inventory")]
    public partial class Inventory
    {
           public Inventory(){


           }
           /// <summary>
           /// Desc:产品库存Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int InventoryId {get;set;}

           /// <summary>
           /// Desc:产品Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ProductId {get;set;}

           /// <summary>
           /// Desc:仓库Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? StoreId {get;set;}

           /// <summary>
           /// Desc:仓库区域Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? StoreAreaId {get;set;}

           /// <summary>
           /// Desc:产品数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Amount {get;set;}

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
