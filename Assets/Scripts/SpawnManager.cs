using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnManager : MonoBehaviour
{
    public UIManager UIManager; //UIManager 스크립트
    public AudioManager AudioManager;   //AudioManager 스크립트

    public Transform XROriginPos;   //XR Origin 위치
    public ARPlaneManager ARPlaneManager;   //ARPlaneManager 컴포넌트    

    public GameObject PikachuPrefab;  //피카츄 프리팹
    public GameObject EeveePrefab;    //이브이 프리팹

    //스폰된 피카츄 & 이브이 게임오브젝트
    public GameObject Pika;
    public GameObject Eev;

    Animator _pikachuAnimator;  //피카츄 애니메이터
    Animator _eeveeAnimator;    //이브이 애니메이터
    
    bool _spawnPE;  //피카츄 & 이브이 스폰확인
    
    bool _destroyPE;    //피카츄 & 이브이 파괴

    public GameObject MapPrefab;
    public bool IsMapSpawned;

    [Header("AZM Related")]
    float _azmMoveSpeed = 10.0f;    //프리져, 썬더, 파이어 이동속도

    //프리져 & 썬더 & 파이어 프리팹
    public GameObject ArticunoPrefab;
    public GameObject ZapdosPrefab;
    public GameObject MoltresPrefab;

    //스폰된 프리져 & 썬더 & 파이어 게입오브젝트
    public GameObject Art;
    public GameObject Zap;
    public GameObject Mol;

    //프리져 & 썬더 & 파이어 스폰확인
    bool _isArticunoSpawned;
    bool _isZapdosSpawned;
    bool _isMoltresSpawned;
    
    float _azmSpawnTimer;   //프리져 & 썬더 & 파이어 스폰타이머

    [Header("Oddish Related")]
    public GameObject OddishPrefab; //뚜벅초 프리팹

    //스폰된 뚜벅초1,2,3 게임오브젝트
    public GameObject Odd1;
    public GameObject Odd2;
    public GameObject Odd3;

    bool _isOddishSpawned;  //뚜벅초 스폰확인
    float _oddishMoveSpeed = 1.0f;  //뚜벅초 이동속도
   
    [Header("BCS Related")]
    //이상해씨 & 파이리 & 꼬부기 프리팹
    public GameObject BulbasaurPrefab;
    public GameObject CharmanderPrefab;
    public GameObject SquirtlePrefab;

    //스폰된 이상해씨 & 파이리 & 꼬부기 게임오브젝트
    public GameObject Bulb;
    public GameObject Char;
    public GameObject Squi;

    //이상해씨 & 파이리 & 꼬부기 스폰확인
    bool _isBulbasaurSpawned;
    bool _isCharmanderSpawned;
    bool _isSquirtleSpawned;

    float _bcsMoveSpeed = 60.0f;    //이상해씨 & 파이리 & 꼬부기 이동속도

    float _bcsCircleR = 0.05f;  //이상해씨 & 파이리 & 꼬부기 회전 반지름
    float _bDeg;    //이상해씨 각도
    float _cDeg;    //파이리 각도
    float _sDeg;    //꼬부기 각도

    [Header("Eeveelution Related")]
    //샤미드 & 쥬피썬더 & 부스터 프리팹
    public GameObject VaporeonPrefab;
    public GameObject JolteonPrefab;
    public GameObject FlareonPrefab;

    //스폰된 샤미드 & 쥬피썬더 & 부스터 게임오브젝트
    public GameObject Vap;
    public GameObject Jolt;
    public GameObject Fla;

    //샤미드 & 쥬피썬더 & 부스터 스폰확인
    bool _isVaporeonSpawned;
    bool _isJolteonSpawned;
    bool _isFlareonSpawned;

    float _eeveelutionMoveSpeed = 90.0f;    //샤미드 & 쥬피썬더 & 부스터 이동속도

    float _circleR = 0.1f;  //샤미드 & 쥬피썬더 & 부스터 회전 반지름
    float _vDeg;    //샤미드 각도
    float _jDeg;    //쥬피썬더 각도
    float _fDeg;    //부스터 각도

    [Header("Mew Related")]
    public GameObject MewPrefab;    //뮤 프리팹

    public GameObject Mew;  //스폰된 뮤 게임오브젝트

    Animator _mewAnimator;  //뮤 애니메이터

    bool _isMewSpawned; //뮤 스폰확인
    bool _isMewDestroyed;   //뮤 파괴확인

    float _mewSpawnTimer;   //뮤 스폰 타이머
    
    //Plane & ARRaycast Manager를 통해 스폰된 피카츄
    [Header("Idle Pikachu")]
    public GameObject IdlePikachuPrefab;    //Idle 피카츄 프리팹
    public GameObject IPikachu; //스폰된 Idle 피카츄 개임오브젝트

    public ARRaycastManager ARRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {         
        SpawnPE();

        if(_spawnPE)
        {
            if((_pikachuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Intro") && _pikachuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                && (_eeveeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Intro") && _eeveeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f))
            {
                UIManager.FaderAnimator.SetTrigger("Activate2");
            }

            if ((_pikachuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Intro") && _pikachuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                && (_eeveeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Intro") && _eeveeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f))
            {
                _spawnPE = false;
                _destroyPE = true;                
            }
        }

        if(_destroyPE)
        {
            _destroyPE = false;

            Destroy(_pikachuAnimator.gameObject);
            Destroy(_eeveeAnimator.gameObject);                        
        }

        SpawnMap();

        //AZM Related Functions
        SpawnAZM();
        TranslateDestroyAZM();

        //Oddish Related Functions
        SpawnOddish();
        TranslateDestroyOddish();

        //BCS Related Functions
        SpawnBCS();
        TranslateBCS();

        //Eeveelution Related Functions
        SpawnEeveelution();
        TranslateEeveelution();

        //Mew Related Functions
        SpawnMew();
        DestroyMew();
        if(_isMewDestroyed)
        {
            Destroy(Mew);
        }

        //Idle Pikachu Related Functions
        SpawnIdlePikachu();
    }

    //피카츄 & 이브이 스폰 메소드
    void SpawnPE()
    {
        if (!UIManager.FaderAnimOver)
        {
            if (UIManager.FaderAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fader")
                && UIManager.FaderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                UIManager.FaderAnimOver = true;
                _spawnPE = true;

                Pika = Instantiate(PikachuPrefab, new Vector3(-0.8f, -0.2f, 3.0f), Quaternion.Euler(0, 180, 0));
                Eev = Instantiate(EeveePrefab, new Vector3(0.8f, -0.2f, 3.0f), Quaternion.Euler(0, 180, 0));

                _pikachuAnimator = Pika.GetComponent<Animator>();
                _eeveeAnimator = Eev.GetComponent<Animator>();

                AudioManager.PikachuAudio = Pika.GetComponent<AudioSource>();
                AudioManager.EeveeAudio = Eev.GetComponent<AudioSource>();
            }
        }
    }

    //맵(레벨) 스폰 메소드
    void SpawnMap()
    {
        if (!IsMapSpawned && UIManager.FaderAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fader2")
        && UIManager.FaderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            IsMapSpawned = true;
            Instantiate(MapPrefab);

            ARPlaneManager.enabled = true;
            AudioManager.SnorlaxAudio = MapPrefab.transform.Find("Snorlax").GetComponent<AudioSource>();
        }
    }

    //뮤 스폰 메소드
    void SpawnMew()
    {
        if(IsMapSpawned)
        {
            _mewSpawnTimer += Time.deltaTime;

            if(_mewSpawnTimer > 30.0f && !_isMewSpawned)
            {
                _isMewSpawned = true;
                Mew = Instantiate(MewPrefab, XROriginPos.position + new Vector3(1, 0, 0.5f), Quaternion.Euler(0,-90,0));
                AudioManager.MewAudio = Mew.GetComponent<AudioSource>();
                _mewAnimator = Mew.GetComponent<Animator>();
            }
        }
    }

    //뮤 파괴 메소드
    void DestroyMew()
    {
        if (Mew != null)
        {
            if (!_isMewDestroyed)
            {
                if (_mewAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move") && _mewAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _isMewDestroyed = true;
                }
            }
        }
    }

    //프리져 & 썬더 & 파이어 스폰 메소드
    void SpawnAZM()
    {
        if(IsMapSpawned)
        {
            _azmSpawnTimer += Time.deltaTime;

            if(_azmSpawnTimer > 10.0f && !_isArticunoSpawned)
            {
                _isArticunoSpawned = true;
                Art = Instantiate(ArticunoPrefab, new Vector3(0, 20, -50), Quaternion.identity);
                AudioManager.ArticunoAudio = Art.GetComponent<AudioSource>();
            }

            if(_azmSpawnTimer > 12.0f && !_isZapdosSpawned)
            {
                _isZapdosSpawned = true;
                Zap = Instantiate(ZapdosPrefab, new Vector3(-5, 20, -50), Quaternion.identity);
                AudioManager.ZapdosAudio = Zap.GetComponent<AudioSource>();
            }

            if(_azmSpawnTimer > 13.0f && !_isMoltresSpawned)
            {
                _isMoltresSpawned = true;
                Mol = Instantiate(MoltresPrefab, new Vector3(5, 20, -50), Quaternion.identity);
                AudioManager.MoltresAudio = Mol.GetComponent<AudioSource>();
            }
        }
    }

    //프리져 & 썬더 & 파이어 이동 AND 파괴 메소드
    void TranslateDestroyAZM()
    {
        if(Art != null)
        {
            Art.transform.Translate(Vector3.forward * _azmMoveSpeed * Time.deltaTime);

            if (Art.transform.position.z >= 150.0f)
            {
                Destroy(Art);
            }
        }

        if (Zap != null)
        {
            Zap.transform.Translate(Vector3.forward * _azmMoveSpeed * Time.deltaTime);

            if (Zap.transform.position.z >= 150.0f)
            {
                Destroy(Zap);
            }
        }

        if(Mol != null)
        {
            Mol.transform.Translate(Vector3.forward * _azmMoveSpeed * Time.deltaTime);

            if (Mol.transform.position.z >= 150.0f)
            {
                Destroy(Mol);
            }
        }
    }

    //뚜벅초 스폰 메소드
    void SpawnOddish()
    {
        if(IsMapSpawned && !_isOddishSpawned)
        {
            _isOddishSpawned = true;

            Odd1 = Instantiate(OddishPrefab, new Vector3(-1, -2, 1), Quaternion.Euler(0, 40, 0));
            Odd2 = Instantiate(OddishPrefab, new Vector3(-1.5f, -2, 1), Quaternion.Euler(0, 40, 0));
            Odd3 = Instantiate(OddishPrefab, new Vector3(-1.2f, -2, 0.3f), Quaternion.Euler(0, 40, 0));
        }
    }

    //뚜벅초 이동 AND 파괴 메소드
    void TranslateDestroyOddish()
    {
        if(Odd1 != null && Odd2 != null && Odd3 != null)
        {
            Odd1.transform.Translate(Vector3.forward * _oddishMoveSpeed * Time.deltaTime);
            Odd2.transform.Translate(Vector3.forward * _oddishMoveSpeed * Time.deltaTime);
            Odd3.transform.Translate(Vector3.forward * _oddishMoveSpeed * Time.deltaTime);

            if(Odd1.transform.position.z >= 20.0f)
            {
                Destroy(Odd1);
            }

            if (Odd2.transform.position.z >= 20.0f)
            {
                Destroy(Odd2);
            }

            if (Odd3.transform.position.z >= 20.0f)
            {
                Destroy(Odd3);
            }
        }
    }

    //이상해씨 & 파이리 & 꼬부기 스폰 메소드
    void SpawnBCS()
    {
        if(IsMapSpawned && !_isBulbasaurSpawned && !_isCharmanderSpawned && !_isSquirtleSpawned)
        {
            _isBulbasaurSpawned = true;
            _isCharmanderSpawned = true;
            _isSquirtleSpawned = true;

            Bulb = Instantiate(BulbasaurPrefab, new Vector3(7, -2, -2), Quaternion.identity);
            Char = Instantiate(CharmanderPrefab, new Vector3(5, -2, -3), Quaternion.identity);
            Squi = Instantiate(SquirtlePrefab, new Vector3(6, -2, 0), Quaternion.identity);

            AudioManager.BulbasaurAudio = Bulb.GetComponent<AudioSource>();
            AudioManager.CharmanderAudio = Char.GetComponent<AudioSource>();
            AudioManager.SquirtleAudio = Squi.GetComponent<AudioSource>();
        }
    }

    //이상해씨 & 파이리 & 꼬부기 이동 메소드
    void TranslateBCS()
    {
        if(Bulb != null)
        {
            _bDeg += Time.deltaTime * _bcsMoveSpeed;
            if (_bDeg < 360)
            {
                var rad = Mathf.Deg2Rad * (_bDeg);
                var x = _bcsCircleR * Mathf.Sin(rad);
                var z = _bcsCircleR * Mathf.Cos(rad);

                Bulb.transform.position = Bulb.transform.position + new Vector3(x, 0, z);
                Bulb.transform.rotation = Quaternion.Euler(0, _bDeg, 0);
            }
            else
            {
                _bDeg = 0;
            }
        }

        if(Char != null)
        {
            _cDeg += Time.deltaTime * _bcsMoveSpeed;
            if (_cDeg < 360)
            {
                var rad = Mathf.Deg2Rad * (_cDeg);
                var x = _bcsCircleR * Mathf.Sin(rad);
                var z = _bcsCircleR * Mathf.Cos(rad);

                Char.transform.position = Char.transform.position + new Vector3(x, 0, z);
                Char.transform.rotation = Quaternion.Euler(0, _cDeg, 0);
            }
            else
            {
                _cDeg = 0;
            }
        }

        if(Squi != null)
        {
            _sDeg += Time.deltaTime * _bcsMoveSpeed;
            if (_sDeg < 360)
            {
                var rad = Mathf.Deg2Rad * (_sDeg);
                var x = _bcsCircleR * Mathf.Sin(rad);
                var z = _bcsCircleR * Mathf.Cos(rad);

                Squi.transform.position = Squi.transform.position + new Vector3(x, 0, z);
                Squi.transform.rotation = Quaternion.Euler(0, _sDeg, 0);
            }
            else
            {
                _sDeg = 0;
            }
        }
    }

    //샤미드 & 쥬피썬더 & 부스터 스폰 메소드
    void SpawnEeveelution()
    {
        if(IsMapSpawned && !_isVaporeonSpawned && !_isJolteonSpawned && !_isFlareonSpawned)
        {
            _isVaporeonSpawned = true;
            _isJolteonSpawned = true;
            _isFlareonSpawned = true;

            Vap = Instantiate(VaporeonPrefab, new Vector3(-14, -2, 9), Quaternion.identity);
            Jolt = Instantiate(JolteonPrefab, new Vector3(-15.5f, -2, 6.5f), Quaternion.identity);
            Fla = Instantiate(FlareonPrefab, new Vector3(-13, -2, 5), Quaternion.identity);

            AudioManager.VaporeonAudio = Vap.GetComponent<AudioSource>();
            AudioManager.JolteonAudio = Jolt.GetComponent<AudioSource>();
            AudioManager.FlareonAudio = Fla.GetComponent<AudioSource>();
        }
    }

    //샤미드 & 쥬피썬더 & 부스터 이동 메소드
    void TranslateEeveelution()
    {
        if (Vap != null)
        {
            _vDeg += Time.deltaTime * _eeveelutionMoveSpeed;
            if (_vDeg < 360)
            {
                var rad = Mathf.Deg2Rad * (_vDeg);
                var x = _circleR * Mathf.Sin(rad);
                var z = _circleR * Mathf.Cos(rad);

                Vap.transform.position = Vap.transform.position + new Vector3(x, 0, z);
                Vap.transform.rotation = Quaternion.Euler(0, _vDeg, 0);
            }
            else
            {
                _vDeg = 0;
            }
        }

        if (Jolt != null)
        {
            _jDeg += Time.deltaTime * _eeveelutionMoveSpeed;
            if (_jDeg < 360)
            {
                var rad = Mathf.Deg2Rad * (_jDeg);
                var x = _circleR * Mathf.Sin(rad);
                var z = _circleR * Mathf.Cos(rad);

                Jolt.transform.position = Jolt.transform.position + new Vector3(x, 0, z);
                Jolt.transform.rotation = Quaternion.Euler(0, _jDeg, 0);
            }
            else
            {
                _jDeg = 0;
            }
        }

        if (Fla != null)
        {
            _fDeg += Time.deltaTime * _eeveelutionMoveSpeed;
            if (_fDeg < 360)
            {
                var rad = Mathf.Deg2Rad * (_fDeg);
                var x = _circleR * Mathf.Sin(rad);
                var z = _circleR * Mathf.Cos(rad);

                Fla.transform.position = Fla.transform.position + new Vector3(x, 0, z);
                Fla.transform.rotation = Quaternion.Euler(0, _fDeg, 0);
            }
            else
            {
                _fDeg = 0;
            }
        }
    }

    //Idle 피카츄 스폰 메소드
    void SpawnIdlePikachu()
    {
        if (Input.touchCount == 0)
            return;

        if(ARRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = hits[0].pose;

            if (IPikachu == null)
            {
                IPikachu = Instantiate(IdlePikachuPrefab, hitPos.position, hitPos.rotation);
                AudioManager.IdlePikachuAudio = IPikachu.GetComponent<AudioSource>();
            }
            else
            {
                IPikachu.transform.position = hitPos.position;
                IPikachu.transform.rotation = hitPos.rotation;
            }
        }

        Vector3 lookPos = Camera.main.transform.position - IPikachu.transform.position;
        lookPos.y = 0;
        IPikachu.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
