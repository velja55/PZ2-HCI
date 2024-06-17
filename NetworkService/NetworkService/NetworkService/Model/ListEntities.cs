using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NetworkService.Model
{
    public static class ListEntities
    {
		public static ObservableCollection<PressureInVentil> pressureInVentils=new ObservableCollection<PressureInVentil>() { new PressureInVentil(0, "Cable1", "Cable sensor", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg"),
                        new PressureInVentil(1, "Digital1", "Digital manometar", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"),
                        new PressureInVentil(3, "Digital2", "Digital manometar","C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"),
                        new PressureInVentil(2, "Cable2", "Cable sensor", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg")

        } ;

        public static ObservableCollection<Canvas> collectionCanvas=new ObservableCollection<Canvas>() { new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    } , new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    } , new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    },new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    }, new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    } };
        public static List<PressureInVentil> objectsOnCanvas =new List<PressureInVentil> { null, null, null, null, null, null, null, null, null, null, null, null  };//za brisanje

        public static ObservableCollection<string> selectedId =new ObservableCollection<string>() {"","","","","","","", "", "", "", "", "", "", "" };
        public static ObservableCollection<string> selectedValue =new ObservableCollection<string>() {"","","","","","","", "", "", "", "", "", "", "" };
        public static List<int> sourceColections =new List<int>() {-1,-1,-1,-1,-1,-1,-1, -1, -1, -1, -1, -1, -1, -1 };
        public static List<int> originalIndexes =new List<int>() {-1,-1,-1,-1,-1,-1,-1, -1, -1, -1, -1, -1, -1, -1 };

        public static ObservableCollection<string> borderBrushes= new ObservableCollection<string>() { "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black" };
        public static ObservableCollection<string> GridBackgrounds = new ObservableCollection<string>() { "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White" };

        public static bool adding = true;

        public static EntitiesByType digitalEntities = new EntitiesByType("Digital manometar");
        public static EntitiesByType cableEntities = new EntitiesByType("Cable sensor");

        public static ObservableCollection<EntitiesByType> EntitiesByTypes = new ObservableCollection<EntitiesByType>() { digitalEntities,cableEntities};

       public static PressureInVentil selectectedForGraph = pressureInVentils[0];
        public static List<Point> points = new List<Point>();
        public static bool addPoints = true;
        public static ObservableCollection<DisplayLine> LinesOnDisplay = new ObservableCollection<DisplayLine>(); 
    }
}
