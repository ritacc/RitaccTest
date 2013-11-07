
namespace MonitorSystem.Web.Moldes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // MetadataTypeAttribute 将 t_SelectPropertyMetadata 标识为类
    //，以携带 t_SelectProperty 类的其他元数据。
    [MetadataTypeAttribute(typeof(t_SelectProperty.t_SelectPropertyMetadata))]
    public partial class t_SelectProperty
    {

        // 通过此类可将自定义特性附加到
        //t_SelectProperty 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class t_SelectPropertyMetadata
        {

            // 元数据类不会实例化。
            private t_SelectPropertyMetadata()
            {
            }

            public string ChannelName { get; set; }

            public int ChannelNO { get; set; }

            public bool ISSelect { get; set; }
        }
    }
}
