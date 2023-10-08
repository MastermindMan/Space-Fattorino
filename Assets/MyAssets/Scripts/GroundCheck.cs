using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckBox(transform.position, transform.localScale / 2, transform.rotation, Layers.Walkable);
        //cast_debug.DrawBox(transform.position, transform.localScale / 2, transform.rotation, Color.red);
    }
}
