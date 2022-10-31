using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Fireball FireballPrefab;
    public float speed => 3 * Mathf.Pow(1.1f, movePotions);
    public HealthBar HealthBar;
    float attackCooldown => 1 * Mathf.Pow(0.9f, attackPotions);
    public float currentAttackCooldown = 0;

    public Text MovePotionText;
    public Text AttackPotionText;

    public float movePotions = 0;
    public float attackPotions = 0;

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
        MovePotionText.text = $"Move speed: {speed}";
        AttackPotionText.text = $"Attack cd: {attackCooldown:f2}|{currentAttackCooldown:f2}";
        if (HealthBar.Health > 0)
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            transform.position += speed * Time.deltaTime * new Vector3(horizontalInput, verticalInput, 0);
            //Debug.Log($"{Camera.main.ScreenToWorldPoint(Input.mousePosition)} -- {transform.position}");

            if (currentAttackCooldown < 0)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    var cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var fireball = Instantiate(FireballPrefab, transform.position, Quaternion.identity, null);
                    fireball.transform.up = new Vector3(cam.x, cam.y) - fireball.transform.position;
                    currentAttackCooldown = attackCooldown;
                }
            }
            else
            {
                currentAttackCooldown -= Time.deltaTime;
            }
        }
        else
        {
            FindObjectOfType<GameController>()?.RestartGame();
        }
    }
}
