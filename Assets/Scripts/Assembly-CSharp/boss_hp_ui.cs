using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class boss_hp_ui : MonoBehaviour
{
	public GameObject boss_obj;

	public boss_hp boss;

	public TextMeshProUGUI hp_text;

	public Image hp_bar;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (boss != null)
		{
			boss_obj.SetActive(value: true);
			hp_text.SetText(boss.hp + "/" + boss.max_hp);
			hp_bar.fillAmount = boss.hp / boss.max_hp;
		}
		else
		{
			boss_obj.SetActive(value: false);
		}
	}
}
