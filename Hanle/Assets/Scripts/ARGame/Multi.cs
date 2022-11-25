using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class Multi : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        //이미지 인식 델리게이트에 실행될 함수를 연결한다.
        imageManager.trackedImagesChanged += OnTrackedImage;
    }
    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        //새로 인식한 이미지들을 모두 순회한다.
        foreach (ARTrackedImage trackedImage in args.added)
        {
            //이미지 라이브러리에서 인식한 이미지의 이름을 읽어온다.
            string imageName = trackedImage.referenceImage.name;

            //Resources 폴더에서 인식한 이미지의 이름과 동일한 이름의 프리팹을 찾음
            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            //검색된 프리팹이 존재하면
            if (imagePrefab != null)
            {
                //이미지에 등록된 자식 오브젝트가 없으면
                if (trackedImage.transform.childCount < 1)
                {
                    // 이미지의 위치에 프리팹을 생성하고 이미지의 자식 오브젝트로 등록한다.
                    GameObject go = Instantiate(imagePrefab, trackedImage.transform.position,
                        trackedImage.transform.rotation);
                    go.transform.SetParent(trackedImage.transform);
                }
            }

        }
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            //이미지에 등록된 자식 오브젝트가 있다면...
            if (trackedImage.transform.childCount > 0)
            {
                //자식 오브젝트의 위치를 이미지의 위치와 동기화한다.
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
            }
        }
    }

}