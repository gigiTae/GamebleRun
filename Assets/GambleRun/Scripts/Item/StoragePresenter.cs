using UnityEngine;
using UnityEngine.UIElements;
using GambleRun.Manager;

namespace GambleRun
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

            BindPointerCallback();
        }
        private void BindPointerCallback()
        {
            _view.PointerDownEvent += OnPointerDown;
            _view.PointerUpEvent += OnPointerUp;
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
            if (item == null)
            {
                return new SlotInit(null, 0, index, true);
            }

            ItemData data = item.Data;

            return new SlotInit(data.Icon, item.Quantity, index, item.IsIdentified);
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
            Item data = _model.Items[slot.SlotIndex];

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

    }
}
