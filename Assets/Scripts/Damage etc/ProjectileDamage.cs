using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage;
    public GameObject Player; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="whatIsPlayer")
        {
            Player.GetComponent<DataPlayer>().healthPlayer -= damage;
        }

        if(other.tag =="whatIsEnemy")
        {
            Debug.Log("Hit Enemy");
        }
    }
}
