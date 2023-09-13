using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] bool MaxOrNew;
    private void Start()
    {
        Text.text = MaxOrNew? $"MaxPoints\n{SaveSystem.load():0}" : $"Points:{Player.points:0}";
    }
    public void ToScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
