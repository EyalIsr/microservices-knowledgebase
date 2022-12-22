namespace KnowledgeJournies.Catalog.DataAccess
{
    public class SourceItem
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string QuotedText { get; set; }
        public string Comment { get; set; }

        public int KnowledgeStoryId { get; set; }
    }
}
