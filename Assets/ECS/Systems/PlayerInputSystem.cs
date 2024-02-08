using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerInputSystem : SystemBase
{
    private PlayerActions inputActions;

    protected override void OnCreate()
    {
        inputActions = new PlayerActions();
        RequireForUpdate<PlayerTag>();
    }

    protected override void OnStartRunning()
    {
        inputActions.Enable();
        inputActions.Simple.Move.started += Handle_MoveStarted;
        inputActions.Simple.Move.performed += Handle_MovePerformed;
        inputActions.Simple.Move.canceled += Handle_MoveCancelled;
    }

    protected override void OnStopRunning()
    {
        inputActions.Simple.Move.started -= Handle_MoveStarted;
        inputActions.Simple.Move.performed -= Handle_MovePerformed;
        inputActions.Simple.Move.canceled -= Handle_MoveCancelled;
        inputActions.Disable();
    }

    protected override void OnUpdate()
    {
        return;
    }

    private void Handle_MoveStarted(InputAction.CallbackContext context)
    {
        foreach ((RefRO<PlayerTag> tag, Entity e) in SystemAPI.Query<RefRO<PlayerTag>>().WithDisabled<InputMoveComponent>().WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<InputMoveComponent>(e, true);
            }
    }

    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        foreach(RefRW<InputMoveComponent> inputMoveComp in 
            SystemAPI.Query<RefRW<InputMoveComponent>>().WithAll<PlayerTag>())
        {
            inputMoveComp.ValueRW.value = context.ReadValue<Vector2>();
        }
    }

    private void Handle_MoveCancelled(InputAction.CallbackContext context)
    {
        foreach(EnabledRefRW<InputMoveComponent> inputMoveComp in SystemAPI.Query<EnabledRefRW<InputMoveComponent>>().WithAll<PlayerTag>())
        {
            inputMoveComp.ValueRW = false;
        }
    }
}
