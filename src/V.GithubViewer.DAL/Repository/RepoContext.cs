using V.GithubViewer.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace V.GithubViewer.DAL.Repository
{
    public class RepoContext : DbContext
    {
        public DbSet<RepoEntity> Repos { get; internal set; }

        public RepoContext(DbContextOptions<RepoContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepoEntity>().HasKey(_ => _.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}