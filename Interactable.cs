using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.5f;
        public string interactableText;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        //virtual means that if another class inherits from this class it has the ability to override and change what the method does.  
        //Much like an interface I guess
        //call this function when the player interacts
        public virtual void Interact(PlayerManager pm) 
        {
            Debug.Log("picking up some shit");

        }

    }

}
