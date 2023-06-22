using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R99 : MonoBehaviour,IWeapon
{
    public Attribute attribute;
    public GameObject bulletPrefab;
    public GameObject cartridgeCasePrefab;

    public float shootInterval=1f;
    public int createBulletNum = 3;
    public Transform shootPos;
    public Vector2 dir = new Vector2(1, 0);
    public ParticleSystem shootParticle;
    public Vector2 randomEuler = new Vector2(-10, 10);


    private MotionModifier motionModifier;
    private float lastTime = 0f;
    private void Awake()
    {
        attribute = GetComponent<Attribute>();
        motionModifier = GetComponent<MotionModifier>();
    }
    public void Execute()
    {
        if(lastTime<=0) {
            lastTime = shootInterval;
            CreateBullet();
            CreateCartridgeCase();
        }
    }

    private void Update()
    {
        if (lastTime > 0)
        {
            lastTime -= Time.deltaTime;
        }
    }

    public Vector2 rightShootCartridgeCaseDir = new Vector2(1, 1);
    public Vector2 leftShootCartridgeCaseDir = new Vector2(1, 1);
    private void CreateCartridgeCase()
    {
        if (cartridgeCasePrefab)
        {
            GameObject bullet = Instantiate(cartridgeCasePrefab);
            bullet.transform.position = shootPos.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
            if (rig == null) {
                rig = bullet.AddComponent<Rigidbody2D>();
                if (dir.x > 0)
                {
                    rig.AddForce(leftShootCartridgeCaseDir,ForceMode2D.Force);
                }
                else {
                    rig.AddForce(rightShootCartridgeCaseDir, ForceMode2D.Force);
                }
            }

        }
    }

    private void CreateBullet() {
        if (bulletPrefab)
        {
            for (int k = 0; k < createBulletNum; k++)
            {
                Bullet bullet = GameObjectPool.Instance.GetOneBullet();
                bullet.transform.position = shootPos.transform.position;
                bullet.transform.rotation = AddRandomEuler(k);
                bullet.caster = attribute;
            }

            PlayShootAnimtion();
            PlayShootAudio();
            AddForceToSelf();
        }
    }

    private Quaternion AddRandomEuler(int k) {
        float maxValue = Mathf.Max(Mathf.Abs(randomEuler.x), Mathf.Abs(randomEuler.y));
        float randomValue = Random.value * maxValue;
        randomValue = Mathf.Clamp(randomValue,randomEuler.x, randomEuler.y);
        return Quaternion.Euler(0, 0, (dir.x > 0 ? 0 : 180)+ randomValue+k*5-5); 
    }

    private void PlayShootAudio() {
        AudioManager.Instance.PlayShootAudio();
    }

    private void AddForceToSelf() {
        if (motionModifier) {
            motionModifier.SetMotionDir(-dir);
            motionModifier.OnBuffExecute(attribute,attribute);
        }
    }

    public void PlayShootAnimtion() {
        if (shootParticle) {
            shootParticle.Play();
        }
    }

    public void SetDir(float x,float y)
    {
        this.dir.x = x;
        this.dir.y = y;
    }
}
