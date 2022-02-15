using System;
using System.Collections.Generic;
using System.Text;
using IWshRuntimeLibrary;

namespace DataManagerPC.Services
{
    public class ShortLinkService
    {
        private string _name;
        private string _parentPath;
        private string _path;

        public IWshShortcut Shortcut { get; private set; }

        public ShortLinkService(string name, string parentPath = null)
        {
            _name = name;
            _parentPath = parentPath;
            _path = Environment.CurrentDirectory + @$"\Shortcuts\{_name}.lnk";

            WshShell shell = new WshShell();

            Shortcut = (IWshShortcut)shell.CreateShortcut(_path);
        }

        public string CreateShotcut()
        {
            Shortcut.TargetPath = _parentPath;

            Shortcut.Save();

            return _path;
        }

    }
}
