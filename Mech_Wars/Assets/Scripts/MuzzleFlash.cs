using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

    SpriteRenderer MuzzleFlashSprite;

	// Use this for initialization
	void Start () {
        MuzzleFlashSprite = GetComponent<SpriteRenderer>();

	}
	public IEnumerator Muzzle()
    {
        MuzzleFlashSprite.enabled = true;
        yield return new WaitForSeconds(0.06f);
        MuzzleFlashSprite.enabled = false;

    }
	
}
