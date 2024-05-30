using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class EntitiesByType
    {
        public string Type { get; set; }
        

        public ObservableCollection<PressureInVentil> Pressures { get; set; }
        
        public EntitiesByType(string type) {
            Type = type;
            Pressures=new ObservableCollection<PressureInVentil>();
        }
    }
}
