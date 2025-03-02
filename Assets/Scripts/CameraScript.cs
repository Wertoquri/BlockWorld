using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject prevObj;
    [SerializeField] GameObject cube;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (prevObj == null)
            {
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                prevObj = hit.transform.gameObject;
            }
            else if (!prevObj.Equals(hit.transform.gameObject))
            {
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                prevObj.GetComponent<MeshRenderer>().material.color = Color.white;
                prevObj = hit.transform.gameObject;
            }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    GameObject.Instantiate(cube, new Vector3(
            //        hit.transform.position.x,
            //        hit.transform.position.y + 1,
            //        hit.transform.position.z
            //        ), Quaternion.identity );
            //}
            //if (Input.GetMouseButtonDown(1))
            //{
            //    GameObject.Destroy(hit.transform.gameObject);
            //}
        }
        else
        {
            if(prevObj != null)
            prevObj.GetComponent<MeshRenderer>().material.color = Color.white;
            prevObj = null;
        }
    }
}
