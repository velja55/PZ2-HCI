using MVVM1;
using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


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


        public TableViewModel()
        {
            AddCommand = new MyICommand(OnAdd);
            TypeSelected = true;
            NameSelected = false;

            SearchCommand = new MyICommand(OnSearch);
            ClearCommand = new MyICommand(OnClear);
            DeleteCommand = new MyICommand(OnDelete);

            Entities = ListEntities.pressureInVentils;
            Types = new ObservableCollection<string> { "Cable sensor", "Digital manometar" };

        }


        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        
        private void OnAdd()
        {
            string path="";
            if (TypeText.Equals("Cable sensor"))
            {
                path = "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg";
            }
            else {
                path = "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg";
            }
            ListEntities.pressureInVentils.Add(new PressureInVentil(Int32.Parse(ID), NameText, TypeText,path));
            ResetFormFields();
        }

        private void OnClear() {
            Entities = ListEntities.pressureInVentils;
            SearchText= String.Empty;
        }

        private void OnSearch() {
            ObservableCollection<PressureInVentil> filtered=new ObservableCollection<PressureInVentil> ();
            if (TypeSelected == true) {
                foreach(PressureInVentil p in Entities) {
                    if (p.Type.Contains(SearchText)) {
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


        private void ResetFormFields()
        {
            ID = String.Empty;
            NameText = String.Empty;
            TypeText = null;
            ValueText = string.Empty;

        }

        private PressureInVentil selectedEntity;

        public PressureInVentil SelectedEntity
        {
            get { return selectedEntity; }
            set { selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }

        private void OnDelete()
        {
            if (SelectedEntity != null)
            {
                Entities.Remove(SelectedEntity);
            }
        }

        
    }
}
