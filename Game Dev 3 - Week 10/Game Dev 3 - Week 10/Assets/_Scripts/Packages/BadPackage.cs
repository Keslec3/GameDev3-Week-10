using UnityEngine;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class BadPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] private GameEvent badPackageCollected;

        public void CollidedLogic()
        {
            badPackageCollected.Raise();
        }
    }
}
