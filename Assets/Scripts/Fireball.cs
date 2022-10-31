using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    public ExplosionSound ExplosionSoundPrefab;
    float speed = 5;
    float lifeTime = 5;
    float damage = 35;
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
        transform.position += speed * Time.deltaTime * transform.up;
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    bool destroying = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!destroying)
        {
            destroying = true;
            var spiderController = collision?.gameObject?.GetComponent<SpiderController>();
            if (spiderController != null)
            {
                spiderController.HealthBar.Health -= damage;
            }
            Instantiate(ExplosionSoundPrefab, transform.position, Quaternion.identity, null);
            Destroy(gameObject, Time.deltaTime * 3);
        }
    }
}
