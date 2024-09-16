using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas Fader;    //페이더 캔버스
    public Animator FaderAnimator;  //페이더 애니메이터 (FadeIn & FadeOut 애니메이션 보유)

    public bool FaderAnimOver;  //페이더 애니메이션 종료확인
}
