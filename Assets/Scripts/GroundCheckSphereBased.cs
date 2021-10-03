using UnityEngine;
using Zenject;

public class GroundCheckSphereBased : GroundCheckerBase
{
    [SerializeField] private LayerMask _ignoreLayers;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _checkRadius;

    private void Update()
    {
        Check();
    }

    protected override void Check()
    {
        WasGroundedLastframe = IsGrounded;
        IsGrounded = Physics.CheckSphere(transform.position + _offset, _checkRadius, ~_ignoreLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.red : Color.green;
        Gizmos.DrawSphere(transform.position + _offset, _checkRadius);
    }
}
