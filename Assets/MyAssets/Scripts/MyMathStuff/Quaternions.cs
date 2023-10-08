using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMathStuff
{

    public static class Quaternions
    {
        //"My" per modo di dire

        public static Quaternion ClampRotation(Quaternion q, Vector3 bounds)
        {
            return ClampRotation(q, bounds.x, bounds.y, bounds.z);
        }
        public static Quaternion ClampRotation(Quaternion q, float x, float y, float z)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, -x, x);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
            angleY = Mathf.Clamp(angleY, -y, y);
            q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

            float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
            angleZ = Mathf.Clamp(angleZ, -z, z);
            q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

            return q.normalized;
        }

        public static Quaternion AroundSphereTransformRotation(Vector3 normalAtTransformPosition, Transform transform)
        {
            var cross = Vector3.Cross(normalAtTransformPosition, transform.forward);
            var newForward = Vector3.Cross(cross, normalAtTransformPosition);
            return Quaternion.LookRotation(newForward, normalAtTransformPosition);
        }

    }

}
