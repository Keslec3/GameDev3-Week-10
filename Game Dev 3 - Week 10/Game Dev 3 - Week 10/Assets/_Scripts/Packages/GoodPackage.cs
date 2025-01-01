using UnityEngine;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class GoodPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] private GameEvent goodPackageCollected; // Event triggered when this package is collected

        public void CollidedLogic()
        {
            goodPackageCollected.Raise(); // Trigger the event
        }
    }
}
