using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpy : Holdable
{

    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

}
