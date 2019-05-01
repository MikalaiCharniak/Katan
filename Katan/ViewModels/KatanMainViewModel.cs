using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Katan.Core;
using Katan.CommonLogic;

namespace Katan.ViewModels
{
    public class KatanMainViewModel : INotifyPropertyChanged
    {

        #region  Observal Props
        private string _inputText;
        private string _outputText;
        private KatanTextAdapter _katanTextAdapter;
        private string _key;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged("InputText");
            }
        }
        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                OnPropertyChanged("OutputText");
            }
        }
        public string KatanKey
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged("KatanKey");
            }
        }
        #endregion

        #region Commands

        private RelayCommand _encryptText;
        public RelayCommand EncryptTextCommand
        {
            get
            {
                return _encryptText ??
                       (_encryptText = new RelayCommand(obj =>
                       {
                           EncryptText();
                       }));
            }
        }
        private void EncryptText()
        {

        }

        private RelayCommand _decryptText;
        public RelayCommand DecryptTextCommand
        {
            get
            {
                return _decryptText ??
                        (_decryptText = new RelayCommand(obj =>
                        {
                            DecryptText();
                        }));
            }
        }
        public void DecryptText()
        {

        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
