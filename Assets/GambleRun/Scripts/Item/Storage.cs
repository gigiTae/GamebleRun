using UnityEngine;
using UnityEngine.UIElements;
using GambleRun.Manager;

namespace GambleRun
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private string _parentVisualElement;
        [SerializeField] private DragDropManager _dragDropManager;
        [SerializeField] private StorageData _testData;
        [SerializeField] protected StorageView _storageView;

        protected StorageData _storageData;
        public StorageData Data => _storageData;
        protected virtual void Awake()
        {
            if (_testData != null)
            {
                _storageData = _testData.Clone();
            }

            BindPointerCallback();
        }

        private void Start()
        {
            SetupStorageView();
        }

        protected virtual void OnDestroy() { }

        /// <summary>
        /// 스토리지를 새로운 데이터로 갱신합니다 
        /// </summary>
        public virtual void RefreshStorage(StorageData storageData)
        {
            if (storageData != null && _storageView != null)
            {
                _storageData = storageData;
                SetupStorageView();
            }
        }

        private void SetupStorageView()
        {
            _storageView.ClearContainer();

            var items = _storageData.Items;

            for (int i = 0; i < items.Count; ++i)
            {
                SlotInit slotInit = CreateSlotInit(items[i], i);
                _storageView.AddSlot(slotInit);
            }
        }

        private SlotInit CreateSlotInit(ItemData item, int index)
        {
            if (item == null)
            {
                return new SlotInit(null, 0, index, true);
            }

            return new SlotInit(item.Icon, item.Stack, index, item.IsIdentified);
        }

        private void BindPointerCallback()
        {
            _storageView.PointerDownEvent += OnPointerDown;
            _storageView.PointerUpEvent += OnPointerUp;
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            // evt.target : 실제로 이벤트를 발생시킨 가장 깊은 곳의 자식 요소.
            // evt.currentTarget: 이벤트를 처리하고 있는 현재 요소.
            if (evt.target is Slot clickedSlot && CanBeginDrag(clickedSlot))
            {
                _dragDropManager.BeginDragDrop(this, clickedSlot.SlotIndex);
            }
        }

        protected virtual bool CanBeginDrag(Slot slot)
        {
            ItemData data = _storageData.Items[slot.SlotIndex];

            if (data == null || !data.IsIdentified)
            {
                return false;
            }

            return true;
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            if (evt.target is Slot dropSlot)
            {
                _dragDropManager.EndDragDrop(this, dropSlot.SlotIndex);
            }
        }

        public void SetItem(ItemData item, int index)
        {
            SlotInit initData = CreateSlotInit(item, index);
            _storageView.RefreshSlot(index, initData);
            _storageData.SetItem(item, index);
        }

        public ItemData GetItemData(int index)
        {
            return _storageData.Items[index];
        }

        public void SetVisible(bool isVisible)
        {
            _storageView.SetVisible(isVisible);
        }

    }
}
