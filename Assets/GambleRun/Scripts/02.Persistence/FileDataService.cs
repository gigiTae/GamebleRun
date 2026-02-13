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

        public void Save<TData>(string name, TData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(name);
            Debug.Log(fileLocation);

            if (!overwrite && File.Exists(fileLocation))
            {
                throw new IOException($"The file '{name}.{_fileExtension}' already exists and cannot be overwritten.");
            }

            File.WriteAllText(fileLocation, _serializer.Serialize(data));
        }

        public TData Load<TData>(string name)
        {
            string fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation))
            {
                Debug.Log($"{fileLocation}을 찾을 수 없습니다");

                // C#에서 default 키워드는 해당 타입의 기본값을 의미합니다.
                // 제네릭(TData)을 사용할 때, 그 타입이 무엇인지 미리 알 수 없으므로
                // "이 타입이 가질 수 있는 가장 기본 상태의 값을 줘"라고 명령하는 것입니다.
                return default;
            }

            return _serializer.Deserialize<TData>(File.ReadAllText(fileLocation));
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
