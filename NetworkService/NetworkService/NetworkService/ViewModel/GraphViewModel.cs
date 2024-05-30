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

        private int l1;
        private int l2;
        private int l3;
        private int l4;
        private int l5;

        public int L1
        {
            get { return l1; }
            set
            {
                l1 = value;
                OnPropertyChanged(nameof(L1));
            }
        }

        public int L2
        {
            get { return l2; }
            set
            {
                l2 = value;
                OnPropertyChanged(nameof(L2));
            }
        }

        public int L3
        {
            get { return l3; }
            set
            {
                l3 = value;
                OnPropertyChanged(nameof(L3));
            }
        }

        public int L4
        {
            get { return l4; }
            set
            {
                l4 = value;
                OnPropertyChanged(nameof(L4));
            }
        }

        public int L5
        {
            get { return l5; }
            set
            {
                l5 = value;
                OnPropertyChanged(nameof(L5));
            }
        }


        private string margins;

        public string Margins
        {
            get { return margins; }
            set { margins = value;
                OnPropertyChanged(nameof(Margins));
            }
        }



        public void ChangeForOff() {
            L1 = 200;
        
        }

        public void ChangeForOn()
        {
            L1 = 60;

        }


        public GraphViewModel()
        {
            ComboboxItems=ListEntities.pressureInVentils;
            L1 = 60;
            L2 = 142;
            L3 = 224;
            L4 = 306;
            L5 = 410;
        }


        public void ChangeLinePositionsForToggle(bool isToggled)
        {
            if (isToggled)
            {
                L1 = 100;
                L2 = 180;
                L3 = 260;
                L4 = 340;
                L5 = 420;
            }
            else
            {
                L1 = 60;
                L2 = 142;
                L3 = 224;
                L4 = 306;
                L5 = 410;
            }
        }
    
    }
}
