using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour, IMovementModule
{
    [Header("Movement settings")]
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _acceleration = 40f;
    [SerializeField] private float _groundMoveForce = .3f;
    [SerializeField] private float _airStrafeForce = .7f;

    private float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;
    public float TargetSpeed { get; private set; }

    private Transform _camera;
    private Vector3 _smoothVelocity;

    private Vector3 _velocity;

    private Vector3 _movementDirection;
    private Vector3 _airDirection;


    [Inject] private ModuleHandlerBase<IMovementModule> _moduleHandler;
    [Inject] private GroundCheckerBase _groundCheck;

    public Vector3 Value { get; private set; }

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

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
        Move();
    }

    private void Move()
    {
        var inputDirection = new Vector3(InputReader.MoveInput.x, 0, InputReader.MoveInput.y);

        var finalSmooth = _groundCheck.IsGrounded ? _groundMoveForce : _airStrafeForce;

        TargetSpeed = _moveSpeed * inputDirection.magnitude;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, TargetSpeed, _acceleration * Time.deltaTime);

        if (_groundCheck.IsGrounded)
        {
            _movementDirection = Quaternion.Euler(0, _camera.eulerAngles.y, 0) * inputDirection;

            if (_airDirection != Vector3.zero)
            {
                _airDirection = Vector3.zero;
            }
        }
        else
        {
            _airDirection += Quaternion.Euler(0, _camera.eulerAngles.y, 0) * inputDirection;
        }

        var finalVelocity = (_movementDirection + _airDirection).normalized * _currentSpeed;
        finalVelocity.y = 0;

        _velocity = Vector3.SmoothDamp(_velocity, finalVelocity, ref _smoothVelocity, finalSmooth);

        Value = _velocity;
    }
}
