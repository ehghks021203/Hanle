using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChungjuWall.UI {
    /// <summary>
    ///     ���� Ŭ���� �� ������ UI�� �����ϴ� Ŭ����
    /// </summary>
    public class GameClearUI : MonoBehaviour {
        void Start() {
            SceneManager.LoadScene("EP2 Story End Scene");
        }
    }
}