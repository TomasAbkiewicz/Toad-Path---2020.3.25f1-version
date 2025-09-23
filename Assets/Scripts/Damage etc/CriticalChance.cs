using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalChance : MonoBehaviour
{
    public bool CompareAttackWeights(int weight)
    {
        float number;
        number = Random.Range(1.0f, 100.0f);
        number = Mathf.RoundToInt(number);  

        for(var i = 0; i < weight; i++)
        {
            if(number == i)
            {
                print(number + "true" + weight);
                return true;
            }
        }

        return false;
    }
}
