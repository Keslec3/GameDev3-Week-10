using UnityEngine;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class LifePackage : MonoBehaviour, ICollidable
    {
        [SerializeField] private GameEvent lifePackageCollected;

        public void CollidedLogic()
        {
            lifePackageCollected.Raise();
        }
    }
}
