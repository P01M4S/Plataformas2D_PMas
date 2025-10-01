using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
