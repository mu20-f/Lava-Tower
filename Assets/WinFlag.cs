using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinFlag : MonoBehaviour
{
    [SerializeField] Animator animLoader;
    // Start is called before the first frame update
    public void PLayScene()
    {

        StartCoroutine("LoadLevel");

    }
    IEnumerator LoadLevel()
    {

        animLoader.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("WinScene");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PLayScene();
    }
}
