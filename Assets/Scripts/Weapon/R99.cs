using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R99 : MonoBehaviour,IWeapon
{
    public Attribute attribute;
    public GameObject bulletPrefab;
    public float shootInterval=1f;
    public Transform shootPos;
    public Vector2 dir = new Vector2(1, 0);

    private float lastTime = 0f;
    private void Awake()
    {
        attribute = GetComponent<Attribute>();
    }
    public void Execute()
    {
        if(lastTime<=0) {
            lastTime = shootInterval;
            CreateBullet();
        }
    }

    private void Update()
    {
        if (lastTime > 0)
        {
            lastTime -= Time.deltaTime;
        }
    }

    private void CreateBullet() {
        if (bulletPrefab) {
            Bullet bullet= GameObjectPool.Instance.GetOneBullet();
            bullet.transform.position = shootPos.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, dir.x>0?0:180);
            bullet.caster = attribute;
        }
    }

    public void SetDir(float x,float y)
    {
        this.dir.x = x;
        this.dir.y = y;
    }
}
