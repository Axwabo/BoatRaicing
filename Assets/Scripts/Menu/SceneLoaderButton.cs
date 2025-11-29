using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{

    public sealed class SceneLoaderButton : ButtonBase
    {

        [SerializeField]
        private string sceneName;

        protected override void Click() => SceneManager.LoadScene(sceneName);

    }

}
