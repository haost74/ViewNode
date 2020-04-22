using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Perceptron.ModelView
{
    public class Main : INotifyPropertyChanged
    {

        private int row = 2;
        public int Row
        {
            get { return row; }
            set
            {
                row = value;
                OnPropertyChanged();
            }
        }

        private int column = 2;
        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<bool> isCheck = new ObservableCollection<bool>();
        public ObservableCollection<bool> IsCheck
        {
            get { return isCheck; }
            set
            {
                isCheck = value;
                OnPropertyChanged();
            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        } 
        #endregion
    }
}
