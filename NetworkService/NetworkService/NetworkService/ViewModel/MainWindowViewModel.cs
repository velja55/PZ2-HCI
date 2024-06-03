using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using NetworkService.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;


namespace NetworkService.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {
        private int count = 2; // Inicijalna vrednost broja objekata u sistemu
                                // ######### ZAMENITI stvarnim brojem elemenata
                                //           zavisno od broja entiteta u listi
        HomeViewModel homeView;
        TableViewModel tableView; 
        GridViewModel gridView;
        GraphViewModel graphView;
        BindableBase currentViewModel;
        private string colspanFrame;
        ObservableCollection<string> TimeForGraph = new ObservableCollection<string>();



        ObservableCollection<string> HomeHelpers= new ObservableCollection<string>
            {
                "Help Item 1",
                "Help Item 2",
                "Help Item 3"
            };




        ObservableCollection<string> GridHelpers = new ObservableCollection<string>
            {
                "Grid Help Item 1",
                "Grid Help Item 2",
                "Help Item 3"
            };


        ObservableCollection<string> TableHelpers = new ObservableCollection<string>
            {
                "Table Help Item 1",
                "Table Help Item 2",
                "Table Item 3"
            };


        private ObservableCollection<string> _helpItems;
        public ObservableCollection<string> HelpItems
        {
            get { return _helpItems; }
            set { _helpItems = value; OnPropertyChanged(nameof(HelpItems)); }
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


        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                _isToggled = value;
                OnPropertyChanged(nameof(IsToggled));
                ColspanFrame = _isToggled ? "2" : "1";
                ToggleText = _isToggled ? "ON" : "OFF";
                HelpWidth = _isToggled ? "0" : "200";
                Messenger.Default.Send<bool>(_isToggled);
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

        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom
        
            ListEntities.pressureInVentils.Add(new PressureInVentil(0, "Cable1","Cable sensor", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg"));
            ListEntities.pressureInVentils.Add(new PressureInVentil(1, "Digital1", "Digital manometar", "C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2\\NetworkService\\NetworkService\\NetworkService\\Images\\digital.jpg"));
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
            HelpItems = HomeHelpers;
            NavCommand = new MyICommand<string>(OnNav);

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
                            int entityId = ListEntities.pressureInVentils[rbr].Id;
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
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }


        public MyICommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homeView;
                    Title = "HOME VIEW";
                    HelpItems = HomeHelpers;
                    break;
                case "table":
                    CurrentViewModel = tableView;
                    Title = "TABLE VIEW";
                    HelpItems = TableHelpers;
                    break;
                case "grid":
                    CurrentViewModel = gridView;
                    Title = "GRID VIEW";
                    HelpItems = GridHelpers;
                    break;
                case "graph":
                    CurrentViewModel = graphView;
                    Title = "GRAPH VIEW";
                    HelpItems = GridHelpers;
                    break;
            }
        }

      
    }
}
