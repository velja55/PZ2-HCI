
using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using static System.Net.Mime.MediaTypeNames;

namespace NetworkService.ViewModel
{
    public class GridViewModel : BindableBase
    {

        private ObservableCollection<PressureInVentil> entities;
        private ObservableCollection<PressureInVentil> sourceCollection;//za brisanje

        public bool dragging;

        PressureInVentil draggedItem;
        public int draggingSourceIndex = -1;

        private ObservableCollection<Canvas> collectionCanvas;
        public ObservableCollection<Canvas> CollectionCanvas
        {
            get { return collectionCanvas; }
            set
            {
                collectionCanvas = value;
                OnPropertyChanged(nameof(CollectionCanvas));
            }
        }

        private ObservableCollection<string> selectedId;
       

        public ObservableCollection<string> SelectedId
        {
            get { return selectedId; }
            set
            {
                selectedId = value;
                OnPropertyChanged(nameof(SelectedId));
            }
        }

        private ObservableCollection<string> selectedValue;

        public ObservableCollection<string> SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }


        List<int> originalIndexs;
        List<PressureInVentil> objectsOnCanvas;


        private ObservableCollection<string> borderBrushes;

        public ObservableCollection<string> BorderBrushes
        {
            get { return borderBrushes; }
            set { borderBrushes = value;
                OnPropertyChanged(nameof(BorderBrushes));
            }
        }


        private ObservableCollection<string> gridbackgrounds;

        public ObservableCollection<string> GridBackgrounds
        {
            get { return gridbackgrounds; }
            set
            {
                gridbackgrounds = value;
                OnPropertyChanged(nameof(GridBackgrounds));
            }
        }




        private ObservableCollection<EntitiesByType> entitiesByTypes;

        public ObservableCollection<EntitiesByType> EntitiesByTypes
        {
            get { return entitiesByTypes; }
            set { entitiesByTypes = value; }
        }



        public ObservableCollection<PressureInVentil> Entities
        {
            get { return entities; }
            set
            {
                entities = value;
                OnPropertyChanged(nameof(Entities));
            }
        }


        List<int> sourceCollections;


        public ICommand SelectionChanged { get; set; }
        public ICommand MouseLeftButtonUp { get; set; }
        public ICommand DragOver { get; set; }

        public ICommand Drop { get; set; }

        public ICommand Delete { get; set; }
        public ICommand MouseLeftButtonDownCanvas { get; set; }



        public GridViewModel()
        {


            dragging = false;
            SelectionChanged = new MyICommand<object>(OnSelectionChanged);
            EntitiesByTypes = new ObservableCollection<EntitiesByType>();
            MouseLeftButtonUp = new MyICommand(OnMouseLeftButtonUp);
            DragOver = new MyICommand<DragEventArgs>(OnDragOver);
            Drop = new MyICommand<object>(OnDrop);
            Delete = new MyICommand<object>(OnDelete);
            SelectedId = new ObservableCollection<string>();
            SelectedValue = new ObservableCollection<string>();
            originalIndexs = new List<int>();
            objectsOnCanvas = new List<PressureInVentil>();
            BorderBrushes = new ObservableCollection<string>();
            GridBackgrounds = new ObservableCollection<string>();
            MouseLeftButtonDownCanvas = new MyICommand<object>(OnMouseLeftButtonDownCanvas);

            EntitiesByType digitalEntities = new EntitiesByType("Digital manometar");
            EntitiesByType cableEntities = new EntitiesByType("Cable sensor");
            sourceCollections = new List<int>();
            foreach (var entity in ListEntities.pressureInVentils)
            {
                if (entity.Type.Equals("Digital manometar"))
                {
                    digitalEntities.Pressures.Add(entity);
                }
                else
                {
                    cableEntities.Pressures.Add(entity);
                }



            }

            if (CollectionCanvas == null)
            {
                CollectionCanvas = new ObservableCollection<Canvas>();
                for (int i = 0; i < 12; i++)
                {
                    CollectionCanvas.Add(new Canvas()
                    {
                        Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue")),
                        AllowDrop = true
                    });

                    SelectedId.Add("");
                    SelectedValue.Add("");
                    originalIndexs.Add(-1);
                    objectsOnCanvas.Add(null);
                    sourceCollections.Add(-1);
                    BorderBrushes.Add("Black");
                    GridBackgrounds.Add("White");
                }

            }


            EntitiesByTypes.Add(digitalEntities);
            EntitiesByTypes.Add(cableEntities);
            Messenger.Default.Register<PressureInVentil>(this, UpdateValueOnCanvas);


        }



