using System.Collections.ObjectModel;

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
