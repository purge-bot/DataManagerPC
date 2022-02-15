using DataManagerPC.Command;
using DataManagerPC.Models;
using DataManagerPC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DataManagerPC.ViewModels
{
    class AccountViewModel : INotifyPropertyChanged
    {
        public BindingList<Account> Accounts { get; set; }

        private ActionApplication _submit;
        private ActionApplication _add;
        private ActionApplication _deleteRow;
        private ActionApplication _openPage;

        private string _path = $"{Environment.CurrentDirectory}\\Accounts.json";
        private FileIOService<Account> _fileIOService;
        private Account _selectedAccount;

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set 
            {
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        public ActionApplication Submit
        {
            get 
            {
                return _submit = new ActionApplication(obj =>
                {
                    _fileIOService.SaveData(Accounts);
                });
            }
        }

        public ActionApplication Add
        {
            get 
            {
                return _add = new ActionApplication(obj =>
                {
                    Account newAccount = new Account();
                    Accounts.Insert(0, newAccount);
                    SelectedAccount = newAccount;
                });
            }
        }

        public ActionApplication DeleteRow
        {
            get
            { 
                return _deleteRow = new ActionApplication(obj =>
                {
                    Accounts.Remove(SelectedAccount);
                });
            }
        }

        public ActionApplication OpenPage
        {
            get 
            {
                return _openPage = new ActionApplication(obj =>
                {
                    Window newWindow = new FileManager();
                    newWindow.Show();
                }, obj =>
                {
                    foreach (Window window in App.Current.Windows)
                    {
                        if(window.Name == "FileWindow")
                        {
                            return false;
                        }
                    }
                    return true;
                }); 
            }
        }

        public AccountViewModel()
        {
            _fileIOService = new FileIOService<Account>(_path);
            Accounts = _fileIOService.LoadData();

            Accounts.ListChanged += Account_ListChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Account_ListChanged(object sender, ListChangedEventArgs e)
        {
            //if(/*e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged ||*/ e.ListChangedType == ListChangedType.ItemDeleted)
          //  {
            //    _fileIOService.SaveData(sender);
            //}
        }
    }
}
