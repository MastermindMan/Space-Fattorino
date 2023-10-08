using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vehicles
{
    public class Engine : MonoBehaviour, IPowerGiver
    {
        [SerializeField] private float powerEmission = 100;
        [SerializeField] private Component powerTargetComponent;

        public Component PowerTargetComponent { get => powerTargetComponent; set => powerTargetComponent = value; }
        public float Power => powerEmission;


        //public IPowerIN Plug => (IPowerIN)plug;

        private void FixedUpdate()
        {
            PowerOutput(powerEmission);
        }

        float usedPower;
        public float PowerOutput(float power)
        {
            usedPower = ((IPowerReciever)PowerTargetComponent).PowerInput(power);
            //Debug.Log("ho usato " + power + " power.");
            return usedPower;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Engine))]
    [CanEditMultipleObjects]
    public class Engine_CustomEditor : MyEditor.PowerConnecter_CustomEditor
    {
    }
    #endif
}