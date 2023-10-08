using Planets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private Planet currentPlanet;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected bool gravityEnabled = true;

    public Rigidbody Rigidbody => rb;

    private void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        if (!rb.IsSleeping() && gravityEnabled)
            rb.AddForce(currentPlanet.GravityVectorAtPoint(transform.position), ForceMode.Acceleration);
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        enabled = true;
    }
    public void DisablePhysics()
    {
        rb.isKinematic = true;
        enabled = false;
    }

}
