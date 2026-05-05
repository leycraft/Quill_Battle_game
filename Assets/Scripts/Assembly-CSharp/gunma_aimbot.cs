using System.Collections;
using UnityEngine;

public class gunma_aimbot : MonoBehaviour
{
	public gunma_aimbot_bullet shot_fired;

	public float life_time = 1f;

	public float bullet_life = 0.34f;

	public bool hard_version;

	public Rigidbody2D crosshair_middle;

	public Rigidbody2D crosshair_left;

	public GameObject bullet_spawn_area;

	private void Start()
	{
		StartCoroutine(shooting());
	}

	private void FixedUpdate()
	{
	}

	private IEnumerator shooting()
	{
		yield return new WaitForSeconds(life_time);
		gunma_aimbot_bullet obj = Object.Instantiate(shot_fired, bullet_spawn_area.transform.position, bullet_spawn_area.transform.rotation);
		obj.will_explode = hard_version;
		obj.life_time = bullet_life;
		Object.Destroy(base.gameObject);
	}
}
