using UnityEngine;

public class gunma_aimbot_bullet : MonoBehaviour
{
	public float bullet_speed = 5f;

	public float life_time = 1f;

	public bool will_explode;

	public GameObject explosion;

	public GameObject spread_shot;

	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		float num = Random.Range(-0.05f, 0.05f);
		life_time += num;
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = base.transform.right * bullet_speed;
		if (will_explode)
		{
			life_time -= Time.deltaTime;
			if (life_time <= 0f)
			{
				do_explode();
			}
		}
	}

	private void do_explode()
	{
		int num = 4;
		float num2 = 360 / num;
		int num3 = Random.Range(0, 2);
		for (int i = 0; i < num; i++)
		{
			if (num3 == 0)
			{
				Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, num2 * (float)i + num2 / 2f));
				Object.Instantiate(spread_shot, base.transform.position, rotation);
			}
			else
			{
				Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, num2 * (float)i));
				Object.Instantiate(spread_shot, base.transform.position, rotation2);
			}
		}
		Object.Instantiate(explosion, base.transform.position, base.transform.rotation);
		Object.Destroy(base.gameObject);
	}
}
