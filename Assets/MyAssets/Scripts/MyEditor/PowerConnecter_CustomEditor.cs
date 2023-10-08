using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vehicles;

#if UNITY_EDITOR
namespace MyEditor
{
    [CustomEditor(typeof(PowerPort))]
    [CanEditMultipleObjects]
    public class PowerConnecter_CustomEditor : Editor
    {
        private SerializedProperty propPowerTargetComp;
        private SerializedProperty propPowerSourceComp;
        private SerializedProperty propDataTargetComp;
        private SerializedProperty propDataSourceComp;

        private IPowerGiver PowerGiver => (Component)target as IPowerGiver;
        private IPowerReciever PowerReciever => (Component)target as IPowerReciever;
        private IDataGiver DataGiver => (Component)target as IDataGiver;
        private IDataReciever DataReciever => (Component)target as IDataReciever;
        private bool IsIPowerGiver => PowerGiver != null && propPowerTargetComp != null;
        private bool IsIPowerReciever => PowerReciever != null && propPowerSourceComp != null;
        private bool IsIDataGiver => DataGiver != null && propDataTargetComp != null;
        private bool IsIDataReciever => DataReciever != null && propDataSourceComp != null;

        private Component PowerTargetComp
        {
            get
            {
                return IsIPowerGiver ? ((IPowerGiver)(Component)target).PowerTargetComponent : null;
            }
            set
            {
                if (IsIPowerGiver)
                {
                    ((IPowerGiver)(Component)target).PowerTargetComponent = value;
                }
                else
                    Debug.LogError("Component is not IPowerGiver");
            }
        }
        private Component PowerSourceComp
        {
            get
            {
                return IsIPowerReciever ? ((IPowerReciever)(Component)target).PowerSourceComponent : null;
            }
            set
            {
                if (IsIPowerReciever)
                {
                    ((IPowerReciever)(Component)target).PowerSourceComponent = value;
                }
                else
                    Debug.LogError("Component is not IPowerReciever");
            }
        }
        private Component DataTargetComp
        {
            get
            {
                return IsIDataGiver ? ((IDataGiver)(Component)target).DataTargetComponent : null;
            }
            set
            {
                if (IsIDataGiver)
                {
                    ((IDataGiver)(Component)target).DataTargetComponent = value;
                }
                else
                    Debug.LogError("Component is not IDataGiver");
            }
        }
        private Component DataSourceComp
        {
            get
            {
                return IsIDataReciever ? ((IDataReciever)(Component)target).DataSourceComponent : null;
            }
            set
            {
                if (IsIDataReciever)
                {
                    ((IDataReciever)(Component)target).DataSourceComponent = value;
                }
                else
                    Debug.LogError("Component is not IDataReciever");
            }
        }

