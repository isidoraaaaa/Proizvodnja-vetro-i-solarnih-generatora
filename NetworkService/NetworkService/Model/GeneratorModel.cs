using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class GeneratorModel : ValidationBase
    {
        private int id;
        private string name;
        private Type typeG;
        private int value;

        public GeneratorModel() { }

        public GeneratorModel(int id) { this.id = id; }

        public GeneratorModel(GeneratorModel g) { Id = g.Id; Name = g.Name; Value = g.Value; TypeG = g.TypeG; }

        public int Id
        {
            get { return id; }
            set { if (this.id != value) { this.id = value; RaisePropertyChanged("Id"); } }
        }
        public string Name
        {
            get { return name; }
            set { if (this.name != value) { this.name = value; RaisePropertyChanged("Name"); } }
        }
        public int Value
        {
            get { return value; }
            set { if (this.value != value) { this.value = value; RaisePropertyChanged("Value"); } }
        }
        public Type TypeG
        {
            get { return typeG; }
            set { typeG = value; RaisePropertyChanged("TypeG"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        protected override void ValidateSelf()
        {
            foreach (GeneratorModel g in ViewModel.GeneratoriViewModel.Generators)
            {
                if (this.id == g.Id)
                {
                    this.ValidationErrors["Id"] = "ALREADY EXISTS!";
                }
            }
            if (this.id < 0 || string.IsNullOrWhiteSpace(this.id.ToString()))
            {
                this.ValidationErrors["Id"] = "INVALID!";
            }
            if (string.IsNullOrWhiteSpace(this.name))
            {
                this.ValidationErrors["Name"] = "EMPTY!";
            }
        }

        public override string ToString()
        {
            return id  + "    " +  name;
        }
    }
}
