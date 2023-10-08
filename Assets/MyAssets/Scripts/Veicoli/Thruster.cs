using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vehicles
{

    public class Thruster : VehicleComponent, IPowerReciever
    {
        [Header("My Components")]
        [SerializeField] private Rigidbody myRb;
        [SerializeField] private Vector3 forcePointOffset;
        [SerializeField] private float powerToForceRatio = 100;
        [SerializeField] private float maxCapacityPowerConsumption = 100;
        [SerializeField] public Component powerSourceComponent;

        public float RequestedPower => float.MaxValue;
        public float P2FRatio => powerToForceRatio;

        public Component PowerSourceComponent { get => powerSourceComponent; set => powerSourceComponent = value; }

        public float PowerInput(float power)
        {
            ApplyTargetForce(power * powerToForceRatio);
            return power;
        }

        public void ApplyTargetForce(float force)
        {
            Debug.DrawRay(transform.position, transform.up * Mathf.Sign(force), Color.red, Time.deltaTime);
            Vehicle.MainRigidbody.AddForceAtPosition(transform.up * force, transform.position + forcePointOffset);
        }

    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Thruster))]
    [CanEditMultipleObjects]
    public class Thruster_CustomEditor : MyEditor.PowerConnecter_CustomEditor
    {
    }
    #endif

}
