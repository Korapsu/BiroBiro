using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
#if UNITY_EDITOR
    [CustomEditor(typeof(Menu))]
    public class MenuEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("resetPoints")){
                print(Player.points);
                SaveSystem.Save(0);
            }

            base.OnInspectorGUI();
        }
    }
#endif
}
