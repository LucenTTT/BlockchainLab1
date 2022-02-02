using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockchainLab1.Annotations;
using Microsoft.Toolkit.Mvvm.Input;

namespace BlockchainLab1
{
    public enum Mode
    {
        String,
        File
    }
    public class AppViewModel : INotifyPropertyChanged
    {
        private Mode _whatHashMode;
        private string _text;
        private string _hash;
        public Mode WhatHashMode
        {
            get { return _whatHashMode; }
            set
            {
                _whatHashMode = value;
                OnPropertyChanged();
            }

        }
        public ICommand SetModeCommand { get; set; }
        public ICommand HashCommand { get; set; }
        public string Text 
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        public string Hash 
        {
            get { return _hash; }
            set
            {
                _hash = value;
                OnPropertyChanged();
            }
        }

        public AppViewModel()
        {
            SetModeCommand = new RelayCommand<Mode>(OnSetModeExecuted);
            HashCommand = new RelayCommand(OnHashExecuted);
        }

        private void OnHashExecuted()
        {
            switch (WhatHashMode)
            {
                case Mode.String:
                    SHA256Hash();
                    break;
                case Mode.File:
                    FindFile();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FindFile()
        {
            var file = new FileInfo(Text);
            if (!file.Exists)
                return;
            var fileStream = file.OpenRead();
            Hash = String.Empty;
            var hash = SHA256Implementation.SHA256Implementation.HashFile(fileStream);
            Hash = Convert.ToBase64String(hash.ToArray());
        }

        private void SHA256Hash()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(Text);
            writer.Flush();
            stream.Position = 0;
            Hash = String.Empty;
            var hash = SHA256Implementation.SHA256Implementation.HashFile(stream);
            Hash = Convert.ToBase64String(hash.ToArray());
        }

        private void OnSetModeExecuted(Mode obj)
        {
            WhatHashMode = obj;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
