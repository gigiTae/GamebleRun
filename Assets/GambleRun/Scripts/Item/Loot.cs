using System.Dynamic;
using UnityEngine;
using System.Collections.Generic;

namespace GambleRun
{
    public class Loot : MonoBehaviour, IInteractable
    {
        [SerializeField] private LootEvent _lootOpenEvent;
        [SerializeField] private LootTableData _table;

        private StorageData _data;
        private InteractionButtonView _interactionView;

        void Awake()
        {
            _interactionView = GetComponent<InteractionButtonView>();
            _data = ScriptableObject.CreateInstance<StorageData>();

            Setup();
        }

        public void Interact()
        {
            _lootOpenEvent.Raise(_data);
        }

        public bool IsInteractable()
        {
            return true;
        }

        public void OnEnterFocus()
        {
            _interactionView.Show(true);
        }

        public void OnExitFocus()
        {
            _interactionView.Show(false);
        }

        private void Setup()
        {
            if (_table != null)
            {
                // Slot
                _data.ResetStorage(_table.SlotCount);

                Debug.Log(_table.SlotCount);

                // FixedItem
                if (_table.FixedItems.Count > 0)
                {
                    foreach (var item in _table.FixedItems)
                    {
                        _data.AddItem(item.Clone());
                    }
                }

                // 아이템 스폰
                int emptySpace = _data.EmptySpaceCount;
                Debug.Log(emptySpace);

                for (int i = 0; i < emptySpace; ++i)
                {
                    _data.AddItem(_table.GetRandomCloneItem());
                }
            }
            else
            {
                Debug.Log($"LootTabl is null {gameObject.name}");
            }
        }
    }
}