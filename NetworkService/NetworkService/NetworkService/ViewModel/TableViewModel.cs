using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;


namespace NetworkService.ViewModel
{
    public class TableViewModel : BindableBase
    {
        private ObservableCollection<PressureInVentil> entities;
        public ObservableCollection<string> Types { get; set; }

        private string _idText;
        private string _nameText;

        private string _valueText;
        private string _typeText;

        public string TypeText
        {
            get { return _typeText; }
            set
            {
                _typeText = value;
                OnPropertyChanged(nameof(TypeText));
            }
        }


        public string ValueText
        {
            get { return _valueText; }
            set
            {
                _valueText = value;
                OnPropertyChanged(nameof(ValueText));
            }
        }





        public string NameText
        {
            get { return _nameText; }
            set
            {
                _nameText = value;
                OnPropertyChanged(nameof(NameText));
            }
        }


        public string ID
        {
            get { return _idText; }
            set
            {
                _idText = value;
                OnPropertyChanged(nameof(ID));
            }
        }



        private bool nameSelected;

        public bool NameSelected
        {
            get { return nameSelected; }
            set { nameSelected = value;
                OnPropertyChanged(nameof(NameSelected));
            }
        }


        private bool typeSelected;

        public bool TypeSelected
        {
            get { return typeSelected; }
            set { typeSelected = value;
                OnPropertyChanged(nameof(TypeSelected));
            }
        }


        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }


        public ObservableCollection<PressureInVentil> Entities
        {
            get { return entities; }
            set {
                entities = value;
                OnPropertyChanged(nameof(Entities));
            }

        }

        private string idErrorLabel;

        public string IdErrorLabel
        {
            get { return idErrorLabel; }
            set { idErrorLabel = value;
                OnPropertyChanged(nameof(IdErrorLabel));
            }
        }


        private string nameErrorLabel;

        public string NameErrorLabel
        {
            get { return nameErrorLabel; }
            set
            {
                nameErrorLabel = value;
                OnPropertyChanged(nameof(NameErrorLabel));
            }
        }


        private string typeErrorLabel;

        public string TypeErrorLabel
        {
            get { return typeErrorLabel; }
            set
            {
                typeErrorLabel = value;
                OnPropertyChanged(nameof(TypeErrorLabel));
            }
        }

        private string searchErrorLabel;

        public string SearchErrorLabel
        {
            get { return searchErrorLabel; }
            set
            {
                searchErrorLabel = value;
                OnPropertyChanged(nameof(SearchErrorLabel));
            }
        }

        private string colorId;

        public string ColorId
        {
            get { return colorId; }
            set
            {
                colorId = value;
                OnPropertyChanged(nameof(ColorId));
            }
        }

        private string colorSearch;

        public string ColorSearch
        {
            get { return colorSearch; }
            set
            {
                colorSearch = value;
                OnPropertyChanged(nameof(ColorSearch));
            }
        }

        private string colorName;

        public string ColorName
        {
            get { return colorName; }
            set
            {
                colorName = value;
                OnPropertyChanged(nameof(ColorName));
            }
        }



        private string borderBrushId;

        public string BorderBrushId
        {
            get { return borderBrushId; }
            set
            {
                borderBrushId = value;
                OnPropertyChanged(nameof(BorderBrushId));
            }
        }

        private string borderBrushName;

        public string BorderBrushName
        {
            get { return borderBrushName; }
            set
            {
                borderBrushName = value;
                OnPropertyChanged(nameof(BorderBrushName));
            }
        }


        private string borderBrushSearch;

        public string BorderBrushSearch
        {
            get { return borderBrushSearch; }
            set
            {
                borderBrushSearch = value;
                OnPropertyChanged(nameof(BorderBrushSearch));
            }
        }


        private string toolTipVisibility;

        public string ToolTipVisibility
        {
            get { return toolTipVisibility; }
            set { toolTipVisibility = value;
                OnPropertyChanged(nameof(ToolTipVisibility));
            }
        }



