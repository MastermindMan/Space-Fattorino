using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vehicles
{
    public class HovercraftCPU : VehicleCPU
    {
        public const int NUMBER_OF_THRUSTERS = 4;
        public const float STANDARD_MASS = 430;

        [SerializeField] private Thruster[] thrusters;

        public Thruster[] Thrusters => thrusters;
        public float[] thrusterCenterOfMassMults = new float[NUMBER_OF_THRUSTERS];

         
        protected override void Start()
        {
            base.Start();
            float maxX = 0, maxZ = 0;
            //vehicle.MainRigidbody.automaticCenterOfMass = false;
            //vehicle.MainRigidbody.centerOfMass = Vector3.zero;
            for (int i = 0; i < NUMBER_OF_THRUSTERS; i++)
            {
                for (int j = i + 1; j < NUMBER_OF_THRUSTERS; j++)
                {
                    maxX = Mathf.Max(maxX, Mathf.Abs(thrusters[i].transform.localPosition.x - thrusters[j].transform.localPosition.x));
                    maxZ = Mathf.Max(maxZ, Mathf.Abs(thrusters[i].transform.localPosition.z - thrusters[j].transform.localPosition.z));
                }
            }
            for(int i = 0; i < NUMBER_OF_THRUSTERS; i++)
            {
                float distPercentX = Mathf.Abs(thrusters[i].transform.localPosition.x - vehicle.MainRigidbody.centerOfMass.x) / maxX;
                float distPercentZ = Mathf.Abs(thrusters[i].transform.localPosition.z - vehicle.MainRigidbody.centerOfMass.z) / maxZ;
                    
                thrusterCenterOfMassMults[i] = (1 - distPercentX) * (1 - distPercentZ);
            }
        }

    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(HovercraftCPU))]
    [CanEditMultipleObjects]
    public class HovercraftCPU_CustomEditor : VehicleCPU_CustomEditor
    {
    }
    #endif

}
