using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Katan.CommonLogic;

namespace Katan.ViewModels
{
    public class KatanAdapterViewModel : INotifyPropertyChanged
    {
        private KatanVisualAdapter _katan;
        private List<int> _exampleBits = new List<int>()
        {
            1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1,
            0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0
        };
        private KatanRound _currentKatanRound;
        public KatanRound CurrentKatanRound
        {
            get => _currentKatanRound;
            set
            {
                _currentKatanRound = value;
                OnPropertyChanged("CurrentKatanRound");
            }
        }
        public KatanVisualAdapter Katan
        {
            get => _katan;
            set
            {
                _katan = value;
                OnPropertyChanged("Katan");
            }
        }


        public KatanAdapterViewModel()
        {
            _katan = new KatanVisualAdapter(Core.Katan.Version.Version32, 90);
            _katan.KatanEncryption(_exampleBits);
            _currentKatanRound = _katan.KatanRounds[0];
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
