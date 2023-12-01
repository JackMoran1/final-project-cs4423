using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand
{
    private PlayerInput playerInput;

    public ShootCommand(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    public void Execute()
    {
        playerInput.Shoot();
    }
}
