using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChungjuWall.UI {
    /// <summary>
    ///     게임 클리어 시 보여질 UI를 관리하는 클래스
    /// </summary>
    public class GameClearUI : MonoBehaviour {
        void Start() {
            SceneManager.LoadScene("EP2 Story End Scene");
        }
    }
}