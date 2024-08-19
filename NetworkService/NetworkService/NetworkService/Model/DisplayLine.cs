using NetworkService.Helpers;

namespace NetworkService.Model
{
    public class DisplayLine:BindableBase
    {
        private double x1;
        private double x2;
        private double y1;
        private double y2;

        public DisplayLine(double x1, double x2, double y1, double y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public double X1
        {
            get { return x1; }
            set
            {
                if(x1 != value)
                {
                    x1 = value;
                    OnPropertyChanged(nameof(X1));
                }
            }
        }

        public double X2
        {
            get { return x2; }
            set
            {
                if(x2 != value)
                {
                    x2 = value;
                    OnPropertyChanged(nameof(X2));
                }
            }
        }

        public double Y1
        {
            get { return y1; }
            set
            {
                if(y1 != value)
                {
                    y1 = value;
                    OnPropertyChanged(nameof(Y1));
                }
            }
        }

        public double Y2
        {
            get { return y2; }
            set
            {
                if(y2 != value)
                {
                    y2 = value;
                    OnPropertyChanged(nameof(Y2));
                }
            }
        }
    }
}
