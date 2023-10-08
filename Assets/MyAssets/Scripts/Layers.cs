using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    private static Layers Instance { get; set; }

    //layers to be set in inspector
    [SerializeField] private LayerMask interactables;
    [SerializeField] private LayerMask nonColliding;
    [SerializeField] private LayerMask walkable;

    //properties
    public static LayerMask Interactables => Instance.interactables;
    public static LayerMask NonColliding => Instance.nonColliding;
    public static LayerMask Walkable => Instance.walkable;

    public static int LayerMaskToInt(LayerMask layerMask)
    {
        for (int i = 0; i < 32; i++)
        {
            if (layerMask == (1 << i))
                return i;
        }
        return -1;
    }

    private void Awake()
    {
        SetSingleton();
    }
    private void SetSingleton()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

}
