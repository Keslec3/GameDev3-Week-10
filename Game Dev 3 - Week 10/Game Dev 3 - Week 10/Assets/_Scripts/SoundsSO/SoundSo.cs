using UnityEngine;

namespace GameDevWithMarco.DataSO
{
    [CreateAssetMenu(fileName = "New Sound Data", menuName = "Scriptable Objects/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public float lowPitchRange; // Minimum pitch for sound variations
        public float highPitchRange; // Maximum pitch for sound variations
        public AudioClip clipToUse; // The audio clip to play
        public float volume; // Volume level for the sound
    }
}
