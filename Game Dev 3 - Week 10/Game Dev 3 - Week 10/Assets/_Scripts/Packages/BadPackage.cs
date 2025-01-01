using UnityEngine;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class BadPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] private GameEvent badPackageCollected; // Event triggered when this package is collected

        public void CollidedLogic()
        {
            badPackageCollected.Raise(); // Trigger the event
        }
    }
}
