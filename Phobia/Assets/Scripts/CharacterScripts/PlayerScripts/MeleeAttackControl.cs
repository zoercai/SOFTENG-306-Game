﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Class which handles player melee attack logic.<para/>
/// </summary>
public class MeleeAttackControl : MonoBehaviour
{
    private Rigidbody rb;

    public float meleeTimeout;
    public int attackDamage;
    public int knockback;

    void Start()
    {
        // Destroy projectile, to simulate a short-ranged melee attack.
        Destroy(gameObject, meleeTimeout);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door")
            || other.gameObject.CompareTag("Wall")
            || other.gameObject.CompareTag("Enemy")
            || other.gameObject.CompareTag("Boss")
            || other.gameObject.CompareTag("Ball"))
        {

            // Destroy bolt on contact.
            Destroy(gameObject);

            // If bolt hits an enemy, deal damage to that enemy.
            HealthControl.dealDamageToEnemy(other.gameObject, attackDamage);
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                // Add a knock back effect to enemies/bosses
                other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * knockback);
            }
        }

        // Block an enemy projectile
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            // Destroy melee attack and enemy's attack
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
