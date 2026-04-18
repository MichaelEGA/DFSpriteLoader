using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


public class SetUpDFSprite : EditorWindow
{
    private string spriteFolderPath = "Assets/";
    private string savePath = "Assets/";
    private string controllerName = "CharacterName";
    private AnimatorController controller;

    [MenuItem("Tools/Set Up DF Sprite")]
    public static void ShowWindow() => GetWindow<SetUpDFSprite>("Set Up DF Sprite");

    private void OnGUI()
    {
        // Folder Selection for Sprites
        EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Sprite Folder", spriteFolderPath);
        if (GUILayout.Button("Browse", GUILayout.Width(60)))
        {
            string absolutePath = EditorUtility.OpenFolderPanel("Select Sprite Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(absolutePath))
            {
                spriteFolderPath = "Assets" + absolutePath.Replace(Application.dataPath, "");
            }
        }
        EditorGUILayout.EndHorizontal();

        // Folder Selection for Saving Animations
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Save Path", savePath);
        if (GUILayout.Button("Browse", GUILayout.Width(60)))
        {
            string absolutePath = EditorUtility.OpenFolderPanel("Select Save Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(absolutePath))
            {
                savePath = "Assets" + absolutePath.Replace(Application.dataPath, "");
            }
        }
        EditorGUILayout.EndHorizontal();

        controllerName = EditorGUILayout.TextField("Controller Name", controllerName);

        GUILayout.Space(10);

        if (GUILayout.Button("Generate All", GUILayout.Height(30)))
        {
            CreateAnimationsAndController();
        }
    }

    private void CreateAnimationsAndController()
    {
        // 1. Create the Animator Controller asset
        string controllerPath = Path.Combine(savePath, controllerName + ".controller");
        controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
        AnimatorControllerLayer rootLayer = controller.layers[0];

        //Add contoller layers
        controller.AddLayer("Front");
        controller.AddLayer("Front Angle Right");
        controller.AddLayer("Front Angle Left");
        controller.AddLayer("Side Right");
        controller.AddLayer("Side Left");
        controller.AddLayer("Rear");
        controller.AddLayer("Rear Angle Right");
        controller.AddLayer("Rear Angle Left");
        controller.AddLayer("Dying");
        controller.AddLayer("Dead");

        // 2. Load and Filter Sprites
        string[] spritePaths = Directory.GetFiles(spriteFolderPath, "*.png", SearchOption.AllDirectories);
        List<Sprite> allSprites = new List<Sprite>();
        foreach (string path in spritePaths)
        {
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            allSprites.AddRange(assets.OfType<Sprite>());
        }

        // define animations
        var animationDefinitions = new List<(string Name, int Start, int End)>
        {
           // Walking
            ("Walking Front", 0, 7),
            ("Walking Front Angle Left", 8, 15),
            ("Walking Side Left", 16, 23),
            ("Walking Rear Angle Left", 24, 31),
            ("Walking Rear", 32, 39),
            ("Walking Rear Angle Right", 40, 47),
            ("Walking Side Right", 48, 55),
            ("Walking Front Angle Right", 56, 63),

            // Ranged Attacks
            ("Ranged Attack Front", 64, 66),
            ("Ranged Attack Recoil Front", 67, 69),
            ("Ranged Attack Front Angle Left", 70, 71),
            ("Ranged Attack Recoil Front Angle Left", 72, 75),
            ("Ranged Attack Side Left", 76, 78),
            ("Ranged Attack Recoil Side Left", 79, 81),
            ("Ranged Attack Rear Angle Left", 82, 84),
            ("Ranged Attack Recoil Rear Angle Left", 85, 87),
            ("Ranged Attack Rear", 88, 90),
            ("Ranged Attack Recoil Rear", 91, 93),
            ("Ranged Attack Rear Angle Right", 94, 96),
            ("Ranged Attack Recoil Rear Angle Right", 97, 99),
            ("Ranged Attack Side Right", 100, 102),
            ("Ranged Attack Recoil Side Right", 103, 105),
            ("Ranged Attack Front Angle Right", 106, 108),
            ("Ranged Attack Recoil Front Angle Right", 109, 111),

            // Pain
            ("Pain Front", 112, 113),
            ("Pain Front Angle Left", 114, 115),
            ("Pain Side Left", 116, 117),
            ("Pain Rear Angle Left", 118, 119),
            ("Pain Rear", 120, 121),
            ("Pain Rear Angle Right", 122, 123),
            ("Pain Side Right", 124, 125),
            ("Pain Front Angle Right", 126, 127),

            // Death
            ("Dying v1", 128, 130),
            ("Dead Corpse", 131, 131),
            ("Dying v2", 132, 135),
            ("Idle Dying", 136, 136),

            // Idle (Single Frame)
            ("Idle Static Front", 136, 136),
            ("Idle Static Front Angle Left", 137, 137),
            ("Idle Static Side Left", 138, 138),
            ("Idle Static Rear Angle Left", 139, 139),
            ("Idle Static Rear", 140, 140),
            ("Idle Static Rear Angle Right", 141, 141),
            ("Idle Static Side Right", 142, 142),
            ("Idle Static Front Angle Right", 143, 143),

            // Melee Attacks
            ("Melee Attack Front", 144, 146),
            ("Melee Attack Recoil Front", 147, 149),
            ("Melee Attack Front Angle Left", 150, 152),
            ("Melee Attack Recoil Front Angle Left", 153, 155),
            ("Melee Attack Side Left", 156, 158),
            ("Melee Attack Recoil Side Left", 159, 161),
            ("Melee Attack Rear Angle Left", 162, 164),
            ("Melee Attack Recoil Rear Angle Left", 165, 167),
            ("Melee Attack Rear", 168, 170),
            ("Melee Attack Recoil Rear", 171, 173),
            ("Melee Attack Rear Angle Right", 174, 176),
            ("Melee Attack Recoil Rear Angle Right", 177, 179),
            ("Melee Attack Side Right", 180, 182),
            ("Melee Attack Recoil Side Right", 183, 185),
            ("Melee Attack Front Angle Right", 186, 188),
            ("Melee Attack Recoil Front Angle Right", 189, 191),

            // Idle (Animated)
            ("Idle Animated Front", 192, 203),
            ("Idle Animated Front Angle Left", 204, 215),
            ("Idle Animated Side Left", 216, 227),
            ("Idle Animated Rear Angle Left", 228, 239),
            ("Idle Animated Rear", 240, 251),
            ("Idle Animated Rear Angle Right", 252, 263),
            ("Idle Animated Side Right", 264, 275),
            ("Idle Animated Front Angle Right", 276, 287)
        };

        List<AnimationClip> AnimationClips = new List<AnimationClip>(); 

        // 3. Generate Clips and Add to Controller
        foreach (var def in animationDefinitions)
        {
            var clipSprites = allSprites
                .Where(s =>
                {
                    int num = ExtractNumberFromName(s.name);
                    return num >= def.Start && num <= def.End;
                })
                .OrderBy(s => ExtractNumberFromName(s.name))
                .ToArray();

            if (clipSprites.Length > 0)
            {
                AnimationClip clip = CreateClip(def.Name, clipSprites);

                //This makes the animation clip loop
                var settings = AnimationUtility.GetAnimationClipSettings(clip);
                settings.loopTime = true;
                AnimationUtility.SetAnimationClipSettings(clip, settings);

                //Set layer of the animation
                int layerNo = 1;

                if (def.Name.Contains("Front Angle Right"))
                {
                    layerNo = 2;
                }
                else if (def.Name.Contains("Front Angle Left"))
                {
                    layerNo = 3;
                }
                else if (def.Name.Contains("Front"))
                {
                    layerNo = 1;
                }
                else if (def.Name.Contains("Side Right"))
                {
                    layerNo = 4;
                }
                else if (def.Name.Contains("Side Left"))
                {
                    layerNo = 5;
                }
                else if (def.Name.Contains("Rear Angle Right"))
                {
                    layerNo = 7;
                }
                else if (def.Name.Contains("Rear Angle Left"))
                {
                    layerNo = 8;
                }
                else if (def.Name.Contains("Rear"))
                {
                    layerNo = 6;
                }
                else if (def.Name.Contains("Dying"))
                {
                    layerNo = 9;
                }
                else if (def.Name.Contains("Dead"))
                {
                    layerNo = 10;
                }

                var layers = controller.layers;
                var stateMachine = layers[layerNo].stateMachine;
                AnimatorState state = stateMachine.AddState(def.Name);
                state.motion = clip; 
                layers[layerNo].defaultWeight = 0f;
                controller.layers = layers;
            }
        }

        // Add the parameters
        controller.AddParameter("isWalking", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isRangedAttack", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isRangedAttackRecoil", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isPain", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isDyingv1", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isDyingv2", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isDeadCorpse", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isIdleStatic", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isIdleAnimated", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isMeleeAttack", AnimatorControllerParameterType.Bool);
        controller.AddParameter("isMeleeAttackRecoil", AnimatorControllerParameterType.Bool);

        LinkStates();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Generated {animationDefinitions.Count} animations in {controllerName}.controller");

        GameObject characterGO = new GameObject();
        characterGO.name = controllerName;

        GameObject animatorGO = new GameObject();
        animatorGO.name = "AnimatorGO";
        animatorGO.AddComponent<SpriteRenderer>();
        Animator animator = animatorGO.AddComponent<Animator>();
        animator.runtimeAnimatorController = controller;
        animatorGO.AddComponent<SpriteHandler>();

        animatorGO.transform.parent = characterGO.transform;
    }

    public void LinkStates()
    {
        if (controller == null)
        {
            Debug.LogError("Select an Animator Controller asset in the Project window first.");
            return;
        }

        Undo.RecordObject(controller, "Link States to Idle");

        foreach (AnimatorControllerLayer layer in controller.layers)
        {
            AnimatorStateMachine stateMachine = layer.stateMachine;

            var idleChildState = stateMachine.states.FirstOrDefault(s =>
            s.state.name.IndexOf("idle", System.StringComparison.OrdinalIgnoreCase) >= 0);

            if (idleChildState.state == null)
            {
                Debug.LogWarning($"No state containing 'idle' found in layer: {layer.name}");
                continue;
            }

            AnimatorState idleState = idleChildState.state;

            // Make the Entry link to the 'Idle' state
            stateMachine.defaultState = idleState;

            // Link all other states using bool parameters
            foreach (var childState in stateMachine.states)
            {
                AnimatorState targetState = childState.state;
                
                if (targetState == idleState)
                {
                    continue;
                }

                string paramName = "none";

                if (targetState.name.Contains("Walking"))
                {
                    paramName = "isWalking";
                }
                else if (targetState.name.Contains("Ranged Attack Recoil"))
                {
                    paramName = "isRangedAttackRecoil";
                }
                else if (targetState.name.Contains("Ranged Attack"))
                {
                    paramName = "isRangedAttack";
                }
                else if (targetState.name.Contains("Pain"))
                {
                    paramName = "isPain";
                }
                else if (targetState.name.Contains("Dying v1"))
                {
                    paramName = "isDyingv1";
                }
                else if (targetState.name.Contains("Dying v2"))
                {
                    paramName = "isDyingv2";
                }
                else if (targetState.name.Contains("Dead Corpse"))
                {
                    paramName = "isDeadCorpse";
                }
                else if (targetState.name.Contains("Idle Animated"))
                {
                    paramName = "isIdleAnimated";
                }
                else if (targetState.name.Contains("Melee Attack Recoil"))
                {
                    paramName = "isMeleeAttackRecoil";
                }
                else if (targetState.name.Contains("Melee Attack"))
                {
                    paramName = "isMeleeAttack";
                }
               
                // Transition: Idle -> Target State (Bool = true)
                var toTarget = idleState.AddTransition(targetState);
                toTarget.AddCondition(AnimatorConditionMode.If, 0, paramName);
                toTarget.hasExitTime = false; //Instant transition
                toTarget.duration = 0.1f; //Brief blend

                // Transition: Target State -> Idle (Bool = false)
                var backToIdle = targetState.AddTransition(idleState);
                backToIdle.AddCondition(AnimatorConditionMode.IfNot, 0, paramName);
                backToIdle.hasExitTime = false;
                backToIdle.duration = 0.1f;
            }
        }

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
        Debug.Log("Successfully linked Animator states!");
    }

    private AnimationClip CreateClip(string name, Sprite[] sprites)
    {
        AnimationClip clip = new AnimationClip { frameRate = 12 };
        EditorCurveBinding spriteBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };

        ObjectReferenceKeyframe[] keys = new ObjectReferenceKeyframe[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            keys[i] = new ObjectReferenceKeyframe
            {
                time = i / clip.frameRate,
                value = sprites[i]
            };
        }

        AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, keys);
        string fullPath = Path.Combine(savePath, name + ".anim");
        AssetDatabase.CreateAsset(clip, AssetDatabase.GenerateUniqueAssetPath(fullPath));
        return clip;
    }

    private int ExtractNumberFromName(string name)
    {
        string resultString = System.Text.RegularExpressions.Regex.Match(name, @"\d+").Value;
        return int.TryParse(resultString, out int num) ? num : -1;
    }
}
