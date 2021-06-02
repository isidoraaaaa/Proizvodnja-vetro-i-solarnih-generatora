using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand BackToHome { get; private set; }
        private GeneratoriViewModel generatoriViewModel = new GeneratoriViewModel();
        private DisplayViewModel displayViewModel = new DisplayViewModel();
        private GraphViewModel graphViewModel = new GraphViewModel();
        private BindableBase currentViewModel;


        public MyICommand UndoCommand { get; set; }


        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<String>(OnNav);
            CurrentViewModel = generatoriViewModel;
            BackToHome = new MyICommand(OnHome);
            UndoCommand = new MyICommand(OnUndo);
        }

        private void OnHome()
        {
            CurrentViewModel = generatoriViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "Home":
                    CurrentViewModel = generatoriViewModel;
                    break;
                case "Display Data":
                    CurrentViewModel = displayViewModel;
                    break;
                case "Graph Data":
                    CurrentViewModel = graphViewModel;
                    break;
            }
        }

        private void OnUndo()
        {

        }

    }
}
