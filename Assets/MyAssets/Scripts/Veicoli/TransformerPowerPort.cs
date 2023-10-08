using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;


namespace Vehicles
{
    public class TransformerPowerPort : PowerPort
    {
        [SerializeField] protected float powerCap = 100;

        public override float RequestedPower => Operation(base.RequestedPower);

        public override float PowerOutput(float power)
        {
            return base.PowerOutput(Operation(power));
        }
        private float Operation(float power)
        {
            return Mathf.Min(power, powerCap);
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(TransformerPowerPort))]
    [CanEditMultipleObjects]
    public class TransformerPowerPort_CustomEditor : MyEditor.PowerConnecter_CustomEditor
    {
    }
    #endif

}