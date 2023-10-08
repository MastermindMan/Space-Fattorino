using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDebugStuff
{

    public static class InfoGetter
    {
        public static string GetParentingPathOfTransform(Transform transform)
        {
            string outPut = "<";
            Stack<Transform> stack = new Stack<Transform>();
            while (transform != null)
            {
                stack.Push(transform);
                transform = transform.parent;
            }
            while (stack.Count > 0)
            {
                outPut += "/" + stack.Pop().name;
            }
            return outPut + ">";
        }
    }

}