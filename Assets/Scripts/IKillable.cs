using System;
using UnityEngine;

public interface IKillable
{
    public event Action<Vector3> OnKilled;
}
