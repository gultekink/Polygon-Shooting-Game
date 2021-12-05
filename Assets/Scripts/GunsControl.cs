using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunsControl : MonoBehaviour
{
    public float damage;
    public float range = 100;
    public ParticleSystem gunPartical;
    public Camera fpsCam;
    public GameObject impact;
    public float pistolFireRate =15f;
    public float rifleFireRate = 15f;
    private float _nextTimetofire=0f;
    public static bool isReloading;
    public int maxAmmo;
    private int _currentAmmo;
    public float reloadtime;
    public AudioSource fireSound;
    public AudioSource reloadSound;
    public float spreadFactor = 0.02f;
    public Animator animator;
    public Text ammoCount;
    public Text targetHealth;
    public static bool _isLevelCompleted;
    public Text successText;
    public Text successText2;
    RaycastHit hit;

  
    void Start()
    {
        _currentAmmo = maxAmmo;
        _isLevelCompleted =false;
        successText.enabled = false;
        successText2.enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
        //PlayAgain();
        if (!_isLevelCompleted)
        {
            TargetHealth();
        ammoCount.text = _currentAmmo.ToString();

        if (Input.GetKeyDown(KeyCode.R) && _currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());

            return;
        }
        if (isReloading)
        {
            return;
        }

        if (GameObject.FindGameObjectWithTag("M4A1"))
        {
            if (Input.GetKey(KeyCode.D) && Time.time >= _nextTimetofire && _currentAmmo>0)
            {
                _nextTimetofire = Time.time + 1 / rifleFireRate;
                Shoot();
                _currentAmmo--;
                fireSound.Play();
                animator.SetBool("isFiring", true);
                if (Time.time >= 0.0001f)
                {
                    animator.SetBool("isFiring", false);
                    }
                        if (_currentAmmo <= 0)
                {
                    Debug.Log("Şarjör Boş");
                }
                }
        }
        else if (Input.GetKeyDown(KeyCode.D) && Time.time >= _nextTimetofire && _currentAmmo > 0)
        {
            fireSound.Play();
            _nextTimetofire = Time.time + 1 / pistolFireRate;
            Shoot();
            animator.SetBool("isFiring",true);
            _currentAmmo--;
            if (_currentAmmo <= 0)
            {
                Debug.Log("Şarjör Boş");
            }

           
        }

        if (Time.time >= _nextTimetofire-0.9f)
        {
            animator.SetBool("isFiring",false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetHealth();
        }

        OpenMenu();

        }

        if (Target.remainTarget <= 0)
        {
            _isLevelCompleted = true;
            StartCoroutine(SuccesScreen());
            StartCoroutine(ChangeScene());
            OpenMenu();
        }

      
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(4f);
        LevelManager.NextLevel();
    }

    void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.M) /*&& SceneManager.GetActiveScene().name !="MenuScene"*/)
        {
            SceneManager.LoadScene("MenuScene");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void TargetHealth()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                targetHealth.text = "Target Health: " + target.health.ToString();
            }
        }
    }

    //void PlayAgain()
    //{
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        PlayerPrefs.DeleteAll();
    //    }
    //}

    void Shoot()
    {
        Debug.Log("Ateş Edildi.");
        gunPartical.Play();
        Vector3 direction = new Vector3(0,0,0);
        direction.x += Random.Range(-spreadFactor, spreadFactor);
        direction.y += Random.Range(-spreadFactor, spreadFactor);
        direction.z += Random.Range(-spreadFactor, spreadFactor);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward - direction, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (hit.transform.tag == "Target")
            {
                Debug.Log("Hedef Vuruldu.");
            }
            else
            {
                Debug.Log("Hedef Vurulmadı.");
            }

            if (target !=null)
            {
             
                target.TakeDamage(damage);
                targetHealth.text ="Target Health: "+ target.health.ToString();
            }
            Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }

      
    }

    IEnumerator SuccesScreen()
    {
        yield return new WaitForSeconds(.8f);
        successText.enabled = true;
        successText2.enabled = true;
    }

    IEnumerator Reload()
    {
        reloadSound.Play();
        isReloading = true;
        animator.SetBool("isReloading",true);
        Debug.Log("Şarjör dolduruluyor.");
        yield return new WaitForSeconds(reloadtime-.2f);
        animator.SetBool("isReloading", false);
        yield return new WaitForSeconds(.4f);
        _currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ResetHealth()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Target")
            {
                Target target = hit.transform.GetComponent<Target>();
                target.health = 100;
            }

        }
    }

  
}

