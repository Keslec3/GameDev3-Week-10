using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco.utilities
{
    public class ParticleSystemAutoDistructor : MonoBehaviour
    {
        private ParticleSystem system;

        void Update()
        {
            if (system == null)
            {
                system = GetComponent<ParticleSystem>(); // Get the ParticleSystem if not already set
            }

            if (system != null && !system.IsAlive(true)) // Check if the particle system has stopped
            {
                Destroy(gameObject); // Destroy the GameObject when the particle system is done
            }
        }
    }
}
