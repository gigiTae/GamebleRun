using UnityEngine;

namespace GambleRun.Manager
{
    [CreateAssetMenu(fileName = "DragDropManager", menuName = "Manager/DragDropManager")]
    public class DragDropManager : ScriptableObject
    {
        // 상태 변수
        private bool _isDragging = false;
        private Storage _startStorage;
        private int _startItemIndex = -1;
        public bool IsDragging => _isDragging;

        public void BeginDragDrop(Storage storage, int itemIndex)
        {
            if (storage == null || storage.GetItemData(itemIndex) == null) return;

            _isDragging = true;
            _startItemIndex = itemIndex;
            _startStorage = storage;
        }

        public void EndDragDrop(Storage endStorage, int endItemIndex)
        {
            if (!_isDragging || endStorage == null)
            {
                ResetState();
                return;
            }

            ItemData startItem = _startStorage.GetItemData(_startItemIndex);
            ItemData endItem = endStorage.GetItemData(endItemIndex);

            if (CanCombine(startItem, endItem, endStorage, endItemIndex))
            {
                CombineItems(startItem, endItem, endStorage, endItemIndex);
            }
            else
            {
                SwapItems(startItem, endItem, endStorage, endItemIndex);
            }

            ResetState();
        }

        private void CombineItems(ItemData start, ItemData end, Storage endStorage, int endIdx)
        {
            // 병합 로직: 시작 아이템 수량을 끝 아이템에 합치고 시작 슬롯 비우기
            end.Count += start.Count;
            _startStorage.SetItem(null, _startItemIndex);
            endStorage.SetItem(end, endIdx);
        }
        private void SwapItems(ItemData start, ItemData end, Storage endStorage, int endIdx)
        {
            _startStorage.SetItem(end, _startItemIndex);
            endStorage.SetItem(start, endIdx);
        }

        private bool CanCombine(ItemData start, ItemData end, Storage endStorage, int endIdx)
        {
            // 같은 아이템이고, 자기 자신으로의 드롭이 아닐 때 (같은 인벤토리 내 같은 슬롯 방지)
            return start != null && end != null &&
                   start.ItemName == end.ItemName &&
                   !(_startStorage == endStorage && _startItemIndex == endIdx);
        }
        private void ResetState()
        {
            _isDragging = false;
            _startStorage = null;
            _startItemIndex = -1;
        }
    }
}

