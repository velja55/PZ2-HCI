using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using Notification.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

//Generalno obrisi svuda usinge koje ne koristis i razmake


namespace NetworkService.ViewModel
{
    public class TableViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<PressureInVentil> entities;
        public ObservableCollection<string> Types { get; set; }
        private string _idText;
        private string _nameText;
        private string _valueText;
        private string _typeText;
        private bool typeSelected;
        private bool nameSelected;
        private string searchText;
        private string idErrorLabel;
        private string nameErrorLabel;
        private string searchErrorLabel;
        private string typeErrorLabel;
        private string colorId;
        private string colorSearch;
        private string colorName;
        private string borderBrushId;
        private string borderBrushName;
        private string borderBrushSearch;
        private string toolTipVisibility;
        private PressureInVentil selectedEntity;
        int idxDeleted = -1;
        #endregion
        #region Properties
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand Focused { get; set; }
        public ICommand LostFocused { get; set; }
        public ICommand ClearInputs { get; set; }
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

        public bool NameSelected
        {
            get { return nameSelected; }
            set { nameSelected = value;
                OnPropertyChanged(nameof(NameSelected));
            }
        }

        public bool TypeSelected
        {
            get { return typeSelected; }
            set { typeSelected = value;
                OnPropertyChanged(nameof(TypeSelected));
            }
        }

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

        public string IdErrorLabel
        {
            get { return idErrorLabel; }
            set { idErrorLabel = value;
                OnPropertyChanged(nameof(IdErrorLabel));
            }
        }

        public string NameErrorLabel
        {
            get { return nameErrorLabel; }
            set
            {
                nameErrorLabel = value;
                OnPropertyChanged(nameof(NameErrorLabel));
            }
        }

        public string TypeErrorLabel
        {
            get { return typeErrorLabel; }
            set
            {
                typeErrorLabel = value;
                OnPropertyChanged(nameof(TypeErrorLabel));
            }
        }

        public string SearchErrorLabel
        {
            get { return searchErrorLabel; }
            set
            {
                searchErrorLabel = value;
                OnPropertyChanged(nameof(SearchErrorLabel));
            }
        }

        public string ColorId
        {
            get { return colorId; }
            set
            {
                colorId = value;
                OnPropertyChanged(nameof(ColorId));
            }
        }

        public string ColorSearch
        {
            get { return colorSearch; }
            set
            {
                colorSearch = value;
                OnPropertyChanged(nameof(ColorSearch));
            }
        }

        public string ColorName
        {
            get { return colorName; }
            set
            {
                colorName = value;
                OnPropertyChanged(nameof(ColorName));
            }
        }

        public string BorderBrushId
        {
            get { return borderBrushId; }
            set
            {
                borderBrushId = value;
                OnPropertyChanged(nameof(BorderBrushId));
            }
        }

        public string BorderBrushName
        {
            get { return borderBrushName; }
            set
            {
                borderBrushName = value;
                OnPropertyChanged(nameof(BorderBrushName));
            }
        }

        public string BorderBrushSearch
        {
            get { return borderBrushSearch; }
            set
            {
                borderBrushSearch = value;
                OnPropertyChanged(nameof(BorderBrushSearch));
            }
        }

        public string ToolTipVisibility
        {
            get { return toolTipVisibility; }
            set { toolTipVisibility = value;
                OnPropertyChanged(nameof(ToolTipVisibility));
            }
        }
        #endregion
        public TableViewModel()
        {
            AddCommand = new MyICommand(OnAdd);
            Focused = new MyICommand<string>(OnFocus);
            TypeSelected = true;
            NameSelected = false;
            
            SearchCommand = new MyICommand(OnSearch);
            ClearCommand = new MyICommand(OnClear);
            DeleteCommand = new MyICommand(OnDelete);
            LostFocused = new MyICommand<string>(OnLostFocus);
            ClearInputs = new MyICommand(ResetFormFields);
            ID = Resources.NetworkService.TableViewModel_Id;//"Input id here"; //ove stvari se binduju kroz resourse fajlove guglaj resx :)
            SearchText = Resources.NetworkService.TableViewModel_Search;
            NameText = Resources.NetworkService.TableViewModel_Name;
            BorderBrushId = Resources.NetworkService.BlackColor; // takodje ovo se binduje na xamlu ne ovde
            BorderBrushName = Resources.NetworkService.BlackColor;
            BorderBrushSearch = Resources.NetworkService.BlackColor;
            ColorId = Resources.NetworkService.GrayColor;
            ColorName = Resources.NetworkService.GrayColor;
            ColorSearch = Resources.NetworkService.GrayColor;
            Entities = ListEntities.pressureInVentils;
            Types = new ObservableCollection<string> { Resources.NetworkService.CableSensorString, Resources.NetworkService.DigitalManometarString };
            Messenger.Default.Register<string>(this,ChangeVisibilityToolTips);
        }
        #region Methods
        private void ChangeVisibilityToolTips(string obj)
        {
            ToolTipVisibility = obj;
        }

