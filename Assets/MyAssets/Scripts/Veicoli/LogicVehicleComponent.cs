using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{

    public class LogicVehicleComponent : MonoBehaviour
    {
        [Header("Vehicle Component Logic")]
        [SerializeField] protected Vehicle vehicle;

        public Vehicle Vehicle => vehicle;

    }

}