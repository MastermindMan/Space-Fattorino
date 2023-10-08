using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{

    public class Planet : MonoBehaviour
    {
        [Header("Planet Stats")]
        [SerializeField] private Vector3 planetCenter;
        [SerializeField] private float gravity;
        [SerializeField] private bool activateDynamicGravity = true;
        [Header("Mesh Generation")]
        [SerializeField][Range(2, 256)] private int resolution = 10;
        [SerializeField] private Shader shader;

        [SerializeField, HideInInspector]
        MeshFilter[] meshFilters;
        TerrainFace[] terrainFaces;

        public float Gravity => gravity;

        private void OnValidate()
        {
            InitializeMeshes();
            GenerateMeshes();
        }

        void InitializeMeshes()
        {
            if (meshFilters == null || meshFilters.Length == 0)
            {
                meshFilters = new MeshFilter[6];
            }
            terrainFaces = new TerrainFace[6];

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

            for (int i = 0; i < 6; i++)
            {
                if (meshFilters[i] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.SetParent(transform, false);

                    meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(shader);
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }

                terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
            }
        }

        void GenerateMeshes()
        {
            foreach (TerrainFace face in terrainFaces)
            {
                face.ConstructMesh();
            }
        }

        public Vector3 GravityDirectionAtPoint(Vector3 point)
        {
            if (activateDynamicGravity)
                return (planetCenter - point).normalized;
            return Vector3.down;
        }
        public Vector3 GravityVectorAtPoint(Vector3 point)
        {
            return GravityDirectionAtPoint(point) * -gravity;
        }

    }

}