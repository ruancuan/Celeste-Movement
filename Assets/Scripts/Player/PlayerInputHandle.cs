using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Mouse1;
    public IWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<R99>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shootKey)) {
            UseWeapon();
        }
    }

    private void UseWeapon() {
        if (weapon!=null) {
            weapon.Execute();
        }
    }
}
