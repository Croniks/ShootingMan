using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class Settings : ScriptableObject
{
    [Range(1, 100)] public float bulletPower;
    [Range(20, 80)] public float bulletSpeed;
    [Range(10, 50)] public float bulletHittingRadius;
    [Range(5, 20)] public float playerSpeed;
    [Range(1, 10)] public int fireSpeed;
}