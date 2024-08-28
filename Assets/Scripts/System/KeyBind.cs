using System.Collections.Generic;
using UnityEngine;

public static class KeyBind
{
    public enum Action
    {
        Move_Forward,
        Move_Backward,
        Move_Left,
        Move_Right,
        Sprint,
        Jump,
        Roll,
        Dash,
        Interact,
        BasicSkill,
        Special_Skill,
        Morph,
    }

    public static Dictionary<Action, KeyCode> keys = new Dictionary<Action, KeyCode>();

    public static KeyCode move_forward => keys[Action.Move_Forward];
    public static KeyCode move_backward => keys[Action.Move_Backward];
    public static KeyCode move_left => keys[Action.Move_Left];
    public static KeyCode move_right => keys[Action.Move_Right];

    public static KeyCode sprint => keys[Action.Sprint];
    public static KeyCode jump => keys[Action.Jump];
    public static KeyCode roll => keys[Action.Roll];
    public static KeyCode dash => keys[Action.Dash];

    public static KeyCode interact => keys[Action.Interact];

    public static KeyCode basicSkill => keys[Action.BasicSkill];
    public static KeyCode special_skill => keys[Action.Special_Skill];

    public static KeyCode morph => keys[Action.Morph];

    static KeyBind()
    {
        keys[Action.Move_Forward] = KeyCode.W;
        keys[Action.Move_Backward] = KeyCode.S;
        keys[Action.Move_Left] = KeyCode.A;
        keys[Action.Move_Right] = KeyCode.D;


        keys[Action.Sprint] = KeyCode.LeftShift;
        keys[Action.Jump] = KeyCode.V;
        keys[Action.Roll] = KeyCode.Space;
        keys[Action.Dash] = KeyCode.LeftControl;

        keys[Action.Interact] = KeyCode.F;

        keys[Action.BasicSkill] = KeyCode.Mouse0;
        keys[Action.Special_Skill] = KeyCode.Alpha1;

        keys[Action.Morph] = KeyCode.R;
    }
}