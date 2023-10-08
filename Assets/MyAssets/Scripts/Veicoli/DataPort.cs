using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using Vehicles;


namespace Vehicles
{

    public class DataPort : MonoBehaviour, IDataReciever, IDataGiver
    {
        [SerializeField] private Component dataSourceComponent;
        [SerializeField] private Component dataTargetComponent;

        public Component DataSourceComponent { get => dataSourceComponent; set => dataSourceComponent = value; }
        public Component DataTargetComponent { get => dataTargetComponent; set => dataTargetComponent = value; }
        public IDataReciever DataReciever => dataTargetComponent as IDataReciever;



        public virtual void DataInput(float power, IDataGiver dataGiver)
        {
            DataOutput(power);
        }
        public virtual void DataOutput(float power)
        {
            if (DataReciever != null)
            {
                DataReciever.DataInput(power, this);
            }
        }


    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DataPort))]
    [CanEditMultipleObjects]
    public class DataPort_CustomEditor : MyEditor.PowerConnecter_CustomEditor
    {
    }
#endif

}