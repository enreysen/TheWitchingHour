using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public GameObject first_menu_visual;
    public GameObject second_menu_visual;
    private bool first_menu = true;
    private bool second_menu = false;
    public string scene_name;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (first_menu){
                first_menu = false;
                second_menu = true;
                first_menu_visual.SetActive(false);
                second_menu_visual.SetActive(true);
            }
            else SceneManager.LoadScene(scene_name);

        }
    }
}   