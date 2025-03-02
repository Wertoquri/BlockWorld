using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private const int VISIBILITY_DISTANCE = 30;
    private Transform playerT;
    private bool isVisible;
    private Vector3 chankPos;

    private void Start()
    {
        playerT = GameObject.Find("Player").transform;
        chankPos = transform.position;
        isVisible = true;
    }
    
    private void SetActivity(bool isActive)
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
        isVisible = isActive;
    }

    private void Update()
    {
        //float distance = Vector3.Distance(chankPos, new Vector3(playerT.transform.position.x,
        //    0f,
        //    playerT.transform.position.z));
        //if (distance > VISIBILITY_DISTANCE && isVisible)
        //{
        //    SetActivity(false);
        //}
        //if (distance < VISIBILITY_DISTANCE && !isVisible)
        //{
        //    SetActivity(true);
        //}
    }

}
