using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{
    public abstract class CpuAlgorithm
    {
        //public VehicleCPU VehicleCPU { get; }

        public abstract int RequiredInputs { get; }
        public abstract int RequiredOutputs { get; }

        public abstract void Execute(float[] data, int[] dataIndexes, float[] outPut);

    }

}