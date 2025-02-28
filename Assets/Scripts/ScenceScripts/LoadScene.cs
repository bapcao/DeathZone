using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AutoTransitionToScene2());
    }

    IEnumerator AutoTransitionToScene2()
    {
        yield return new WaitForSeconds(55f);
        SceneManager.LoadScene(2);
    }
}