using MyPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Timeline;

namespace Vehicles
{

    public class PowerPort : MonoBehaviour, IPowerGiver, IPowerReciever
    {
        [SerializeField] private Component powerSourceComponent;
        [SerializeField] private Component powerTargetComponent;
        //[SerializeField] private PortType portType;

        public Component PowerSourceComponent { get => powerSourceComponent; set => powerSourceComponent = value; }
        public Component PowerTargetComponent { get => powerTargetComponent; set => powerTargetComponent = value; }
        public IPowerReciever PowerTarget => powerTargetComponent as IPowerReciever;
        //public PortType PortType => portType;

        private bool PowerRecieverExists => powerSourceComponent != null;
        public virtual float RequestedPower {
            get
            {
                return PowerTarget != null ? PowerTarget.RequestedPower : 0;
            }
        }


        public virtual float PowerInput(float power)
        {
            return PowerOutput(power);
        }
        public virtual float PowerOutput(float power)
        {
            return PowerTarget != null ? PowerTarget.PowerInput(power) : 0;
        }

    }

}