using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    private const string IsHoveredAnimatorParameterName = "IsHovered";
    private const string IsHeartAnimatorParameterName = "IsHeart";
    private const string IsSkullAnimatorParameterName = "IsSkull";

    private Animator animator;
    private int x;
    private int y;

    void Awake()
    {
        animator = GetComponent<Animator>();
        ChallengeManager.Instance.ChallengeTurnTaken.AddListener(OnChallengeTurnTaken);
    }

    void OnMouseDown()
    {
        ChallengeManager.Instance.Move(x, y);
    }

    void OnMouseEnter()
    {
        animator.SetBool(IsHoveredAnimatorParameterName, true);
    }

    void OnMouseExit()
    {
        animator.SetBool(IsHoveredAnimatorParameterName, false);
    }

    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    private void OnChallengeTurnTaken()
    {
        UpdatePiece();
    }

    private void UpdatePiece()
    {
        int fieldIndex = x + y * Board.BoardSize;
        PieceType pieceType = ChallengeManager.Instance.Fields[fieldIndex];
        if (pieceType == PieceType.Heart)
        {
            animator.SetBool(IsHeartAnimatorParameterName, true);
        }
        else if (pieceType == PieceType.Skull)
        {
            animator.SetBool(IsSkullAnimatorParameterName, true);
        }
    }
}
