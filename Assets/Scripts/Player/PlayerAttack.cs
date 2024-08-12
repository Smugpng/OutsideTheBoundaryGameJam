using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce, upwardForce;

    public float cooldown, spread, reloadTimer, timeBetweenShots;
    public int magSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloding;

    private Camera cam;
    public Transform attackPoint;

    private bool allowInvoke = true;
    //graphics
    public TextMeshProUGUI ammoDisplay;
    private void Awake()
    {
        bulletsLeft = magSize;
        cam = GetComponentInChildren<Camera>();
        readyToShoot = true;
    }
    private void Update()
    {
        Inputs();
        if(ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft/bulletsPerTap + "/" + magSize/bulletsPerTap);
       }
    }
    private void Inputs()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else  shooting = Input.GetKey(KeyCode.Mouse0);


        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloding)
        {
            Reload();
        }
        if (readyToShoot && shooting && !reloding && bulletsLeft <= 0)
        {
            Reload();
        }
        if (readyToShoot && shooting && !reloding && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else 
        {
            targetPoint = ray.GetPoint(75);
        }
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;
        if (allowInvoke)
        {
            Invoke("resetShot", timeBetweenShots);
            allowInvoke = false;
        }
        if(bulletsShot < bulletsPerTap & bulletsLeft <0) { Invoke("Shoot", timeBetweenShots); }
    }
    private void resetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload()
    {
        reloding = true;
        Invoke("ReloadFinished", reloadTimer);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloding = false;   
    }
}
