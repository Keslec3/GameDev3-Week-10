using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevWithMarco.Managers
{
    public class VariablesSetter : MonoBehaviour
    {
        [SerializeField] Animator transitionAnim;

        private void Awake()
        {
            // Sets the fade variable
            MyScenemanager.Instance.transitionAnim = transitionAnim;

            if (SceneManager.GetActiveScene().name == "scn_GameOver")
            {
                VfxManager.Instance.glitch.intensity = 0;
            }
        }
    }
}
