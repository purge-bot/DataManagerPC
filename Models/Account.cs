using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataManagerPC.Models
{
    public class Account : INotifyPropertyChanged
    {
        private string _resource;
        private string _login;
        private string _password;
        private bool _synced;

        public string Resource
        {
            get { return _resource; }
            set
            {
                if (value == _resource)
                    return;

                _resource = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                if (value == _login)
                    return;

                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password)
                    return;

                _password = value;
                OnPropertyChanged();
            }
        }
        
        public bool Synced 
        {
            get { return _synced; }
            set
            {
                if (value == _synced)
                    return;

                _synced = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
