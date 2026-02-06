using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun
{
    public class Storage : MonoBehaviour
    {
        public string StorageParentName;

        [SerializeField] private StorageData testData;

        private UIDocument _uiDocument;
        private StorageView _storageView;
        private StorageData _storageData;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _storageView = new StorageView();

            VisualElement StorageView = _uiDocument.rootVisualElement.Q(StorageParentName);

            if (StorageView != null)
            {
                StorageView.Add(_storageView);
            }
            else
            {
                return;
            }

            if (testData != null)
            {
                _storageData = testData;
            }

            RefreshView();
        }

        public void RefreshView()
        {
            if (testData != null && _storageView != null)
            {
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
                SlotViewInit slotData = new(icon, count);
                _storageView.AddSlot(slotData);
            }

        }
    }
}
