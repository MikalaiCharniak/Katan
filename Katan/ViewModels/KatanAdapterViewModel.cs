using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Katan.CommonLogic;

namespace Katan.ViewModels
{
    public class KatanAdapterViewModel : INotifyPropertyChanged
    {
        private List<int> _exampleBits = new List<int>()
        {
            1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1,
            0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0
        };

        #region Observal Props
        private int _currentRound = 0;
        private KatanVisualAdapter _katan;
        private KatanRound _currentKatanRound;
        public int CurrentRound
        {
            get => _currentRound;
            set
            {
                _currentRound = value;
                OnPropertyChanged("CurrentRound");
            }
        }
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

        #endregion

        #region Commands

        private RelayCommand _nextRound;
        public RelayCommand NextRoundCommand
        {
            get
            {
                return _nextRound ??
                    (_nextRound = new RelayCommand(obj =>
                    {
                        NextRound();
                    }));

            }
        }
        private void NextRound()
        {
            if (CurrentRound == 253)
            {
                CurrentKatanRound = Katan.KatanRounds[CurrentRound];
            }
            else
            {
                CurrentKatanRound = Katan.KatanRounds[CurrentRound++];
            }
        }

        private RelayCommand _previosRound;
        public RelayCommand PreviosRoundCommand
        {
            get
            {
                return _previosRound ??
                    (_previosRound = new RelayCommand(obj =>
                    {
                        PreviosRound();
                    }));
            }
        }
        private void PreviosRound()
        {
            if (CurrentRound == 0)
            {
                CurrentKatanRound = Katan.KatanRounds[CurrentRound];
            }
            else
            {
                CurrentKatanRound = Katan.KatanRounds[CurrentRound--];
            }
        }

        #endregion

        public KatanAdapterViewModel()
        {
            _katan = new KatanVisualAdapter(Core.Katan.Version.Version32, 90);
            _katan.KatanEncryption(_exampleBits);
            _currentKatanRound = _katan.KatanRounds[CurrentRound];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
