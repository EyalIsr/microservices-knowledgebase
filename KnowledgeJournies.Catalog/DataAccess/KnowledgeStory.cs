namespace KnowledgeJournies.Catalog.DataAccess
{
    public class KnowledgeStory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public List<SourceItem> SourceItems { get; set; }
    }
}
