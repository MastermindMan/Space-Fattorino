using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace Vehicles.CpuAlgorithms
{
    [System.Serializable]
    public class VerticalSpeedRegolator_Hovercraft : CpuAlgorithm
    {
        [SerializeField] private HovercraftCPU hCPU;
        [SerializeField] private PidController pidController;
        //public VehicleCPU VehicleCPU => vehicleCPU;

        private static int NumberOfThrusters => HovercraftCPU.NUMBER_OF_THRUSTERS;
        private static float VehicleMass => HovercraftCPU.STANDARD_MASS;

        public override int RequiredInputs => 1;    //target vehicle speed.

        public override int RequiredOutputs => NumberOfThrusters;

        public VerticalSpeedRegolator_Hovercraft(VehicleCPU vehicleCPU)
        {
            hCPU = (HovercraftCPU)vehicleCPU;
            pidController = new PidController(16 * VehicleMass, 10 * VehicleMass, 0 * VehicleMass, 0, Mathf.Infinity);
        }

        //private float[] powerOutPut = new float[NumberOfThrusters];
        public override void Execute(float[] data, int[] dataIndexes, float[] outPut)
        {
            float requestedTotalForce = pidController.Seek(data[dataIndexes[0]], hCPU.Vehicle.MainRigidbody.velocity.y);
            for (int i = 0; i < HovercraftCPU.NUMBER_OF_THRUSTERS; i++)
            {
                float requestedThrusterForce = requestedTotalForce * hCPU.thrusterCenterOfMassMults[i];
                outPut[i] = requestedThrusterForce / hCPU.Thrusters[i].P2FRatio;
            }
        }

    }

}