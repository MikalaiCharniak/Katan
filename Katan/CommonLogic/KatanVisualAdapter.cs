using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Katan.CommonLogic
{
    public class KatanVisualAdapter : Katan.Core.Katan, INotifyPropertyChanged
    {
        private List<KatanRound> _katanRoundsList;
        public List<KatanRound> KatanRounds
        {
            get => _katanRoundsList;
            set
            {
                _katanRoundsList = value;
                OnPropertyChanged("KatanRounds");
            }
        }

        public KatanVisualAdapter(Katan.Core.Katan.Version version, int key)
            : base(version, key)
        {
        }

        public override List<int> KatanEncryption(List<int> plainTextBits)
        {
            KatanRounds = new List<KatanRound>();
            _secondRegister = plainTextBits.Take(_secondRegisterCapacity).ToList();
            _firstRegister = plainTextBits.Skip(_secondRegisterCapacity).ToList();
            for (int round = 0; round != 254; round++)
            {
                KatanRoundEncription(round);
                if ((int)KatanVersion > 32)
                {
                    KatanRoundEncription(round);
                }
                if ((int)KatanVersion > 48)
                {
                    KatanRoundEncription(round);
                }
            }
            return _secondRegister.Concat(_firstRegister).ToList();
        }

        protected override void KatanRoundEncription(int round)
        {
            int k_a = _key[2 * round];
            int k_b = _key[2 * round + 1];

            int f_a = _firstRegister[_setX[0]] ^ _firstRegister[_setX[1]]
                ^ (_firstRegister[_setX[2]] & _firstRegister[_setX[3]]) ^ k_a;
            if (_IR[round] != 0)
            {
                f_a ^= _firstRegister[_setX[4]];
            }

            int f_b = _secondRegister[_setY[0]] ^ _secondRegister[_setY[1]]
                ^ (_secondRegister[_setY[2]] & _secondRegister[_setY[3]])
                ^ (_secondRegister[_setY[4]] & _secondRegister[_setY[5]])
                ^ k_b;

            KatanRounds.Add(new KatanRound()
            {
                Round = round,
                K_A = k_a,
                K_B = k_b,
                F_A = f_a,
                F_B = f_b,
                FirstRegister = _firstRegister,
                SecondRegister = _secondRegister
            });

            _firstRegister.RemoveAt(_firstRegisterCapacity - 1);
            _firstRegister.Insert(0, f_b);

            _secondRegister.RemoveAt(_secondRegisterCapacity - 1);
            _secondRegister.Insert(0, f_a);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

    public class KatanRound : INotifyPropertyChanged
    {
        #region Observal Props
        private int k_a;
        private int k_b;
        private int f_a;
        private int f_b;
        private int round;
        private List<int> _firstRegister;
        private List<int> _secondRegister;

        public int K_A
        {
            get => k_a;
            set
            {
                k_a = value;
                OnPropertyChanged("K_A");
            }
        }
        public int K_B
        {
            get => k_b;
            set
            {
                k_b = value;
                OnPropertyChanged("K_B");
            }
        }
        public int Round
        {
            get => round;
            set
            {
                round = value;
                OnPropertyChanged("Round");
            }
        }
        public int F_A
        {
            get => f_a;
            set
            {
                f_a = value;
                OnPropertyChanged("F_A");
            }
        }
        public int F_B
        {
            get => f_b;
            set
            {
                f_b = value;
                OnPropertyChanged("F_B");
            }
        }
        public List<int> FirstRegister
        {
            get => _firstRegister;

            set
            {
                _firstRegister = value;
                OnPropertyChanged("FirstRegister");
            }
        }
        public List<int> SecondRegister
        {
            get => _secondRegister;

            set
            {
                _secondRegister = value;
                OnPropertyChanged("SecondRegister");
            }
        }

        #endregion

        public KatanRound()
        {
            FirstRegister = new List<int>();
            SecondRegister = new List<int>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
