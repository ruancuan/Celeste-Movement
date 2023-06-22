using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Execute();
    public void SetDir(float x,float y);
    public void PlayShootAnimtion();
}
