using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOwer : MonoBehaviour
{
    [Header("Set in Inspector")]
    Rigidbody tower;
    public GameObject projectilePrefab;
    public Transform shotDir;
    private float timeShot;
    public float startTime;

    void Start()
    {
        tower = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 diff = pos - transform.position;
        diff.Normalize();
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ-90);
        if(timeShot <= 0)
        {
            if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(projectilePrefab, shotDir.position, transform.rotation);
                timeShot = startTime;

            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
    }
    
    

}
