using MyPhysics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vehicles.CpuAlgorithms;

namespace Vehicles
{
    public class VehicleCPU : VehicleComponent, IPowerReciever, IDataReciever, IDataGiver
    {
        [SerializeField] private float requiredPower;
        [SerializeField] protected CPU_DataPortInfo[] dataPortsInfo;
        [SerializeField] private DataPort[] dataSource;
        [SerializeField] private DataPort[] dataTargets;
        [SerializeField] private Component powerSourceComponent;
        //[HideInInspector][SerializeField] public Component powerTargetComponent;

        private float[] data;

        //private bool powered = true;

        public float RequestedPower => requiredPower;
        public float[] Data => data;
        public DataPort[] DataTargets => dataTargets;
        public CPU_DataPortInfo[] DataPorts => dataPortsInfo;

        public Component PowerSourceComponent { get => powerSourceComponent; set => powerSourceComponent = value; }

        public Component DataSourceComponent { get => null; set { } }
        public Component DataTargetComponent { get => null; set { } }

        protected virtual void Start()
        {
            data = new float[dataSource.Length];
            InitializeAlgorithms();
        }

        private void InitializeAlgorithms()
        {
            foreach (var dataPort in dataPortsInfo)
                dataPort.Initialize(this);
        }
        private void ExecutePorts()
        {
            for (int i = 0; i < DataPorts.Length; i++)
            {
                if (dataPortsInfo[i].HasDataTargets)
                {
                    dataPortsInfo[i].ExecuteDataProcessing();
                }
            }
        }

        public void SetPortDataAlgorithmIndex(int portIndex, int algIndex)
        {
            dataPortsInfo[portIndex].SetCpuAlgorithmByIndex(this, algIndex);
        }

        public float PowerInput(float power)
        {
            if (power >= requiredPower)
            {
                ExecutePorts();
                return requiredPower;
            }
            return power;
        }
        public float PowerOutput(float power)
        {
            //qui non è usato.
            return power;
        }

        public void DataInput(float data, IDataGiver dataOwner)
        {
            for (int i = 0; i < dataSource.Length; i++)
            {
                if (dataSource[i] == (DataPort)dataOwner)
                {
                    this.data[i] = data;
                    return;
                }
            }
        }

        public void DataOutput(float data)
        {
            //non usato.
            throw new NotImplementedException();
        }
    }

    [System.Serializable]
    public class CPU_DataPortInfo
    {
        private VehicleCPU parentCPU;
        //[SerializeField] private float[] data;
        [SerializeField] private int[] dataSourceIndexes;
        [SerializeField] private int[] dataTargetIndexes;
        [SerializeField] private int cpuAlgorithmIndex;
        private CpuAlgorithm cpuAlgorithm;
        [SerializeField] private float[] outPuts;

        private float[] Data => parentCPU.Data;
        public int CpuAlgorithmIndex => cpuAlgorithmIndex;
        public bool HasDataTargets => dataTargetIndexes.Length > 0;

        public void Initialize(VehicleCPU cpu)
        {
            parentCPU = cpu;
            SetCpuAlgorithmByIndex(parentCPU, cpuAlgorithmIndex);
            //data = new float[cpuAlgorithm.RequiredInputs];
            //dataTargetIndexes = new int[cpuAlgorithm.RequiredOutputs];
            outPuts = new float[cpuAlgorithm.RequiredOutputs];
        }
        public void SetDataSource(int[] dataSourceIndexes)
        {
            this.dataSourceIndexes = dataSourceIndexes;
        }
        public void SetDataTargets(int[] dataTargetIndexes)
        {
            this.dataTargetIndexes = dataTargetIndexes;
            outPuts = new float[dataTargetIndexes.Length];
        }
        public void SetCpuAlgorithmByIndex(VehicleCPU callingCpu, int cpuAlgorithmIndex)
        {
            this.cpuAlgorithmIndex = cpuAlgorithmIndex;
            this.cpuAlgorithm = CPU_Algorithms.GetCpuAlgorithmByIndex(callingCpu, cpuAlgorithmIndex);
        }
        public void ExecuteDataProcessing()
        {
            cpuAlgorithm.Execute(Data, dataSourceIndexes, outPuts);
            for (int i = 0; i < dataTargetIndexes.Length; i++)
            {
                //Debug.Log("Giving " + outPuts[i] + " to " + parentCPU.DataTargets[dataTargetIndexes[i]].name);
                parentCPU.DataTargets[dataTargetIndexes[i]].DataInput(outPuts[i], parentCPU);
            }
        }

    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(VehicleCPU))]
    [CanEditMultipleObjects]
    public class VehicleCPU_CustomEditor : MyEditor.PowerConnecter_CustomEditor
    {
    }
    #endif


}
