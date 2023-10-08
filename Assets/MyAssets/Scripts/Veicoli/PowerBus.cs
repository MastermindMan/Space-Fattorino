using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vehicles
{

    public class PowerBus : MonoBehaviour, IPowerReciever, IPowerGiver
    {
        [SerializeField] private SubPowerBus[] subPowerBus;
        [SerializeField] private Component powerSourceComponent;
        //[HideInInspector][SerializeField] public Component powerTargetComponent;

        public float RequestedPower => GetTotalRequestedPower();

        public Component PowerSourceComponent { get => powerSourceComponent; set => powerSourceComponent = value; }
        public Component PowerTargetComponent { get => null; set { } }

        private float GetTotalRequestedPower()
        {
            float total = 0;
            for (int i = 0; i < subPowerBus.Length; i++)
                for (int j = 0; j < subPowerBus[i].powerPorts.Length; j++)
                    total += subPowerBus[i].powerPorts[j].RequestedPower;
            return total;
        }
        private (int, float)[] orderedPowerRequests;

        public float PowerInput(float power)
        {
            return PowerOutput(power);
        }
        public float PowerOutput(float power)
        {

            return DivideAndSendPower(power);
        }
        private float DivideAndSendPower(float power)
        {
            float powerLeft = power;
            for (int i = 0; i < subPowerBus.Length; i++)
            {
                OrderSubBusPowerRequests(i);
                for (int j = 0; j < orderedPowerRequests.Length; j++)
                {
                    powerLeft -= subPowerBus[i].powerPorts[orderedPowerRequests[j].Item1].PowerInput(powerLeft / (orderedPowerRequests.Length - j));
                }
            }
            return power - powerLeft;
        }
        private void OrderSubBusPowerRequests(int busIndex)
        {
            orderedPowerRequests = new (int, float)[subPowerBus[busIndex].powerPorts.Length];
            for (int i = 0; i < orderedPowerRequests.Length; i++)
            {
                //Debug.Log("checking " + MyDebugStuff.InfoGetter.GetParentingPathOfTransform(subPowerBus[busIndex].powerPorts[i].transform));
                orderedPowerRequests[i] = (i, subPowerBus[busIndex].powerPorts[i].RequestedPower);
            }
            MyMathStuff.Sorting.HeapSort(orderedPowerRequests);
        }

    }

    [System.Serializable]
    public struct SubPowerBus
    {
        public PowerPort[] powerPorts;
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(PowerBus))]
    [CanEditMultipleObjects]
    public class PowerBus_CustomEditor : MyEditor.PowerConnecter_CustomEditor
    {
    }
    #endif

}
