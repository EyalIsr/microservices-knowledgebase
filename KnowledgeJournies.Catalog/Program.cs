using KnowledgeJournies.Catalog.DataAccess;
using KnowledgeJournies.Catalog.Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogDbContext>(opts =>
    {
        opts.EnableSensitiveDataLogging();
        opts.EnableDetailedErrors();
        opts.UseNpgsql(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.MapGet("/journey/{id}", async (int id, CatalogDbContext dbContext) =>
{
    var entity = await dbContext.KnowledgeStories.FindAsync(id);
    return Results.Ok(entity);
});

app.MapPost("/journey", async (KnowledgeStoryDto story, CatalogDbContext dbContext) =>
{
    var storyEntity = new KnowledgeStory
    {
        Description = story.Description,
        Name = story.Name,
        Tags = story.Tags
    };
    await dbContext.KnowledgeStories.AddAsync(storyEntity);
    await dbContext.SaveChangesAsync();
});

app.MapPut("/journey/{id}", async (int id, SourceItemDto sourceItem, CatalogDbContext dbContext) => {
    var storyEntity = await dbContext.KnowledgeStories.FindAsync(id);
    if (storyEntity == null)
    {
        throw new ArgumentException($"{id} is not a valid journey id");
    }

    var sourceItemEntity = new SourceItem
    {
        Comment = sourceItem.Comment,
        QuotedText = sourceItem.QuotedText,
        Url = sourceItem.Url
    };
    storyEntity.SourceItems.Add(sourceItemEntity);
    dbContext.KnowledgeStories.Update(storyEntity);
    await dbContext.SaveChangesAsync();
});

app.Run();
