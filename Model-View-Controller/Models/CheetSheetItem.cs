namespace Model_View_Controller.Models
{
    public class CheetSheetItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CodeSnippet { get; set; }

        public string AdditionalInfo { get; set; }

        public List<UsefulLink> usefulLinks { get; set; }
    }
}
