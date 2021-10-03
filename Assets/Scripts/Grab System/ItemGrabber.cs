using UnityEngine;
using Zenject;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _movetowardsSpeed;
    [SerializeField] private float _distanceToRelease;

    [SerializeField] private Transform _tempParent;

    [Inject]
    private IRayCreator _rayCreator;

    private Pickupable _pickedObject;

    private bool _isGrabbed;

    private void OnEnable()
    {
        InputReader.IsGrabPressedAction += TryGrab;
    }

    private void OnDisable()
    {
        InputReader.IsGrabPressedAction -= TryGrab;
    }

    private void Update()
    {
        if (_pickedObject == null)
        {
            return;
        }

        var distance = Vector3.Distance(_tempParent.position, _pickedObject.transform.position);

        if (distance != 0)
        {
            PositionObject();
        }
        else if (distance > _distanceToRelease)
        {
            Release();
        }
    }

    private Pickupable GetPickupable()
    {
        Ray ray = _rayCreator.CreateRay();

        if (Physics.Raycast(ray, out var hit, _grabDistance))
        {
            Pickupable target = hit.transform.gameObject.GetComponent<Pickupable>();

            if (target != null)
            {
                return target;
            }
        }

        return null;
    }

    private void TryGrab()
    {
        if (!_isGrabbed)
        {
            var target = GetPickupable();
            
            if (target != null)
            {
                Grab(target);
                return;
            }
            
        }
        else
        {
            Throw();
        }
    }

    private void Grab(Pickupable target)
    {
        _pickedObject = target;

        _pickedObject.Rigidbody.useGravity = false;
        _pickedObject.Rigidbody.freezeRotation = true;
        _pickedObject.transform.SetParent(_tempParent);

        _isGrabbed = true;
    }

    private void Release()
    {
        _pickedObject.Rigidbody.useGravity = true;
        _pickedObject.Rigidbody.freezeRotation = false;
        _pickedObject.transform.parent = null;

        _isGrabbed = false;

        _pickedObject = null;
    }

    private void Throw()
    {
        _pickedObject.Rigidbody.AddForce(_tempParent.forward * _throwForce, ForceMode.Impulse);

        Release();    
    }

    private void PositionObject()
    {      
        _pickedObject.Rigidbody.velocity = Vector3.zero;
        _pickedObject.Rigidbody.angularVelocity = Vector3.zero;

        // could use lerp instead but I don't want to make special timer that will count from 0.0 to 1.0
        _pickedObject.transform.position = Vector3.MoveTowards(_pickedObject.transform.position, _tempParent.position, _movetowardsSpeed * Time.deltaTime);
    }

}