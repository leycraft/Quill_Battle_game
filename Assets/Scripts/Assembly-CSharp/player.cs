using UnityEngine;

public class player : MonoBehaviour
{
	private Animator wing;

	private Rigidbody2D rb;

	public float move_force = 5f;

	public float jump_force = 5f;

	public float dash_force = 50f;

	public GameObject after_image;

	private float dash_mode;

	private float afterimage_timer;

	private Vector2 movement;

	private player_attack p_atk;

	private void Start()
	{
		wing = base.gameObject.transform.Find("visual/visual_quill/wing1").gameObject.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		p_atk = GetComponent<player_attack>();
	}

	private void Update()
	{
		movement = new Vector2(Input.GetAxis("Horizontal") * move_force, Input.GetAxis("Vertical") * jump_force);
		if (dash_mode > 0f)
		{
			dash_mode -= Time.deltaTime;
			afterimage_timer -= Time.deltaTime;
			if (afterimage_timer <= 0f)
			{
				afterimage_timer = 0.05f;
				GameObject gameObject = Object.Instantiate(after_image, base.transform.position, Quaternion.identity);
				if (p_atk.get_crosshair_pos().x < 0f)
				{
					gameObject.transform.localScale = new Vector3(-1f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				}
			}
		}
		if (dash_mode > 0f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f)
		{
			wing.SetTrigger("fast");
		}
		else
		{
			wing.SetTrigger("slow");
		}
	}

	private void FixedUpdate()
	{
		move_char(movement);
	}

	private void move_char(Vector2 direction)
	{
		if (dash_mode <= 0f)
		{
			rb.MovePosition((Vector2)base.transform.position + direction * Time.deltaTime);
			return;
		}
		Vector2 vector = direction.normalized * dash_force;
		rb.MovePosition((Vector2)base.transform.position + vector * Time.deltaTime);
	}

	public void enter_dash_mode(float duration)
	{
		dash_mode = duration;
	}
}
