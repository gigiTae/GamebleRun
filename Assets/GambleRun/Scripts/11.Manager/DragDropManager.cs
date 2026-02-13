using UnityEngine;

using GambleRun.Items;
using GambleRun.Storages;

namespace GambleRun.Manager
{
    [CreateAssetMenu(fileName = "DragDropManager", menuName = "Manager/DragDropManager")]
    public class DragDropManager : ScriptableObject
    {
        // 상태 변수
        private bool _isDragging = false;
        private StoragePresenter _startPresenter;
        private int _startItemIndex = -1;
        public bool IsDragging => _isDragging;
        private void OnEnable()
        {
            ResetState();
        }

        public void BeginDragDrop(StoragePresenter presenter, int itemIndex)
        {
            if (presenter == null || presenter.GetItem(itemIndex) == null) return;

            _isDragging = true;
            _startItemIndex = itemIndex;
            _startPresenter = presenter;
        }

        public void EndDragDrop(StoragePresenter endPresenter, int endItemIndex)
        {
            if (!_isDragging || endPresenter == null)
            {
                ResetState();
                return;
            }

            Item startItem = _startPresenter.GetItem(_startItemIndex);
            Item endItem = endPresenter.GetItem(endItemIndex);

            if (CanCombine(startItem, endItem, endPresenter, endItemIndex))
            {
                CombineItems(startItem, endItem, endPresenter, endItemIndex);
            }
            else
            {
                SwapItems(startItem, endItem, endPresenter, endItemIndex);
            }

            ResetState();
        }

        private void CombineItems(Item start, Item end, StoragePresenter endPresenter, int endIdx)
        {
            // 병합 로직: 시작 아이템 수량을 끝 아이템에 합치고 시작 슬롯 비우기
            end.Quantity += start.Quantity;
            _startPresenter.SetItem(null, _startItemIndex);
            endPresenter.SetItem(end, endIdx);
        }
        private void SwapItems(Item start, Item end, StoragePresenter endPresenter, int endIdx)
        {
            _startPresenter.SetItem(end, _startItemIndex);
            endPresenter.SetItem(start, endIdx);
        }

        private bool CanCombine(Item start, Item end, StoragePresenter endPresenter, int endIdx)
        {
            // TODO : MaxStack 로직 추가

            ItemData stratData = start.Data;
            ItemData endData = end?.Data;

            // 같은 아이템이고, 자기 자신으로의 드롭이 아닐 때 (같은 인벤토리 내 같은 슬롯 방지)
            return Item.IsValid(start) && Item.IsValid(end) &&
                   stratData.ItemName == endData.ItemName &&
                   !(_startPresenter == endPresenter && _startItemIndex == endIdx)
                   && stratData.IsStackable && endData.IsStackable;
        }

        private void ResetState()
        {
            _isDragging = false;
            _startPresenter = null;
            _startItemIndex = -1;
        }
    }
}

