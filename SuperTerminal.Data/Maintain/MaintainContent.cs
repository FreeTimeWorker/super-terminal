using Microsoft.EntityFrameworkCore;

namespace SuperTerminal.Data.Maintain
{
    public partial class MaintainContent : DbContext
    {
        public MaintainContent(DbContextOptions<MaintainContent> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
