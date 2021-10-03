using UnityEngine;

public abstract class ModuleHandlerBase<T> : MonoBehaviour, IObserver<T>
{
    public abstract void Subscribe(T sub);
    public abstract void Unsubscribe(T sub);
    protected abstract void UpdateModules();
}
