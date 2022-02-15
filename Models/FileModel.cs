using DataManagerPC.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DataManagerPC.Models
{
    class FileModel
    {
        private string[] _descriptions;

        private string _path;

        public string PATH
        {
            get { return _path; }
            set { _path = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _descriptions = value.Split('.');
                string partial = "";
                for (int i = 0; i < _descriptions.Length - 1; i++)
                {
                    if (partial != _descriptions[^1])
                    {
                        partial += _descriptions[i];
                    }
                }    
                _name = partial.Split('\\')[^1];
            }
        }

        private string _shortcutPath;

        public string ShortcutPath
        {
            get 
            {
                return _shortcutPath; 
            }
            set { _shortcutPath = value; }
        }


        private long _size;

        public long Size
        {
            get { return _size/1024; }
            set { _size = value; }
        }


    }
}