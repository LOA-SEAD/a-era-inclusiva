using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hall : MonoBehaviour
{
    public string LibrarySceneName;
    public string ClassroomSceneName;
    public string StaffroomSceneName;
    public string SRMSceneName;

    public void LoadLibraryScene() {
        SceneManager.LoadScene(LibrarySceneName);
    }

    public void LoadClassroomScene() {
        SceneManager.LoadScene(ClassroomSceneName);
    }

    public void LoadStaffroomScene() {
        SceneManager.LoadScene(StaffroomSceneName);
    }

    public void LoadSRMScene() {
        SceneManager.LoadScene(SRMSceneName);
    }
}
