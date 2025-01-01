using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDevWithMarco.effects;
using Kino;

namespace GameDevWithMarco.Managers
{
    public class VfxManager : Singleton<VfxManager>
    {
        [Header("ScreenShake")]
        public Animator camAnim; // Reference to the camera's shake animation

        [Header("Particles")]
        public GameObject goodPickupParticles;
        public GameObject badPickupParticles;
        public Transform particleSpawnerPos;

        [Header("PlayerFeedback")]
        public GameObject subtractPointsPrompt;
        public GameObject addPointsPrompt;
        public Transform subtractPointsPromptPosition;
        public Transform addPointsPromptPosition;

        [Header("GlitchEffect")]
        public DigitalGlitch glitch;
        [SerializeField] float glitchEnd = 0.95f;
        [SerializeField] float glitchStageFour = 0.16f;
        [SerializeField] float glitchStageThree = 0.12f;
        [SerializeField] float glitchStageTwo = 0.08f;
        [SerializeField] float glitchStageOne = 0.04f;
        [SerializeField] float glitchZero = 0f;
        [SerializeField] float gameOverGlitchDelay = 0f;

        protected override void Awake()
        {
            base.Awake();

            // Apply effects specific to the Game Over scene
            if (SceneManager.GetActiveScene().name == "scn_GameOver")
            {
                GameOverSceneEffects();
                gameOverGlitchDelay = Mathf.Clamp(gameOverGlitchDelay, 0, 1);
            }
        }

        private void Update()
        {
            if (glitch == null) return;

            // Trigger glitch effect based on scene
            if (SceneManager.GetActiveScene().name == "scn_Level1")
            {
                GlitchWhenFailing();
            }

            if (SceneManager.GetActiveScene().name == "scn_GameOver")
            {
                gameOverGlitchDelay += 0.05f * Time.deltaTime;
                glitch.intensity -= gameOverGlitchDelay;
                if (glitch.intensity <= glitchStageOne)
                {
                    glitch.intensity = glitchStageOne;
                }
            }
        }

        public void CamShake()
        {
            if (camAnim != null)
            {
                camAnim.SetTrigger("shake"); // Trigger camera shake animation
            }
            else
            {
                Debug.LogError("Camera Animator is null.");
            }
        }

        public void GoodPickupParticles()
        {
            PickupParticles(goodPickupParticles);
        }

        private void PickupParticles(GameObject particles)
        {
            if (particles == null || particleSpawnerPos == null)
            {
                Debug.LogError("Particles or ParticleSpawnerPos is null.");
                return;
            }

            // Spawn and destroy particles
            var explosion = Instantiate(particles, particleSpawnerPos.position, Quaternion.identity);
            Destroy(explosion, 0.8f);
        }

        public void BadPickupParticles()
        {
            PickupParticles(badPickupParticles);
        }

        public void AddPointsPromptMethod()
        {
            var canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (addPointsPrompt == null || addPointsPromptPosition == null || canvas == null)
            {
                Debug.LogError("Add Points Prompt dependencies are missing.");
                return;
            }

            // Show points prompt
            var addPoints = Instantiate(addPointsPrompt, addPointsPromptPosition.position, Quaternion.identity);
            addPoints.transform.SetParent(canvas.transform, false);
            addPoints.transform.position = addPointsPromptPosition.position;
            Destroy(addPoints, 0.8f);
        }

        public void SubtractPointsPromptMethod()
        {
            var canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (subtractPointsPrompt == null || subtractPointsPromptPosition == null || canvas == null)
            {
                Debug.LogError("Subtract Points Prompt dependencies are missing.");
                return;
            }

            // Show points deduction prompt
            var subtractPoints = Instantiate(subtractPointsPrompt, subtractPointsPromptPosition.position, Quaternion.identity);
            subtractPoints.transform.SetParent(canvas.transform, false);
            subtractPoints.transform.position = subtractPointsPromptPosition.position;
            Destroy(subtractPoints, 0.8f);
        }

        public void GlitchWhenFailing()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager instance is null.");
                return;
            }

            // Adjust glitch intensity based on lives
            switch (GameManager.Instance.lives)
            {
                case 5: glitch.intensity = glitchZero; break;
                case 4: glitch.intensity = Mathf.Lerp(glitchZero, glitchStageOne, 1); break;
                case 3: glitch.intensity = Mathf.Lerp(glitchStageOne, glitchStageTwo, 1); break;
                case 2: glitch.intensity = Mathf.Lerp(glitchStageTwo, glitchStageThree, 1); break;
                case 1: glitch.intensity = Mathf.Lerp(glitchStageThree, glitchStageFour, 1); break;
                case 0: glitch.intensity = Mathf.Lerp(glitchStageFour, glitchEnd, 0.2f); break;
            }
        }

        private void GameOverSceneEffects()
        {
            // Set glitch intensity for Game Over scene
            if (glitch != null)
            {
                glitch.intensity = glitchEnd;
            }
            else
            {
                Debug.LogError("Glitch effect is null.");
            }
        }
    }
}
