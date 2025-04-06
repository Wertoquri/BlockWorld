using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetting : MonoBehaviour
{
    [SerializeField] int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
            GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }

    }

    private void OnDestroy(){
        Destroy(transform.parent.gameObject);
    }
}
