using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Katan.Core;
using Katan.CommonLogic;
using System.Windows;

namespace Katan.ViewModels
{
    public class KatanMainViewModel : INotifyPropertyChanged
    {

        #region  Observal Props
        private string _inputText;
        private string _outputText;
        private KatanTextAdapter _katanTextAdapter;
        private string _key;
        private int _currentKatanVersion;

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
        public int CurrentKatanVersion
        {
            get => _currentKatanVersion;
            set
            {
                _currentKatanVersion = value;
                OnPropertyChanged("KatanVersion");
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
            try
            {
                if (_katanTextAdapter == null ||
                    (int)_katanTextAdapter.Katan.KatanVersion != CurrentKatanVersion)
                {
                    _katanTextAdapter = new KatanTextAdapter(new Core.Katan((Core.Katan.Version)CurrentKatanVersion,
                        Int32.Parse(KatanKey)));
                }
                OutputText = _katanTextAdapter.KatanEncryptText(InputText);
                _katanTextAdapter.ClearBuffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            try
            {
                if (_katanTextAdapter == null ||
                (int)_katanTextAdapter.Katan.KatanVersion != CurrentKatanVersion)
                {
                    _katanTextAdapter = new KatanTextAdapter(new Core.Katan((Core.Katan.Version)CurrentKatanVersion,
                        Int32.Parse(KatanKey)));
                }
                InputText = _katanTextAdapter.AltKatanDecryptText(OutputText);
                InputText = _katanTextAdapter.SpecialRetransformText(InputText);
                _katanTextAdapter.ClearBuffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private RelayCommand _loadFile;
        public RelayCommand LoadFileCommand
        {
            get => _loadFile ??
                   (_loadFile = new RelayCommand(obj =>
                  {
                      LoadFile();
                  }));
        }
        public void LoadFile()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            var result = dialog.ShowDialog();
            if (result == true)
            {
                InputText = System.IO.File.ReadAllText(dialog.FileName);
            }
        }

        private RelayCommand _saveFile;
        public RelayCommand SaveFileCommand
        {
            get => _saveFile ??
                   (_saveFile = new RelayCommand(obj =>
                   {
                       SaveFile();
                   }));
        }
        public void SaveFile()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                System.IO.File.WriteAllText(saveFileDialog.FileName, OutputText);
        }

        #endregion

        public KatanMainViewModel()
        {
            CurrentKatanVersion = (int)Core.Katan.Version.Version32;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
