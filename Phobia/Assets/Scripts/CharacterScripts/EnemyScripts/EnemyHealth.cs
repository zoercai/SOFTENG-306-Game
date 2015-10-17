﻿using UnityEngine;

/**
 * 
 * Class which handles enemy health logic.
 * 
 **/
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int lethalLow;
	public int scoreAwarded = 0;

	private bool isDed = false;
    private Animator anim;

    void Start()
    {
        try
        {
            anim = GetComponent<EnemyAnimatorFinding>().getEnemyAnimator();
        }
        catch
        {
            Debug.Log("Did not find EnemyAnimatorFinding script!");
        }
        
    }

    void Awake()
    {
        // Set the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (gameObject.transform.position.y < lethalLow)
        {
            TakeDamage(startingHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        // Reduce current health by the amount of damage taken.
        currentHealth -= amount;

        if (anim != null)
        {
            EnemyAnimatorController.ExecuteAnimation(anim, "Hit");
        }

        EnemyFlash temp12 = this.gameObject.GetComponent<EnemyFlash> ();
		if (temp12 != null) {
			StartCoroutine (temp12.Flash ());
		}

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0) {
			if (isDed == false) {
				isDed = true;
				Debug.Log ("Enemy Destroyed!");
				if (this.tag == "Enemy" || this.tag == "Boss") {
					// Increment score when destroyed.
					Debug.Log ("INCREMEMNTING!");
					TEMPScoreScript.Instance.IncrementScore (scoreAwarded);
				}

                if (anim != null)
                {
                    EnemyAnimatorController.ExecuteAnimation(anim, "Die");
                }

                SpiderAnimation temp = GetComponent<SpiderAnimation> ();
				if (temp != null) {
					Debug.Log ("THIS IS A SPIDER!");
					temp.spiderKilled ();
				} else {
					Destroy (gameObject, 1f);
				}

			}
		}
    }
}
