using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public Slash SlashPrefab;
    public Scorch ScorchPrefab;
    float speed = 1;
    public HealthBar HealthBar;
    public AttackPotion AttackPotionPrefab;
    public HealPotion HealPotionPrefab;
    public MovePotion MovePotionPrefab;
    GameController gameController;
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        player = GameObject.Find("Player");
    }

    bool attacking = false;
    IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        var slash = Instantiate(SlashPrefab, transform.position, Quaternion.identity, null);
        slash.transform.up = player.transform.position - slash.transform.position;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    // Update is called once per frame
    bool destroying = false;
    void Update()
    {
        if (!gameController.running)
        {
            return;
        }
        if (HealthBar.Health > 0)
        {
            if (!attacking)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 0.65)
                {
                    StartCoroutine(Attack());
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        player.transform.position, speed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (!destroying)
            {
                destroying = true;
                var rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    Instantiate(AttackPotionPrefab, transform.position, Quaternion.identity, null);
                }
                else if (rand == 1)
                {
                    Instantiate(HealPotionPrefab, transform.position, Quaternion.identity, null);
                }
                else if (rand == 2)
                {
                    Instantiate(MovePotionPrefab, transform.position, Quaternion.identity, null);
                }
                FindObjectOfType<GameController>().Score += 1;
                Instantiate(ScorchPrefab, transform.position, Quaternion.identity, null);
                Destroy(gameObject, Time.deltaTime * 3);
            }
        }
    }
}
