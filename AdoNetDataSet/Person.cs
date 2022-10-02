using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDataSet
{
    public class Person : INotifyPropertyChanged
    {
        private string _name;
        private int _age;

        public int Id
        {
            get;

            set;
        }
        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get => _age;

            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public double Height { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
