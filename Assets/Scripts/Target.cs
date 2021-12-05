using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public  float health = 100f;
    public static float remainTarget;

    void Start()
    {
        remainTarget = 5;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Hedef yok edildi.");
            Destroy(gameObject);
            remainTarget -= 1;
            health = 0;
        }
    }

    
  
}
