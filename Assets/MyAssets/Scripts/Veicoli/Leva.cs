using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Vehicles
{

    public class Leva : MonoBehaviour, IDataGiver
    {
        [Range(-1,1)][SerializeField] private float apertura;
        [SerializeField] private float mult = 10;
        [SerializeField] private Component dataTargetComponent;

        public IDataReciever DataReciever => dataTargetComponent as IDataReciever;
        public float Data => apertura * mult;

        public Component DataTargetComponent { get => dataTargetComponent; set => dataTargetComponent = value; }

        public void SetApertura(float apertura)
        {
            this.apertura = apertura;
            OnValueChange();
        }
        public void SetMult(float val)
        {
            this.mult = val;
            OnValueChange();
        }
        public void OnValueChange()
        {
            DataOutput(Data);
        }
        public void DataOutput(float data)
        {
            DataReciever.DataInput(data, this);
        }

    }

  
    #if UNITY_EDITOR
    [CustomEditor(typeof(Leva))]
    [CanEditMultipleObjects]
    public class Leva_CustomEditor : Editor
    {
        private Leva Leva => (Leva)target;
        private float lastValue;

        private void Awake()
        {
            lastValue = Leva.Data;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (lastValue != Leva.Data)
            {
                lastValue = Leva.Data;
                Leva.OnValueChange();
            }
        }
    }
    #endif

}