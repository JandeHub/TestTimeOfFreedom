using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public int health;

    public int maxHealth;

    public abstract void RestHealth(int restHealthValue);
}