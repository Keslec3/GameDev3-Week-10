using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.Managers;
using GameDevWithMarco.effects;

namespace GameDevWithMarco.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public int score;
        public float timeLeft = 60f;
        public int[] packageValues = new int[] { 12345, -5434 }; // Positive and negative package values
        public int successRate;
        public int lives = 5;
        public float playTime = 0;

        [SerializeField] GameEvent restartGame;
        [SerializeField] GameEvent gameOver;

        protected override void Awake()
        {
            base.Awake();
            Initialisation(); // Set initial values
        }

        void Update()
        {
            ValuesClamping(); // Keep values within allowed ranges
            TimeGoingDown(); // Decrease time left in level

            if (SceneManager.GetActiveScene().name == "scn_Level1")
            {
                StartCounting(); // Track playtime
            }
            else if (SceneManager.GetActiveScene().name == "scn_GameOver" && Input.anyKeyDown)
            {
                restartGame.Raise(); // Restart game on key press
            }
        }

        private void Initialisation()
        {
            playTime = 0f;
            score = 0;
            successRate = 0;
        }

        private void TimeGoingDown()
        {
            if (SceneManager.GetActiveScene().name == "scn_MainMenu" || 
                SceneManager.GetActiveScene().name == "scn_GameOver") return;

            timeLeft -= Time.deltaTime; // Decrease time during gameplay
        }

        private void ValuesClamping()
        {
            score = Mathf.Clamp(score, 0, 100000000); // Prevent score overflow
            successRate = Mathf.Clamp(successRate, 0, 100);
            timeLeft = Mathf.Clamp(timeLeft, 0, 120);
            lives = Mathf.Clamp(lives, 0, 10);
        }

        private void StartCounting()
        {
            playTime += Time.deltaTime; // Increment playtime
        }

        public void GreenPackLogic()
        {
            score += packageValues[0]; // Increase score
            successRate++;
        }

        public void RedPackLogic()
        {
            // Decrease score or reset it
            if (score >= 543)
            {
                score += packageValues[1];
            }
            else
            {
                score = 0;
            }
            successRate -= 3;
            lives--; // Decrease lives
            if (lives <= 0)
            {
                gameOver.Raise(); // Trigger Game Over event
            }
        }

        public void LifePackageLogic()
        {
            lives++; // Add a life
        }

        public void RestartGame()
        {
            // Reset all gameplay values
            score = 0;
            successRate = 0;
            lives = 5;
            playTime = 0f;
        }
    }
}
