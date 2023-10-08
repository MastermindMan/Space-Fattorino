using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories.Equippables
{

    public class Gun : Equippable
    {
        public override void OnAction1()
        {
            Debug.Log("Baang!");
        }

        public override void OnAction2()
        {
            Debug.Log("PistolAction2!");
        }
    }

}
