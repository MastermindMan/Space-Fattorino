using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{

    public interface IDataGiver
    {
        public Component DataTargetComponent { get; set; }

        public void DataOutput(float data);

    }

}