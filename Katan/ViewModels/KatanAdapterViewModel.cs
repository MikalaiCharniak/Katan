using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Katan.CommonLogic;

namespace Katan.ViewModels
{
    public class KatanAdapterViewModel : INotifyPropertyChanged
    {
        private int _currentRound = 0;
        private List<int> _exampleBits = new List<int>()
        {
            1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1,
            0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0
        };

        #region Observal Props
        private KatanVisualAdapter _katan;
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
        private void NextRound() => CurrentKatanRound = _katan.KatanRounds[_currentRound++];

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
        private void PreviosRound() => CurrentKatanRound =_katan.KatanRounds[_currentRound--];

        #endregion

        public KatanAdapterViewModel()
        {
            _katan = new KatanVisualAdapter(Core.Katan.Version.Version32, 90);
            _katan.KatanEncryption(_exampleBits);
            _currentKatanRound = _katan.KatanRounds[_currentRound];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
