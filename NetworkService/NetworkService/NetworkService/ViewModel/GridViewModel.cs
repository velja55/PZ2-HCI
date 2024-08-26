
using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class GridViewModel : BindableBase
    {
        #region Fields
        private List<Point> points = ListEntities.points;
        private bool addPoints = ListEntities.addPoints;
        public ObservableCollection<DisplayLine> LinesOnDisplay { get; set; }
        private ObservableCollection<PressureInVentil> entities;
        private ObservableCollection<PressureInVentil> sourceCollection;//za brisanje
        public int sourceCanvas = -1;
        public bool dragging;
        private int drawSource = -1;
        private int drawTarget = -1;
        public PressureInVentil draggedItem;
        public int draggingSourceIndex = -1;
        private ObservableCollection<Canvas> collectionCanvas;
        private ObservableCollection<string> selectedValue;
        public List<int> originalIndexs;
        public List<PressureInVentil> objectsOnCanvas;
        private ObservableCollection<string> borderBrushes;
        private ObservableCollection<string> gridbackgrounds;
        private ObservableCollection<EntitiesByType> entitiesByTypes;
        public MainWindow _mainWindow;
        List<int> sourceCollections;
        static bool dropped = false;
        private object selectedItem;
        #endregion
        #region Properties
        public List<Point> Points
        {
            get { return points; }
            set { 
                points= value;
                OnPropertyChanged(nameof(Points));
            }
        }
        public object SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public List<PressureInVentil> ObjectsOnCanvas
        {
            get { return objectsOnCanvas; }
            set {
                objectsOnCanvas = value;
                OnPropertyChanged(nameof(ObjectsOnCanvas));
            }
        }

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
        public int DrawSource {
            get { return drawSource; }
            set {
                drawSource = value;
                OnPropertyChanged(nameof(DrawSource));
            }
        }
        public int DrawTarget
        {
            get { return drawTarget; }
            set
            {
                drawTarget = value;
                OnPropertyChanged(nameof(DrawTarget));
            }
        }
        public ObservableCollection<string> SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        public ObservableCollection<string> BorderBrushes
        {
            get { return borderBrushes; }
            set
            {
                borderBrushes = value;
                OnPropertyChanged(nameof(BorderBrushes));
            }
        }

        public ObservableCollection<string> GridBackgrounds
        {
            get { return gridbackgrounds; }
            set
            {
                gridbackgrounds = value;
                OnPropertyChanged(nameof(GridBackgrounds));
            }
        }

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

        public List<int> OriginalIndexes
        {
            get { return originalIndexs; }
            set { 
                originalIndexs = value;
                OnPropertyChanged(nameof(originalIndexs));
            }
        }
        public ICommand SelectionChanged { get; set; }
        public ICommand MouseLeftButtonUp { get; set; }
        public ICommand DragOver { get; set; }
        public ICommand Drop { get; set; }
        public ICommand Delete { get; set; }
        public ICommand MouseLeftButtonDownCanvas { get; set; }
        public ICommand MouseLeftButtonUpGrid { get; set; }
        public ICommand StarDraw { get; set; }
        public ICommand EndDraw { get; set; }
        #endregion
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
            StarDraw = new MyICommand<string>(OnStartDraw);
            EndDraw = new MyICommand<string>(OnEndDraw);
            EntitiesByType digitalEntities = ListEntities.digitalEntities;
            EntitiesByType cableEntities = ListEntities.cableEntities;
            sourceCollections = new List<int>();
            LinesOnDisplay = ListEntities.LinesOnDisplay;
            CollectionCanvas = ListEntities.collectionCanvas;
            SelectedId = ListEntities.selectedId;
            SelectedValue = ListEntities.selectedValue;
            objectsOnCanvas = ListEntities.objectsOnCanvas;
            sourceCollections = ListEntities.sourceColections;
            BorderBrushes = ListEntities.borderBrushes;
            GridBackgrounds = ListEntities.GridBackgrounds;
            originalIndexs = ListEntities.originalIndexes;
            EntitiesByTypes = ListEntities.EntitiesByTypes;
            _mainWindow = null;
            foreach (var entity in ListEntities.pressureInVentils)
            {

                if (entity.Type.Equals("Digital manometar"))
                {
                    PressureInVentil p = digitalEntities.Pressures.ToList().Find(x => x.Id == entity.Id);
                    PressureInVentil po = objectsOnCanvas.ToList().Where(x => x != null).FirstOrDefault(x => x.Id == entity.Id);
                    if (p == null && po == null)
                    {
                        digitalEntities.Pressures.Add(entity);
                    }
                }
                else
                {
                    PressureInVentil p = cableEntities.Pressures.ToList().Find(x => x.Id == entity.Id);
                    PressureInVentil po = objectsOnCanvas.ToList().ToList().Where(x => x != null).FirstOrDefault(x => x.Id == entity.Id);
                    if (p == null && po == null)
                    {
                        cableEntities.Pressures.Add(entity);
                    }
                }
            }
            if (addPoints)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Point p = new Point(46 + j * 116, 60 + i * 165);
                        points.Add(p);
                    }
                }
                addPoints = false;
            }
            Messenger.Default.Register<PressureInVentil>(this, UpdateValueOnCanvas);
            Messenger.Default.Register<int>(this, DeleteonCanvasAndView);
        }
        #region Methods
        public void OnStartDraw(string value)
        {
            int index = int.Parse(value);
            if (objectsOnCanvas[index] == null)
            {
                drawSource = -1;
                drawTarget = -1;
                return;
            }
            drawSource = -1;
            drawTarget = -1;
            drawSource = index;
        }

        public void OnEndDraw(string value)
        {
            int index = int.Parse(value);
            if (drawSource == -1 || index == -1)
            {
                return;
            }
            if (objectsOnCanvas[index] == null)
            {
                drawSource = -1;
                drawTarget = -1;
                return;
            }
            Point startPoint = points.ElementAt(drawSource);
            Point endPoint = points.ElementAt(index);
            DisplayLine todelete = null;
            foreach (DisplayLine d in LinesOnDisplay)
            {
                if (d.Y1 == startPoint.Y && d.X1 == startPoint.X && d.X2 == endPoint.X && d.Y2 == endPoint.Y)
                {
                    todelete = d;
                    break;
                }
                else if (d.Y1 == endPoint.Y && d.X1 == endPoint.X && d.X2 == startPoint.X && d.Y2 == startPoint.Y)
                {
                    todelete = d;
                    break;
                }
            }

            if (todelete != null)
            {
                LinesOnDisplay.Remove(todelete);
                return;
            }
            DisplayLine dl = new DisplayLine(startPoint.X, endPoint.X, startPoint.Y, endPoint.Y);
            LinesOnDisplay.Add(dl);
        }

        public void DeleteonCanvasAndView(int obj)
        {
            PressureInVentil p = EntitiesByTypes[0].Pressures.ToList().Find(x => x.Id == obj);
            if (p != null)
            {
                EntitiesByTypes[0].Pressures.Remove(p);
            }
            else
            {
                PressureInVentil pf = EntitiesByTypes[1].Pressures.ToList().Find(x => x.Id == obj);
                if (pf != null)
                {
                    EntitiesByTypes[1].Pressures.Remove(pf);
                }
            }

            int index = 0;
            foreach (PressureInVentil pr in ListEntities.objectsOnCanvas)
            {
                if (pr != null && index < points.Count)
                {
                    if (pr.Id == obj)
                    {
                        Point point = points.ElementAt(index);
                        List<DisplayLine> toDeleteLine = new List<DisplayLine>();
                        foreach (DisplayLine d in LinesOnDisplay)
                        {
                            if (d.X1 == point.X && d.Y1 == point.Y)
                            {
                                toDeleteLine.Add(d);
                            }
                            else if (d.X2 == point.X && d.Y2 == point.Y)
                            {
                                toDeleteLine.Add(d);

                            }
                        }
                        foreach (DisplayLine dl in toDeleteLine)
                        {
                            LinesOnDisplay.Remove(dl);
                        }
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

                    if (sourceCollection != null)
                    {
                        sourceCollection.Remove(draggedItem);
                    }
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

                    if (sourceCanvas != -1)
                    {


                        Point newPoint = points.ElementAt(index);
                        Point point = points.ElementAt(sourceCanvas);


                        foreach (DisplayLine d in LinesOnDisplay)
                        {
                            if (d.X1 == point.X && d.Y1 == point.Y)
                            {
                                d.X1 = newPoint.X;
                                d.Y1 = newPoint.Y;
                            }
                            else if (d.X2 == point.X && d.Y2 == point.Y)
                            {
                                d.X2 = newPoint.X;
                                d.Y2 = newPoint.Y;

                            }
                        }
                        index = sourceCanvas;
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

        private ObservableCollection<PressureInVentil> originalCollection;
        private int originalIndex;
        private void OnSelectionChanged(object obj)
        {
            if (!dragging && obj is PressureInVentil)
            {
                dragging = true;
                draggedItem = (PressureInVentil)obj;
                if (EntitiesByTypes != null)
                {
                    var sourceEntities = EntitiesByTypes.FirstOrDefault(et => et.Pressures.Contains(draggedItem));

                    if (sourceEntities != null)
                    {
                        sourceCollection = sourceEntities.Pressures;
                        originalCollection = sourceEntities.Pressures;
                        originalIndex = sourceEntities.Pressures.IndexOf(draggedItem);

                    }
                    if (_mainWindow == null)
                    {
                        DragDrop.DoDragDrop(System.Windows.Application.Current.MainWindow, draggedItem, DragDropEffects.Move);
                    }
                }
            }
        }

        public void OnDelete(object parameter)
        {

            int index = Convert.ToInt32(parameter);
            if (objectsOnCanvas[index] != null)
            {
                CollectionCanvas[index].Background = (Brush)(new BrushConverter().ConvertFrom(Resources.NetworkService.ColorLightBlue));
                selectedId[index] = "";
                selectedValue[index] = "";
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

                Point point = points.ElementAt(index);

                List<DisplayLine> toDeleteLine = new List<DisplayLine>();

                foreach (DisplayLine d in LinesOnDisplay)
                {
                    if (d.X1 == point.X && d.Y1 == point.Y)
                    {
                        toDeleteLine.Add(d);
                    }
                    else if (d.X2 == point.X && d.Y2 == point.Y)
                    {
                        toDeleteLine.Add(d);

                    }
                }
                foreach (DisplayLine dl in toDeleteLine)
                {
                    LinesOnDisplay.Remove(dl);
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

        public void UpdateValueOnCanvas(PressureInVentil p)
        {

            int idx = -1;
            for (int i = 0; i < objectsOnCanvas.Count; i++)
            {
                if (SelectedId[i].Equals(p.Id.ToString()))
                {
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
                else
                {
                    BorderBrushes[idx] = "Black";
                    GridBackgrounds[idx] = "LightSteelBlue";
                }
            }
        }
        #endregion
    }

}

