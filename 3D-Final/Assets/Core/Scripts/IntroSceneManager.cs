using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    [Header("Time")]
    public float at;
    public float bt;
    public float bt1;
    public float bt2;
    public float bt3;
    public float bt4;
    public float bt5;

    [Header("Text")]
    public GameObject a;
    public GameObject b;
    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public GameObject b4;
    public GameObject b5;

    public Button skipBtn;

    private void Start()
    {
        skipBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.GetComponent<AudioSource>().Play();
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scenes.NewMain.ToString());
        });
        AudioManager.Instance.GetComponent<AudioSource>().Stop();
        StartCoroutine(IntroVideo());
    }

    IEnumerator IntroVideo()
    {
        a.SetActive(true);
        b.SetActive(false);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(false);
        b5.SetActive(false);
        yield return new WaitForSeconds(at);
        a.SetActive(false);
        b.SetActive(true);
        b1.SetActive(true);
        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(false);
        b5.SetActive(false);
        yield return new WaitForSeconds(bt);
        a.SetActive(false);
        b.SetActive(true);
        b1.SetActive(false);
        b2.SetActive(true);
        b3.SetActive(false);
        b4.SetActive(false);
        b5.SetActive(false);
        yield return new WaitForSeconds(bt2);
        a.SetActive(false);
        b.SetActive(true);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(true);
        b4.SetActive(false);
        b5.SetActive(false);
        yield return new WaitForSeconds(bt3);
        a.SetActive(false);
        b.SetActive(true);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(true);
        b5.SetActive(false);
        yield return new WaitForSeconds(bt4);
        a.SetActive(false);
        b.SetActive(true);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(false);
        b5.SetActive(true);
        yield return new WaitForSeconds(bt5);
        AudioManager.Instance.GetComponent<AudioSource>().Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scenes.NewMain.ToString());
    }
}
