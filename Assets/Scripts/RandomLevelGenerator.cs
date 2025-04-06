using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject groundPref, grassPref, chestPref, bedrockPref;
    private int baseHight = 2,
        maxBlockCountY = 10,
        chunkSize = 16,
        perlinNoiseSenivity = 25,
        chunkCount = 4;
    private float seedX, seedY;

    private void CreateChunk(int chunkNumX, int chunkNumZ)
    {
        GameObject chunk = new GameObject();
        float chunkX = chunkNumX * chunkSize / 2;
        float chunkZ = chunkNumZ * chunkSize / 2;
        chunk.transform.position = new Vector3(chunkX, 0, chunkZ);
        chunk.name = "Chunk: " + chunkNumX + ", " + chunkNumZ;
        chunk.AddComponent<MeshFilter>();
        chunk.AddComponent<MeshRenderer>();
        chunk.AddComponent<Chunk>();

        for (int x = chunkNumX * chunkSize; x < chunkNumX * chunkSize + chunkSize; x++)
        {
            for (int z = chunkNumZ * chunkSize; z < chunkNumZ * chunkSize + chunkSize; z++)
            {
                float xSample = seedX + (float)x / perlinNoiseSenivity;
                float ySample = seedY + (float)z / perlinNoiseSenivity;
                float sample = Mathf.PerlinNoise(xSample, ySample);
                int height = baseHight + (int)(sample * maxBlockCountY);

                for (int y = 0; y < height; y++)
                {
                    GameObject temp;
                    if (y == height - 1)
                    {
                        temp = Instantiate(grassPref, new Vector3(x, y, z), Quaternion.identity);
                        CreateChest(x, height, z);
                    }
                    else if(y == 0)
                    {
                        temp = Instantiate(bedrockPref, new Vector3(x, y, z), Quaternion.identity);
                    }
                    else
                    {
                        temp = Instantiate(groundPref, new Vector3(x, y, z), Quaternion.identity);
                    }
                    temp.transform.SetParent(chunk.transform);
                }


            }
        }
        chunk.transform.SetParent(transform);
    }

    private void Start()
    {
        //seedX = Random.Range(0, 10);
        //seedY = Random.Range(0, 10);
        //for (int x = 0; x < chunkCount; x++)
        //{
        //    for (int z = 0; z < chunkCount; z++)
        //    {
        //        CreateChunk(x, z);
        //    }
        //}
    }

    [ContextMenu("CreateLevel")]
    public void CreateLevel()
    {
        seedX = Random.Range(0, 10);
        seedY = Random.Range(0, 10);
        for (int x = 0; x < chunkCount; x++)
        {
            for (int z = 0; z < chunkCount; z++)
            {
                CreateChunk(x, z);
            }
        }
    }

    public void CreateChest(float x, float y, float z)
    {
        int chance = Random.Range(0, 100);
        if(chance > 98)
        {
            GameObject temp = Instantiate(chestPref, new Vector3(x, y, z), Quaternion.identity);
            temp.transform.SetParent(transform);
        }
    }
}