        private void OnEnable()
        {
            propPowerSourceComp = serializedObject.FindProperty("powerSourceComponent");
            propPowerTargetComp = serializedObject.FindProperty("powerTargetComponent");
            propDataSourceComp = serializedObject.FindProperty("dataSourceComponent");
            propDataTargetComp = serializedObject.FindProperty("dataTargetComponent");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("POWER COMPONENT FINDER");
            if (IsIPowerReciever)
                EditorGUILayout.PropertyField(propPowerSourceComp, new GUIContent("Power Source Component"), GUILayout.Height(20));
            if (IsIPowerGiver)
                EditorGUILayout.PropertyField(propPowerTargetComp, new GUIContent("Power Target Component"), GUILayout.Height(20));
            if (IsIDataReciever)
                EditorGUILayout.PropertyField(propDataSourceComp, new GUIContent("Data Source Component"), GUILayout.Height(20));
            if (IsIDataGiver)
                EditorGUILayout.PropertyField(propDataTargetComp, new GUIContent("Data Target Component"), GUILayout.Height(20));
            
            //if (GUILayout.Button("Update Connections"))
            //{
            PowerCheck();
            DataCheck();
            UpdateInfoLabel();
            //}


            serializedObject.ApplyModifiedProperties();
        }
        private void PowerCheck()
        {
            if (IsIPowerGiver)
            {
                if (PowerTargetComp && PowerTargetComp is not IPowerReciever)
                {
                    Debug.Log(">>>>> Changing PowerTargetComp of " + ((Component)target).name);
                    foreach (Component comp in PowerTargetComp.GetComponents<Component>())
                    {
                        if (comp is IPowerReciever)
                        {
                            propPowerTargetComp.objectReferenceValue = comp;
                            //((IPowerReciever)comp).PowerSourceComponent = (Component)target;
                            break;
                        }
                    }
                }
            }
            if (IsIPowerReciever)
            {
                if (PowerSourceComp && PowerSourceComp is not IPowerGiver)
                {
                    Debug.Log(">>>>> Changing PowerSourceComp of " + ((Component)target).name);
                    foreach (Component comp in PowerSourceComp.GetComponents<Component>())
                    {
                        if (comp is IPowerGiver)
                        {
                            propPowerSourceComp.objectReferenceValue = comp;
                            //((IPowerGiver)comp).PowerTargetComponent = (Component)target;
                            break;
                        }
                    }
                }
            }
        }
        private void DataCheck()
        {
            if (IsIDataGiver)
            {
                if (DataTargetComp && DataTargetComp is not IDataReciever)
                {
                    Debug.Log(">>>>> Changing DataTargetComp of " + ((Component)target).name);
                    foreach (Component comp in DataTargetComp.GetComponents<Component>())
                    {
                        if (comp is IDataReciever)
                        {
                            propDataTargetComp.objectReferenceValue = comp;
                            //((IDataReciever)comp).DataSourceComponent = (Component)target;
                            break;
                        }
                    }
                }
            }
            if (IsIDataReciever)
            {
                if (DataSourceComp && DataSourceComp is not IDataGiver)
                {
                    Debug.Log(">>>>> Changing DataSourceComp of " + ((Component)target).name);
                    foreach (Component comp in DataSourceComp.GetComponents<Component>())
                    {
                        if (comp is IDataGiver)
                        {
                            propDataSourceComp.objectReferenceValue = comp;
                            //((IDataGiver)comp).DataTargetComponent = (Component)target;
                            break;
                        }
                    }
                }
            }
        }

        private void UpdateInfoLabel()
        {
            Color defaultColor = GUI.contentColor;
            bool sourcePowerFound = PowerSourceComp && PowerSourceComp is IPowerGiver;
            bool targetPowerFound = PowerTargetComp && PowerTargetComp is IPowerReciever;
            bool sourceDataFound = DataSourceComp && DataSourceComp is IDataGiver;
            bool targetDataFound = DataTargetComp && DataTargetComp is IDataReciever;

            if (IsIPowerReciever)
            {
                GUI.contentColor = sourcePowerFound ? Color.green : Color.red;
                EditorGUILayout.LabelField(sourcePowerFound ? "PowerSource is valid." : "PowerSource not found.");
            }
            if (IsIPowerGiver)
            {
                GUI.contentColor = targetPowerFound ? Color.green : Color.red;
                EditorGUILayout.LabelField(targetPowerFound ? "PowerReciever is valid." : "PowerReciever not found.");
            }

            if (IsIDataReciever)
            {
                GUI.contentColor = sourceDataFound ? Color.green : Color.red;
                EditorGUILayout.LabelField(sourceDataFound ? "DataSource is valid." : "DataSource not found.");
            }
            if (IsIDataGiver)
            {
                GUI.contentColor = targetDataFound ? Color.green : Color.red;
                EditorGUILayout.LabelField(targetDataFound ? "DataReciever is valid." : "DataReciever not found.");
            }

            GUI.contentColor = defaultColor;
        }
        
    }

}
#endif