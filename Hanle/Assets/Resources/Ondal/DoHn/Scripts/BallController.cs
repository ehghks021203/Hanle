using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] OndalManager ondalManager;
    [SerializeField] GameObject effect;

    public float resetTime = 3.0f;
    public float captureRate = 0.3f;

    Rigidbody rb;
    bool isReady = true;
    Vector2 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReady) {
            return;
        }
        SetBallPosition(Camera.main.transform);

#region ThrowBall_inEditor
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0)) {
                startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0)) {
                float dragDistance = Input.mousePosition.y - startPos.y;

                // AR 카메라를 기준으로 던질 방향(전방 45도 위쪽)을 설정
                Vector3 throwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                // 물리 능력을 활성화하고 준비 상태를 False로 바꾼다.
                rb.isKinematic = false;
                isReady = false;

                // 던질 방향 * 손가락 드래그 거리만큼 공에 물리적 힘을 가한다.
                rb.AddForce(throwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);

                // 3초 후의 공의 위치 및 속도를 초기화한다.
                Invoke("ResetBall", resetTime);
            }
            return;
        }
#endregion
        
#region ThrowBall_inMobile
        if (Input.touchCount > 0 && isReady) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                startPos = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended) {
                float dragDistance = touch.position.y - startPos.y;

                Vector3 throwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                rb.isKinematic = false;
                isReady = false;

                rb.AddForce(throwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);
                Invoke("ResetBall", resetTime);
            }
        }
#endregion
    }

    void SetBallPosition(Transform anchor) {
        Vector3 offset = anchor.forward * 0.5f + anchor.up * -0.2f;

        transform.position = anchor.position + offset;
    }

    private void OnCollisionEnter(Collision collision) {
        if (isReady) {
            return;
        }
        Instantiate(effect, collision.transform.position, Camera.main.transform.rotation);

        var res = collision.gameObject.GetComponent<Enemy>().DecreaseHealth();
        if (res == -1) {
            ondalManager.totalEnemy--;
        }
        ResetBall();
        SetBallPosition(Camera.main.transform);
    }

    private void ResetBall() {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        isReady = true;

        gameObject.SetActive(true);
    }
}
