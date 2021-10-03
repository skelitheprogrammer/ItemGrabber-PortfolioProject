using System.Collections.Generic;
using UnityEngine;

public class MovementModuleHandler : ModuleHandlerBase<IMovementModule>
{
    [SerializeField] private CharacterController _controller;

    private Vector3 _movement;

    private readonly HashSet<IMovementModule> _modifiers = new HashSet<IMovementModule>();

    private void Update()
    {
        UpdateModules();
    }

    protected override void UpdateModules()
    {
        _movement = Vector3.zero;

        foreach (var item in _modifiers)
        {
            item.OnUpdateModule();
            _movement += item.Value;
        }

        _controller.Move(_movement * Time.deltaTime);
    }

    public override void Subscribe(IMovementModule sub)
    {
        _modifiers.Add(sub);

    }

    public override void Unsubscribe(IMovementModule sub)
    {
        _modifiers.Remove(sub);
    }
}
