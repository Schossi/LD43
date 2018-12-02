using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    private Animator _muzzleFlashAnimator;
    private GameObject _shotTemplate;

	// Use this for initialization
	void Start () {
        _muzzleFlashAnimator = transform.Find("MuzzleFlash").GetComponent<Animator>();
        _shotTemplate = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot()
    {
        _muzzleFlashAnimator.SetTrigger("shoot");

        GameObject shot = Instantiate(_shotTemplate,null,true);
        shot.transform.position = _shotTemplate.transform.position;
        shot.transform.rotation = _shotTemplate.transform.rotation;
        shot.SetActive(true);
    }
}
