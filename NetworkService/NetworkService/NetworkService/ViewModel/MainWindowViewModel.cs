using MVVM1;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using NetworkService.Model;
using Notification.Wpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;


namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields
        private int count = 4;
        BindableBase currentViewModel;
        private string colspanFrame;
        ObservableCollection<string> TimeForGraph = new ObservableCollection<string>();
        private NotificationManager notificationManager;
        private ObservableCollection<string> colors = new ObservableCollection<string>() { "#2B55FF", "White", "White", "White" };
        private string help;
        private bool _isToggled;
        private string toggleText;
        private string helpWidth;
        private string toolTipVisibility;
        private string title;
        public MyICommand<string> NavCommand { get; private set; }
        public ICommand ExitCommand { get; set; }
        public ICommand ToggleCommand { get; set; }
        public ICommand MainWindowDelete { get; set; }
        public NavigationDictionary navigationDictionaty = new NavigationDictionary();
        public Action CloseAction { get; set; }
        #endregion
        #region Propertys
        public ObservableCollection<string> Colors
        {
            get { return colors; }
            set
            {
                colors = value;
                OnPropertyChanged(nameof(Colors));
            }
        }

        public string Help
        {
            get { return help; }
            set
            {
                help = value;
                OnPropertyChanged(nameof(Help));
            }
        }

        public string HelpWidth
        {
            get { return helpWidth; }
            set
            {
                helpWidth = value;
                OnPropertyChanged(nameof(HelpWidth));
            }
        }

        public string ToggleText
        {
            get { return toggleText; }
            set
            {
                toggleText = value;
                OnPropertyChanged(nameof(ToggleText));
            }
        }

        public string ColspanFrame
        {
            get { return colspanFrame; }
            set
            {
                colspanFrame = value;
                OnPropertyChanged(nameof(ColspanFrame));
            }
        }

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

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
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
        #endregion
        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom   
            CurrentViewModel = navigationDictionaty.collection["home"].ViewModel;
            Title= navigationDictionaty.collection["home"].Title;
            Help= navigationDictionaty.collection["home"].Help;
            ToggleText = "OFF";
            NavCommand = new MyICommand<string>(OnNav);
            ExitCommand = new MyICommand(OnExit);
            MainWindowDelete = new MyICommand(OnDelete);
            Messenger.Default.Register<NotificationContent>(this, ShowToastNotification);
            notificationManager = new NotificationManager();
            ToggleCommand = new MyICommand(OnToggle);
        }
        #region Methods
        private void OnDelete()
        {
            if (CurrentViewModel == navigationDictionaty.collection["table"].ViewModel)
            {
               ((TableViewModel)navigationDictionaty.collection["table"].ViewModel).DeleteCommand.Execute(null);
            }
        }

        private void OnToggle()
        {
            IsToggled = !IsToggled;
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
                            foreach (PressureInVentil p in ListEntities.pressureInVentils)
                            {
                                if (p.Id == rbr)
                                {
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
            Messenger.Default.Register<int>(this, OnAdd);
        }

        private void OnAdd(int obj)
        {
            count += 1;
        }
       
        private void OnNav(string destination)
        {
            CurrentViewModel = navigationDictionaty.collection[destination].ViewModel;
            Title = navigationDictionaty.collection[destination].Title;
            Colors[0] = navigationDictionaty.collection[destination].Color0;
            Colors[1] = navigationDictionaty.collection[destination].Color1;
            Colors[2] = navigationDictionaty.collection[destination].Color2;
            Colors[3] = navigationDictionaty.collection[destination].Color3;
            Help = navigationDictionaty.collection[destination].Help;
        }

        #endregion
    }
}
