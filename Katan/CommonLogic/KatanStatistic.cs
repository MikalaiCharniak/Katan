using System;
using Katan.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Katan.CommonLogic
{
    public class KatanStatistic :  INotifyPropertyChanged
    {

        private TimeSpan _roundEncryptionTime;
        private TimeSpan _roundDecryptionTime;
        private TimeSpan _totalEncryptionTime;
        private TimeSpan _totalDecryptionTime;
        private int _sizeInfoEncrypt;
        private int _sizeInfoDecrypt;

        public TimeSpan RoundEncryptionTime
        {
            get => _roundEncryptionTime;
            set
            {
                _roundEncryptionTime = value;
                OnPropertyChanged("RoundEncryptionTime");
            }
        }
        public TimeSpan RoundDecryptionTime
        {
            get => _roundDecryptionTime;
            set
            {
                _roundDecryptionTime = value;
                OnPropertyChanged("RoundDecryptionTime");
            }
        }
        public TimeSpan TotalEncryptionTime
        {
            get => _totalEncryptionTime;
            set
            {
                _totalEncryptionTime = value;
                OnPropertyChanged("TotalEncryptionTime");
            }
        }
        public TimeSpan TotalDecryptionTime
        {
            get => _totalDecryptionTime;
            set
            {
                _totalDecryptionTime = value;
                OnPropertyChanged("TotalDecryptionTime");
            }
        }
        public int SizeInfoEncrypt
        {
            get => _sizeInfoEncrypt;
            set
            {
                _sizeInfoEncrypt = value;
                OnPropertyChanged("SizeInfoEncrypt");
            }
        }
        public int SizeInfoDecrypt
        {
            get => _sizeInfoDecrypt;
            set
            {
                _sizeInfoDecrypt = value;
                OnPropertyChanged("SizeInfoDecrypt");
            }
        }

        public void EncryptStatistic(KatanTextAdapter adapter)
        {
            RoundEncryptionTime = adapter.Katan.RoundEncryptionTime;
            TotalEncryptionTime = adapter.Katan.TotalEncryptionTime;
            SizeInfoEncrypt = (int)adapter.Katan.KatanVersion * adapter.blocks.Count;
        }

        public void DecryptStatistic(KatanTextAdapter adapter)
        {
            RoundDecryptionTime = adapter.Katan.RoundDecryptionTime;
            TotalDecryptionTime = adapter.Katan.TotalDecryptionTime;
            SizeInfoDecrypt = (int)adapter.Katan.KatanVersion * adapter.blocks.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
