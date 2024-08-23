using System.ComponentModel;

namespace NetworkService.Model
{
    public class Krug
    {
		private int radius;
        public event PropertyChangedEventHandler PropertyChanged;
        public int Radius
		{
			get { return radius; }
			set { radius = value;
				OnPropertyChanged(nameof(Radius));
			}
		}


		private string color;

		public string Color
		{
			get { return color; }
			set { color = value;
				OnPropertyChanged(nameof(Color));
			}
		}

        public Krug(int radius, string color)
        {
            Radius = radius;
            Color = color;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }


        }
    }
}
