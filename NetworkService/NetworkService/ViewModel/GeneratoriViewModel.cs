using NetworkService.Model;
using NetworkService.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetworkService.ViewModel
{
    public class GeneratoriViewModel : BindableBase
    {
        public static List<string> GeneratorTypes { get; set; } = new List<string> { "Vetro Generator", "Solarni Generator" };
        public static ObservableCollection<GeneratorModel> Generators { get; set; } = new ObservableCollection<GeneratorModel>();
        public static ObservableCollection<GeneratorModel> GeneratorsSearched { get; set; } = new ObservableCollection<GeneratorModel>();

        private GeneratorModel currentGenerator = new GeneratorModel();


        private GeneratorModel selectedGenerator;

        private string selectedTypeGenerator = string.Empty;

        private string pathFirst = "pack://application:,,,/Images/";
        private string path;

        private bool isNameChecked = true;
        private bool isTypeChecked = false;
        private string searchText;
        private string idText;
        private string nameText;

        private bool isShiftChecked = false;

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand SearchCommand { get; set; }

        public MyICommand UndoCommand { get; set; }

        public MyICommand<TextBox> SearchTipe { get; set; }
        public MyICommand<TextBox> NameTipe { get; set; }
        public MyICommand<TextBox> IdTipe { get; set; }

        public MyICommand<Button> DugmicFunkcija { get; set; }

        private bool tb_search = false;
        private bool tb_ime = false;
        private bool tb_id = false;

        public GeneratoriViewModel()
        {
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            SearchCommand = new MyICommand(OnSearch);
            SearchTipe = new MyICommand<TextBox>(SearchType);
            IdTipe = new MyICommand<TextBox>(IdType);
            NameTipe = new MyICommand<TextBox>(NameType);
            DugmicFunkcija = new MyICommand<Button>(OnType);
            //UndoCommand = new MyICommand(OnUndo);
        }

        public void SearchType(TextBox obj)
        {
            tb_search = true;
            tb_id = false;
            tb_ime = false;
        }

        private void IdType(TextBox obj)
        {
            tb_search = false;
            tb_id = true;
            tb_ime = false;
        }

        private void NameType(TextBox obj)
        {
            tb_search = false;
            tb_id = false;
            tb_ime = true;
        }

        public void OnType(Button obj)
        {
            string[] split = obj.Name.ToString().Split('_');  
            switch (split[1])
            {
                case "space":
                    if (tb_search)
                        SearchText += " ";
                    if (tb_id)
                        IdText += " ";
                    if (tb_ime)
                        NameText += " ";
                    break;

                case "delete":
                    if (tb_search && SearchText != string.Empty)
                        SearchText = SearchText.Remove(SearchText.Length-1);
                    if (tb_id && idText != string.Empty)
                        IdText = IdText.Remove(IdText.Length - 1);
                    if (tb_ime && NameText != string.Empty)
                        NameText = NameText.Remove(NameText.Length - 1);
                    break;

                case "shift":
                    isShiftChecked = true;
                    break;

                default:
                    if (tb_search)
                    {
                        if (isShiftChecked)
                        {
                            SearchText += split[1].ToUpper();
                            isShiftChecked = false;
                        }
                        else
                            SearchText += split[1];
                    }
                    if (tb_id)
                    {
                        if (isShiftChecked)
                        {
                            IdText += split[1].ToUpper();
                            isShiftChecked = false;
                        }
                        else
                            IdText += split[1];
                    }
                    if (tb_ime)
                    {
                        if (isShiftChecked)
                        {
                            NameText += split[1].ToUpper();
                            isShiftChecked = false;
                        }
                        else
                            NameText += split[1];
                    }
                    break;
            }
        }

        public GeneratorModel CurrentGenerator
        {
            get { return currentGenerator; }
            set
            {
                currentGenerator = value;
                OnPropertyChanged("CurrentGenerator");
            }
        }
        public bool IsNameChecked
        {
            get { return isNameChecked; }
            set { isNameChecked = value; OnPropertyChanged("IsNameChecked"); }
        }

        public bool IsTypeChecked
        {
            get { return isTypeChecked; }
            set { isTypeChecked = value; OnPropertyChanged("IsTypeChecked"); }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }

        public string IdText
        {
            get { return idText; }
            set
            {
                idText = value;
                OnPropertyChanged("IdText");
            }
        }

        public string NameText
        {
            get { return nameText; }
            set
            {
                if (nameText != value)
                {
                    nameText = value;
                    OnPropertyChanged("NameText");
                }
            }
        }

        public string Path
        {
            get { return path; }
            set { path = value; OnPropertyChanged("Path"); }
        }

        public string SelectedTypeGenerator
        {
            get { return selectedTypeGenerator; }
            set
            {
                if (selectedTypeGenerator != value)
                {
                    selectedTypeGenerator = value;
                    Path = pathFirst + value.ToString() + ".png";
                    OnPropertyChanged("Path");
                    OnPropertyChanged("SelectedTypeGenerator");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public GeneratorModel SelectedGenerator
        {
            get { return selectedGenerator; }
            set { selectedGenerator = value; DeleteCommand.RaiseCanExecuteChanged(); }
        }

        private void OnSearch()
        {
            if (IsNameChecked)
            {
                GeneratorsSearched.Clear();
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    for (int i = 0; i < Generators.Count(); i++)
                        GeneratorsSearched.Add(Generators[i]);
                }
                else
                {
                    for (int i = 0; i < Generators.Count(); i++)
                    {
                        if (Generators[i].Name.Contains(SearchText.Trim()))
                            GeneratorsSearched.Add(Generators[i]);
                    }
                }
            }
            else
            {
                GeneratorsSearched.Clear();
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    for (int i = 0; i < Generators.Count(); i++)
                        GeneratorsSearched.Add(Generators[i]);
                }
                else
                {
                    for (int i = 0; i < Generators.Count(); i++)
                    {
                        if (Generators[i].TypeG.Name.Contains(SearchText))
                            GeneratorsSearched.Add(Generators[i]);
                    }
                }
            }
        }

        private bool CanDelete()
        {
            return SelectedGenerator != null;
        }

        private void OnDelete()
        {
            Generators.Remove(SelectedGenerator);
            GeneratorsSearched.Remove(SelectedGenerator);
        }

        private bool CanAdd()
        {
            if (IdText != null)
            {
                try { CurrentGenerator.Id = Int32.Parse(IdText); }
                catch
                {
                    MessageBox.Show("Please check your ID entry!");
                }
            }
            if (NameText != null)
                CurrentGenerator.Name = NameText;
            if (SelectedTypeGenerator != null && CurrentGenerator.Id != 0 && CurrentGenerator.Name != null)
                return true;
            else
                return false;
        }

        private void OnAdd()
        {
            CurrentGenerator.Validate();
            if (CurrentGenerator.IsValid)
            {
                Model.Type tmp = new Model.Type(SelectedTypeGenerator, pathFirst + SelectedTypeGenerator + ".png");
                Generators.Add(new GeneratorModel { Id = CurrentGenerator.Id, Name = CurrentGenerator.Name, Value = CurrentGenerator.Value, TypeG = tmp });
                GeneratorsSearched.Add(new GeneratorModel { Id = CurrentGenerator.Id, Name = CurrentGenerator.Name, Value = CurrentGenerator.Value, TypeG = tmp });
            }
        }



    }
}
