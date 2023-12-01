using System;
using UnityEngine;

[System.Serializable]
public class BuffOption
{
    public string description;
    public Action<Player> applyBuff;

    public BuffOption(string description, Action<Player> applyBuff)
    {
        this.description = description;
        this.applyBuff = applyBuff;
    }
}
