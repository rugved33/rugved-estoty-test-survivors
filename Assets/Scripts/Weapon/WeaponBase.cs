
using UnityEngine;

namespace SurvivorGame
{
    public abstract class WeaponBase : MonoBehaviour , IWeapon
    {
        public abstract void Attack();
    }
}
