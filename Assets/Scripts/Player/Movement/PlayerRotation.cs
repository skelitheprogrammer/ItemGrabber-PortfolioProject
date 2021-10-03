using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _origin;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(0.0f, _origin.eulerAngles.y, 0.0f);
    }
}
