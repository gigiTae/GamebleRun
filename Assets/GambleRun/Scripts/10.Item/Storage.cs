using GambleRun.Manager;
using UnityEngine;
using GambleRun.UI;

namespace GambleRun
{
    public class Storage : MonoBehaviour
    {
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
        public void InitializeStorage(StorageData data)
        {
            if (data.Items.Count == 0)
            {
                for (int i = 0; i < 10; ++i)
                {
                    data.Items.Add(null);
                }
            }

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