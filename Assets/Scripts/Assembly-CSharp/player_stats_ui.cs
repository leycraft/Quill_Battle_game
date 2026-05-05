using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_stats_ui : MonoBehaviour
{
	private player_attack pa;

	private player_hp hp;

	public TextMeshProUGUI stats_text;

	public TextMeshProUGUI atk_text;

	public Image hp_bar;

	public Image range_bar;

	public Image magic_bar;

	public Image range_base;

	public Image magic_base;

	private string attack_text;

	private void Start()
	{
		pa = GameObject.Find("player").GetComponent<player_attack>();
		hp = GameObject.Find("player/hurtbox").GetComponent<player_hp>();
	}

	private void Update()
	{
		if (pa.attack_mode == 0)
		{
			attack_text = "Melee";
		}
		else if (pa.attack_mode == 1)
		{
			attack_text = "Range";
		}
		else if (pa.attack_mode == 2)
		{
			attack_text = "Magic";
		}
		stats_text.SetText(hp.hp.ToString("F0") + "\n" + pa.range_stamina.ToString("F0") + "% \n" + pa.magic_stamina.ToString("F0") + "% \n");
		atk_text.SetText(attack_text);
		hp_bar.fillAmount = hp.hp / hp.hp_cap;
		range_bar.fillAmount = pa.range_stamina / 100f;
		magic_bar.fillAmount = pa.magic_stamina / 100f;
		if (pa.range_fatigue)
		{
			range_base.color = Color.red;
		}
		else
		{
			range_base.color = Color.white;
		}
		if (pa.magic_fatigue)
		{
			magic_base.color = Color.red;
		}
		else
		{
			magic_base.color = Color.white;
		}
	}
}
