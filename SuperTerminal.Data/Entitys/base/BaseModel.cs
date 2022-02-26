using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperTerminal.Data
{
    public abstract class BaseModel : IModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Comment("主键"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//主键自增
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("创建时间")]
        public DateTime? CreateOn { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Comment("创建人")]
        public int? CreateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Comment("更新时间")]
        public DateTime? UpdateOn { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("更新人")]
        public int? UpdateBy { get; set; }
        /// <summary>
        /// 是否已经删除
        /// </summary>
        [Comment("是否已经删除"), DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
