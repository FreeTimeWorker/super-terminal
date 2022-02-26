using System;

namespace SuperTerminal.Data
{
    public interface IModel
    {
        /// <summary>
        /// Id
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        int? CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreateOn { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        int? UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? UpdateOn { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
