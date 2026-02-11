using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


namespace GambleRun.Persistence
{
    public class FileDataService : IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;
        private string _fileExtension;
        
        public FileDataService(ISerializer serializer)
        {
            // OS별 표준 저장 경로를 자동으로 가져옴(앱 삭제 전까지 보존됨)
            _dataPath = Application.persistentDataPath;
            _serializer = serializer;
            _fileExtension = "json";
        }
        string GetPathToFile(string fileName)
        {
            return Path.Combine(_dataPath, string.Concat(fileName, ".", _fileExtension));
        }

        public void Save(GameData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(data.FileName);
            Debug.Log(fileLocation);

            if (!overwrite && File.Exists(fileLocation))
            {
                throw new IOException($"The file '{data.FileName}.{_fileExtension}' already exists and cannot be overwritten.");
            }

            File.WriteAllText(fileLocation, _serializer.Serialize(data));
        }

        public GameData Load(string name)
        {
            string fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation))
            {
                throw new ArgumentException($"No persisted GameData with name '{name}'");
            }

            return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
        }

        public void Delete(string name)
        {
            string fileLocation = GetPathToFile(name);

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
            }
        }

        public void DeleteAll()
        {
            foreach (string filePath in Directory.GetFiles(_dataPath))
            {
                File.Delete(filePath);
            }
        }

        //public IEnumerable<string> ListSaves()
        //{
        //    foreach (string path in Directory.EnumerateFiles(_dataPath))
        //    {
        //        if (Path.GetExtension(path) == _fileExtension)
        //        {
        //            yield return Path.GetFileNameWithoutExtension(path);
        //        }
        //    }
        //}
    }
}
