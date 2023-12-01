using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private PlayerInput playerInput;
    private Vector3 direction;

    public MoveCommand(PlayerInput playerInput, Vector3 direction)
    {
        this.playerInput = playerInput;
        this.direction = direction;
    }

    public void Execute()
    {
        playerInput.Move(direction);
    }
}
