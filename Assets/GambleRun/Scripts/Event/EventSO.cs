using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "Scriptable Objects/EventSO")]
public abstract class EventSO<T> : ScriptableObject
{
    private event Action<T> OnRaised;

    public void Raise(T data) => OnRaised?.Invoke(data);

    public void Subscribe(Action<T> listener) => OnRaised += listener;
    public void Unsubscribe(Action<T> listener) => OnRaised -= listener;

    // 플레이 모드 종료 시 초기화 (에디터 편의성)
    protected virtual void OnEnable() => OnRaised = null;
}
