﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Creates shots spawning in four different directions from the enemy.<para/>
/// </summary>
public class EnemyPewPew : MonoBehaviour {
	public GameObject shot;
	public bool shoot = true;
	public Transform shotSpawn;
	public Transform shotSpawn1;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	
	float timer;
	
	private float timeBetweenAttacks = 1.5f;
	private Animator anim;

	// Use this for initialization
	void Start() {
		anim = GetComponent<EnemyAnimatorFinding>().getEnemyAnimator();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && shoot) {
			timer = 0f;
			
			// Cast animations if they exist
			if (anim != null)
			{
				EnemyAnimatorController.ExecuteAnimation(anim, "Cast");
			}
			// Instaniate the four shots
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
			Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
			Instantiate(shot, shotSpawn3.position, shotSpawn3.rotation);
		}
	}
}
