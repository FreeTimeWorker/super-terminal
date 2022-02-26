using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Data.Entitys
{
    public class TestModel:BaseModel
    {
        [Comment("范围")]
        public int Range { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Comment("电话")]
        public string Tel { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        [Comment("名字")]
        public string Name { get; set; }
    }
}
