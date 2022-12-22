using Microsoft.EntityFrameworkCore;

namespace KnowledgeJournies.Catalog.DataAccess
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext() { }
        public CatalogDbContext(DbContextOptions options) : base(options) { }

        public DbSet<KnowledgeStory> KnowledgeStories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<KnowledgeStory>(entity =>
            {
                entity.HasMany(e => e.SourceItems)
                    .WithOne()
                    .HasForeignKey(e => e.KnowledgeStoryId);
            });
            SnakeCaseTableNames(modelBuilder);
        }

        private static void SnakeCaseTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KnowledgeStory>(entity => { entity.ToTable("knowledge_story"); });
        }
    }
}
