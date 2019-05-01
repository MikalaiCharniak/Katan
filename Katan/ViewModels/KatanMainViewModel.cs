using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                _katanTextAdapter = new KatanTextAdapter(new Core.Katan((Core.Katan.Version)32, 90));
                OutputText = _katanTextAdapter.KatanEncryptText(InputText);
            }
            catch (Exception ex)
            {

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
                _katanTextAdapter = new KatanTextAdapter(new Core.Katan((Core.Katan.Version)32, 90));
                InputText = _katanTextAdapter.AltKatanDecryptText(OutputText);
                InputText = _katanTextAdapter.SpecialRetransformText(InputText);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
