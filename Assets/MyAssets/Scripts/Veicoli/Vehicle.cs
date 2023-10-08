using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planets;
using MyPhysics;
using UnityEditor;

namespace Vehicles
{

    public class Vehicle : MonoBehaviour
    {
        [SerializeField] private Rigidbody mainRigidbody;
        [SerializeField] private Transform drivingSpot;
        [SerializeField] private Planet currentPlanet;
        private Transform currentPilot;
        
        public Rigidbody MainRigidbody => mainRigidbody;
        private float GravityAccelleration => currentPlanet.Gravity;
        private float GravityForce => GravityAccelleration * mainRigidbody.mass;
        public Vector3 GravityDirection => currentPlanet.GravityDirectionAtPoint(transform.position);
        public Vector3 GravityVector => currentPlanet.GravityVectorAtPoint(transform.position);

        private void Start()
        {
            Initialization();
        }
        public Vector3 lastSpeed, lastAcc;
        public Vector3 acc;

        private void FixedUpdate()
        {
            ApplyGravity();
            
            acc = (mainRigidbody.velocity - lastSpeed) / Time.fixedDeltaTime;

            //DebuggingPid(true);

            lastSpeed = mainRigidbody.velocity;
            lastAcc = acc;

            Debug.Log(mainRigidbody.centerOfMass);
        }



        private void Initialization()
        {
            InitializeVeichleStats();
        }

        private void InitializeVeichleStats()
        {
            foreach (VehicleComponent vehicleComponent in GetComponentsInChildren<VehicleComponent>())
            {
                mainRigidbody.mass += vehicleComponent.Mass;
            }
        }

        private void ApplyGravity()
        {
            mainRigidbody.AddForce(Vector3.up * GravityAccelleration, ForceMode.Acceleration);
        }
        public Vector3 CalculateNextFrameVelocity(Vector3 force) //funziona solo se non ci sono interventi esterni (ovvero altre forze oltre a quella specificata)
        {
            Vector3 speedIncrement = (force / MainRigidbody.mass) / Time.fixedDeltaTime;
            return mainRigidbody.velocity + speedIncrement;
        }

        public void DriveVehicle(Transform pilot)
        {
            currentPilot = pilot;
            SetOnDrivingPosition(pilot);
        }
        public void StopDrivingVehicle()
        {
            ExitFromDrivingPosition(currentPilot);
            currentPilot = null;
        }
        public void SetOnDrivingPosition(Transform pilot)
        {
            pilot.SetParent(drivingSpot);
            pilot.localPosition = Vector3.zero;
        }
        public void ExitFromDrivingPosition(Transform pilot)
        {
            pilot.SetParent(null);
            pilot.position = transform.position + Vector3.left * 3;
        }

        private void MoveController(Vector2 direction)
        {
            mainRigidbody.AddForce(transform.TransformDirection(new Vector3(direction.x, 0, direction.y)));
        }

        public void SubscribeToEvents()
        {
            EventsManager.MovementDirection.AddListener(MoveController);
        }
        public void UnSubscribeToEvents()
        {
            EventsManager.MovementDirection.RemoveListener(MoveController);
        }

        /*public PidController pid = new PidController();
        private void DebuggingPid(bool applyForce)
        {
            Debug.DrawRay(transform.position, GravityDirection * Mathf.Sign(pid.Seek(mainRigidbody.velocity.y, 0)), Color.red, Time.fixedDeltaTime);
            float force = pid.Seek(mainRigidbody.velocity.y, 0);
            Debug.Log(">>> DEBUG PID THINKS WE NEED " + force + " force.");
            if(applyForce)
                mainRigidbody.AddForce(GravityDirection * force);
        }*/
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Vehicle))]
    class Vehicle_Inspector : Editor
    {
        private GameObject gameobjectOfTarget;

        private void Awake()
        {
            gameobjectOfTarget = ((Component)target).gameObject;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Add/Remove Twiddle Component"))
            {
                VehiclePidTwiddler tw = gameobjectOfTarget.GetComponent<VehiclePidTwiddler>();
                if (tw)
                    DestroyImmediate(tw);
                else
                    gameobjectOfTarget.gameObject.AddComponent<VehiclePidTwiddler>();
            }
        }
    }
    #endif


}
