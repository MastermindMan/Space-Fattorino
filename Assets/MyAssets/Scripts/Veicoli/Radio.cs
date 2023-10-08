using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{

    public class Radio : MonoBehaviour//, IPowerReciever, IDataReader, IDataReader
    {
        private string sound;

        public float Data => 42094;

        public float RequestedPower => 10;

        public void DataInput(float data)
        {
            sound = data.ToString();
        }

        public float PowerInput(float power)
        {
            throw new System.NotImplementedException();
        }
    }

}