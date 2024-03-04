using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI subText;

    private void Start()
    {
        
    }

    public void SetText(int index)
    {
        mainText.fontSize = 45;
        subText.fontSize = 36;

        mainText.lineSpacing = 0;
        mainText.alignment = TextAlignmentOptions.Left;

        if (index == 1)
        {
            mainText.fontSize = 56;
            mainText.lineSpacing = 14;

            mainText.SetText(
                $"식물에 취약한 속성의 스크롤 블록 적용 시\n" +
                $"식물이 죽고 타일이 잠금 상태가 되므로" +
                $"\n주의해야돼요!" );


            subText.fontSize = 38;

            subText.SetText(
                $"*잠금 상태의 타일에는 스크롤 블록을 사용할 수 없습니다");
        }
        else if (index == 3)
        {
            mainText.fontSize = 54;
            mainText.lineSpacing = 28;

            mainText.SetText(
                $"리롤 버튼을 터치해서 스크롤을 교체해봅시다\n" +
                $"게이지가 한 칸 찰 때마다 1회 사용 가능해요");

            subText.SetText(
                $" ");
        }
        else if (index == 4)
        {
            mainText.fontSize = 46;
            mainText.lineSpacing = 14;

            mainText.SetText(
                $"일정 점수에 도달하면 미니게임이 열려요\n\n" +
                $"지금은 마나 맥 미니게임이 열렸어요\n" +
                $"마나가 사라지기 전에 드래그 해보세요!");

            subText.SetText(
                $"*미니게임 클리어 시 일정향의 점수와 골드를 얻을 수 있습니다");
        }
        else if (index == 5)
        {
            mainText.fontSize = 46;

            mainText.SetText(
                $"그리폰이 밭 위를 날아다니며 농사를 방해하고 있어요\n" +
                $"터치해서 쫓아내보세요!");

            subText.SetText(
              $"*그리핀을 터치하면 점수와 골드를 획득할 수 있습니다");
        }
        else if (index == 6)
        {
            mainText.fontSize = 55;
            mainText.alignment = TextAlignmentOptions.Center;

            mainText.SetText(
                $"모든 설명이 끝났습니다!\n\n" +
                $"열심히 농사를 지어봅시다!");

            subText.SetText(
               $" ");
        }
    }
}
