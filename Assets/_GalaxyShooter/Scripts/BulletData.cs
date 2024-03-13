using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public float speed = 10;
    public int damage = 1;
}
