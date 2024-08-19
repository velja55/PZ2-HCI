using NetworkService.ViewModel;
using System.Collections.Generic;

namespace NetworkService.Model
{
    public class NavigationDictionary
    {
        public Dictionary<string,NavigationElement> collection = new Dictionary<string,NavigationElement>();

        public NavigationDictionary() {
            collection.Add("home", new NavigationElement(new HomeViewModel(), "HOME VIEW", Resources.NetworkService.ColorLightBlue, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.HomeHelp));
            collection.Add("table", new NavigationElement(new TableViewModel(), "TABLE VIEW", Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorLightBlue, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.TableHelp));
            collection.Add("grid", new NavigationElement(new GridViewModel(), "GRID VIEW", Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorLightBlue, Resources.NetworkService.ColorWhite, Resources.NetworkService.GridHelp));
            collection.Add("graph", new NavigationElement(new GraphViewModel(), "GRAPH VIEW", Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorLightBlue, Resources.NetworkService.GraphHelp));
        }
        

    }
}
