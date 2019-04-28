using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private List<ListViewItem> _registerView;
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
        public List<ListViewItem> RegisterView
        {
            get => _registerView;
            set
            {
                _registerView = value;
                OnPropertyChanged("RegisterView");
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
                SetRegisterView();
            }
            else
            {
                CurrentKatanRound = Katan.KatanRounds[CurrentRound++];
                SetRegisterView();
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
                SetRegisterView();
            }
            else
            {
                CurrentKatanRound = Katan.KatanRounds[CurrentRound--];
                SetRegisterView();
            }
        }

        #endregion

        public KatanAdapterViewModel()
        {
            _katan = new KatanVisualAdapter(Core.Katan.Version.Version32, 90);
            _katan.KatanEncryption(_exampleBits);
            _currentKatanRound = _katan.KatanRounds[CurrentRound];
            _registerView = new List<ListViewItem>();
            SetRegisterView();
        }

        private void SetRegisterView()
        {
            RegisterView.Clear();
            var buffer = new List<ListViewItem>();
            for (int i = 0; i < CurrentKatanRound.FirstRegister.Count; i++)
            {
                buffer.Add(new ListViewItem()
                {
                    Content = CurrentKatanRound.FirstRegister[i].ToString(),
                    Height = 50,
                    Width = 50,
                    Style = Katan.SetX.Contains(i + 1) ?
                    KatanAdapterStyles.RegisterActiveCellStyle : KatanAdapterStyles.RegisterSimpleCellStyle
                });
            }
            RegisterView = buffer;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public static class KatanAdapterStyles
    {
        public static readonly Style RegisterSimpleCellStyle;
        public static readonly Style RegisterActiveCellStyle;

        static KatanAdapterStyles()
        {
            RegisterSimpleCellStyle = new Style();
            RegisterActiveCellStyle = new Style();
            InitRegisterCellSimpleStyle();
            InitRegisterActiveSimpleStyle();
        }

        private static void InitRegisterCellSimpleStyle()
        {
            RegisterSimpleCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.BackgroundProperty, Value = new SolidColorBrush(Colors.Black)});
            RegisterSimpleCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.FontFamilyProperty, Value = new FontFamily("Verdana") });
            RegisterSimpleCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.ForegroundProperty, Value = new SolidColorBrush(Colors.White) });
            RegisterSimpleCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });
            RegisterSimpleCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.MarginProperty, Value = new Thickness(5) });

        }
        private static void InitRegisterActiveSimpleStyle()
        {
            RegisterActiveCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.BackgroundProperty, Value = new SolidColorBrush(Colors.Red) });
            RegisterActiveCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.FontFamilyProperty, Value = new FontFamily("Verdana") });
            RegisterActiveCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.ForegroundProperty, Value = new SolidColorBrush(Colors.White) });
            RegisterActiveCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });
            RegisterSimpleCellStyle.Setters.Add(new Setter
            { Property = ListViewItem.MarginProperty, Value = new Thickness(5) });
        }
    }
}
