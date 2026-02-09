using System.Dynamic;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace GambleRun
{
    /// <summary>
    /// 전리품 클래스 
    /// </summary>
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
                List<ItemData> items = _table.GetRandomCloneItems();
                _data.UpdateItems(items);
                _data.SetAllItemsIdentification(false);
            }
            else
            {
                Debug.Log($"LootTabl is null {gameObject.name}");
            }
        }

    }
}