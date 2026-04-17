using UnityEditor.Animations;
using UnityEngine;


public class SpriteHandler : MonoBehaviour
{
    //Animator Reference
    public Animator animator;

    //Rotation of Sprite
    public GameObject camera;
    public Vector3 targetPosition;
    public Vector3 targetDirection;
    public float angle;

    //Animation options
    public enum animationOptions { isWalking, isRangedAttack, isRangedAttackRecoil, isPain, isDyingv1, isDyingv2, isDeadCorpse, isIdleAnimated, isMeleeAttack, isMeleeAttackRecoil }
    public animationOptions selectedAnimation;

    // Update is called once per frame
    void Update()
    {
        SpriteHandlerFunctions.GetAnimator(this);
        SpriteHandlerFunctions.GetMainCamera(this);
        SpriteHandlerFunctions.CalculateRotation(this);
        SpriteHandlerFunctions.PlayAnimation(this);
    }
}
