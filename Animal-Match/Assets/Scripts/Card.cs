using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer cardRenderer; //카드의 뒷면 이미지, 카드의 스트라이트 랜더러 컴포넌트를 끌고와서 넣을 예정

    [SerializeField]
    private Sprite animalSprite; //카드의 앞면 이미지

    [SerializeField]
    private Sprite backSprite; //뒤집혔다가 다시 돌아와야 하므로 카드의 뒷면 이미지를 넣어줄 것임

    private bool isFlipped = false; //카드가 뒤집혀졌는지 확인하는 변수 선언
    private bool isFlipping = false; //카드가 뒤집혀지는 동안 또 뒤집혀지는 걸 방지하기 위한 변수

    public  int cardID;
    public void SetCardID(int id){
        cardID = id; //카드 아이디는 전달받은 id값을 가짐

    }
    public void SetAnimalSprite(Sprite sprite){
        animalSprite = sprite; //카드의 앞면에 전달받은 Sprite sprite를 넣어준다
    }

    void OnMouseDown(){ //카드를 마우스로 눌렀을 때 호출되는 함수 
        if (!isFlipping){ //카드가 뒤집혀지는 중이 아닌 경우에만 카드를 뒤집을 수 있음
            FlipCard(); //FlipCard 호출
        }
        
    }
    public void FlipCard(){

        isFlipping = true;
        Vector3 originalScale = transform.localScale; //vector형태로 원래 카드의 스케일 값을 저장하는 변수 선언
        Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z); //카드 스케일의 값이 0인 변수 선언

        //Card 스크립트를 가지고 있는 오브젝트의 스케일을 타켓 스케일로 바꾸는데 0.2초 동안 바꿔라
        //.Oncomplete는 DOScale이 끝나면 괄호 안의 작업을 하라는 의미
        transform.DOScale(targetScale, 0.2f).OnComplete(()=>
        {
            isFlipped = !isFlipped; // 클릭하면 변수의 참, 거짓 값이 바뀌고 맞는 if, else문을 실행
            if (isFlipped){ //클릭이 되서 뒤집혔으면
                cardRenderer.sprite = animalSprite; //카드 이미지를 앞면으로 바꾸어라
            }
            else{ //아니면
                cardRenderer.sprite = backSprite; // 카드 이미지를 뒷면으로 바꾸어라
            }

            //뒤집기가 끝나면 다시 카드를 원래 스케일로 바꿔라
            transform.DOScale(originalScale, 0.2f).OnComplete(()=> {
                isFlipping = false; // 카드 뒤집는 중 끝
            }); 
        });



    }


}
