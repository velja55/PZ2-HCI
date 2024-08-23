using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NetworkService.Model
{
    public static class ListEntities
    {
		public static ObservableCollection<PressureInVentil> pressureInVentils=new ObservableCollection<PressureInVentil>() { new PressureInVentil(0, "Cable1", "Cable sensor", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg"),
                        new PressureInVentil(1, "Digital1", Resources.NetworkService.DigitalManometarString, "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"),
                        new PressureInVentil(3, "Digital2", Resources.NetworkService.DigitalManometarString,"C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"),
                        new PressureInVentil(2, "Cable2", Resources.NetworkService.CableSensorString, "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg")

        } ;

        public static ObservableCollection<Canvas> collectionCanvas=new ObservableCollection<Canvas>() { new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    } , new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    } , new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.LightBlueColor)),
                        AllowDrop = true
                    } };
        public static List<PressureInVentil> objectsOnCanvas =new List<PressureInVentil> { null, null, null, null, null, null, null, null, null, null, null, null  };//za brisanje

        public static ObservableCollection<string> selectedId =new ObservableCollection<string>() {"","","","","","","", "", "", "", "", "", "", "" };
        public static ObservableCollection<string> selectedValue =new ObservableCollection<string>() {"","","","","","","", "", "", "", "", "", "", "" };
        public static List<int> sourceColections =new List<int>() {-1,-1,-1,-1,-1,-1,-1, -1, -1, -1, -1, -1, -1, -1 };
        public static List<int> originalIndexes =new List<int>() {-1,-1,-1,-1,-1,-1,-1, -1, -1, -1, -1, -1, -1, -1 };

        public static ObservableCollection<string> borderBrushes= new ObservableCollection<string>() { Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor, Resources.NetworkService.BlackColor };
        public static ObservableCollection<string> GridBackgrounds = new ObservableCollection<string>() { Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite, Resources.NetworkService.ColorWhite };

        public static bool adding = true;

        public static EntitiesByType digitalEntities = new EntitiesByType(Resources.NetworkService.DigitalManometarString);
        public static EntitiesByType cableEntities = new EntitiesByType(Resources.NetworkService.CableSensorString);

        public static ObservableCollection<EntitiesByType> EntitiesByTypes = new ObservableCollection<EntitiesByType>() { digitalEntities,cableEntities};

       public static PressureInVentil selectectedForGraph = pressureInVentils[0];
        public static List<Point> points = new List<Point>();
        public static bool addPoints = true;
        public static ObservableCollection<DisplayLine> LinesOnDisplay = new ObservableCollection<DisplayLine>(); 
    }
}
