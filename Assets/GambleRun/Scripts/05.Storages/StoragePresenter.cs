using UnityEngine;
using UnityEngine.UIElements;
using GambleRun.Manager;
using GambleRun.UI;
using GambleRun.Items;

namespace GambleRun.Storages
{
    public class StoragePresenter
    {
        protected readonly StorageModel _model;
        private readonly StorageView _view;
        private readonly DragDropManager _dragDropManager;

        public StoragePresenter(StorageModel model, StorageView view, DragDropManager dragDrop)
        {
            _model = model;
            _view = view;
            _dragDropManager = dragDrop;
        }

        public void BindPointerCallback()
        {
            _view.PointerDownEvent += OnPointerDown;
            _view.PointerUpEvent += OnPointerUp;
            _view.RootPointerUpEvent += OnRootPointerUp;
        }

        public void ReleasePointerCallback()
        {
            _view.PointerDownEvent -= OnPointerDown;
            _view.PointerUpEvent -= OnPointerUp;
            _view.RootPointerUpEvent -= OnRootPointerUp;
        }

        public void BindStorageData(StorageData data)
        {
            _model.Bind(data);
            SetupStorageView();
        }

        private void SetupStorageView()
        {
            _view.ClearContainer();

            var items = _model.Items;

            for (int i = 0; i < items.Count; ++i)
            {
                SlotInit slotInit = CreateSlotInit(items[i], i);
                _view.AddSlot(slotInit);
            }
        }

        private SlotInit CreateSlotInit(Item item, int index)
        {
            ItemData data = item?.Data;

            if (!Item.IsValid(item))
            {
                return new SlotInit(null,
                    0,
                    index,
                    string.Empty,
                    string.Empty,
                    ItemRarity.None,
                    true);
            }

            return new SlotInit(data.Icon, item.Quantity, index,
                data.ItemName, data.Description, data.Rarity, item.IsIdentified);
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            // 드래그 드랍 시작
            // evt.target : 실제로 이벤트를 발생시킨 가장 깊은 곳의 자식 요소.
            // evt.currentTarget: 이벤트를 처리하고 있는 현재 요소.
            if (evt.target is Slot clickedSlot && CanBeginDrag(clickedSlot))
            {
                _dragDropManager.RequestBeginDragDrop(this, clickedSlot.SlotIndex, clickedSlot);
            }
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            // 드래그 드랍 성공
            if (evt.target is Slot dropSlot && _dragDropManager.IsDragging)
            {
                _dragDropManager.RequestEndDragDrop(this, dropSlot.SlotIndex);
                evt.StopPropagation();
            }
        }

        private void OnRootPointerUp(PointerUpEvent evt)
        {
            // 드래그 드랍 중지
            if (_dragDropManager.IsDragging)
            {
                _dragDropManager.RequestEndDragDrop(null, 0);
            }
        }

        protected virtual bool CanBeginDrag(Slot slot)
        {
            Item data = _model.Items[slot.SlotIndex];

            if (data == null || !data.IsIdentified)
            {
                return false;
            }

            return true;
        }


        public void SetItem(Item item, int index)
        {
            SlotInit initData = CreateSlotInit(item, index);
            _view.RefreshSlot(index, initData);
            _model.SetItem(item, index);
        }

        public Item GetItem(int index)
        {
            return _model.Items[index];
        }

        public void SetVisible(bool isVisible)
        {
            _view.SetVisible(isVisible);
        }

        public Sprite GetItemIcon(int itemIndex)
        {
            var items = _model?.Data?.Items;
            if (items == null) return null;

            if (itemIndex < 0 || itemIndex >= items.Count) return null;

            return items[itemIndex]?.Data?.Icon;
        }

    }
}
