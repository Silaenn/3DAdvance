using System;
using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
	public float time = 0.05f;
	
	private float timer;

	private Light _light;

	private void Awake()
	{
		_light = GetComponent<Light>();
	}


	void OnEnable()
	{
		timer = time;
		StartCoroutine(Flicker());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	IEnumerator Flicker()
	{
		while (true)
		{
			_light.enabled = !_light.enabled;
			
			do
			{
				timer -= Time.deltaTime;
				yield return null;
			}
			
			while(timer > 0);
			timer = time;
		}
	}
}
