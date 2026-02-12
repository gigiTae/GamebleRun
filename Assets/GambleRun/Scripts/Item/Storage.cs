using GambleRun;
using GambleRun.Manager;
using GambleRun.Persistence;
using UnityEngine;

namespace GambleRun
{
    public class Storage : MonoBehaviour, IBind<StorageData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();

        [SerializeField] protected StorageView _view;
        [SerializeField] private DragDropManager _dragDropManager;
        private StorageModel _model;
        private StoragePresenter _presenter;

        protected virtual void Awake()
        {
            _model = new StorageModel();
            _presenter = new StoragePresenter(_model, _view, _dragDropManager);
        }

        protected virtual void OnDestroy() { }
        public void Bind(StorageData data)
        {
            if (data.Items.Count == 0)
            {
                for (int i = 0; i < 10; ++i)
                {
                    data.Items.Add(null);
                }
            }

            data.Id = Id;
            _presenter.BindStorageData(data);
        }


        public void SetVisible(bool isVisible)
        {
            _view.SetVisible(isVisible);
        }

        public StorageData GetStorageData()
        {
            return _model.Data;
        }
    }

}