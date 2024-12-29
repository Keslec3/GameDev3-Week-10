using UnityEngine;

namespace GameDevWithMarco.DataSO
{
    [CreateAssetMenu(fileName = "New Sound Data", menuName = "Scriptable Objects/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public float lowPitchRange;
        public float highPitchRange;
        public AudioClip clipToUse;
        public float volume;
    }
}

