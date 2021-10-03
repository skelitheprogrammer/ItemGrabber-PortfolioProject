using UnityEngine;
using Zenject;

public class PlayerJumping : MonoBehaviour, IMovementModule
{
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _jumpTimeout = .3f;

    private float _jumpTimeoutDelta;

    private float _currentJumpHeight;
    private bool _isJumping;

    [Inject] private GravityBase _gravity;
    [Inject] private GroundCheckerBase _groundCheck;
    [Inject] private ModuleHandlerBase<IMovementModule> _moduleHandler;

    public Vector3 Value { get; private set; }

    private void OnEnable()
    {
        InputReader.IsJumpPressedAction += Jump;
        _moduleHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        InputReader.IsJumpPressedAction -= Jump;
        _moduleHandler.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        JumpCheck();
    }

    private void Jump()
    {
        if (!_groundCheck.IsGrounded) return;

        var jumpHeight = Mathf.Sqrt(-2 * _jumpHeight * _gravity.Gravity);

        if (_jumpTimeoutDelta <= 0)
        {
            _currentJumpHeight = jumpHeight;
            _isJumping = true;
        }
    }

    private void JumpCheck()
    {
        if (_groundCheck.IsGrounded)
        {
            if (_currentJumpHeight > 0 && !_isJumping)
            {
                _currentJumpHeight = 0;
            }

            if (_jumpTimeoutDelta > 0)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }

            _isJumping = false;
        }
        else
        {
            if (_jumpTimeoutDelta != _jumpTimeout)
            {
                _jumpTimeoutDelta = _jumpTimeout;
            }
        }

        Value = Vector3.up * _currentJumpHeight;
    }
}