        public TableViewModel()
        {
            AddCommand = new MyICommand(OnAdd);
            Focused = new MyICommand<string>(OnFocus);
            TypeSelected = true;
            NameSelected = false;
            SearchText = "Input search here";
            SearchCommand = new MyICommand(OnSearch);
            ClearCommand = new MyICommand(OnClear);
            DeleteCommand = new MyICommand(OnDelete);
            LostFocused = new MyICommand<string>(OnLostFocus);
            ClearInputs = new MyICommand(ResetFormFields);
            ID = "Input id here";
            NameText = "Input name here";
            BorderBrushId = "Black";
            BorderBrushName = "Black";
            BorderBrushSearch = "Black";
            ColorId = "Gray";
            ColorName = "Gray";
            ColorSearch = "Gray";

            Entities = ListEntities.pressureInVentils;
            Types = new ObservableCollection<string> { "Cable sensor", "Digital manometar" };

            Messenger.Default.Register<string>(this,ChangeVisibilityToolTips);
            
        }

        private void ChangeVisibilityToolTips(string obj)
        {
            ToolTipVisibility = obj;
        }

        private void OnLostFocus(string obj)
        {
            if (obj.Equals("id")) {
                if (ID.Equals("")) {
                    ID = "Input id here";
                    ColorId = "Gray";

                }
            }
            else if (obj.Equals("name"))
            {
                if (NameText.Equals(""))
                {
                    NameText = "Input name here";
                    ColorName = "Gray";
                }

            }else if (obj.Equals("search"))
                {
                    if (SearchText.Equals(""))
                    {
                        SearchText = "Input search here";
                        ColorSearch = "Gray";
                    }

                }
        }

        private void OnFocus(string obj)
        {
            if (obj.Equals("id")) {
                if (ID.Equals("Input id here")) {
                    ID = "";
                    ColorId = "Black";
                }
            }
            else if (obj.Equals("name"))
            {
                if (NameText.Equals("Input name here"))
                {
                    NameText = "";
                    ColorName = "Black";

                }

            }
            else if (obj.Equals("search"))
            {
                if (SearchText.Equals("Input search here"))
                {
                    SearchText = "";
                    ColorSearch = "Black";

                }

            }
        }




        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand Focused { get; set; }
        public ICommand LostFocused { get; set; }
        public ICommand ClearInputs { get; set; }

        private void OnAdd()
        {
            string path = "";
            

            bool addi = true;
            bool addn = true;
            bool addt = true;

            if (ID.Equals("") || ID.Equals("Input id here")) {
                IdErrorLabel = "Id can't be emty!";
                BorderBrushId = "Red";
                addi = false;
                

            }
            else {
                IdErrorLabel = "";
                BorderBrushId = "Black";
            }

            if (NameText.Equals("") || NameText.Equals("Input name here"))
            {
                NameErrorLabel = "Name can't be emty!";
                addn = false;
                BorderBrushName = "Red";
                

            }
            else
            {
                NameErrorLabel = "";

                BorderBrushName = "Black";
            }

            if (TypeText == null || TypeText.Equals(""))
            {
                TypeErrorLabel = "You must chose one type";
                addt = false;
               
            }
            else {
                TypeErrorLabel = "";
            }


            int res;
            if (!Int32.TryParse(ID, out res) && addi==true)
            {
                IdErrorLabel = "Id must be number";
                addi = false;
                BorderBrushId = "Red";
               
            }
            else if(addi==true){
                if (res < 0)
                {
                    IdErrorLabel = "Id must positive number";
                    addi = false;
                    BorderBrushId = "Red";
                    

                }
                else {
                    IdErrorLabel = "";
                    BorderBrushId = "Black";
                }
            }



            if (addi == true && addn == true && addt == true)
            {
                if (TypeText.Equals("Cable sensor"))
                {
                    path = "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg";
                }
                else
                {
                    path = "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg";
                }

                PressureInVentil p = ListEntities.pressureInVentils.ToList().Find(x => x.Id == res);

                if (p == null)
                {
                    ListEntities.pressureInVentils.Add(new PressureInVentil(res, NameText, TypeText, path));

                    var notificationContent = new NotificationContent
                    {
                        Title = "Succes",
                        Message = $"Entity whit Id {res} succesfully added!",
                        Type = NotificationType.Success,
                        TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                        RowsCount = 2,
                        CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                        Background = new SolidColorBrush(Colors.LimeGreen),
                        Foreground = new SolidColorBrush(Colors.White),


                    };


                    Messenger.Default.Send<NotificationContent>(notificationContent);
                    Messenger.Default.Send<int>(1);
                    ResetFormFields();
                }
                else
                {
                    var notificationContent = new NotificationContent
                    {
                        Title = "Error",
                        Message = $"Entity with Id {res} already exists!",
                        Type = NotificationType.Error,
                        TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                        RowsCount = 2,
                        CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                        Background = new SolidColorBrush(Colors.Red),
                        Foreground = new SolidColorBrush(Colors.White),


                    };


                    Messenger.Default.Send<NotificationContent>(notificationContent);

                }
            }
            else {
                var notificationContent = new NotificationContent
                {
                    Title = "Error",
                    Message = "Not all fields for add are corectlly filed!",
                    Type = NotificationType.Error,
                    TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                    RowsCount = 2,
                    CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                };


                Messenger.Default.Send<NotificationContent>(notificationContent);
            }
        }

