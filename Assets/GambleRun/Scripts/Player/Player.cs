using GambleRun.Persistence;
using UnityEngine;
using UnityEngine.InputSystem.XR;


namespace GambleRun
{
    public class PlayerData : ISaveable
    {
        [field: SerializeField] public SerializableGuid Id { get; set; }
    }


    public class Player : MonoBehaviour , IBind<PlayerData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();

        private PlayerData _playerData;

        public void Bind(PlayerData data)
        {
            _playerData = data;
            data.Id = Id;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}