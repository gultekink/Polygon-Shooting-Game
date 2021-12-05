using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public Image M4A4Image;
    public Image BerettaImage;
    public GameObject gameObject;
    private string _currentGun;

    void Start()
    {
        M4A4Image.enabled = false;
        BerettaImage.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SwitchWeapon();
            CheckCurrentGun();
        }
    }

    void SwitchWeapon()
    {
        if (GunsControl.isReloading == false)
            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(!weapon.gameObject.activeSelf);
                Debug.Log("Silah değiştirildi");
                
            }
    }

    void CheckCurrentGun()
    {
        if (gameObject.activeSelf)
        {
            M4A4Image.enabled = false;
            BerettaImage.enabled = true;
        }
        else
        {
            M4A4Image.enabled = true;
            BerettaImage.enabled = false;
        }
    }

}
