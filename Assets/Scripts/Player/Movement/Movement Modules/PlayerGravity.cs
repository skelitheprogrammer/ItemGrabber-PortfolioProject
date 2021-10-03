using UnityEngine;
using Zenject;

public class PlayerGravity : GravityBase, IMovementModule
{
    [SerializeField] private float _gravity = -20f;
    public override float Gravity => _gravity;

    [SerializeField] private float _groundGravity = -7;

    [SerializeField] private float _maxFallSpeed = -30f;

    private float _currentFallSpeed;

    [Inject] private ModuleHandlerBase<IMovementModule> _moduleHandler;
    [Inject] private GroundCheckerBase _groundCheck;

    public Vector3 Value { get; private set; }

    private void OnEnable()
    {
        _moduleHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        ApplyGravity();
    }

    protected override void ApplyGravity()
    {
        bool isGrounded = _groundCheck.IsGrounded;
        bool wasGrounded = _groundCheck.WasGroundedLastframe;

        if (isGrounded)
        {
            _currentFallSpeed = _groundGravity;
        } 
        else if (wasGrounded)
        {
            _currentFallSpeed = 0;
        }
        else
        {
            _currentFallSpeed = Mathf.MoveTowards(_currentFallSpeed, -_maxFallSpeed, _gravity * Time.deltaTime);
        }

        Value = Vector3.up * _currentFallSpeed;
    }

}
