using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class DisplayViewModel : BindableBase
    {

        public static ObservableCollection<GeneratorModel> DisplayGenerator { get; set; }

        public MyICommand<Canvas> Drop_Command { get; set; }
        public MyICommand<Canvas> DragOver_Command { get; set; }
        public MyICommand<Canvas> MouseUp_Command { get; set; }
        public MyICommand<ListView> ListView_SelectionChanged { get; set; }
        public MyICommand<Canvas> Remove_Command { get; set; }
        public MyICommand BackToHome { get; set; }

        private GeneratorModel currentGenerator = new GeneratorModel();
        private GeneratorModel selectedGenerator = new GeneratorModel();

        private bool dragging = false;

        public GeneratorModel CurrentGenerator
        {
            get { return currentGenerator; }
            set { currentGenerator = value; OnPropertyChanged("CurrentGenerator"); }
        }
        public GeneratorModel SelectedGenerator
        {
            get { return selectedGenerator; }
            set { selectedGenerator = value; OnPropertyChanged("SelectedGenerator"); }
        }
        public DisplayViewModel()
        {
            Drop_Command = new MyICommand<Canvas>(OnDrop);
            DragOver_Command = new MyICommand<Canvas>(OnDragOver);
            MouseUp_Command = new MyICommand<Canvas>(OnMouseUp);
            ListView_SelectionChanged = new MyICommand<ListView>(OnListViewChange);
            Remove_Command = new MyICommand<Canvas>(OnRemove);
        }

        private void OnRemove(Canvas obj)
        {
            if (obj.Resources["taken"] != null)
            {
                string[] podeli = ((TextBlock)((obj).Children[0])).Text.Split(',');
                string name = podeli[1];
                int value = Int32.Parse(((TextBlock)((obj).Children[1])).Text);
                int id = Int32.Parse(((TextBlock)((obj).Children[2])).Text);
                Model.Type t = new Model.Type();
                t.Name = ((TextBlock)((obj).Children[3])).Text;
                t.ImgSrc = ((TextBlock)((obj).Children[4])).Text;
                obj.Background = Brushes.LightGray;
                DisplayGenerator.Add(new GeneratorModel() { Name = name, Value = value, Id = id, TypeG = t });
                ((TextBlock)((obj).Children[5])).Text = string.Empty;
                ((TextBlock)((obj).Children[0])).Text = string.Empty;
                ((TextBlock)((obj).Children[1])).Text = string.Empty;
                ((TextBlock)((obj).Children[2])).Text = string.Empty;
                ((TextBlock)((obj).Children[3])).Text = string.Empty;
                ((TextBlock)((obj).Children[4])).Text = string.Empty;
                obj.Resources.Remove("taken");
            }
        }

        private void OnDrop(Canvas obj)
        {
            if (SelectedGenerator != null)
            {
                if ((obj).Resources["taken"] == null)
                {
                    BitmapImage slika = new BitmapImage();
                    slika.BeginInit();
                    slika.UriSource = new Uri(SelectedGenerator.TypeG.ImgSrc);
                    slika.EndInit();
                    (obj).Background = new ImageBrush(slika);
                    if (SelectedGenerator.Value>5 || selectedGenerator.Value == 0)
                        ((TextBlock)((obj).Children[5])).Text = "GRESKA";
                    ((TextBlock)((obj).Children[0])).Text = SelectedGenerator.Id + "," + SelectedGenerator.Name;
                    ((TextBlock)((obj).Children[1])).Text = SelectedGenerator.Value.ToString();
                    ((TextBlock)((obj).Children[2])).Text = SelectedGenerator.Id.ToString();
                    ((TextBlock)((obj).Children[3])).Text = SelectedGenerator.TypeG.Name;
                    ((TextBlock)((obj).Children[4])).Text = SelectedGenerator.TypeG.ImgSrc;
                    (obj).Resources.Add("taken", true);
                }
                SelectedGenerator = null;
                dragging = false;
            }
        }

        private void OnDragOver(Canvas obj)
        {
            if (obj.Resources["taken"] != null)
                obj.AllowDrop = false;
            else
                obj.AllowDrop = true;
        }

        private void OnMouseUp(Canvas obj)
        {
            CurrentGenerator = null;
            SelectedGenerator = null;
            dragging = false;
        }

        private void OnListViewChange(ListView obj)
        {
            if (!dragging)
            {
                dragging = true;
                CurrentGenerator = SelectedGenerator;
                DragDrop.DoDragDrop(obj, CurrentGenerator, DragDropEffects.Move);
                DisplayGenerator.Remove(CurrentGenerator);
            }
        }

    }
}
