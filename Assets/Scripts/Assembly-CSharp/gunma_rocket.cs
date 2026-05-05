using UnityEngine;

public class gunma_rocket : MonoBehaviour
{
	public GameObject spread_shot;

	public GameObject explosion;

	public float bullet_speed = 10f;

	public float turn_speed = 5f;

	private Rigidbody2D rb;

	public float life_time_max = 10f;

	private float life_time;

	public bool diagonal;

	public bool add_spread;

	private GameObject temp_point;

	private void Start()
	{
		life_time = life_time_max;
		rb = GetComponent<Rigidbody2D>();
		float num = Random.Range(-2f, 2f);
		bullet_speed += num;
		temp_point = GameObject.Find("gunma_guide_point");
	}

	private void FixedUpdate()
	{
		life_time -= Time.deltaTime;
		rocket_movement();
		Vector2 vector = base.transform.position - temp_point.transform.position;
		if (vector.magnitude < 5f)
		{
			turn_speed += 25f * Time.deltaTime;
		}
		if ((life_time <= 3f || vector.magnitude < 8f) && temp_point.name != "temp_point_gunma_r")
		{
			temp_point = new GameObject();
			temp_point.name = "temp_point_gunma_r";
			temp_point.transform.position = GameObject.Find("gunma_guide_point").transform.position;
		}
		if (life_time <= 0f || (double)vector.magnitude <= 0.8)
		{
			do_explode();
		}
	}

	private void do_explode()
	{
		if (add_spread)
		{
			int num = 4;
			float num2 = 360 / num;
			for (int i = 0; i < num; i++)
			{
				if (diagonal)
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
		}
		Object.Instantiate(explosion, base.transform.position, base.transform.rotation);
		Object.Destroy(temp_point);
		Object.Destroy(base.gameObject);
	}

	private void rocket_movement()
	{
		if (temp_point != null)
		{
			rb.linearVelocity = base.transform.right * bullet_speed;
			if (life_time < life_time_max - 0.25f)
			{
				Vector2 vector = temp_point.transform.position - base.transform.position;
				vector.Normalize();
				float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
				Quaternion b = Quaternion.Euler(0f, 0f, z);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * turn_speed);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "player")
		{
			do_explode();
		}
	}
}
