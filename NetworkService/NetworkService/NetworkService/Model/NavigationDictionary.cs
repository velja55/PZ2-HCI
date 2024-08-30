using NetworkService.Properties;
using NetworkService.ViewModel;
using System.Collections.Generic;

namespace NetworkService.Model
{
    public class NavigationDictionary
    {
        public Dictionary<string,NavigationElement> collection = new Dictionary<string,NavigationElement>();

        public NavigationDictionary() {
            collection.Add("home", new NavigationElement(new HomeViewModel(), "HOME VIEW", Resources.ColorLightBlue, Resources.ColorWhite, Resources.ColorWhite, Resources.ColorWhite, Resources.HomeHelp));
            collection.Add("table", new NavigationElement(new TableViewModel(), "TABLE VIEW", Resources.ColorWhite, Resources.ColorLightBlue, Resources.ColorWhite, Resources.ColorWhite, Resources.TableHelp));
            collection.Add("grid", new NavigationElement(new GridViewModel(), "GRID VIEW", Resources.ColorWhite, Resources.ColorWhite, Resources.ColorLightBlue, Resources.ColorWhite, Resources.GridHelp));
            collection.Add("graph", new NavigationElement(new GraphViewModel(), "GRAPH VIEW", Resources.ColorWhite, Resources.ColorWhite, Resources.ColorWhite, Resources.ColorLightBlue, Resources.GraphHelp));
        }
        

    }
}
