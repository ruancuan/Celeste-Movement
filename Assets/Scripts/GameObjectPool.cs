using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameObjectPoolType { 
    Bullet
}
public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    public GameObject bulletPrefab;

    protected override void Init()
    {
        base.Init();
        bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet");
    }

    //public Stack<Bullet> bulletPool = new Stack<Bullet>();
    private Dictionary<GameObjectPoolType, Stack<GameObject>> objectPool = new Dictionary<GameObjectPoolType, Stack<GameObject>>();

    private Stack<T> InitObjectPool<T>(GameObjectPoolType type)
    {
        Stack<T> bulletPool = new Stack<T>();
        objectPool.Add(type, bulletPool as Stack<GameObject>);
        return bulletPool;
    }
    public Bullet GetOneBullet() {
        Bullet b;
        Stack<GameObject> bulletPool;
        bool v = objectPool.TryGetValue(GameObjectPoolType.Bullet, out bulletPool);
        if (bulletPool == null)
        {
            bulletPool = InitObjectPool<GameObject>(GameObjectPoolType.Bullet);
        }

        if (bulletPool.Count > 0) {
            Bullet go =bulletPool.Pop().GetComponent<Bullet>();
            if (go != null)
            {
                go.enabled = true;
                go.gameObject.SetActive(true);
            }
            else {
                LogManager.Instance.LogError("object deletion component");
            }
            return go;
        }

        GameObject bullet = Instantiate(bulletPrefab);
        b = bullet.GetComponent<Bullet>();
        if (b == null)
        {
            b = bullet.AddComponent<Bullet>();
        }
        return b;
    }

    public void PushOneBullet(Bullet go)
    {
        Stack<GameObject> bulletPool;
        objectPool.TryGetValue(GameObjectPoolType.Bullet, out bulletPool);
        if (bulletPool == null)
        {
            bulletPool = InitObjectPool<GameObject>(GameObjectPoolType.Bullet);
        }

        bulletPool.Push(go.gameObject);

        go.enabled = false;
        go.gameObject.SetActive(false);
    }
}
