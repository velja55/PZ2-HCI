
using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        private int sourceCanvas=-1;

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
        public ICommand MouseLeftButtonUpGrid { get; set; }



        public GridViewModel()
        {


            dragging = false;
            SelectionChanged = new MyICommand<object>(OnSelectionChanged);
           
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
            


            EntitiesByType digitalEntities = ListEntities.digitalEntities;
            EntitiesByType cableEntities = ListEntities.cableEntities;
            sourceCollections = new List<int>();


              CollectionCanvas=ListEntities.collectionCanvas;
            SelectedId=ListEntities.selectedId;
            SelectedValue=ListEntities.selectedValue;
            objectsOnCanvas =ListEntities.objectsOnCanvas;
            sourceCollections = ListEntities.sourceColections;
            BorderBrushes=ListEntities.borderBrushes;
            GridBackgrounds=ListEntities.GridBackgrounds;
            originalIndexs = ListEntities.originalIndexes;
            EntitiesByTypes =ListEntities.EntitiesByTypes;



            foreach (var entity in ListEntities.pressureInVentils)
            {

                if (entity.Type.Equals("Digital manometar"))
                {
                    PressureInVentil p = digitalEntities.Pressures.ToList().Find(x => x.Id == entity.Id);
                    PressureInVentil po = objectsOnCanvas.ToList().Where(x => x != null).FirstOrDefault(x => x.Id == entity.Id);
                    if (p == null && po==null)
                    {
                        digitalEntities.Pressures.Add(entity);
                    }
                }
                else
                {
                    PressureInVentil p = cableEntities.Pressures.ToList().Find(x => x.Id == entity.Id);
                    PressureInVentil po = objectsOnCanvas.ToList().ToList().Where(x => x != null).FirstOrDefault(x => x.Id == entity.Id);
                    if (p == null && po==null)
                    {
                        cableEntities.Pressures.Add(entity);
                    }
                }



            }
          
            
            Messenger.Default.Register<PressureInVentil>(this, UpdateValueOnCanvas);
            Messenger.Default.Register<int>(this, DeleteonCanvasAndView);
        }

        private void DeleteonCanvasAndView(int obj)
        {
            PressureInVentil p = EntitiesByTypes[0].Pressures.ToList().Find(x => x.Id == obj);
            if (p != null)
            {
                EntitiesByTypes[0].Pressures.Remove(p);
            }
            else {
                PressureInVentil pf = EntitiesByTypes[1].Pressures.ToList().Find(x => x.Id == obj);
                if (pf != null)
                {
                    EntitiesByTypes[1].Pressures.Remove(pf);
                }
            }

            int index = 0;
            foreach (PressureInVentil pr in  ListEntities.objectsOnCanvas) {
                if (pr != null)
                {
                    if (pr.Id == obj)
                    {
                       
                        selectedId[index] = "";
                        selectedValue[index] = "";
                        objectsOnCanvas[index] = null;
                        originalIndexs[index] = -1;
                        GridBackgrounds[index] = "White";
                        CollectionCanvas[index].Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue"));
                        break;
                    }
                  
                }
                index++;
            }

            





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

                    if (sourceCanvas!=-1)
                    {
                        index=sourceCanvas;
                        selectedId[index] = "";
                        selectedValue[index] = "";
                        objectsOnCanvas[index] = null;
                        originalIndexs[index] = -1;
                        GridBackgrounds[index] = "White";
                        CollectionCanvas[index].Background = (Brush)(new BrushConverter().ConvertFrom("LightSteelBlue"));
                    
                    }
                    sourceCanvas = -1;
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
            if (!dragging )
            {
               
                int index = Convert.ToInt32(obj);
                sourceCanvas = index;
                if (objectsOnCanvas[index] != null)
                {
                    dragging = true;
                    draggedItem = (PressureInVentil)objectsOnCanvas[index];
                    draggingSourceIndex = originalIndexs[index];
                    DragDrop.DoDragDrop(CollectionCanvas[index], draggedItem, DragDropEffects.Move);
                    

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
