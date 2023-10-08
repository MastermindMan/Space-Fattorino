using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Vehicles.CpuAlgorithms
{
    public class NoOperation : CpuAlgorithm
    {
        public override int RequiredInputs => 1;

        public override int RequiredOutputs => 1;

        public override void Execute(float[] data, int[] dataIndexes, float[] outPut)
        {
            outPut[0] = data[0];
        }

    }

}
