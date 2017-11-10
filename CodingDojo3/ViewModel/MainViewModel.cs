using CodingDojo3.DataSimulation;
using CodingDojo3.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CodingDojo3.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Listen fuer Demo Daten
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }
        public ObservableCollection<string> SensorModeSelectionList { get; private set; }
        public ObservableCollection<string> ActorModeSelectionList { get; private set; }


        private List<ItemVm> modelItems = new List<ItemVm>();
        //variablen fuer datum und uhrzeit
        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();


        //properties fuer derzeitige Zeit und Datum
        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            //Listen erstellen
            SensorList = new ObservableCollection<ItemVm>();
            ActorList = new ObservableCollection<ItemVm>();
            SensorModeSelectionList = new ObservableCollection<string>();
            ActorModeSelectionList = new ObservableCollection<string>();

            //ModeSelectionListen befuellen
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                SensorModeSelectionList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ActorModeSelectionList.Add(item);

            }

            //neuer timer erstellen
            DispatcherTimer timer = new DispatcherTimer();
            //intervall angeben --> zeit wann uhrzeit aktualisisert wird
            timer.Interval = new TimeSpan(0, 0, 1);
            //Uhrezit und Datum mit aktueller Zeit befuellen
            timer.Tick += UpdateTime;

            //DemoDaten laden
            LoadData();

            //Timer starten
            timer.Start();

        }

        //DemoDaten laden
        private void LoadData()
        {
            Simulator sim = new Simulator(modelItems);
            //ueberpruefen ob Sensor oder Actor und zu Liste hinzufuegen
            foreach (var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }

        }

        //derzeitige Zeit in properties speichern
        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }
    }
}