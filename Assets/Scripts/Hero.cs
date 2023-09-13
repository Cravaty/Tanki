using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    static public Hero S;
    [Header("Set in Inspector")]
    public float speed = 5f;
    public int health = 10;
    public float rollSpeed = 30;
    public float timeSleep;
    public float kok = 3f;
    public Text HeroHP;
    private BoundsCheck bndCheck;
Rigidbody SS;
    


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
        SS = GetComponent<Rigidbody>();
        timeSleep = kok;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (health == 0)
        {
            Destroy(gameObject);
            DelayedRestart(2f);

        }
        if (Enemy.S.health == 0)
        {
            
            DelayedRestart(2f);

        }



        float xAxis = Input.GetAxis("Horizontal");
        
        Quaternion r = Quaternion.Euler(0f,0f, rollSpeed * -xAxis * Time.deltaTime);

        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = yAxis * transform.up * speed * Time.deltaTime ;
        Vector3 poz = SS.position + pos;
        Quaternion curugol = SS.rotation* r;
        SS.MovePosition(poz);
        SS.MoveRotation(curugol);

        if (timeSleep == 0)
        {


            if (bndCheck.ofUp && !bndCheck.ofDown)
            {
                print("kosnyl");
                SS.transform.position = -SS.transform.position;
            }
            if (bndCheck.ofDown && !bndCheck.ofUp)
            {
                print("kosnyl");
                SS.transform.position = -SS.transform.position;
            }
            if (bndCheck.ofLeft)
            {
                print("kosnyl");
                SS.transform.position = -SS.transform.position;
            }
            if (bndCheck.ofRight)
            {
                print("kosnyl");
                SS.transform.position = -SS.transform.position;
            }
        }
        timer();
        //pos.y = -pos.y; 

    }
    void timer()
    {
        if (timeSleep > 0)
            timeSleep--;
        else
            timeSleep = kok;
    }
    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;
        if (otherGo.tag == "ProjectileEnemy")
        {

            Destroy(otherGo);
            health -= 5;
            int HP = int.Parse(HeroHP.text);
            HP -= 5;
            HeroHP.text = HP.ToString();
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

