using DataManagerPC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace DataManagerPC.Services
{
    public class FileIOService<T>
    {
        private string _path;

        public FileIOService(string path)
        {
            _path = path;
        }

        public BindingList<T> LoadData()
        {
            if (File.Exists(_path) == false)
            {
                File.CreateText(_path).Dispose();
                return new BindingList<T>();
            }

            using (StreamReader reader = File.OpenText(_path))
            {
                string fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<T>>(fileText);
            }
        }

        public void SaveData(object objectList)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string readData = JsonConvert.SerializeObject(objectList);
                writer.Write(readData);
            }
        }

    }
}
