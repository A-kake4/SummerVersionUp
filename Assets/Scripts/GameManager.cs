using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ShowCard P1;
    [SerializeField] ShowCard P2;

    [SerializeField] Sprite draw1;
    [SerializeField] Sprite draw2;

    [SerializeField] TextMesh hpTextP1;
    [SerializeField] TextMesh hpTextP2;

    [SerializeField] TextMesh mpTextP1;
    [SerializeField] TextMesh mpTextP2;

    [SerializeField] TextMesh atkTextP1;
    [SerializeField] TextMesh atkTextP2;

    [SerializeField] TextMesh defTextP1;
    [SerializeField] TextMesh defTextP2;

    [SerializeField] TextMesh deckTextP1;
    [SerializeField] TextMesh deckTextP2;

    int commandCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SetCommand(commandCount++);
            Debug.Log("Enterが押された");
        }
    }

    void SetCommand(int com)
    {
        switch (com)
        {
            case 0:
                // MP1の攻撃
                P1.HandleStateTransition();
                break;
            case 1: 
                P1.HandleStateTransition();
                SetMPText(mpTextP1, 9);
                SetHPText(hpTextP2, 1880);
                break;
            case 2:

                // MP2のバフを使ってMP1の攻撃
                P2.HandleStateTransition();
                break;
            case 3:
                P2.HandleStateTransition();
                SetMPText(mpTextP2, 6);
                SetAttackText(atkTextP2, 240);
                break;
            case 4:
                P2.HandleStateTransition();
                break;
            case 5:
                P2.HandleStateTransition();
                SetMPText(mpTextP2, 5);
                SetHPText(hpTextP1, 800);
                break;
            case 6:
                SetMPText(mpTextP1, 10);

                // ドローしてMP2の防御バフカードを使う
                P1.ChangeLastCard(draw1);
                SetDeckText(deckTextP1, 34);
                break;
            case 7:
                P1.HandleStateTransition();
                Debug.Log("P1の防御バフカードを使用");
                break;
            case 8:
                P1.HandleStateTransition();
                SetMPText(mpTextP1, 8);
                SetDefenseText(defTextP1, 90);
                break;
            case 9:
                SetMPText(mpTextP2, 8);

                // ドローしてMP2の攻撃バフカードを使う
                P2.ChangeLastCard(draw2);
                SetDeckText(deckTextP2, 34);
                break;
            case 10:
                P2.HandleStateTransition();
                break;
            case 11:
                P2.HandleStateTransition();
                SetMPText(mpTextP2, 6);
                SetAttackText(atkTextP2, 480);
                break;
            case 13:
                SetMPText(mpTextP1 ,10);

                // 先攻がドローして必殺技
                P1.ChangeLastCard(draw1);
                SetDeckText(deckTextP1, 33);
                break;
            case 14:
                P1.HandleStateTransition();
                break;
            case 15:
                SetMPText(mpTextP1, 2);
                P1.HandleStateTransition();
                SetHPText(hpTextP2, 1600);
                break;
            case 16:
                SetMPText(mpTextP2, 8);

                // 後攻がドローして必殺技
                P2.ChangeLastCard(draw2);
                SetDeckText(deckTextP2, 33);
                break;
            case 17:
                P2.HandleStateTransition();
                break;
            case 18:
                SetMPText(mpTextP2, 0);
                P2.HandleStateTransition();
                SetHPText(hpTextP1, 0);
                break;
        }
    }

    void SetHPText(TextMesh hpText, int newHP)
    {
        hpText.text = "HP:" + newHP.ToString();
    }

    void SetMPText(TextMesh mpText, int newMP)
    {
        mpText.text = "MP:" + newMP.ToString();
    }

    void SetAttackText(TextMesh atkText, int newATK)
    {
        atkText.text = "ATK:" + newATK.ToString();
    }

    void SetDefenseText(TextMesh defText, int newDEF)
    {
       defText.text = "DEF:" + newDEF.ToString();
    }

    void SetDeckText(TextMesh deckText, int newDeck)
    {
        deckText.text = newDeck.ToString();
    }
}
