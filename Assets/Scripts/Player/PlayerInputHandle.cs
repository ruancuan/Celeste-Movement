using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    public Transform cameraFollow;
    public float offset=2f;
    public KeyCode shootKey = KeyCode.Mouse1;
    public IWeapon weapon;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<R99>();
        leftPosition = new Vector3(-offset, -2, 0);
        rightPosition = new Vector3(offset, -2, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shootKey)) {
            UseWeapon();
        }
        float xRaw = Input.GetAxisRaw("Horizontal");
        if (xRaw != 0 && weapon!=null) {
            weapon.SetDir(xRaw, 0);
            if (cameraFollow != null) {
                if (xRaw > 0)
                {
                    cameraFollow.transform.position = rightPosition;    
                }
                else
                {
                    cameraFollow.transform.position = leftPosition;
                }
            }
        }
    }



    private void UseWeapon() {
        if (weapon!=null) {
            weapon.Execute();
        }
    }
}
