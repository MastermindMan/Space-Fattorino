using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InclinationControlTest : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform[] thrusters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float totalForceNeeded = rb.mass * Physics.gravity.magnitude;

        for (int i = 0; i < thrusters.Length; i++)
        {
            float forceNeeded = totalForceNeeded / thrusters.Length;
            rb.AddForceAtPosition(Vector3.up * forceNeeded, thrusters[i].position);
        }

    }
}
