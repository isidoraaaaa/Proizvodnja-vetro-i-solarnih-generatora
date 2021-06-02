using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Type : ValidationBase
    {
        private string name;
        private string imgSrc;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        protected override void ValidateSelf()
        {
            throw new NotImplementedException();
        }

        public Type() { Name = string.Empty; ImgSrc = string.Empty; }

        public Type(string ime, string slika_source) { Name = ime; ImgSrc = slika_source; }

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); }
        }

        public string ImgSrc
        {
            get { return imgSrc; }
            set { imgSrc = value; RaisePropertyChanged("ImgSrc"); }
        }
    }
}