        private void OnClear() {
            Entities = ListEntities.pressureInVentils;
            SearchText= String.Empty;
        }

        private void OnSearch() {



            if (SearchText.Equals("") || SearchText.Equals("Input search here"))
            {
                SearchErrorLabel = "Search input can't be empty!";
                BorderBrushSearch = "Red";
                var notificationContent = new NotificationContent
                {
                    Title = "Error",
                    Message = "Not all fields for search are corectlly filed!",
                    Type = NotificationType.Error,
                    TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                    RowsCount = 2,
                    CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                    Background = new SolidColorBrush(Colors.Red),
                    Foreground = new SolidColorBrush(Colors.White),


                };


                Messenger.Default.Send<NotificationContent>(notificationContent);

            }
            else
            {
                SearchErrorLabel = "";
                BorderBrushSearch = "Black";
                ObservableCollection<PressureInVentil> filtered = new ObservableCollection<PressureInVentil>();
                if (TypeSelected == true)
                {
                    foreach (PressureInVentil p in Entities)
                    {
                        if (p.Type.Contains(SearchText))
                        {
                            filtered.Add(p);
                        }
                    }
                }
                else if (NameSelected == true)
                {
                    foreach (PressureInVentil p in Entities)
                    {
                        if (p.Name.Contains(SearchText))
                        {
                            filtered.Add(p);
                        }
                    }
                }

                Entities = filtered;
            }
        }


        private void ResetFormFields()
        {
            ID = String.Empty;
            NameText = String.Empty;
            TypeText = null;
            ValueText = string.Empty;
            IdErrorLabel = "";
            NameErrorLabel = "";


        }

        private PressureInVentil selectedEntity;

        public PressureInVentil SelectedEntity
        {
            get { return selectedEntity; }
            set { selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
               
            }
        }

        int idxDeleted = -1;

        private void OnDelete()
        {
            if (SelectedEntity != null)
            {
                if (System.Windows.MessageBox.Show($"Are you sure want to delete entity with id {SelectedEntity.Id}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    idxDeleted = SelectedEntity.Id;
                    var notificationContent = new NotificationContent
                {
                    Title = "Success",
                    Message = $"Successfuly removed Entity with id {SelectedEntity.Id}!",
                    Type = NotificationType.Success,
                    TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                    RowsCount = 2,
                    CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                    Background = new SolidColorBrush(Colors.LimeGreen),
                    Foreground = new SolidColorBrush(Colors.White),
                

                };

                Entities.Remove(SelectedEntity);
                Messenger.Default.Send<NotificationContent>(notificationContent);
                Messenger.Default.Send<int>(idxDeleted);
                    }
            }
            else
            {
                var notificationContent = new NotificationContent
                {
                    Title = "Warning",
                    Message = "To delete you must select entity from table!",
                    Type = NotificationType.Warning,
                    TrimType = NotificationTextTrimType.AttachIfMoreRows, // Will show attach button on message
                    RowsCount = 2,
                    CloseOnClick = true, // Set true if u want close message when left mouse button click on message (base = true)

                    Background = new SolidColorBrush(Colors.Orange),
                    Foreground = new SolidColorBrush(Colors.White),


                };
                Messenger.Default.Send<NotificationContent>(notificationContent);
            }

            
        }

        
    }
}
