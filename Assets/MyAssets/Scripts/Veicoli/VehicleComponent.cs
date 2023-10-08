using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{

    public class VehicleComponent : LogicVehicleComponent
    {
        [Header("Component Stats")]
        [SerializeField] protected float mass = 1.0f;

        public float Mass => mass;

    }

}
