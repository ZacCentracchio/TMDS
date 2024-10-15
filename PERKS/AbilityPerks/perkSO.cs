using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perkSO : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float maxUses;

    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent) { }
}
