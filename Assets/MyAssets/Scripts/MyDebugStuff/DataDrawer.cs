using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDebugStuff
{

    public class DataDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private const int MAX_DATA = 100;
        private const float LENGHT = 16;
        private const float HEIGHT = 9;

        private int index = 0;
        private float minY = 99999, maxY = -99999;
        private float YDiff => maxY - minY;

        private void Start()
        {
            lineRenderer.positionCount = MAX_DATA;
        }
        public void AddPoint(Vector2 point)
        {
            if (point.y < minY)
            {
                UpdateVertical(maxY - point.y);
                minY = point.y;
            }
            if (point.y > maxY)
            {
                UpdateVertical(point.y - minY);
                maxY = point.y;
            }

            lineRenderer.SetPosition(index, GetPlacement(index, point));

            if (index == MAX_DATA - 1)
                index = 0;
            else
                index++;
        }
        private Vector3 GetPlacement(int index, Vector2 point)
        {
            return new Vector3(
                (float)index / (MAX_DATA - 1) * LENGHT,
                point.y * HEIGHT / YDiff,
                0);
        }
        private void UpdateVertical(float newYDiff)
        {
            float ratio = newYDiff / YDiff;
            for (int i = 0; i < index; i++)
            {
                Vector3 pos = lineRenderer.GetPosition(index);
                pos.y *= ratio;
                lineRenderer.SetPosition(i, pos);
            }
        }

    }

}