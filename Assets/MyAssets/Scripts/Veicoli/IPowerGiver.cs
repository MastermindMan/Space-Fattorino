using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{
    public interface IPowerGiver
    {
        public Component PowerTargetComponent { get; set; }
        //public IPowerReciever PowerTarget { get { return (IPowerReciever)PowerTargetComponent; } set { PowerTargetComponent = (Component)value; } }

        public float PowerOutput(float power);
    }

}
