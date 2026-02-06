using UnityEditor.SceneManagement;
using UnityEngine;


namespace GambleRun.Manager
{
    [CreateAssetMenu(fileName = "DragDropManager", menuName = "Manager/DragDropManager")]
    public class DragDropManager : ScriptableObject
    {

        private bool _isDragging = false;
        public bool IsDragging => _isDragging;

        private Storage _startStorage;
        private int _startItemIndex = -1;

        public void BeginDragDrop(Storage storage, int itemIndex)
        {
            _isDragging = true;
            _startItemIndex = itemIndex;
            _startStorage = storage;

            Debug.Log("DragDrop Start");
        }

        public void EndDragDrop(Storage endStorage, int endItemIndex)
        {
            if (!_isDragging)
            {
                return;
            }

            if(endStorage != _startStorage)
            {
                Debug.Log("Diff");
            }

            // 1. 같은 공간
            ItemData startItem = _startStorage.GetItemData(_startItemIndex);
            ItemData endItem = endStorage.GetItemData(endItemIndex);

            _startStorage.SetItem(endItem, _startItemIndex);
            endStorage.SetItem(startItem, endItemIndex);

            _isDragging = false;
            _startItemIndex = -1;
            _startStorage = null;
        }

    }
}

