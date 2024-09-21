using System;
using UnityEngine;

public interface IDamagable
{
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }

    public void TakeDamageFrom(int damage, Vector3 fromWorldPoint, int effectMultiplyer = 1);

    public Action Died { set; }
}