using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{

    public class PressureInVentil:INotifyPropertyChanged
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value;
				OnPropertyChanged(nameof(Id));
			}
		}

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value;
                OnPropertyChanged(nameof(Type));
            }
        }


        private double _value;

        public double Value        {
            get { return _value; }
            set { _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }


        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }


        public List<double> lastFive = new List<double>() { 0, 0, 0, 0, 0 };
        public List<string> lastFiveTime = new List<string>() { "", "", "", "","" };

        private int brojac;

        public int Brojac
        {
            get { return brojac; }
            set { brojac = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

         
        }

        public PressureInVentil(int id, string name, string type,string image)
        {
            Id = id;
            Name = name;
            Type = type;
            Value = 0;
            Image = image;
            Brojac = 0;
        }

        public List<int> LastFiveMessures = new List<int>();

    }
}
