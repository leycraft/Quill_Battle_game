using System.Collections.Generic;
using UnityEngine;

public class boss_base : MonoBehaviour
{
	protected Rigidbody2D rb;

	protected boss_hp hp;

	public string attack_file = "";

	public List<string> allowed_attacks;

	public Animator spellcard;

	public Animator spellcard_body;

	public string attack_type = "";

	protected int attack_sequence;

	protected float attack_timer;

	protected int repeat_attack;

	protected int spellcard_phase;

	protected int ending_phase;

	protected bool is_escaping;

	protected Transform goto_point;

	protected void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		hp = GetComponent<boss_hp>();
		read_file();
	}

	private void FixedUpdate()
	{
	}

	protected void read_file()
	{
		if (!(attack_file != "") || allowed_attacks.Count != 0)
		{
			return;
		}
		string[] array = Resources.Load<TextAsset>(attack_file).text.Split("\n");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				allowed_attacks.Add(array[i]);
			}
		}
	}

	protected void reset_attack()
	{
		attack_type = "";
		attack_sequence = 0;
		attack_timer = 0f;
		repeat_attack = 0;
	}

	protected void initiate_new_attack()
	{
		int index = Random.Range(0, allowed_attacks.Count);
		attack_type = allowed_attacks[index];
	}

	protected void move_to(float speed, bool slow_in = true)
	{
		Vector2 vector = goto_point.position - base.transform.position;
		vector = ((!(vector.magnitude >= 3f) && slow_in) ? (vector.normalized * speed * (vector.magnitude / 3f)) : (vector.normalized * speed));
		rb.MovePosition((Vector2)base.transform.position + vector * Time.deltaTime);
	}
}
