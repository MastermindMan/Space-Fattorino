using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{
    
    public interface IPowerReciever
    {
        public Component PowerSourceComponent { get; set; }

        public float RequestedPower { get; }
            
        public float PowerInput(float power);

    }

}