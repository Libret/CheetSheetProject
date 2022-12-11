using Model_View_Controller.Models;

namespace Model_View_Controller.Repositories
{
    public class UsefulLinkRepository
    {
        private static readonly string stringUsefulLink = "UsefulLink";

        public static void AddNewUsefulLink(UsefulLink usefulLink, string? cheetSheetItemId)
        {
            var id = Guid.NewGuid();
            SQLTableManagement.InsertData(stringUsefulLink, "Id, LinkAddress, LinkOrder, CheetSheetItemId", $"\"{id}\", \"{usefulLink.LinkAddress}\", \"{usefulLink.LinkOrder}\", \"{cheetSheetItemId}\"");
        }

        public static List<UsefulLink> GetAllLtnks()
        {
            var allUsefulLinks = new List<UsefulLink>();
            var sqlite_datareader = SQLTableManagement.ReadData(stringUsefulLink, null);
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string usefulLinkAdress = sqlite_datareader.GetString(1);
                int order = sqlite_datareader.GetInt32(2);
                allUsefulLinks.Add(new UsefulLink
                {
                    Id = id,
                    LinkAddress = usefulLinkAdress,
                    LinkOrder = order
                });
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return allUsefulLinks;
        }

        public static List<UsefulLink> GetAllLinksByItemId(string cheetSheetItemId)
        {
            var allUsefulLinksForItem = new List<UsefulLink>();
            var clause = $"CheetSheetItemId = \"{cheetSheetItemId}\"";
            var sqlite_datareader = SQLTableManagement.ReadData(stringUsefulLink, clause);
            while (sqlite_datareader.Read())
            {
                string id = sqlite_datareader.GetString(0);
                string linkAddress = sqlite_datareader.GetString(1);
                int order = sqlite_datareader.GetInt32(2);

                allUsefulLinksForItem.Add(new UsefulLink
                {
                    Id = id,
                    LinkAddress = linkAddress,
                    LinkOrder = order,
                }) ;
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return allUsefulLinksForItem;
        }

        public static UsefulLink? GetCheetLink(string id)
        {
            string clause = $"id = \"{id}\"";
            var sqlite_datareader = SQLTableManagement.ReadData(stringUsefulLink, clause);
            while (sqlite_datareader.Read())
            {
                string linkAddress = sqlite_datareader.GetString(1);
                int order = sqlite_datareader.GetInt32(2);
                SqliteConnect.CoseConnections(sqlite_datareader);
                return new UsefulLink
                {
                    Id = id,
                    LinkAddress = linkAddress,
                    LinkOrder = order
                };
            }
            SqliteConnect.CoseConnections(sqlite_datareader);
            return null;
        }

        public static void UpdateLinkById(string id, UsefulLink usefulLink)
        {
            var clause = $"Id = \"{id}\"";
            var setLink = "";
            if(usefulLink.LinkAddress != null)
            {
                setLink += $"LinkAddress = \"{usefulLink.LinkAddress}\", ";
            }
            setLink += $"LinkOrder = \"{usefulLink.LinkOrder}\"";
            SQLTableManagement.UpdateData(stringUsefulLink, setLink, clause);
        }

        public static void DeleteLinkById(string id)
        {
            var clause = $"Id = \"{id}\"";
            SQLTableManagement.DeleteData(stringUsefulLink, clause);
        }

        public static void DeleteLinkByUrl(string link)
        {
            var clause = $"LinkAddress = \"{link}\"";
            SQLTableManagement.DeleteData(stringUsefulLink, clause);
        }

        public static void DeleteLinkByItemId(string cheetSheetItemId)
        {
            var clause = $"CheetSheetItemId = \"{cheetSheetItemId}\"";
            SQLTableManagement.DeleteData(stringUsefulLink, clause);
        }
    }
}
