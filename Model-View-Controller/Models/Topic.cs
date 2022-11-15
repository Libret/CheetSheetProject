namespace Model_View_Controller.Models
{
    public class Topic
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<CheetSheetItem> CheetSheetItems { get; set; }

        public Topic()
        {
            CheetSheetItems = new List<CheetSheetItem>();
        }
    }
}
