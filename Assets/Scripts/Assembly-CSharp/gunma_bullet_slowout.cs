using UnityEngine;

public class gunma_bullet_slowout : MonoBehaviour
{
	public float bullet_speed = 5f;

	public float max_bullet_speed = 30f;

	public float speed_increase = 5f;

	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (bullet_speed < max_bullet_speed)
		{
			bullet_speed += speed_increase * Time.deltaTime;
		}
		else
		{
			bullet_speed = max_bullet_speed;
		}
		rb.linearVelocity = base.transform.right * bullet_speed;
	}
}
