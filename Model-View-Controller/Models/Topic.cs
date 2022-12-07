namespace Model_View_Controller.Models
{
    public class Topic
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<CheatSheetItem> CheetSheetItems { get; set; }

        public Topic()
        {
            CheetSheetItems = new List<CheatSheetItem>();
        }
    }
}
