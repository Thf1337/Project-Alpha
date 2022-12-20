using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Level To Load")] 
        public string newGameLevel;
        private string _levelToLoad;

        public void NewGame()
        {
            SceneManager.LoadScene(newGameLevel);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
