using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class GraphViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<PressureInVentil> comboboxItems;
        private int l1;
        private int l2;
        private int l3;
        private int l4;
        private int l5;
        private int lineBottom;
        private int lineBottom2;
        private double centerY = 120;
        public double Ellipse1Left => L1 - (Radius1 / 2);
        public double Ellipse2Left => L2 - (Radius2 / 2);
        public double Ellipse3Left => L3 - (Radius3 / 2);
        public double Ellipse4Left => L4 - (Radius4 / 2);
        public double Ellipse5Left => L5 - (Radius5 / 2);
        public double TextBlock1 => L1 - 20;
        public double TextBlock2=> L2 - 20;
        public double TextBlock3=> L3 - 20;
        public double TextBlock4=> L4 - 20;
        public double TextBlock5=> L5 - 20;
        public double Ellipse1Top => centerY - (Radius1 / 2);
        public double Ellipse2Top => centerY - (Radius2 / 2);
        public double Ellipse3Top => centerY - (Radius3 / 2);
        public double Ellipse4Top => centerY - (Radius4 / 2);
        public double Ellipse5Top => centerY - (Radius5 / 2);
        private PressureInVentil selectedEntity;
        private string color1;
        private string selectedImage;
        private string color2;
        private string color3;
        private string color4;
        private string color5;
        private int radius1;
        private int radius2;
        private int radius3;
        private int radius4;
        private int radius5;
        private string margins;
        private string text1;
        private string text2;
        private string text3;
        private string text4;
        private string text5;
        private int selectedId;
        private double selectedValue;
        private string selectedName;
        private string radiusProba;
        public ICommand SelectionChanged { get; set; }
        #endregion
        #region Properties
        public ObservableCollection<PressureInVentil> ComboboxItems
        {
            get { return comboboxItems; }
            set
            {
                comboboxItems = value;
                OnPropertyChanged(nameof(ComboboxItems));
            }
        }

        public string SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        public string Color1
        {
            get { return color1; }
            set
            {
                color1 = value;
                OnPropertyChanged(nameof(Color1));
            }
        }

        public string Color2
        {
            get { return color2; }
            set
            {
                color2 = value;
                OnPropertyChanged(nameof(Color2));
            }
        }

        public string Color3
        {
            get { return color3; }
            set
            {
                color3 = value;
                OnPropertyChanged(nameof(Color3));
            }

        }

        public string Color4
        {
            get { return color4; }
            set
            {
                color4 = value;
                OnPropertyChanged(nameof(Color4));
            }
        }

        public string Color5
        {
            get { return color5; }
            set
            {
                color5 = value;
                OnPropertyChanged(nameof(Color5));
            }
        }

        public PressureInVentil SelectedEntity
        {
            get { return selectedEntity; }
            set { selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
                UpdateSelectedEntityProperties();
            }
        }

        public int Radius1
        {
            get { return radius1; }
            set { radius1 = value;
                OnPropertyChanged(nameof(Radius1));
                OnPropertyChanged(nameof(Ellipse1Left));
                OnPropertyChanged(nameof(Ellipse1Top));
            }
        }

        public int Radius2
        {
            get { return radius2; }
            set
            {
                radius2 = value;
                OnPropertyChanged(nameof(Radius2));
                OnPropertyChanged(nameof(Ellipse2Left));
                OnPropertyChanged(nameof(Ellipse2Top));
            }
        }

        public int Radius3
        {
            get { return radius3; }
            set
            {
                radius3 = value;
                OnPropertyChanged(nameof(Radius3));
                OnPropertyChanged(nameof(Ellipse3Left));
                OnPropertyChanged(nameof(Ellipse3Top));
            }
        }

        public int Radius4
        {
            get { return radius4; }
            set
            {
                radius4 = value;
                OnPropertyChanged(nameof(Radius4));
                OnPropertyChanged(nameof(Ellipse4Left));
                OnPropertyChanged(nameof(Ellipse4Top));
            }
        }

        public int Radius5
        {
            get { return radius5; }
            set
            {
                radius5 = value;
                OnPropertyChanged(nameof(Radius5));
                OnPropertyChanged(nameof(Ellipse5Left));
                OnPropertyChanged(nameof(Ellipse5Top));
            }
        }

        public int L1
        {
            get { return l1; }
            set
            {
                l1 = value;
                OnPropertyChanged(nameof(L1));
                OnPropertyChanged(nameof(Ellipse1Left));
                OnPropertyChanged(nameof(TextBlock1));
            }
        }

        public int L2
        {
            get { return l2; }
            set
            {
                l2 = value;
                OnPropertyChanged(nameof(L2));
                OnPropertyChanged(nameof(Ellipse2Left));
                OnPropertyChanged(nameof(TextBlock2));
            }
        }

        public int L3
        {
            get { return l3; }
            set
            {
                l3 = value;
                OnPropertyChanged(nameof(L3));
                OnPropertyChanged(nameof(Ellipse3Left));
                OnPropertyChanged(nameof(TextBlock3));
            }
        }

        public int L4
        {
            get { return l4; }
            set
            {
                l4 = value;
                OnPropertyChanged(nameof(L4));
                OnPropertyChanged(nameof(Ellipse4Left));
                OnPropertyChanged(nameof(TextBlock4));
            }
        }

        public int L5
        {
            get { return l5; }
            set
            {
                l5 = value;
                OnPropertyChanged(nameof(L5));
                OnPropertyChanged(nameof(Ellipse5Left));
                OnPropertyChanged(nameof(TextBlock5));
            }
        }

        public int LineBottom
        {
            get { return lineBottom; }
            set
            {
                lineBottom = value;
                OnPropertyChanged(nameof(LineBottom));
            }
        }

        public int LineBottom2
        {
            get { return lineBottom2; }
            set
            {
                lineBottom2 = value;
                OnPropertyChanged(nameof(LineBottom2));
            }
        }

        public string Margins
        {
            get { return margins; }
            set { margins = value;
                OnPropertyChanged(nameof(Margins));
            }
        }

        public string Text1
        {
            get { return text1; }
            set { text1 = value;
                OnPropertyChanged(nameof(Text1));
            }
        }

        public string Text2
        {
            get { return text2; }
            set
            {
                text2 = value;
                OnPropertyChanged(nameof(Text2));
            }

        }

        public string Text3
        {
            get { return text3; }
            set
            {
                text3 = value;
                OnPropertyChanged(nameof(Text3));
            }

        }

        public string Text4
        {
            get { return text4; }
            set
            {
                text4 = value;
                OnPropertyChanged(nameof(Text4));
            }

        }

        public string Text5
        {
            get { return text5; }
            set
            {
                text5 = value;
                OnPropertyChanged(nameof(Text5));
            }

        }

        public int SelectedId
        {
            get { return selectedId; }
            set { selectedId = value;
                OnPropertyChanged(nameof(SelectedId));
            }
        }

        public double SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        public string SelectedName
        {
            get { return selectedName; }
            set
            {
                selectedName = value;
                OnPropertyChanged(nameof(SelectedName));
            }
        }
        #endregion

        public GraphViewModel()
        {
            ComboboxItems = ListEntities.pressureInVentils;
            L1 = 60;
            L2 = 142;
            L3 = 224;
            L4 = 306;
            L5 = 388;
            LineBottom = 10;
            LineBottom2 = 430;
            SelectedEntity = ListEntities.selectectedForGraph;
            Text1 = SelectedEntity.lastFiveTime[0].ToString();
            Text2 = SelectedEntity.lastFiveTime[1].ToString();
            Text3 = SelectedEntity.lastFiveTime[2].ToString();
            Text4 = SelectedEntity.lastFiveTime[3].ToString();
            Text5 = SelectedEntity.lastFiveTime[4].ToString();
            SelectedId = SelectedEntity.Id;
            SelectedName = SelectedEntity.Name;
            SelectedValue = selectedEntity.Value;
            SelectedImage = SelectedEntity.Image;
            SelectionChanged = new MyICommand<object>(OnSelectionChanged);
            Messenger.Default.Register<PressureInVentil>(this, ChangeRadiusGraph);
            Messenger.Default.Register<bool>(this, ChangeLinePositionsForToggle);
        }
        #region Methods
        public void OnSelectionChanged(object obj)
        {
            SelectedEntity = (PressureInVentil)obj;
            ListEntities.selectectedForGraph = (PressureInVentil)obj;
            List<string> linesWithIdTwo = new List<string>();

            SelectedId = SelectedEntity.Id;
            SelectedName = SelectedEntity.Name;
            SelectedValue = SelectedEntity.Value;
            SelectedImage = SelectedEntity.Image;
            RadiusProba = "100";
            Radius1 = (int)SelectedEntity.lastFive[0] * 2 + 1;
            Radius2 = (int)SelectedEntity.lastFive[1] * 2 + 1;
            Radius3 = (int)SelectedEntity.lastFive[2] * 2 + 1;
            Radius4 = (int)SelectedEntity.lastFive[3] * 2 + 1;
            Radius5 = (int)SelectedEntity.lastFive[4] * 2 + 1;

            Color1 = (SelectedEntity.lastFive[0] > 4 && SelectedEntity.lastFive[0] < 16)
                      ? Resources.NetworkService.ColorBlue
                      : Resources.NetworkService.RedColor;

            Color2 = (SelectedEntity.lastFive[1] > 4 && SelectedEntity.lastFive[1] < 16)
                      ? Resources.NetworkService.ColorBlue
                      : Resources.NetworkService.RedColor;

            Color3 = (SelectedEntity.lastFive[2] > 4 && SelectedEntity.lastFive[2] < 16)
                      ? Resources.NetworkService.ColorBlue
                      : Resources.NetworkService.RedColor;

            Color4 = (SelectedEntity.lastFive[3] > 4 && SelectedEntity.lastFive[3] < 16)
                      ? Resources.NetworkService.ColorBlue
                      : Resources.NetworkService.RedColor;

            Color5 = (SelectedEntity.lastFive[4] > 4 && SelectedEntity.lastFive[4] < 16)
                      ? Resources.NetworkService.ColorBlue
                      : Resources.NetworkService.RedColor;

            Text1 = SelectedEntity.lastFiveTime[0];
            Text2 = SelectedEntity.lastFiveTime[1];
            Text3 = SelectedEntity.lastFiveTime[2];
            Text4 = SelectedEntity.lastFiveTime[3];
            Text5 = SelectedEntity.lastFiveTime[4];
        }

        public void ChangeRadiusGraph(PressureInVentil entity)
        {
            if (SelectedEntity != null && entity.Id == SelectedEntity.Id)
            {
                Radius1 = (int)SelectedEntity.lastFive[0] * 2 + 1;
                Radius2 = (int)SelectedEntity.lastFive[1] * 2 + 1;
                Radius3 = (int)SelectedEntity.lastFive[2] * 2 + 1;
                Radius4 = (int)SelectedEntity.lastFive[3] * 2 + 1;
                Radius5 = (int)SelectedEntity.lastFive[4] * 2 + 1;

                Color1 = (SelectedEntity.lastFive[0] > 4 && SelectedEntity.lastFive[0] < 16)
                         ? Resources.NetworkService.ColorBlue
                         : Resources.NetworkService.RedColor;

                Color2 = (SelectedEntity.lastFive[1] > 4 && SelectedEntity.lastFive[1] < 16)
                         ? Resources.NetworkService.ColorBlue
                         : Resources.NetworkService.RedColor;

                Color3 = (SelectedEntity.lastFive[2] > 4 && SelectedEntity.lastFive[2] < 16)
                         ? Resources.NetworkService.ColorBlue
                         : Resources.NetworkService.RedColor;

                Color4 = (SelectedEntity.lastFive[3] > 4 && SelectedEntity.lastFive[3] < 16)
                         ? Resources.NetworkService.ColorBlue
                         : Resources.NetworkService.RedColor;

                Color5 = (SelectedEntity.lastFive[4] > 4 && SelectedEntity.lastFive[4] < 16)
                         ? Resources.NetworkService.ColorBlue
                         : Resources.NetworkService.RedColor;

                Text1 = SelectedEntity.lastFiveTime[0];
                Text2 = SelectedEntity.lastFiveTime[1];
                Text3 = SelectedEntity.lastFiveTime[2];
                Text4 = SelectedEntity.lastFiveTime[3];
                Text5 = SelectedEntity.lastFiveTime[4];
                SelectedValue = SelectedEntity.Value;
            }
        }

        private void UpdateSelectedEntityProperties()
        {
            Radius1 = (int)SelectedEntity.lastFive[0] * 2 + 1;
            Radius2 = (int)SelectedEntity.lastFive[1] * 2 + 1;
            Radius3 = (int)SelectedEntity.lastFive[2] * 2 + 1;
            Radius4 = (int)SelectedEntity.lastFive[3] * 2 + 1;
            Radius5 = (int)SelectedEntity.lastFive[4] * 2 + 1;

            Color1 = GetColorBasedOnValue(SelectedEntity.lastFive[0]);
            Color2 = GetColorBasedOnValue(SelectedEntity.lastFive[1]);
            Color3 = GetColorBasedOnValue(SelectedEntity.lastFive[2]);
            Color4 = GetColorBasedOnValue(SelectedEntity.lastFive[3]);
            Color5 = GetColorBasedOnValue(SelectedEntity.lastFive[4]);
        }

        private string GetColorBasedOnValue(double value)
        {
            return (value > 4 && value < 16) ? Resources.NetworkService.ColorBlue : Resources.NetworkService.RedColor;
        }

        public void ChangeLinePositionsForToggle(bool isToggled)
        {
            if (isToggled)
            {
                L1 = 150;
                L2 = 232;
                L3 = 314;
                L4 = 396;
                L5 = 478;
                LineBottom = 100;
                LineBottom2 = 520;
            }
            else
            {
                L1 = 60;
                L2 = 142;
                L3 = 224;
                L4 = 306;
                L5 = 388;
                LineBottom = 10;
                LineBottom2 = 430;
            }
        }

        public string RadiusProba
        {
            get { return radiusProba; }
            set
            {
                radiusProba = value;
                OnPropertyChanged($"{nameof(RadiusProba)}");
            }
        }
        #endregion

    }
}
