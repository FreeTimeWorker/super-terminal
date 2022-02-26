using Microsoft.EntityFrameworkCore;
using SuperTerminal.Data.Entitys;

namespace SuperTerminal.Data.Maintain
{
    public partial class MaintainContent : DbContext
    {
        public DbSet<SysUser> SysUser { get; set; }

        public DbSet<TestModel> TestModel { get; set; }
    }
}
