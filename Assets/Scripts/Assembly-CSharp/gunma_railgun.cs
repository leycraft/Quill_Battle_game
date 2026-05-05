using UnityEngine;

public class gunma_railgun : MonoBehaviour
{
	public GameObject railgun_boom;

	public GameObject railgun_bullet;

	public float bullet_speed = 5f;

	public float boom_timer = 0.5f;

	public bool fire_bullet;

	private float boom_timer_counter = 0.5f;

	private int bullet_fire_counter;

	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		boom_timer_counter = boom_timer;
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = base.transform.right * bullet_speed;
		boom_timer_counter -= Time.deltaTime;
		if (!(boom_timer_counter <= 0f))
		{
			return;
		}
		Object.Instantiate(railgun_boom, base.transform.position, base.transform.rotation);
		if (fire_bullet)
		{
			if (bullet_fire_counter == 0)
			{
				Quaternion rotation = base.transform.rotation;
				rotation *= Quaternion.Euler(0f, 0f, 90f);
				Object.Instantiate(railgun_bullet, base.transform.position, rotation);
				rotation = base.transform.rotation;
				rotation *= Quaternion.Euler(0f, 0f, -90f);
				Object.Instantiate(railgun_bullet, base.transform.position, rotation);
			}
			bullet_fire_counter++;
			bullet_fire_counter %= 2;
		}
		boom_timer_counter = boom_timer;
	}
}
