using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _origin;

    [Space(5f)]
    [SerializeField] private float _topClamp = 80f;
    [SerializeField] private float _bottomClamp = -80f;
    
    [Space(5f)]
    [SerializeField] private float _sensitivityX = 1f;
    [SerializeField] private float _sensitivityY = 1f;

    public Vector2 _rotation;

    private void LateUpdate()
    {
        CameraRotate();
    }

    public void CameraRotate()
    {   
        _rotation.y += InputReader.RotateInput.x * _sensitivityX;
        _rotation.x += InputReader.RotateInput.y * _sensitivityY;

        _rotation.x = Mathf.Clamp(_rotation.x, _bottomClamp, _topClamp);

        _origin.rotation = Quaternion.Euler(-_rotation.x, _rotation.y, 0);
    }
}
