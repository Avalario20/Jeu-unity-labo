using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;

	[SerializeField] private float attackCooldown;
	private PlayerMouvement playerMovement;
	private float cooldownTimer = Mathf.Infinity;

	void Update()
	{
		if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown )
		{
			Shoot();
			PlayerMouvement.instance.animator.SetTrigger("attack");
		}
		cooldownTimer += Time.deltaTime;
	}

	void Shoot()
	{
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}


