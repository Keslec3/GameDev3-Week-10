using UnityEngine;

namespace GameDevWithMarco.effects
{
    public class Ripple : MonoBehaviour
    {
        public Material RippleMaterial;
        public float MaxAmount = 50f;
        [Range(0, 1)] public float Friction = .9f;
        private float Amount = 0f;

        public Transform ripplePos;
        private Vector3 pos;
        private Camera cam;

        private void Start()
        {
            cam = GetComponent<Camera>();
            pos = cam.WorldToScreenPoint(ripplePos.position); // Set initial ripple position
        }

        void Update()
        {
            pos = cam.WorldToScreenPoint(ripplePos.position); // Update position
            RippleMaterial.SetFloat("_Amount", Amount);
            Amount *= Friction; // Fade ripple effect
        }

        public void RippleReaction()
        {
            Amount = MaxAmount; // Trigger ripple
            RippleMaterial.SetFloat("_CenterX", pos.x);
            RippleMaterial.SetFloat("_CenterY", pos.y);
        }

        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            Graphics.Blit(src, dst, RippleMaterial); // Apply ripple effect
        }
    }
}
