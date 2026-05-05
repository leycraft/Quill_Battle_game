using UnityEngine;

public class bullet_movement : MonoBehaviour
{
	public float bullet_speed = 5f;

	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = base.transform.right * bullet_speed;
	}
}
