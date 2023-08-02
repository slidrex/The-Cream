using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private int selectedID = -1;
    private InputListener _manager;
    public void SceneID(int id)
    {
        selectedID = id;    }

    public void LoadScene()
    {
        if (selectedID >= 0)
            SceneManager.LoadScene(selectedID);
    }
}
