using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //public const float speed = 0.2f;

    public const int side_length = 150;

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

        



    }

    void FixedUpdate()
    {



        UpdateBullet();



        if (InstanceManager.instance.playerManager.myPlayerInfo == null)
        {
            return;
        }

        if (InstanceManager.instance.playerManager.myPlayerInfo.state != 0)
        {
            return;
        }


        bool moved = false;


        float speed = Time.deltaTime * 15;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed = 20;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < side_length)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
                moved = true;
            }

        }else if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y < side_length)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, 0);
                moved = true;
            }

        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.y > -side_length)
            {

                transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
                moved = true;

            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x > -side_length)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
                moved = true;
            }
        }

        if (Input.GetKey(KeyCode.K))
        {         
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.eulerAngles.y, transform.localEulerAngles.z + 0.5f);   
        }
        else if (Input.GetKey(KeyCode.L))
        {
            
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 0.5f);
            
        }
        


        if (Input.GetKeyDown(KeyCode.J))
        {
            shoot();
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayeAttackAni();
        }


        if (Input.GetKeyDown(KeyCode.U))
        {
            sendCreateMotionMsg(0, InstanceManager.instance.playerManager.myPlayerInfo.gameObject.transform.position);
        }
        if (moved) { 
            CheckEatScoreMotion();
        }
    }


    public void sendCreateMotionMsg(int type , Vector3 posi) {

        CSCreateMotionMsg sendMsg = new CSCreateMotionMsg();
        sendMsg.type = type;
        sendMsg.posi = new Vector2Msg(posi.x , posi.y);
        InstanceManager.instance.netClient.Send(sendMsg);
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


        //Debug.Log("OnColliderEnter .....");

    }





    private void CheckEatScoreMotion() {
        Vector3 myPosi = InstanceManager.instance.playerManager.myPlayerInfo.gameObject.transform.position ;
        foreach (long uid in InstanceManager.instance.playerManager.uid2motionMap.Keys) {
            GameObject motion;
            InstanceManager.instance.playerManager.uid2motionMap.TryGetValue(uid , out motion);
            Vector3 posi = motion.transform.position;


            if (Vector3.Distance( posi , myPosi ) < 10) {
                CSEatScoreMotionMsg sendMsg = new CSEatScoreMotionMsg();
                sendMsg.motionUid = uid;
                InstanceManager.instance.netClient.Send(sendMsg);
                
            }
        }
    
    }



    void PlayeAttackAni() {

        animator.SetBool("Attack",true);
    }




    public long GetSysTime() {
        TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
        return (long)ts.TotalMilliseconds;
    }

}
