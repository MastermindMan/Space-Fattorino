using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Vehicles
{

    public class OnOffPowerPort : PowerPort
    {
        /*[SerializeField] private bool isOn = true;

        public override float RequestedPower => Operation(base.RequestedPower);

        public void DataInput(float data)
        {
            isOn = MyMathStuff.IngameDataManagement.GetFloatFromType(data) >= 0.5f;
        }

        public override void OnDataChange(float data)
        {
            throw new System.NotImplementedException();
        }

        public override float PowerInput(float power, IPowerGiver powerOwner)
        {
            return base.PowerInput(Operation(power), powerOwner);
        }

        private float Operation(float power)
        {
            return isOn ? power : 0;
        }
        */
    }


}
