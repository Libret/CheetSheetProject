using Model_View_Controller.Models;

namespace Model_View_Controller.Repositories
{
    public class UsefulLinkRepository
    {
        public static void AddNewUsefulLink(string usefulLinkAdress, int order, string uheetSheetItemId)
        {
            var id = Guid.NewGuid();
            SQLTableManagement.InsertData("Topic", "Id, LinkAddress, LinkOrder, CheetSheetItemId", $"\"{id}\", \"{usefulLinkAdress}\", \"{order}\", \"{uheetSheetItemId}\"");
        }

        public static List<UsefulLink> GetAllTopics()
        {
            var allUsefulLinks = new List<UsefulLink>();
            var sqlite_datareader = SQLTableManagement.ReadData("UsefulLink");
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string usefulLinkAdress = sqlite_datareader.GetString(1);
                string order = sqlite_datareader.GetString(2);
                allUsefulLinks.Add(new UsefulLink
                {
                    Id = id,
                    LinkAddress = usefulLinkAdress,
                    Order = int.Parse(order)
                });
            }
            SQLTableManagement.GetSQLiteConnection().Close();
            return allUsefulLinks;
        }
    }
}
