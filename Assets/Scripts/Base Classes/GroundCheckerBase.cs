using UnityEngine;

public abstract class GroundCheckerBase : MonoBehaviour
{
    public bool IsGrounded { get; protected set; }
    public bool WasGroundedLastframe { get; protected set; }
    protected abstract void Check();
}