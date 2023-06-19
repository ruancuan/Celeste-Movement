using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    public GameObject bulletPrefab;
    public Stack<Bullet> bulletPool = new Stack<Bullet>();

    public Bullet GetOneBullet() {
        Bullet b;
        if (bulletPool.Count > 0) {
            Bullet go =bulletPool.Pop();
            go.enabled = true;
            go.gameObject.SetActive(true);

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

    public void PushOneBullet(Bullet go) {
        bulletPool.Push(go);
        go.enabled = false;
        go.gameObject.SetActive(false);
    }
}
