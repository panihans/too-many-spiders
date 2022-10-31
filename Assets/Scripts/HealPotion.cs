using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    void Start()
    {
        Debug.Log("potion");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerController = collision?.gameObject?.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.HealthBar.Health = Mathf.Min(playerController.HealthBar.Health + 10, 100);
            Destroy(gameObject);
        }
    }
}
