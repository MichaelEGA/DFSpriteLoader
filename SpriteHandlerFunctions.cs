using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public static class SpriteHandlerFunctions
{
    //Setup Functions
    public static void GetMainCamera(SpriteHandler spriteRotation)
    {
        if (spriteRotation.camera == null)
        {
            Camera cameraScript = Camera.main;

            if (cameraScript != null)
            {
                spriteRotation.camera = cameraScript.gameObject;
            }
        }
    }

    public static void GetAnimator(SpriteHandler spriteRotation)
    {
        if (spriteRotation.animator == null)
        {
            spriteRotation.animator = spriteRotation.GetComponent<Animator>();
        }
    }

    //Rotation Functions

    public static void CalculateRotation(SpriteHandler spriteRotation)
    {
        Transform cameraPosition = spriteRotation.camera.transform;

        spriteRotation.targetPosition = new Vector3(cameraPosition.position.x, spriteRotation.transform.position.y, cameraPosition.position.z);
        spriteRotation.targetDirection = spriteRotation.targetPosition - spriteRotation.transform.position;

        spriteRotation.angle = Vector3.SignedAngle(spriteRotation.targetDirection, spriteRotation.transform.parent.forward, Vector3.up);

        SetRotation(spriteRotation);

        spriteRotation.transform.rotation = Quaternion.LookRotation(spriteRotation.targetDirection);
    }

    public static void SetRotation(SpriteHandler spriteRotation)
    {
        float angle = spriteRotation.angle;
        Animator animator = spriteRotation.animator;

        //front
        if (angle > -22.5f && angle < 22.6f)
        {
            //front

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 1f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            //front angle right

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 1f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            //side right

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 1f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            //rear angle right

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 1f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle <= -157.5 || angle >= 157.5f)
        {
            //rear

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 1f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle >= -157.4f && angle < -112.5f)
        {
            //rear angle left

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 1f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            //side left

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 0f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 1f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
        else if (angle >= -67.5f && angle <= -22.5f)
        {
            //front angle left

            animator.SetLayerWeight(0, 0f); //Base Layer
            animator.SetLayerWeight(1, 0f); //Front Layer
            animator.SetLayerWeight(2, 0f); //Front Angle Right Layer
            animator.SetLayerWeight(3, 1f); //Front Angle Left Layer
            animator.SetLayerWeight(4, 0f); //Side Right
            animator.SetLayerWeight(5, 0f); //Side Left
            animator.SetLayerWeight(6, 0f); //Rear layer
            animator.SetLayerWeight(7, 0f); //Rear Angle Right layer
            animator.SetLayerWeight(8, 0f); //Rear Angle Left layer
            animator.SetLayerWeight(9, 0f); //Dying
            animator.SetLayerWeight(10, 0f); //Dead
        }
    }

    //Animation Functions

    public static void PlayAnimation(SpriteHandler spriteRotation)
    {
        Animator animator = spriteRotation.animator;

        string selectedAnimation = spriteRotation.selectedAnimation.ToString();

        animator.SetBool(selectedAnimation, true);

        List<string> allParameterNames = new List<string>();

        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name != selectedAnimation)
            {
                animator.SetBool(param.name, false);
            }
        }
    }

}
