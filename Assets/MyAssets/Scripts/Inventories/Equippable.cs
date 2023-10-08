using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{

    public abstract class Equippable : Holdable
    {
        //[Header("Equippable Data")]
        //[SerializeField] private Vector3 equipPositionOffset;
        //[SerializeField] private Vector3 equipRotationOffset;

        public abstract void OnAction1();
        public abstract void OnAction2();

        public virtual void OnPickUp()
        {
            gravityEnabled = false;
            Rigidbody.isKinematic = true;
            gameObject.layer = Layers.NonColliding;
            //transform.localPosition = equipPositionOffset;
            //transform.localRotation = Quaternion.Euler(equipRotationOffset);
        }
        public virtual void OnDrop()
        {
            gravityEnabled = true;
            Rigidbody.isKinematic = false;
            gameObject.layer = Layers.LayerMaskToInt(Layers.Interactables);
            Rigidbody.AddForce(new Vector3(0.5f, 0.2f, 1f), ForceMode.Impulse);
        }

    }

}
