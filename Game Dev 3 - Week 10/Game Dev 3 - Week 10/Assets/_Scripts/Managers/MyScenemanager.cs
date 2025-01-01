using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevWithMarco.Managers
{
    public class MyScenemanager : Singleton<MyScenemanager>
    {
        public Animator transitionAnim;
        private string gameLevel = "scn_Level1"; // Name of the main game level
        private string gameOver = "scn_GameOver"; // Name of the game over scene

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "scn_MainMenu")
            {
                StartCoroutine(WaitAndLoadNewScene()); // Trigger scene load from main menu
            }
        }

        IEnumerator WaitAndLoadNewScene()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transitionAnim.SetTrigger("end"); // Play transition animation
                yield return new WaitForSeconds(1.5f); // Wait for animation to finish
                SceneManager.LoadScene(gameLevel); // Load the main game level
            }
        }

        public void GameOverReaction()
        {
            StartCoroutine(GameOver()); // Trigger game over scene
        }

        IEnumerator GameOver()
        {
            transitionAnim.SetTrigger("end"); // Play transition animation
            yield return new WaitForSeconds(1.5f); // Wait for animation to finish
            SceneManager.LoadScene(gameOver); // Load the game over scene
        }

        public void RestartGameReaction()
        {
            StartCoroutine(RestartGame()); // Restart the game
        }

        IEnumerator RestartGame()
        {
            transitionAnim.SetTrigger("end"); // Play transition animation
            yield return new WaitForSeconds(1.5f); // Wait for animation to finish
            SceneManager.LoadScene(gameLevel); // Reload the game level
            GameManager.Instance.RestartGame(); // Reset game variables
        }
    }
}
