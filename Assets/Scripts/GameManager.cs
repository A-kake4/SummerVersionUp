using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ShowCard P1;
    [SerializeField] ShowCard P2;

    [SerializeField] Sprite draw1;
    [SerializeField] Sprite draw2;

    [SerializeField] TextMesh hpTextP1;
    [SerializeField] TextMesh hpTextP2;

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
            Debug.Log("Enter‚ª‰Ÿ‚³‚ê‚½");
        }
    }

    void SetCommand(int com)
    {
        switch (com)
        {
            case 0:
                P1.HandleStateTransition();
                break;
            case 1: 
                P1.HandleStateTransition();
                SetHPText(hpTextP2, 950);
                break;
            case 2:
                P2.HandleStateTransition();
                break;
            case 3:
                P2.HandleStateTransition();
                SetHPText(hpTextP1, 900);
                break;
            case 4:
                P1.ChangeLastCard(draw1);
                break;
            case 5:
                P1.HandleStateTransition();
                break;
            case 6:
                P1.HandleStateTransition();
                break;
            case 7:
                P2.ChangeLastCard(draw1);
                break;
            case 8:
                P2.HandleStateTransition();
                break;
            case 9:
                P2.HandleStateTransition();
                break;
        }
    }

    void SetHPText(TextMesh hpText, int newHP)
    {
        hpText.text = "HP: " + newHP.ToString();
    }
}
