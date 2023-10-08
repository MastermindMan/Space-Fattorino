using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{

    public class GiroscopeRotator : LogicVehicleComponent
    {
        [Header("Giroscope Stats")]
        [SerializeField] private float maxRotation = 30;
        //[SerializeField] private float rotatingSpeed = 30;
        [Header("Giroscope Targets")]
        [SerializeField] private List<VehicleComponent> targets = new List<VehicleComponent>();

        private void FixedUpdate()
        {
            foreach (VehicleComponent vComponent in targets)
            {
                //Quaternion resultingQuaternion = Quaternion.RotateTowards(vComponent.transform.rotation, Quaternion.Euler(-Vehicle.GravityDirection), rotatingSpeed * Time.deltaTime);
                Quaternion resultingQuaternion = Quaternion.Euler(-Vehicle.GravityDirection);
                Quaternion resultingLocalQuaternion = resultingQuaternion * Quaternion.Inverse(vComponent.transform.parent.rotation);

                vComponent.transform.localRotation = MyMathStuff.Quaternions.ClampRotation(resultingLocalQuaternion, new Vector3(maxRotation, 0, maxRotation));
            }
        }



    }

}
