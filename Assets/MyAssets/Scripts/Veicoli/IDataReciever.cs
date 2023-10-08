using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Vehicles
{

    public interface IDataReciever
    {
        public Component DataSourceComponent { get; set; }

        public void DataInput(float data, IDataGiver dataOwner);

    }

}
