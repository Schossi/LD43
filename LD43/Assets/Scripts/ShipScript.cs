using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    private static Vector3 _inputOffset = new Vector3(0, 0.2f, 0);

    private Rigidbody2D _rigidbody;
    private Animator _weaponSystemLightsAnimator;

    private GunScript[] _guns;

    private bool _isShooting;
    private float _shootingTimer;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weaponSystemLightsAnimator = transform.FindDeepChild("WeaponSystemLights")?.GetComponent<Animator>();
        _guns = transform.GetComponentsInChildren<GunScript>();
        
        startShooting();
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
        updateShooting();
    }

    private void updateMovement()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 viewportPosition = Camera.main.WorldToViewportPoint(mousePosition);

        //if (!viewportPosition.x.IsInRange(0, 1) || !viewportPosition.y.IsInRange(0, 1))
        //    return;

        if (Vector2.Distance(mousePosition, transform.position) > Constants.movementThreshold)
            stopShooting();
        else
            startShooting();

        _rigidbody.MovePosition(mousePosition + _inputOffset);
    }

    private void startShooting()
    {
        if (_isShooting)
            return;

        _isShooting = true;
        _shootingTimer = 0;
    }
    private void stopShooting()
    {
        if (!_isShooting)
            return;

        _isShooting = false;
    }
    private void updateShooting()
    {
        if (!_isShooting)
            return;

        _shootingTimer += Time.deltaTime;

        if (_shootingTimer >= Constants.ShotInterval)
        {
            shoot();
            _shootingTimer = 0;
        }
    }

    private void shoot()
    {
        foreach (GunScript gun in _guns)
        {
            gun.Shoot();
        }

        _weaponSystemLightsAnimator?.SetTrigger("fire");
    }
}
