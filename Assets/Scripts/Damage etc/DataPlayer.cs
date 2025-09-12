using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPlayer : MonoBehaviour
{
    public int healthPlayer;

    public Slider visualHealth;


    private void Update()
    {

        visualHealth.GetComponent<Slider>().value = healthPlayer;
        if (healthPlayer <= 0)
        {
            Debug.Log("GAME OVER");
            Destroy (gameObject);
        }
    }
}
