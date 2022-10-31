using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPotion : MonoBehaviour
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
            playerController.attackPotions += 1;
            Destroy(gameObject);
        }
    }
}
