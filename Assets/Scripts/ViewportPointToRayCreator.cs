using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ViewportPointToRayCreator : IRayCreator
{
    private readonly Camera _camera;

    [Inject]
    public ViewportPointToRayCreator(Camera camera)
    {
        _camera = camera;
    }

    public Ray CreateRay()
    {
        var centerScreenPosition = new Vector3(.5f, .5f, 0);

        return _camera.ViewportPointToRay(centerScreenPosition);
    }
}

public class MouseViewportPointToRayCreator : IRayCreator
{
    [Inject]
    private Camera _camera;

    public Ray CreateRay()
    {
        Debug.Log("1");
        var mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        return _camera.ViewportPointToRay(mousePosition);
    }
}
