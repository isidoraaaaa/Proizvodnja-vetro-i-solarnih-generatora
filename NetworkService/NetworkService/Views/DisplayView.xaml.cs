using NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkService.Views
{
    /// <summary>
    /// Interaction logic for DisplayView.xaml
    /// </summary>
    public partial class DisplayView : UserControl
    {
        public DisplayView()
        {
            DisplayViewModel.DisplayGenerator = new System.Collections.ObjectModel.ObservableCollection<Model.GeneratorModel>();
            foreach (Model.GeneratorModel g in GeneratoriViewModel.GeneratorsSearched)
            {
                DisplayViewModel.DisplayGenerator.Add(g);
            }

            InitializeComponent();
        }
    }
}
