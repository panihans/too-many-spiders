using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    // Start is called before the first frame update
    float lifeTime = 0.5f;
    float damage = 30;
    GameController gameController;
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.running)
        {
            return;
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    bool didDamage = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (didDamage)
        {
            return;
        }
        didDamage = true;
        var playerController = collision?.gameObject?.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.HealthBar.Health -= damage;
        }
        Destroy(gameObject, Time.deltaTime * 5);
    }
}
