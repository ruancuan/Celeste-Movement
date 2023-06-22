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
            bullet.transform.rotation = AddRandomEuler();
            bullet.caster = attribute;

            PlayShootAnimtion();
            PlayShootAudio();
            AddForceToSelf();
        }
    }

    private Quaternion AddRandomEuler() {
        float maxValue = Mathf.Max(Mathf.Abs(randomEuler.x), Mathf.Abs(randomEuler.y));
        float randomValue = Random.value * maxValue;
        randomValue = Mathf.Clamp(randomValue,randomEuler.x, randomEuler.y);
        return Quaternion.Euler(0, 0, (dir.x > 0 ? 0 : 180)+ randomValue); 
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
