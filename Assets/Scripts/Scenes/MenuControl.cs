using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject settingmenu;
    [SerializeField] private float alphaSpeed = 1.0f;
    private bool isFade = false;
    private CanvasGroup canvasGroup;
    public AudioSource button;
    
    void Start()
    {
        canvasGroup=settingmenu.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (isFade)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }
    private void FadeOut()
    {
        canvasGroup.alpha-=alphaSpeed*Time.deltaTime;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void FadeIn()
    {
        canvasGroup.alpha += alphaSpeed * Time.deltaTime;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnClickBtnActiveSetting()
    {
        button.Play();
        isFade=!isFade;
    }
    public void OnClickBtnNextScene()
    {
        button.Play();
        SceneManager.LoadScene(1);
    }
    
    public void OnClickBtnQuitGame()
    {
        button.Play();
        Debug.Log("ÍË³öÓÎÏ·");
        Application.Quit();
    }
}
