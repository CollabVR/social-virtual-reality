using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUserInterface : MonoBehaviour
{
    public GameObject interactUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showInteractUI(bool show)
    {
        interactUI.SetActive(show);
    }

}
