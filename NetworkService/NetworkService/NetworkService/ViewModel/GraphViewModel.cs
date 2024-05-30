using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class GraphViewModel:BindableBase
    {
        public ObservableCollection<PressureInVentil> ComboboxItems { get; set;}

        public GraphViewModel()
        {
            ComboboxItems=ListEntities.pressureInVentils;
        }
    }
}
