using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataEnemy : MonoBehaviour
{
    public int enemyHealth;
    public Slider enemyHealthBar;


    private void update()
    {
        enemyHealthBar.value = enemyHealth;
    }
}
