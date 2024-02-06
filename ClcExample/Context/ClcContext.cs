using ClcExample.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ClcExample.Context;

public class ClcContext : DbContext

{
    public ClcContext(DbContextOptions<ClcContext> options) : base(options) { }
    
    public virtual DbSet<Link> Links { get; set; }
}
