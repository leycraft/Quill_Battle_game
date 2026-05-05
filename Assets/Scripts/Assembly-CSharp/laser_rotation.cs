using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_rotation : MonoBehaviour
{
	private GameObject target;

	public float rotation_speed = 5f;

	public float life_time = 5f;

	public GameObject bullet;

	public List<GameObject> bullet_spawn = new List<GameObject>();

	public float bullet_cooldown = 1f;

	private float bullet_cooldown_timer = 1f;

	private void Start()
	{
		base.gameObject.transform.SetParent(GameObject.Find("EN_gunma").transform, worldPositionStays: true);
		target = GameObject.Find("gunma_guide_point");
		bullet_cooldown_timer = bullet_cooldown;
		StartCoroutine(laser_end());
	}

	private void Update()
	{
		Vector2 vector = target.transform.position - base.transform.position;
		vector.Normalize();
		float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		Quaternion quaternion = Quaternion.Euler(0f, 0f, z);
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, quaternion, rotation_speed * Time.deltaTime);
		bullet_cooldown_timer -= Time.deltaTime;
		if (bullet_cooldown_timer <= 0f)
		{
			for (int i = 0; i < bullet_spawn.Count; i++)
			{
				Object.Instantiate(bullet, bullet_spawn[i].transform.position, quaternion);
			}
			bullet_cooldown_timer = bullet_cooldown;
		}
	}

	private IEnumerator laser_end()
	{
		yield return new WaitForSeconds(life_time);
		Object.Destroy(base.gameObject);
	}
}
