using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SpawnManager SpawnManager;   //SpawnManager 스크립트

    [Header("BGM Audio")]    
    AudioSource _audioSource;
    public AudioClip WindClip;
    bool _isWindPlaying;

    [Header("Pikachu")]
    public AudioSource PikachuAudio;

    [Header("Eevee")]
    public AudioSource EeveeAudio;
    bool _isEeveeAudioPlaying;

    [Header("Articuno")]
    public AudioSource ArticunoAudio;
    bool _isArticunoAudioPlaying;

    [Header("Zapdos")]
    public AudioSource ZapdosAudio;
    bool _isZapdosAudioPlaying;

    [Header("Moltres")]
    public AudioSource MoltresAudio;
    bool _isMoltresAudioPlaying;

    [Header("Mew")]
    public AudioSource MewAudio;
    bool _isMewAudioPlaying;

    [Header("Pokemon Audios")]
    //포켓몬 오디오소스
    public AudioSource BulbasaurAudio;
    public AudioSource CharmanderAudio;
    public AudioSource SquirtleAudio;
    public AudioSource VaporeonAudio;
    public AudioSource JolteonAudio;
    public AudioSource FlareonAudio;
    public AudioSource SnorlaxAudio;
    public AudioSource IdlePikachuAudio;

    [Header("Pokemon RaycastHit")]
    //포켓몬 RaycastHit 확인
    public bool IsBulbasaurHit;
    public bool IsCharmanderHit;
    public bool IsSquirtleHit;
    public bool IsVaporeonHit;
    public bool IsJolteonHit;
    public bool IsFlareonHit;
    public bool IsSnorlaxHit;
    public bool IsArticunoHit;
    public bool IsZapdosHit;
    public bool IsMoltresHit;
    public bool IsMewHit;
    public bool IsIdlePikachuHit;

    [Header("Pokemon Audio is Playing?")]
    //포켓몬 오디오 재생확인
    bool _isBulbasaurAudioPlaying;
    bool _isCharmanderAudioPlaying;
    bool _isSquirtleAudioPlaying;
    bool _isVaporeonAudioPlaying;
    bool _isJolteonAudioPlaying;
    bool _isFlareonAudioPlaying;
    bool _isSnorlaxAudioPlaying;
    bool _isIdlePikachuAudioPlaying;

    [Header("Idle Pikachu Audio Clips")]
    public AudioClip[] IdlePikachuClips = new AudioClip[3]; //Idle 피카츄 오디오 클립들


    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PEAudio();

        MapAudio();

        BCSAudio();

        EeveelutionAudio();

        AZMAudio();

        MewSpawnAudio();
        IdlePikachuSpawnAudio();
    }

    //피카츄 & 이브이 오디오 메소드
    void PEAudio()
    {
        if (SpawnManager.Pika != null)
        {
            if (!PikachuAudio.isPlaying && !_isEeveeAudioPlaying)
            {
                _isEeveeAudioPlaying = true;
                EeveeAudio.PlayOneShot(EeveeAudio.clip);
            }
        }
    }

    //PlaneManager를 통해 스폰된 피카츄 오디오 메소드
    //피카츄 터치 시 랜덤으로 울음소리 재생
    void IdlePikachuSpawnAudio()
    {
        if(IsIdlePikachuHit)
        {
            if(SpawnManager.IPikachu != null && !_isIdlePikachuAudioPlaying)
            {
                _isIdlePikachuAudioPlaying = true;
                int clipRandom = Random.Range(0, 3);
                IdlePikachuAudio.PlayOneShot(IdlePikachuClips[clipRandom]);
            }

            if(!IdlePikachuAudio.isPlaying)
            {
                IsIdlePikachuHit = false;
                _isIdlePikachuAudioPlaying = false;
                IdlePikachuAudio.clip = null;
            }
        }
    }

    //맵(레벨) 오디오 메소드
    void MapAudio()
    {
        if (SpawnManager.IsMapSpawned && !_isWindPlaying)
        {
            _isWindPlaying = true;
            _audioSource.clip = WindClip;
            _audioSource.PlayOneShot(WindClip);
            _audioSource.volume = 1.0f;            
        }
    }

    //이상해씨, 파이리, 꼬부기 오디오 메소드
    void BCSAudio()
    {
        if(IsBulbasaurHit)
        {
            if(SpawnManager.Bulb != null && !_isBulbasaurAudioPlaying)
            {
                _isBulbasaurAudioPlaying = true;
                BulbasaurAudio.PlayOneShot(BulbasaurAudio.clip);
            }

            if(!BulbasaurAudio.isPlaying)
            {
                IsBulbasaurHit = false;
                _isBulbasaurAudioPlaying = false;
            }
        }

        if(IsCharmanderHit)
        {
            if(SpawnManager.Char != null && !_isCharmanderAudioPlaying)
            {
                _isCharmanderAudioPlaying = true;
                CharmanderAudio.PlayOneShot(CharmanderAudio.clip);
            }

            if(!CharmanderAudio.isPlaying)
            {
                IsCharmanderHit = false;
                _isCharmanderAudioPlaying = false;
            }
        }

        if(IsSquirtleHit)
        {
            if(SpawnManager.Squi != null && !_isSquirtleAudioPlaying)
            {
                _isSquirtleAudioPlaying = true;
                SquirtleAudio.PlayOneShot(SquirtleAudio.clip);
            }

            if(!SquirtleAudio.isPlaying)
            {
                IsSquirtleHit = false;
                _isSquirtleAudioPlaying = false;
            }
        }
    }

    //샤미드, 쥬피썬더, 부스터 오디오 메소드
    void EeveelutionAudio()
    {
        if(IsVaporeonHit)
        {
            if(SpawnManager.Vap != null && !_isVaporeonAudioPlaying)
            {
                _isVaporeonAudioPlaying = true;
                VaporeonAudio.PlayOneShot(VaporeonAudio.clip);
            }

            if(!VaporeonAudio.isPlaying)
            {
                IsVaporeonHit = false;
                _isVaporeonAudioPlaying = false;
            }
        }

        if (IsJolteonHit)
        {
            if (SpawnManager.Jolt != null && !_isJolteonAudioPlaying)
            {
                _isJolteonAudioPlaying = true;
                JolteonAudio.PlayOneShot(JolteonAudio.clip);
            }

            if (!JolteonAudio.isPlaying)
            {
                IsJolteonHit = false;
                _isJolteonAudioPlaying = false;
            }
        }

        if (IsFlareonHit)
        {
            if (SpawnManager.Fla != null && !_isFlareonAudioPlaying)
            {
                _isFlareonAudioPlaying = true;
                FlareonAudio.PlayOneShot(FlareonAudio.clip);
            }

            if (!FlareonAudio.isPlaying)
            {
                IsFlareonHit = false;
                _isFlareonAudioPlaying = false;
            }
        }
    }

    //잠만보 오디오 메소드
    void SnorlaxHitAudio()
    {
        if(IsSnorlaxHit)
        {
            if(!_isSnorlaxAudioPlaying)
            {
                _isSnorlaxAudioPlaying = true;
                SnorlaxAudio.PlayOneShot(SnorlaxAudio.clip);
            }

            if(!SnorlaxAudio.isPlaying)
            {
                IsSnorlaxHit = false;
                _isSnorlaxAudioPlaying = false;
            }
        }
    }

    //프리져, 썬더, 파이어 오디오 메소드
    void AZMAudio()
    {
        if(IsArticunoHit)
        {
            if(SpawnManager.Art != null && !_isArticunoAudioPlaying)
            {
                _isArticunoAudioPlaying = true;
                ArticunoAudio.PlayOneShot(ArticunoAudio.clip);
            }

            if(!ArticunoAudio.isPlaying)
            {
                IsArticunoHit = false;
                _isArticunoAudioPlaying = false;
            }
        }

        if (IsZapdosHit)
        {
            if (SpawnManager.Zap != null && !_isZapdosAudioPlaying)
            {
                _isZapdosAudioPlaying = true;
                ZapdosAudio.PlayOneShot(ZapdosAudio.clip);
            }

            if (!ZapdosAudio.isPlaying)
            {
                IsZapdosHit = false;
                _isZapdosAudioPlaying = false;
            }
        }

        if (IsMoltresHit)
        {
            if (SpawnManager.Mol != null && !_isMoltresAudioPlaying)
            {
                _isMoltresAudioPlaying = true;
                MoltresAudio.PlayOneShot(MoltresAudio.clip);
            }

            if (!MoltresAudio.isPlaying)
            {
                IsMoltresHit = false;
                _isMoltresAudioPlaying = false;
            }
        }
    }

    //뮤 오디오 메소드
    void MewSpawnAudio()
    {
        if(IsMewHit)
        {
            if(SpawnManager.Mew != null && !_isMewAudioPlaying)
            {
                _isMewAudioPlaying = true;
                MewAudio.PlayOneShot(MewAudio.clip);
            }

            if(!MewAudio.isPlaying)
            {
                IsMewHit = false;
                _isMewAudioPlaying = false;
            }
        }
    }    
}
