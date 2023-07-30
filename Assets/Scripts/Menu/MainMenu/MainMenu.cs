using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private int selectedID = -1;
 
    public void SceneID(int id)
    {
        selectedID = id;
    }
    public void LoadScene()
    {
        if(selectedID >= 0)
            SceneManager.LoadScene(selectedID);
    }
}
