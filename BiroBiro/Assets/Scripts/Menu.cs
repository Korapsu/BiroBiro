using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    private void Start()
    {
        Text.text = $"MaxPoints\n{SaveSystem.load():0}";
    }
    public void ToScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
