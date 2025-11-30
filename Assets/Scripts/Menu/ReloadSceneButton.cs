using UnityEngine.SceneManagement;

namespace Menu
{

    public sealed class ReloadSceneButton : ButtonBase
    {

        protected override void Click() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
