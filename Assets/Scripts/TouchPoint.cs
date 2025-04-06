using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPoint : MonoBehaviour
{
    private AIPathFinder pf;

    private void Awake(){
        pf = GetComponentInParent<AIPathFinder>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            pf.Explosion();
        }else if (other.gameObject.CompareTag("Block")){
            pf.Jump();
        }
    }
}