        private void OnLostFocus(string obj)
        {
            if (obj.Equals("id")) {
                if (ID.Equals("")) {
                    ID = Resources.NetworkService.TableViewModel_Id;
                    ColorId = Resources.NetworkService.GrayColor;

                }
            }
            else if (obj.Equals("name"))
            {
                if (NameText.Equals(""))
                {
                    NameText = Resources.NetworkService.TableViewModel_Name;
                    ColorName = Resources.NetworkService.GrayColor;
                }

            }else if (obj.Equals("search"))
                {
                    if (SearchText.Equals(""))
                    {
                        SearchText =Resources.NetworkService.TableViewModel_Search;
                        ColorSearch =Resources.NetworkService.GrayColor;
                    }

                }
        }

        private void OnFocus(string obj)
        {
            if (obj.Equals("id")) {
                if (ID.Equals(Resources.NetworkService.TableViewModel_Id)) {
                    ID = String.Empty;
                    ColorId = Resources.NetworkService.BlackColor;
                }
            }
            else if (obj.Equals("name"))
            {
                if (NameText.Equals(Resources.NetworkService.TableViewModel_Name))
                {
                    NameText =String.Empty;
                    ColorName = Resources.NetworkService.BlackColor;

                }

            }
            else if (obj.Equals("search"))
            {
                if (SearchText.Equals(Resources.NetworkService.TableViewModel_Search))
                {
                    SearchText = String.Empty;
                    ColorSearch = Resources.NetworkService.BlackColor;

                }

            }
        }

        public void OnAdd()
        {
            string path = "";
            bool addi = Validator.ValidateId(ID, out string idError, out string borderBrushId);
            bool addn = Validator.ValidateName(NameText, out string nameError, out string borderBrushName);
            bool addt = Validator.ValidateType(TypeText, out string typeError);

            IdErrorLabel = idError;
            BorderBrushId = borderBrushId;
            NameErrorLabel = nameError;
            BorderBrushName = borderBrushName;
            TypeErrorLabel = typeError;

            if (addi && addn && addt)
            {
                path = TypeText.Equals(Resources.NetworkService.CableSensorString) ?
                    "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg" ://ovde kopiranje
                    "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg";

                PressureInVentil p = ListEntities.pressureInVentils.ToList().Find(x => x.Id == int.Parse(ID));

                if (p == null)
                {
                    ListEntities.pressureInVentils.Add(new PressureInVentil(int.Parse(ID), NameText, TypeText, path));
                    ShowNotification("Success", $"Entity with Id {ID} successfully added!", NotificationType.Success, Colors.LimeGreen);
                    Messenger.Default.Send<int>(1);
                    ResetFormFields();
                }
                else
                {
                    ShowNotification("Error", $"Entity with Id {ID} already exists!", NotificationType.Error, Colors.Red);
                }
            }
            else
            {
                ShowNotification("Error", "Not all fields for add are correctly filled!", NotificationType.Error, Colors.Red);
            }
        }

        public void OnSearch()
        {
            bool searchValid = Validator.ValidateSearch(SearchText, out string searchError, out string borderBrushSearch);

            SearchErrorLabel = searchError;
            BorderBrushSearch = borderBrushSearch;

            if (searchValid)
            {
                ObservableCollection<PressureInVentil> filtered = new ObservableCollection<PressureInVentil>();

                if (TypeSelected)
                {
                    foreach (PressureInVentil p in Entities)
                    {
                        if (p.Type.Contains(SearchText))
                        {
                            filtered.Add(p);
                        }
                    }
                }
                else if (NameSelected)
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
            else
            {
                ShowNotification("Error", "Not all fields for search are correctly filled!", NotificationType.Error, Colors.Red);
            }
        }

        public void ShowNotification(string title, string message, NotificationType type, Color color)
        {
            var notificationContent = new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type,
                TrimType = NotificationTextTrimType.AttachIfMoreRows,
                RowsCount = 2,
                CloseOnClick = true,
                Background = new SolidColorBrush(color),
                Foreground = new SolidColorBrush(Colors.White),
            };

            Messenger.Default.Send<NotificationContent>(notificationContent);
        }

        private void OnClear() {
            Entities = ListEntities.pressureInVentils;
            SearchText= String.Empty;
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

        public PressureInVentil SelectedEntity
        {
            get { return selectedEntity; }
            set { selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }
        public void OnDelete()
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
        #endregion
    }
}
