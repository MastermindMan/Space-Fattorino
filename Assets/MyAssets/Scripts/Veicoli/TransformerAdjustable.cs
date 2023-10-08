using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vehicles
{

    public class TransformerAdjustable : TransformerPowerPort, IDataReciever
    {
        [SerializeField] private float maxPowerCap = 200;
        [SerializeField] private Component dataSourceComponent;

        public override float RequestedPower => base.RequestedPower;

        public Component DataSourceComponent { get => dataSourceComponent; set => dataSourceComponent = value; }

        public void DataInput(float data, IDataGiver dataOwner)
        {
            SetPowerCap(data);
        }

        public void SetPowerCap(float newValue)
        {
            powerCap = Mathf.Min(newValue, maxPowerCap);
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TransformerAdjustable))]
    [CanEditMultipleObjects]
    public class TransformerAdjustable_CustomEditor : TransformerPowerPort_CustomEditor
    {
    }
    #endif

}
