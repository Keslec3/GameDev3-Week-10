using UnityEngine;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class GoodPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] private GameEvent goodPackageCollected;

        public void CollidedLogic()
        {
            goodPackageCollected.Raise();
        }
    }
}
