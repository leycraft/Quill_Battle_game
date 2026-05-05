using UnityEngine;

public class gunma_drone : MonoBehaviour
{
	public GameObject bullet;

	private Rigidbody2D rb;

	public float rotation_speed_high = 5f;

	public float rotation_speed_low = 5f;

	private float rotation_speed;

	public float speed = 30f;

	public float fire_time = 10f;

	private float fire_time_counter = 10f;

	protected Transform goto_point;

	private GameObject drone_location;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		drone_location = base.transform.Find("drone").gameObject;
		fire_time_counter = Random.Range(fire_time - 1f, fire_time + 1f);
		rotation_speed = Random.Range(rotation_speed_low, rotation_speed_high);
		if (Random.Range(0, 2) == 0)
		{
			rotation_speed = 0f - rotation_speed;
		}
	}

	private void FixedUpdate()
	{
		if (goto_point != null)
		{
			move_to(speed);
		}
		else
		{
			goto_point = GameObject.Find("gunma_guide_point").transform;
		}
		base.gameObject.transform.Rotate(0f, 0f, rotation_speed * Time.deltaTime);
		fire_bullet();
	}

	protected void move_to(float speed, bool slow_in = true)
	{
		Vector2 vector = goto_point.position - base.transform.position;
		vector = ((!(vector.magnitude >= 3f) && slow_in) ? (vector.normalized * speed * (vector.magnitude / 3f)) : (vector.normalized * speed));
		rb.MovePosition((Vector2)base.transform.position + vector * Time.deltaTime);
	}

	private void fire_bullet()
	{
		fire_time_counter -= Time.deltaTime;
		if (fire_time_counter < 0f)
		{
			fire_time_counter = Random.Range(fire_time - 1f, fire_time + 1f);
			GameObject gameObject = GameObject.Find("gunma_guide_point");
			Vector3 normalized = (drone_location.transform.position - gameObject.transform.position).normalized;
			float num = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num - 90f));
			Object.Instantiate(bullet, drone_location.transform.position, rotation).GetComponent<bullet_movement>().bullet_speed = 27f;
		}
	}
}
