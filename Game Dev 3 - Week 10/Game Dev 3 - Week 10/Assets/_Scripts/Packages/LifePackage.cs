using UnityEngine;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class LifePackage : MonoBehaviour, ICollidable
    {
        [SerializeField] private GameEvent lifePackageCollected; // Event triggered when this package is collected

        public void CollidedLogic()
        {
            lifePackageCollected.Raise(); // Trigger the event
        }
    }
}
