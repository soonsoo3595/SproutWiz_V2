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
                $"�Ĺ��� ����� �Ӽ��� ��ũ�� ��� ���� ��\n" +
                $"�Ĺ��� �װ� Ÿ���� ��� ���°� �ǹǷ�" +
                $"\n�����ؾߵſ�!" );


            subText.fontSize = 38;

            subText.SetText(
                $"*��� ������ Ÿ�Ͽ��� ��ũ�� ����� ����� �� �����ϴ�");
        }
        else if (index == 3)
        {
            mainText.fontSize = 54;
            mainText.lineSpacing = 28;

            mainText.SetText(
                $"���� ��ư�� ��ġ�ؼ� ��ũ���� ��ü�غ��ô�\n" +
                $"�������� �� ĭ �� ������ 1ȸ ��� �����ؿ�");

            subText.SetText(
                $" ");
        }
        else if (index == 4)
        {
            mainText.fontSize = 46;
            mainText.lineSpacing = 14;

            mainText.SetText(
                $"���� ������ �����ϸ� �̴ϰ����� ������\n\n" +
                $"������ ���� �� �̴ϰ����� ���Ⱦ��\n" +
                $"������ ������� ���� �巡�� �غ�����!");

            subText.SetText(
                $"*�̴ϰ��� Ŭ���� �� �������� ������ ��带 ���� �� �ֽ��ϴ�");
        }
        else if (index == 5)
        {
            mainText.fontSize = 46;

            mainText.SetText(
                $"�׸����� �� ���� ���ƴٴϸ� ��縦 �����ϰ� �־��\n" +
                $"��ġ�ؼ� �ѾƳ�������!");

            subText.SetText(
              $"*�׸����� ��ġ�ϸ� ������ ��带 ȹ���� �� �ֽ��ϴ�");
        }
        else if (index == 6)
        {
            mainText.fontSize = 55;
            mainText.alignment = TextAlignmentOptions.Center;

            mainText.SetText(
                $"��� ������ �������ϴ�!\n\n" +
                $"������ ��縦 ����ô�!");

            subText.SetText(
               $" ");
        }
    }
}
