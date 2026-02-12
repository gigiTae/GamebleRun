using System.Collections;
using UnityEngine;

namespace GambleRun
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

        public override void Bind(StorageData data)
        {
            base.Bind(data);
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
                    ItemData itemData = item.Data;
                    item.IsIdentified = true;

                    SlotInit initData = new(
                            itemData.Icon,
                            item.Quantity,
                            i,
                            item.IsIdentified
                        );

                    _view.RefreshSlot(i, initData);
                }
            }

            _identifyingCorutine = null;
        }


    }

}