        private void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PressureInVentil)))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }



        static bool dropped = false;

        private void OnDrop(object parametar)
        {

            if (draggedItem != null)
            {
                int index = Convert.ToInt32(parametar);
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(draggedItem.Image, UriKind.Relative);
                logo.EndInit();
                dropped = false;
                if (objectsOnCanvas[index] == null)
                {
                    CollectionCanvas[index].Background = new ImageBrush(logo);
                    SelectedId[index] = draggedItem.Id.ToString();
                    SelectedValue[index] = draggedItem.Value.ToString();
                    GridBackgrounds[index] = "LightSteelBlue";
                    sourceCollection.Remove(draggedItem);
                    if (draggedItem.Type.Equals("Cable sensor"))
                    {
                        sourceCollections[index] = 1;
                    }
                    else
                    {
                        sourceCollections[index] = 0;
                    }
                    originalIndexs[index] = originalIndex;
                    objectsOnCanvas[index] = draggedItem;
                    draggedItem = null;

                    dragging = false;
                    dropped = true;

                }

            }

        }

        private void OnMouseLeftButtonUp()
        {
            draggedItem = null;

            dragging = false;
            draggingSourceIndex = -1;
        }

        private object selectedItem;

        public object SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        private ObservableCollection<PressureInVentil> originalCollection;
        private int originalIndex;
        private void OnSelectionChanged(object obj)
        {
            if (!dragging && obj is PressureInVentil)
            {

                dragging = true;
                draggedItem = (PressureInVentil)obj;
                var sourceEntities = EntitiesByTypes.FirstOrDefault(et => et.Pressures.Contains(draggedItem));
                if (sourceEntities != null)
                {
                    sourceCollection = sourceEntities.Pressures;
                    originalCollection = sourceEntities.Pressures;
                    originalIndex = sourceEntities.Pressures.IndexOf(draggedItem);

                }
                DragDrop.DoDragDrop(System.Windows.Application.Current.MainWindow, draggedItem, DragDropEffects.Move);

            }
        }




        private void OnDelete(object parameter)
        {

            int index = Convert.ToInt32(parameter);
            if (objectsOnCanvas[index] != null)
            {
                CollectionCanvas[index].Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue"));
                selectedId[index] = "";
                selectedValue[index] = "";
                // originalCollection.Insert(originalIndexs[index], objectsOnCanvas[index]);

                if (sourceCollections[index] == 0)
                {
              
                    EntitiesByTypes[0].Pressures.Add(objectsOnCanvas[index]);
                    SortObservableCollectionDescending(EntitiesByTypes[0].Pressures);
                }
                else
                {
                    EntitiesByTypes[1].Pressures.Add(objectsOnCanvas[index]);
                    SortObservableCollectionDescending(EntitiesByTypes[1].Pressures);
                }
                sourceCollections[index] = -1;
                originalIndexs[index] = -1;
                objectsOnCanvas[index] = null;
                GridBackgrounds[index] = "White";
                BorderBrushes[index] = "Black";
            }

        }


        private void OnMouseLeftButtonDownCanvas(object obj)
        {
            if (!dragging)
            {
                int index = Convert.ToInt32(obj);
                if (objectsOnCanvas[index] != null)
                {
                    dragging = true;
                    draggedItem = (PressureInVentil)objectsOnCanvas[index];
                    draggingSourceIndex = originalIndexs[index];
                    DragDrop.DoDragDrop(CollectionCanvas[index], draggedItem, DragDropEffects.Move);
                    if (dropped)
                    {

                        selectedId[index] = "";
                        selectedValue[index] = "";
                        objectsOnCanvas[index] = null;
                        originalIndexs[index] = -1;
                        GridBackgrounds[index] = "White";
                        CollectionCanvas[index].Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue"));
                    }

                }





            }
        }

        private void SortObservableCollectionDescending(ObservableCollection<PressureInVentil> collection)
        {
            var sortedList = collection.OrderBy(a => a.Id).ToList();
            collection.Clear();
            foreach (var item in sortedList)
            {
                collection.Add(item);
            }
        }



        public void UpdateValueOnCanvas(PressureInVentil p) {

            int idx = -1;
            for (int i = 0; i < objectsOnCanvas.Count; i++) {
                if (SelectedId[i].Equals(p.Id.ToString())) {
                    idx = i;
                    break;
                }
            }
            if (idx != -1)
            {
                SelectedId[idx] = p.Id.ToString();
                SelectedValue[idx] = p.Value.ToString();
                if (p.Value < 4 || p.Value > 16)
                {
                    BorderBrushes[idx] = "Red";
                    GridBackgrounds[idx] = "HotPink";
                }
                else {
                    BorderBrushes[idx] = "Black";
                    GridBackgrounds[idx] = "LightSteelBlue";
                }
            }
        }

    }
}
