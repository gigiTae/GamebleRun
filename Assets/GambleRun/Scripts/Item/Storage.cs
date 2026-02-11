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
                Sprite icon = items[i] == null ? null : items[i].Icon;
                uint count = items[i] == null ? 0 : items[i].Stack;
                bool isIdentified = items[i] == null ? true : items[i].IsIdentified;
                SlotInit slotData = new(icon, count, i, isIdentified);
                _storageView.AddSlot(slotData);
            }
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

        public void SetItem(ItemData item, int itemIndex2)
        {
            Sprite icon = item == null ? null : item.Icon;
            uint count = item == null ? 0 : item.Stack;
            bool isIdentified = item == null ? true : item.IsIdentified;

            SlotInit initData = new(icon, count, itemIndex2, isIdentified);
            _storageView.RefreshSlot(itemIndex2, initData);
            _storageData.SetItem(item, itemIndex2);
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
