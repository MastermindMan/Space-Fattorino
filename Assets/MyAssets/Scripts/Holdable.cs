using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planets;

public class Holdable : PhysicsObject
{
    private bool isHeld;

    public bool IsHeld => isHeld;

    public void Hold()
    {
        Player.PlayerInteraction.Hold(this);
    }

    public virtual void OnHoldingStart()
    {
        DisablePhysics();
        isHeld = true;
    }
    public virtual void OnHoldingEnd()
    {
        isHeld = false;
        EnablePhysics();
    }


}
