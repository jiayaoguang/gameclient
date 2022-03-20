using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //public const float speed = 0.2f;

    public const int side_length = 500;

    public GameObject bulletPrefab;

    private LinkedList<GameObject> bulletQueue = new LinkedList<GameObject>();

    public Animator animator;


    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x , 0, transform.position.z);
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        for (int i = 0;i < 100; i++) {
            
            //GameObject bullet = Instantiate( bulletPrefab );
            //bullet.SetActive(true);
            //bullet.GetComponent<Transform>().position = new Vector3(10,0,10);
        }


        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (InstanceManager.instance.playerManager.myPlayerInfo == null) {
            return;
        }


        float speed = Time.deltaTime * 10;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            speed = 20;
        }

        if (Input.GetKey(KeyCode.W)) {
            if (transform.position.y < side_length) { 
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
            }
            
        }
        else if(Input.GetKey(KeyCode.S)) {
            if (transform.position.y > -side_length)
            { 
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, 0);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -side_length)
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < side_length)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y , 0);
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            shoot();
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayeAttackAni();
        }



    }

    void FixedUpdate()
    {
        UpdateBullet();
    }

public void UpdateBullet()
    {

        if (InstanceManager.instance.playerManager.myPlayerInfo == null)
        {
            return;
        }

        GameObject bullet = InstanceManager.instance.playerManager.myPlayerInfo.bullet;

        if (bullet == null)
        {
            return;
        }

        if (!bullet.activeInHierarchy)
        {
            return;
        }
        //long now = GetSysTime();

        if (InstanceManager.instance.playerManager.myPlayerInfo.bulletEndTime <= 0) {
            bullet.SetActive(false);
            return;
        }

        InstanceManager.instance.playerManager.myPlayerInfo.bulletEndTime -= 20;

        Vector3 oldPosi = bullet.GetComponent<Transform>().position;
        Vector3 dir = InstanceManager.instance.playerManager.myPlayerInfo.bulletDir;

        bullet.GetComponent<Transform>().position
            = new Vector3(oldPosi.x + dir.x * 0.4f, oldPosi.y + dir.y * 0.4f, 0);


    }



    public void shoot() {


        if (InstanceManager.instance.playerManager.myPlayerInfo == null) {
            return;
        }

        GameObject bullet = InstanceManager.instance.playerManager.myPlayerInfo.bullet;

        if (bullet == null) {
            bullet =  GameObject.Instantiate(bulletPrefab);
            InstanceManager.instance.playerManager.myPlayerInfo.bullet = bullet;

            Debug.Log("Instantiate(bulletPrefab)===========");
        }
        else if (bullet.activeInHierarchy) {
            return;
        }

        
        bullet.transform.position 
            = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);


        InstanceManager.instance.playerManager.myPlayerInfo.bulletEndTime = 1000L * 2;

        bullet.SetActive(true);

        Debug.Log("shoot");




    }




    void OnCollisionEnter(Collision other)                   //参数是必须的，类型是Collision，不然调用不成功

    {
        //这个函数在碰撞开始时候调用，


        Debug.Log("OnColliderEnter .....");

    }


    void PlayeAttackAni() {

        animator.SetBool("Attack",true);
    }




    public long GetSysTime() {
        TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
        return (long)ts.TotalMilliseconds;
    }

}
