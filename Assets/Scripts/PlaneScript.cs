using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    [SerializeField] GameObject cube;
    IEnumerator Spawn()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    GameObject temp = Instantiate(cube, new Vector3(
                        transform.position.x + k,
                        transform.position.y + i,
                        transform.position.z + j
                        ), Quaternion.identity);
                    temp.GetComponent<MeshRenderer>().material.color = new Color(
                        (float)k / 10,
                        (float)i / 10,
                        (float)j / 10
                        );

                    temp.transform.SetParent(transform);

                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }
    }

    private void Start()
    {
        SpawnMorePhyramids();
    }

    void SpawnPhypamid(Vector3 pos)
    {

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0 + i; j < 10 - i; j++)
            {
                for (int k = 0 + i; k < 10 - i; k++)
                {
                    GameObject temp = Instantiate(cube, new Vector3(
                        pos.x + k,
                        pos.y + i,
                        pos.z + j
                        ), Quaternion.identity);
                    temp.GetComponent<MeshRenderer>().material.color = new Color(
                        (float)k / 10,
                        (float)i / 10,
                        (float)j / 10
                        );
                }
            }
        }
    }

    void SpawnMorePhyramids()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                SpawnPhypamid(new Vector3(10f * i, 0f, 10f * j));
            }
        }
    }
}
