using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using NetworkService.Views;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Media3D;


namespace NetworkService.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {
        private int count = 4; // Inicijalna vrednost broja objekata u sistemu
                                // ######### ZAMENITI stvarnim brojem elemenata
                                //           zavisno od broja entiteta u listi
        HomeViewModel homeView;
        TableViewModel tableView; 
        GridViewModel gridView;
        GraphViewModel graphView;
        BindableBase currentViewModel;
        private string colspanFrame;
        ObservableCollection<string> TimeForGraph = new ObservableCollection<string>();
        private NotificationManager notificationManager;

        private Style styleWithTips;
        private Style styleNoTips;


        private ObservableCollection<string> colors = new ObservableCollection<string>() { "#2B55FF", "White", "White", "White" };

        public ObservableCollection<string> Colors { 
            get { return colors; }
            set {
                colors = value;
                OnPropertyChanged(nameof(Colors));
            }
        }


       

        private string help;

        public string Help
        {
            get { return help; }
            set { help = value;
                OnPropertyChanged(nameof(Help));
            }
        }




        private bool _isToggled;
        private string toggleText;

        private string helpWidth;

        public string HelpWidth
        {
            get { return helpWidth; }
            set { helpWidth = value; 
                OnPropertyChanged(nameof(HelpWidth));
            }
        }






        public string ToggleText
        {
            get { return toggleText; }
            set { toggleText = value;
                OnPropertyChanged(nameof(ToggleText));
            }
        }




        public string ColspanFrame
        {
            get { return colspanFrame; }
            set { colspanFrame = value;
                OnPropertyChanged(nameof(ColspanFrame));
            }
        }


        private string toolTipVisibility;

