using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStateMachine;
using Vehicles;
using Planets;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerStateMachineHolder playerStateMachineHolder;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private PlayerInventory playerInventory;
    [Header("Bho")]
    [SerializeField] private Planet currentPlanet;

    public static CameraControl CameraControl => Instance.cameraControl;
    public static PlayerMovement PlayerMovement => Instance.playerMovement;
    public static PlayerStateMachineHolder PlayerStateMachineHolder => Instance.playerStateMachineHolder;
    public static PlayerInteraction PlayerInteraction => Instance.playerInteraction;
    public static PlayerInventory PlayerInventory => Instance.playerInventory;
    public static Planet CurrentPlanet => Instance.currentPlanet;
    public static Vector3 FowardDirection => Instance.cameraControl.transform.forward;


    private void Awake()
    {
        InitializeSingleton();
    }
    private void FixedUpdate()
    {
        RotateAccordingToPlanet();
    }

    private void InitializeSingleton()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

    private void RotateAccordingToPlanet()
    {
        transform.rotation = MyMathStuff.Quaternions.AroundSphereTransformRotation(-currentPlanet.GravityDirectionAtPoint(transform.position), transform);
    }

    public void DriveVehicle(Vehicle vehicle)
    {
        vehicle.DriveVehicle(transform);
        playerStateMachineHolder.ChangeState(PlayerStatesEnum.driving, vehicle);
    }
    public void StopDrivingVehicle(Vehicle vehicle)
    {
        vehicle.StopDrivingVehicle();
        playerStateMachineHolder.ChangeState(PlayerStatesEnum.standard);
    }

}
