using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Vehicles;
using static UnityEngine.GraphicsBuffer;

#if UNITY_EDITOR

namespace MyEditor
{
    public static class VehiclesUtil
    {
        /*[MenuItem("Utility/Vehicles/Set Power Connections")]
        public static void SetCorrectPowerComponents()
        {
            MonoBehaviour[] monos = Object.FindObjectsOfType<MonoBehaviour>();
            foreach (MonoBehaviour mono in monos)
            {
                List<Object> editedSerializedObjects = new List<Object> ();
                SetCorrectPowerComponents_IPowerReciever(mono, editedSerializedObjects);
                SetCorrectPowerComponents_IPowerGiver(mono, editedSerializedObjects);
                SetCorrectDataComponents_IDataReciever(mono, editedSerializedObjects);
                SetCorrectDataComponents_IDataGiver(mono, editedSerializedObjects);
                var so = new SerializedObject(editedSerializedObjects.ToArray());
                so.FindProperty("m_LocalPosition").vector3Value = Vector3.zero;
                so.ApplyModifiedProperties();
            }
        }
        private static void SetCorrectPowerComponents_IPowerReciever(MonoBehaviour mono, List<Object> editedSerializedObjects) 
        {
            IPowerReciever powerReciever = mono as IPowerReciever;
            if (powerReciever == null)
                return;
            if (powerReciever.PowerSourceComponent)
            {
                powerReciever.PowerSourceComponent = (Component)powerReciever.PowerSourceComponent.gameObject.GetComponent<IPowerGiver>();
                AddToEditedSerializedObjectList(powerReciever.PowerSourceComponent, editedSerializedObjects);
                if (powerReciever.PowerSourceComponent)
                {
                    ((IPowerGiver)powerReciever.PowerSourceComponent).PowerTargetComponent = mono;
                    AddToEditedSerializedObjectList(mono, editedSerializedObjects);
                }
            }
            else
                powerReciever.PowerSourceComponent = null;
        }
        private static void SetCorrectPowerComponents_IPowerGiver(MonoBehaviour mono, List<Object> editedSerializedObjects)
        {
            IPowerGiver powerGiver = mono as IPowerGiver;
            if (powerGiver == null)
                return;
            if (powerGiver.PowerTargetComponent)
            {
                powerGiver.PowerTargetComponent = (Component)powerGiver.PowerTargetComponent.gameObject.GetComponent<IPowerReciever>();
                AddToEditedSerializedObjectList(powerGiver.PowerTargetComponent, editedSerializedObjects);
                if (powerGiver.PowerTargetComponent)
                {
                    ((IPowerReciever)powerGiver.PowerTargetComponent).PowerSourceComponent = mono;
                    AddToEditedSerializedObjectList(mono, editedSerializedObjects);
                }
            }
            else
                powerGiver.PowerTargetComponent = null;
        }
        private static void SetCorrectDataComponents_IDataReciever(MonoBehaviour mono, List<Object> editedSerializedObjects)
        {
            IDataReciever dataReciever = mono as IDataReciever;
            if (dataReciever == null)
                return;
            if (dataReciever.DataSourceComponent)
            {
                dataReciever.DataSourceComponent = (Component)dataReciever.DataSourceComponent.gameObject.GetComponent<IDataGiver>();
                AddToEditedSerializedObjectList(dataReciever.DataSourceComponent, editedSerializedObjects);
                if (dataReciever.DataSourceComponent)
                {
                    ((IDataGiver)dataReciever.DataSourceComponent).DataTargetComponent = mono;
                    AddToEditedSerializedObjectList(mono, editedSerializedObjects);
                }
            }
            else
                dataReciever.DataSourceComponent = null;
        }
        private static void SetCorrectDataComponents_IDataGiver(MonoBehaviour mono, List<Object> editedSerializedObjects)
        {
            IDataGiver dataGiver = mono as IDataGiver;
            if (dataGiver == null)
                return;
            if (dataGiver.DataTargetComponent)
            {
                dataGiver.DataTargetComponent = (Component)dataGiver.DataTargetComponent.gameObject.GetComponent<IDataReciever>();
                AddToEditedSerializedObjectList(dataGiver.DataTargetComponent, editedSerializedObjects);
                if (dataGiver.DataTargetComponent)
                {
                    ((IDataReciever)dataGiver.DataTargetComponent).DataSourceComponent = mono;
                    AddToEditedSerializedObjectList(mono, editedSerializedObjects);
                }
            }
            else
                dataGiver.DataTargetComponent = null;
        }
        private static void AddToEditedSerializedObjectList(Object obj, List<Object> editedSerializedObjects)
        {
            if (!editedSerializedObjects.Contains(obj))
                editedSerializedObjects.Add(obj);
        }
        */
    }

}

#endif