using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab; //카드 프리펩 오브젝트
    [SerializeField]
    private Sprite[] cardSprites; //카드의 앞면이 될 스프라이트 배열

    private List<int> cardIDList = new List<int>(); //종류가 같은 카드를 각각 2장씩 묶기 위해 리스트 생성
    void Start()
    {
        GenerateCardID();
        ShuffleCardID();//만들어질 카드 아이드를 섞을 함수 호출
        InitBoard(); //보드 초기화 함수 호출
    }

    void GenerateCardID(){ //카드의 종류를 구분지을 카드 아이디 생성
        //0, 0, 1, 1, 2, 2, 3, 3, 4, 4 ... 9, 9 총 20개
        for (int i =0; i < cardSprites.Length; i++){
            cardIDList.Add(i);
            cardIDList.Add(i);
        }
    }

    void ShuffleCardID (){
        int cardCount = cardIDList.Count; //리스트의 길이를 재는 연산자는 count임 length 아님
        for (int i = 0; i < cardCount; i++){
            //카드 아이디 리스트의 0, 0, 1, 1...숫자들 중 랜덤으로 하나를 뽑음
            int randomIndex = Random.Range(i, cardCount);
            //그걸 i인덱스에 해당하는 숫자와 서로 뒤바꿈
            int temp = cardIDList[randomIndex];
            cardIDList[randomIndex] = cardIDList[i];
            cardIDList[i] = temp;

        }
    }

    void InitBoard(){ //보드 초기화
        float spaceY = 1.8f; //카드 생성시에 카드 간의 y간격 값
        //row (-2, -1, 0, 1, 2)
        // 0 - 2 = -2 *spaceY = -3.6
        // 1 - 2 = -1 *spaceY = -1.8 
        // 2 - 2 = 0 y값이 0인 중점이 되는 카드 
        // 3 - 2 = 1 *spaceY = 1.8
        // 4 - 2 = 2 *spaceY = 3.6
        // rowCount / 2 = ? -> 나오는 정수 값이 보드의 y=0에 위치하는 중심점 카드가 됨
        // (row - (int)(rowCount / 2)) * spcaeY // 카드의 y좌표를 구하는 식

        //col (-1.5, -0.5, 0.5, 1.5)
        // 0 - (colCount / 2) = -2 + 0.5 = -1.5
        // 1 - 2 = -1 + 0.5 = -0.5
        // 2 - 2 = 0 + 0.5 = 0.5
        // 3 - 2 = 1 + 0.5 = 1.5

        float spaceX = 1.3f; //카드 생성시의 카드 간의 x간격 값
        //(col - (colCount /2))*spaceX + (spaceX / 2) // space/2를 더해줘야 중심점 x = 0을 기준으로 대칭을 이루게됨
        // -2, -0.7, 0.7, 2 //실제 카드들의 x좌표


        int rowCount =5; //세로 카드 수
        int colCount =4; //가로 카드 수

        int cardIndex = 0; //카드에 앞면 카드에 해당하는 것들을 넣기 위한 인덱스

        for (int row = 0; row < rowCount; row++){
            for (int col = 0; col < colCount; col++){
                float posX = (col - (colCount /2))*spaceX + (spaceX / 2);
                float posY = (row - (int)(rowCount / 2)) * spaceY;
                Vector3 pos = new Vector3(posX, posY, 0f); // 카드가 만들어질 위치 함수
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity); 
                //카드를 만들어 카드오브젝트에 저장. 만들 오브젝트, 위치, 회전 
                Card card = cardObject.GetComponent<Card>(); // 카드의 컴포넌트를 카드오브젝트에 가져오고 그걸 카드에 저장
                int cardID = cardIDList[cardIndex++]; //카드 아이디에 0, 0, 1, 1, 2, 2...값 넣기
                card.SetCardID(cardID); // 카드 스크립트중 setcardid 함수에 카드아이디리스트로 받은 카드아이디값 전달
                card.SetAnimalSprite(cardSprites[cardID]); //인덱스에 따라 카드에 카드 이미지 넣기

            }
        }


    }
}
