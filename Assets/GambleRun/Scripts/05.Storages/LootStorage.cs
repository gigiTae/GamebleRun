using System.Collections;
using UnityEngine;
using GambleRun.Event;
using GambleRun.Items;
using Unity.VisualScripting;

namespace GambleRun.Storages
{
    /// <summary>
    /// 전리품 스토리지: 아이템을 순차적으로 감별하는 로직 포함
    /// </summary>
    public class LootStorage : Storage
    {
        [SerializeField] private float _identifyDelay = 2.0f;
        [SerializeField] private InventoryEvent _inventoryCloseEvent;

        private WaitForSeconds _identifyDelayWait;

        private Coroutine _identifyingCorutine;
        protected override void Awake()
        {
            base.Awake();

            _identifyDelayWait = new WaitForSeconds(_identifyDelay);

            if (_inventoryCloseEvent != null)
            {
                _inventoryCloseEvent.Subscribe(StopIdentifyStorage);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_inventoryCloseEvent != null)
            {
                _inventoryCloseEvent.Unsubscribe(StopIdentifyStorage);
            }
        }

        /// <summary>
        /// 바인딩과 아이템 식별을 시작합니다
        /// </summary>
        public void BindAndIdentify(StorageData data)
        {
            InitializeStorage(data);
            StartIdentifyStorage();
        }

        private void StartIdentifyStorage()
        {
            StopIdentifyStorage();
            _identifyingCorutine = StartCoroutine(IdentifyItemCoroutine());
        }

        private void StopIdentifyStorage()
        {
            if (_identifyingCorutine != null)
            {
                StopCoroutine(_identifyingCorutine);
                _identifyingCorutine = null;
            }
        }

        private IEnumerator IdentifyItemCoroutine()
        {
            StorageData storageData = GetStorageData();
            int itemCount = storageData.Items.Count;

            for (int i = 0; i < itemCount; ++i)
            {
                Item item = storageData.Items[i];

                if (item != null && !item.IsIdentified)
                {
                    yield return _identifyDelayWait;

                    if (item == null) continue;
                    item.IsIdentified = true;
                    _presenter.SetItem(item, i);
                }
            }

            _identifyingCorutine = null;
        }


    }

}