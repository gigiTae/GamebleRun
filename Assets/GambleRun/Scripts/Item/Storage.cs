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

        private UIDocument _uiDocument;
        private StorageView _storageView;
        private StorageData _storageData;

        public StorageData Data => _storageData;

        private void Awake()
        {
            _uiDocument = GetComponentInParent<UIDocument>();

            MakeView();

            // TestData
            if (_testData != null)
            {
                _storageData = _testData.Clone();
            }

            BindPointerCallback();
            RefreshStorage(_testData);
        }


        /// <summary>
        /// 스토리지를 새로운 데이터로 갱신합니다 
        /// </summary>
        public void RefreshStorage(StorageData storageData)
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
                uint count = items[i] == null ? 0 : items[i].Count;
                SlotViewInit slotData = new(icon, count, i);
                _storageView.AddSlot(slotData);
            }
        }

        private void BindPointerCallback()
        {
            _storageView.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _storageView.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            // evt.target : 실제로 이벤트를 발생시킨 가장 깊은 곳의 자식 요소.
            // evt.currentTarget: 이벤트를 처리하고 있는 현재 요소.
            if (evt.target is SlotView clickedSlot)
            {
                _dragDropManager.BeginDragDrop(this, clickedSlot.SlotIndex);
            }
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            if (evt.target is SlotView dropSlot)
            {
                _dragDropManager.EndDragDrop(this, dropSlot.SlotIndex);
            }
        }

        public void SetItem(ItemData item, int itemIndex2)
        {
            Sprite icon = item == null ? null : item.Icon;
            uint count = item == null ? 0 : item.Count;
            SlotViewInit initData = new(icon, count, itemIndex2);
            _storageView.RefreshSlot(itemIndex2, initData);
            _storageData.SetItem(item, itemIndex2);
        }

        public ItemData GetItemData(int index)
        {
            return _storageData.Items[index];
        }

        private void MakeView()
        {
            _storageView = new StorageView();
            VisualElement parentView = _uiDocument.rootVisualElement.Q(_parentVisualElement);

            if (parentView != null)
            {
                parentView.Add(_storageView);
            }
        }

        public void SetVisible(bool isVisible)
        {
            VisualElement parentView = _uiDocument.rootVisualElement.Q(_parentVisualElement);
            parentView.style.visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
