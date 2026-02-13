using UnityEngine;
using GambleRun.Persistence;

namespace GambleRun
{
    /// <summary>
    /// Storage 컴포넌트와 SaveLoadManager 사이의 가교 역할을 하는 클래스입니다.
    /// 저장이 필요한 Storage 오브젝트에만 이 컴포넌트를 추가합니다.
    /// </summary>
    [RequireComponent(typeof(Storage))]
    public class StorageDataBinder : MonoBehaviour, IBind<StorageData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();

        private Storage _storage;

        private void Awake()
        {
            _storage = GetComponent<Storage>();
        }

        /// <summary>
        /// SaveLoadManager.ApplyGameData() 실행 시 호출되어 데이터를 전달받습니다.
        /// </summary>
        public void Bind(StorageData data)
        {
            if (_storage != null)
            {
                // 데이터의 ID를 Binder의 ID와 일치시켜 데이터 무결성을 보장합니다.
                data.Id = Id;

                // 실제 기능을 담당하는 Storage에 데이터를 주입합니다.
                _storage.InitializeStorage(data);
            }
        }
    }
}