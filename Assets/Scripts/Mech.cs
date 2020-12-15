using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mech : EnemyController
{
	public LineRenderer BigCanon01L;
	public LineRenderer BigCanon01R;
	public LineRenderer BigCanon02L;
	public LineRenderer BigCanon02R;

	public AudioClip audioBigCanon;

	Animator animator;


	//Big Canons
	void ShootBigCanonA()
	{

		audioSource.clip = audioBigCanon;
		audioSource.Play();

		Color c = BigCanon01L.material.GetColor("_TintColor");
		c.a = 1f;
		BigCanon01L.material.SetColor("_TintColor", c);
		BigCanon01R.material.SetColor("_TintColor", c);
		StartCoroutine("FadoutBigCanon01");
	}

	IEnumerator FadoutBigCanon01()
	{
		Color c = BigCanon01L.material.GetColor("_TintColor");
		while (c.a > 0)
		{
			c.a -= 0.1f;
			BigCanon01L.material.SetColor("_TintColor", c);
			BigCanon01R.material.SetColor("_TintColor", c);
			yield return null;
		}
	}

	void ShootBigCanonB()
	{

		audioSource.clip = audioBigCanon;
		audioSource.Play();

		Color c = BigCanon01L.material.GetColor("_TintColor");
		c.a = 1f;
		BigCanon02L.material.SetColor("_TintColor", c);
		BigCanon02R.material.SetColor("_TintColor", c);
		StartCoroutine("FadoutBigCanon02");
	}

	IEnumerator FadoutBigCanon02()
	{
		Color c = BigCanon02L.material.GetColor("_TintColor");
		while (c.a > 0)
		{
			c.a -= 0.1f;
			BigCanon02L.material.SetColor("_TintColor", c);
			BigCanon02R.material.SetColor("_TintColor", c);
			yield return null;
		}
	}

}
