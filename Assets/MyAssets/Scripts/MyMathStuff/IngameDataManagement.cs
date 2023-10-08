using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMathStuff
{

    public static class IngameDataManagement
    {
       
        public static float GetFloatFromType<T>(T variable)
        {
            switch (variable)
            {
                case float f:
                    return f;
                case int i:
                    return i;
                case Vector2 v2:
                    return v2.magnitude;
                case Vector3 v3:
                    return v3.magnitude;
                default:
                    return variable.GetHashCode();
            }
        }

    }

}

