using UnityEngine;

public abstract class GravityBase : MonoBehaviour
{
    public abstract float Gravity { get; }

    protected abstract void ApplyGravity();
}