using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] PlayerController playerController;
	[SerializeField] Slider healthBar;

	void Update()
	{
		healthBar.value = playerController.health;
	}
}
