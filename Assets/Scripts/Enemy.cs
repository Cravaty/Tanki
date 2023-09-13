using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    static public Enemy S;
    [Header("Set in Inspector")]
    public float speed = 5f;
    public int health = 10;
    public float rollSpeed = 30;
    public float kok = 3f;
    public float timeSleep;
    public float pif = 3f;
    public float paf;
    public Text EnemyHP;
    private BoundsCheck bndCheck;
    float ran;
    Rigidbody _Enemy;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        if (S == null)
            S = this;
        else
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S");
    }

    // Start is called before the first frame update
    void Start()
    {
        _Enemy= GetComponent<Rigidbody>();
        timeSleep = kok;
        paf = pif;
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health == 0)
        {
            Destroy(gameObject);
            DelayedRestart(2f);

        }
        if (Hero.S.health == 0)
        {
            
            DelayedRestart(2f);

        }
        Vector3 pos = transform.up * speed * Time.deltaTime;
        Vector3 poz = _Enemy.position + pos;
        _Enemy.MovePosition(poz);
        if(timeSleep == 0)
            ran = MoveE();
        Quaternion r = Quaternion.Euler(0f, 0f, rollSpeed * ran * Time.deltaTime);
        
        Quaternion curugol = _Enemy.rotation * r;
        _Enemy.MoveRotation(curugol);
        if (paf == 0)
        {


            if (bndCheck.ofUp && !bndCheck.ofDown)
            {
                print("kosnyl");
                _Enemy.transform.position = -_Enemy.transform.position;
            }
            if (bndCheck.ofDown && !bndCheck.ofUp)
            {
                print("kosnyl");
                _Enemy.transform.position = -_Enemy.transform.position;
            }
            if (bndCheck.ofLeft)
            {
                print("kosnyl");
                _Enemy.transform.position = -_Enemy.transform.position;
            }
            if (bndCheck.ofRight)
            {
                print("kosnyl");
                _Enemy.transform.position = -_Enemy.transform.position;
            }
            

        }
        timer();
        _timer();


    }
    float MoveE()
    {
        
        float ran = Random.Range(-1, 2);
        return ran;
    }
    void timer()
    {
        if (timeSleep > 0)
            timeSleep--;
        else
            timeSleep = kok;
    }
    void _timer()
    {
        if (paf > 0)
            paf--;
        else
            paf = pif;
    }

    private void OnCollisionEnter(Collision coll)
    {
        
        GameObject otherGo = coll.gameObject;
        if (otherGo.tag == "ProjectileHero")
        {
            
            Destroy(otherGo);
            health -= 5;
            int HP =int.Parse(EnemyHP.text);
            HP -= 5;
            EnemyHP.text = HP.ToString();
        }
        
    }
    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_0");
    }
}