        public string ToolTipVisibility
        {
            get { return toolTipVisibility; }
            set
            {
                toolTipVisibility = value;
                OnPropertyChanged(nameof(ToolTipVisibility));
            }
        }



        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                _isToggled = value;
                OnPropertyChanged(nameof(IsToggled));
                ColspanFrame = _isToggled ? "2" : "1";
                HelpWidth = _isToggled ? "0" : "200";
                Messenger.Default.Send<bool>(_isToggled);
                string send;
                send = _isToggled ? "False" : "True";
                ToolTipVisibility = send;
                Messenger.Default.Send<string>(send);
            }
        }





        private string title;

        public string Title
        {
            get { return title; }
            set { title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        public TableViewModel TableView
        {
            get
            {
                if (tableView == null)
                    tableView = new TableViewModel();
                return tableView;
            }
        }

        public BindableBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }

            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public GridViewModel GridView
        {
            get
            {
                if (gridView == null)
                    gridView = new GridViewModel();
                return gridView;
            }
        }

        public GraphViewModel GraphView
        {
            get
            {
                if (graphView == null)
                    graphView = new GraphViewModel();
                return graphView;
            }
        }


        public Action CloseAction { get; set; }


        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom
        /*
            ListEntities.pressureInVentils.Add();
            ListEntities.pressureInVentils.Add();*/
            /* ListEntities.pressureInVentils.Add(new PressureInVentil(3, "Digital2", "Digital manometar", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"));
             ListEntities.pressureInVentils.Add(new PressureInVentil(4, "Cable2", "Cable sensor", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"));
         */

            homeView = new HomeViewModel();
            tableView = new TableViewModel();
            gridView = new GridViewModel();
            graphView = new GraphViewModel();
            graphView.L1 = 100;
            CurrentViewModel = homeView;
            Title = "HOME VIEW";
            ToggleText = "OFF";
            Help = "To go to the table page press\n the TABLE button\n" +
                 "-------------------------\n" +
                "To go to the grid page press\n the GRID button\n" +
                 "-------------------------\n" +
                "To go to the graphic page press\n the GRAPHIC button\n" +
                 "-------------------------\n" +
                "To exit from app press\n EXIT button\n" +
                    "\nSHORTCUTS\n" +
                "\nHome Ctrl+1\n" +
                "Table Ctrl+2\n" +
                "Grid Ctrl+3\n" +
                "Graphic Ctrl+4\n" +
                "Trn off help Ctrl+H\n" +
                "Exit Esc";
            NavCommand = new MyICommand<string>(OnNav);
            ExitCommand = new MyICommand(OnExit);
            MainWindowDelete = new MyICommand(OnDelete);
            Messenger.Default.Register<NotificationContent>(this, ShowToastNotification);
            notificationManager = new NotificationManager();
            ToggleCommand = new MyICommand(OnToggle);


        }

        private void OnDelete()
        {
            if (CurrentViewModel == TableView) {
                TableView.DeleteCommand.Execute(null);
            }
        }

        private void OnToggle()
        {
            IsToggled=!IsToggled;
        }

        private void OnExit()
        {

            if (MessageBox.Show("Are you sure want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CloseAction?.Invoke();
            }
        }

        private void ShowToastNotification(NotificationContent obj)
        {
            notificationManager.Show(obj, "MainWindowNotificationArea");
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25675);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        
                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"

                            //################ IMPLEMENTACIJA ####################

                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji

                            string incomingId = incomming.Substring(incomming.IndexOf('_') + 1, 1);
                            int rbr = int.Parse(incomingId);


                            bool isDeleted = true;
                            foreach (PressureInVentil p in ListEntities.pressureInVentils) {
                                if (p.Id == rbr) { 
                                    isDeleted = false; 
                                    break;
                                }
                            }

                            if (!isDeleted)
                            {
                                PressureInVentil pr = ListEntities.pressureInVentils.ToList().Find(x => x.Id == rbr);
                                int entityId = pr.Id;
                                double value = double.Parse(incomming.Substring(incomming.IndexOf(':') + 1));

                                foreach (PressureInVentil p in ListEntities.pressureInVentils)
                                {
                                    if (entityId == p.Id)
                                    {
                                        p.Value = value;
                                        p.lastFive[p.Brojac % 5] = value;

                                        DateTime now = DateTime.Now;

                                        TimeSpan timePart = now.TimeOfDay;


                                        string timeString = timePart.ToString(@"hh\:mm\:ss");
                                        p.lastFiveTime[p.Brojac % 5] = timeString;
                                        p.Brojac++;
                                        Messenger.Default.Send<PressureInVentil>(p);

                                        using (StreamWriter sr = File.AppendText("Log.txt"))
                                        {

                                            sr.WriteLine($"{DateTime.Now},{p.Id},{value}");
                                        }

                                        break;
                                    }
                                }

                            }

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
            Messenger.Default.Register<int>(this,OnAdd);
        }

        private void OnAdd(int obj)
        {
            count += 1;
        }

        

        public MyICommand<string> NavCommand { get; private set; }
        public ICommand ExitCommand { get; set; }

        public ICommand ToggleCommand { get; set; }
        public ICommand MainWindowDelete { get; set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homeView;
                    Title = "HOME VIEW";
                   
                    Colors[0] = "#2B55FF";
                    Colors[1] = "White";
                    Colors[2] = "White";
                    Colors[3] = "White";
                    Help = "To go to the table page press\n the TABLE button\n" +
                 "-------------------------\n" +
                "To go to the grid page press\n the GRID button\n" +
                 "-------------------------\n" +
                "To go to the graphic page press\n the GRAPHIC button\n" +
                 "-------------------------\n" +
                "To exit from app press\n EXIT button\n" +
                    "\nSHORTCUTS\n" +
                "\nHome Ctrl+1\n" +
                "Table Ctrl+2\n" +
                "Grid Ctrl+3\n" +
                "Graphic Ctrl+4\n" +
                "Trn off help Ctrl+H\n" +
                "Exit Esc";
                    break;
                case "table":
                    CurrentViewModel = tableView;
                    Title = "TABLE VIEW";
                    
                    Colors[1] = "#2B55FF";
                    Colors[0] = "White";
                    Colors[2] = "White";
                    Colors[3] = "White";
                    Help = "To delete entity you need to\nselect it from table\nand press DELETE button\n" +
               "------------------------------------\n" +
                            "To serach entities you need to\nchose do you want \nto seach by type or name\nand type text in search input\nand press button SEARCH\n"+
               "-------------------------------------\n" +
                             "To restore table in previus state\npress button RESET SEARCH\n"+
               "--------------------------------------\n" +
               "To Add new entity you need \nto input all information of entity\nand Press ADD button\n"+
               "--------------------------------------\n" +

                  "\nSHORTCUTS\n" +
              "\nHome Ctrl+1\n" +
              "Table Ctrl+2\n" +
              "Grid Ctrl+3\n" +
              "Graphic Ctrl+4\n" +
              "Trn off help Ctrl+H\n" +
              "Exit Esc";


                    break;
                case "grid":
                    CurrentViewModel = gridView;
                    Title = "GRID VIEW";
                    Colors[2] = "#2B55FF";
                    Colors[1] = "White";
                    Colors[0] = "White";
                    Colors[3] = "White";
                    Help = "Select entity from tree view and\n put on the network.\n" +
               "--------------------------------------\n" +
                           "You can change  position of \nentity on the grid.\nDrag and drop from canvas \nto another canvas.\n" +
                "--------------------------------------\n" +
                "To connect the entities \nin grid you have to\nhold the right click\nand drag to the desired one.\n" +
                "--------------------------------------\n" +
                "To delete entities from grid\npress button x on right \ntop of canvas,\n" +
                "--------------------------------------\n" +
                "\nSHORTCUTS\n" +
                "\nHome Ctrl+1\n" +
                "Table Ctrl+2\n" +
                "Grid Ctrl+3\n" +
                "Graphic Ctrl+4\n" +
                "Trn off help Ctrl+H\n" +
                "Exit Esc";
                    break;
                case "graph":
                    CurrentViewModel = graphView;
                    Title = "GRAPH VIEW";
                   
                    Colors[3] = "#2B55FF";
                    Colors[1] = "White";
                    Colors[2] = "White";
                    Colors[0] = "White";
                    Help="To see five last mesurements and\n theirs values you need to select \nentity from Combobox\n"
                    + "--------------------------------------\n" +
                "\nSHORTCUTS\n" +
                "\nHome Ctrl+1\n" +
                "Table Ctrl+2\n" +
                "Grid Ctrl+3\n" +
                "Graphic Ctrl+4\n" +
                "Trn off help Ctrl+H\n" +
                "Exit Esc";
                    break;
            }
        }

      
    }
}
