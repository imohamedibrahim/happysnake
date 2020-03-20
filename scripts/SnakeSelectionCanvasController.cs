using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSelectionCanvasController : MonoBehaviour
{
    [SerializeField]
    private MenuChanger menuChanger;
    
    public void goToMenu()
    {
        menuChanger.goToMainMenu();
    }
}
