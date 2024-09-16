using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PokeballManager : MonoBehaviour
{
    public GameObject Pointer;  //화살표 이미지 UI 게임오브젝트
    public GameObject Pokeball; //몬스터볼 게임오브젝트

    public Animator PokeballAnimator;   //몬스터볼 애니메이터
    
    bool _isPokeballDestroyed;  //몬스터볼 파괴확인

    public UIManager UIManager; //UIManager 스크립트
    public AudioManager AudioManager;   //AudioManager 스크립트

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;

            Vector3 newTouchPos = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(newTouchPos);

            Debug.DrawRay(transform.position, (touchWorldPosition - transform.position).normalized * 1000, Color.red, 5);

            if(Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.collider.tag == "Pokeball")
                {                    
                    Pointer.gameObject.SetActive(false);
                    PokeballAnimator.SetTrigger("Activate");
                    UIManager.FaderAnimator.SetTrigger("Activate");
                }

                PokemonHit(hitInfo);
            }
        }

        //몬스터볼 파괴되지 않았을 경우, 몬스터볼 애니메이션 종료 시 몬스터볼 파괴
        if (!_isPokeballDestroyed)
        {
            if (PokeballAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pokeball") && PokeballAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                _isPokeballDestroyed = true;
            }
        }

        if(_isPokeballDestroyed)
        {
            Destroy(Pokeball);
        }
    }

    //RaycastHit에 닿은 포켓몬 울음소리 재생(AuidoManager의 각 포켓몬 Boolean 활성)
    void PokemonHit(RaycastHit hit)
    {
        if(hit.collider.tag == "Bulbasaur")
        {
            AudioManager.IsBulbasaurHit = true;
        }

        if (hit.collider.tag == "Charmander")
        {
            AudioManager.IsCharmanderHit = true;
        }

        if (hit.collider.tag == "Squirtle")
        {
            AudioManager.IsSquirtleHit = true;
        }

        if (hit.collider.tag == "Vaporeon")
        {
            AudioManager.IsVaporeonHit = true;
        }

        if (hit.collider.tag == "Jolteon")
        {
            AudioManager.IsJolteonHit = true;
        }

        if (hit.collider.tag == "Flareon")
        {
            AudioManager.IsFlareonHit = true;
        }

        if (hit.collider.tag == "Snorlax")
        {
            AudioManager.IsSnorlaxHit = true;
        }

        if(hit.collider.tag == "Articuno")
        {
            AudioManager.IsArticunoHit = true;
        }

        if(hit.collider.tag == "Zapdos")
        {
            AudioManager.IsZapdosHit = true;
        }

        if(hit.collider.tag == "Moltres")
        {
            AudioManager.IsMoltresHit = true;
        }

        if(hit.collider.tag == "Mew")
        {
            AudioManager.IsMewHit = true;
        }

        if(hit.collider.tag == "IdlePikachu")
        {
            AudioManager.IsIdlePikachuHit = true;
        }
    }
}
