using DataManagerPC.Command;
using DataManagerPC.Models;
using DataManagerPC.Services;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;

namespace DataManagerPC.ViewModels
{


    class FileViewModel : IDropTarget, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<FileModel> Files { get; set; }
        public BindingList<FileModel> NewFiles { get; set; }

        private string _pathDirectory = $"{Environment.CurrentDirectory}\\Shortcuts";
        private string _path = $"{Environment.CurrentDirectory}\\Files.json";
        private FileIOService<FileModel> _fileIOService;
        private DirectoryInfo _dir;

        private FileModel _selectedFile;
        private ActionApplication _create;
        private ActionApplication _submit;
        private ActionApplication _update;

        public FileModel SelectedFile
        {
            get { return _selectedFile; }
            set 
            { 
                _selectedFile = value;
                OnPropertyChanged();
            }
        }

        public ActionApplication Create
        {
            get 
            {
                return _create = new ActionApplication(obj =>
                {
                    string[] allfiles = Directory.GetFiles(Environment.CurrentDirectory);
                    foreach (var file in Files)
                    {
                        if(!allfiles.Contains(file.PATH))
                        {
                            ShortLinkService shortLink = new ShortLinkService(file.Name, file.PATH);
                            shortLink.CreateShotcut();
                        }
                    }
                });
            }
        }

        public ActionApplication Submit
        {
            get
            {
                return _submit = new ActionApplication(obj =>
                {
                    foreach (var file in NewFiles)
                    {
                        Files.Insert(0, file);
                        ShortLinkService shortLink = new ShortLinkService(file.Name, file.PATH);
                        shortLink.CreateShotcut();   
                    }

                    NewFiles.Clear();
                    _fileIOService.SaveData(Files);
                });
            }
        }

        public ActionApplication Update
        {
            get
            {
                return _update = new ActionApplication(obj =>
                {
                    /*foreach (var file in Files)
                    {
                        file.PATH = GetLnkTarget(file.ShortcutPath);
                    }
                    _fileIOService.SaveData(Files);*/
                    InitFiles();
                });
            }
        }

        public FileViewModel()
        {
            Files = new BindingList<FileModel>();
            NewFiles = new BindingList<FileModel>();
            _fileIOService = new FileIOService<FileModel>(_path);

            _dir = new DirectoryInfo(_pathDirectory);

            if (!_dir.Exists)
            {
                _dir.Create();
            }

            InitFiles();

        }

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as System.Windows.IDataObject;

            dropInfo.Effects = dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;
            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                var files = dataObject.GetFileDropList();
                foreach (var file in files)
                {
                    AddFile(file);
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void InitFiles()
        {
            Files.Clear();

            FileInfo[] shortcuts = _dir.GetFiles();

            foreach (var file in shortcuts)
            {
                try
                {
                    FileModel newFile = new FileModel();
                    newFile.ShortcutPath = file.FullName;
                    newFile.Name = file.FullName;

                    ShortLinkService lnk = new ShortLinkService(newFile.Name);
                    newFile.PATH = lnk.Shortcut.TargetPath;

                    FileInfo fileInf = new FileInfo(newFile.PATH);
                    newFile.Size = fileInf.Length;

                    Files.Insert(0, newFile);

                    _fileIOService.SaveData(Files);
                }
                catch (Exception)
                {
                    
                }

            }
        }

        private void AddFile(string file)
        {
            FileModel newFile = new FileModel();
            newFile.PATH = file;
            newFile.Name = file;

            FileInfo fileInf = new FileInfo(newFile.PATH);
            newFile.Size = fileInf.Length;

            NewFiles.Insert(0, newFile);
            SelectedFile = newFile;
        }
    }
}
