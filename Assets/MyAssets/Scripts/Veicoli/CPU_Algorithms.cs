using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPhysics;

namespace Vehicles.CpuAlgorithms
{

    public static class CPU_Algorithms
    {
        public static CpuAlgorithm GetCpuAlgorithmByIndex(VehicleCPU callingCpu, int algorithmIndex)
        {
            switch (algorithmIndex)
            {
                default:
                case 0:
                    return new NoOperation();
                case 1:
                    return new VerticalSpeedRegolator_Hovercraft(callingCpu);
            }
        }

    }

}