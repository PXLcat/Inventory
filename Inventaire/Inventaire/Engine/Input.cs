using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Input 
    {
        /// <summary>
        /// Pour prendre en compte les input clavier et souris.
        /// </summary>
        /// <param name="oldMouseState"></param>
        /// <param name="oldKbState"></param>
        /// <returns></returns>
        public static List<InputType> DefineInputs(ref MouseState oldMouseState, ref KeyboardState oldKbState)
        {
            List<InputType> inputs = new List<InputType>();
            inputs.AddRange(DefineInputs(ref oldMouseState));
            inputs.AddRange(DefineInputs(ref oldKbState));

            return inputs;
        }

        /// <summary>
        /// Pour prendre en compte les input souris uniquement.
        /// </summary>
        /// <param name="oldMouseState"></param>
        /// <returns></returns>
        public static List<InputType> DefineInputs(ref MouseState oldMouseState)
        {
            List<InputType> inputs = new List<InputType>();
            MouseState newMouseState = Mouse.GetState();

            if ((newMouseState.LeftButton == ButtonState.Pressed) && newMouseState != oldMouseState)
            {
                inputs.Add(InputType.LEFT_CLICK);
                Console.Write("input clic");
            }

            return inputs;
        }

        /// <summary>
        /// Pour prendre en compte les input clavier uniquement.
        /// </summary>
        /// <param name="oldKbState"></param>
        /// <returns></returns>
        public static List<InputType> DefineInputs(ref KeyboardState oldKbState) //TODO devrait y en avoir 2, un pour les NPC, un pour joueur avec entrée clavier
        {
            List<InputType> inputs = new List<InputType>();
            KeyboardState newKbState = Keyboard.GetState();
            if (newKbState.IsKeyDown(Keys.Right))
            {
                inputs.Add(InputType.MOVE_RIGHT);
                Console.Write("input move right");
            }
            if (newKbState.IsKeyDown(Keys.Left)) //TODO mettre left et right sur un pied d'égalité
            {
                inputs.Add(InputType.MOVE_LEFT);
                Console.Write("input move left");
            }
            if (newKbState.IsKeyDown(Keys.Up) && newKbState != oldKbState) 
            { //mettre à part les conditions à rallonge?
                inputs.Add(InputType.SINGLE_UP);
                Console.Write("input single up");
            }
            if (newKbState.IsKeyDown(Keys.Down) && newKbState != oldKbState) 
            { //mettre à part les conditions à rallonge?
                inputs.Add(InputType.SINGLE_DOWN);
                Console.Write("input single down");
            }


            if (newKbState.IsKeyDown(Keys.Enter) && newKbState != oldKbState)
            {
                inputs.Add(InputType.START);
                Console.Write("input start (enter)");
            }
            if (newKbState.IsKeyDown(Keys.Back) && newKbState != oldKbState)
            {
                inputs.Add(InputType.RETURNTOMENU);
                Console.Write("input returntomenu (backspace)");
            }


            //____________________



            oldKbState = newKbState;

            return inputs;

        }

        //public static List<InputType> DefineFileInputs() //inputs auto
        //{
        //    List<InputType> inputs = new List<InputType>();
        //    inputs.Add(InputType.MOVE_RIGHT);
        //    return inputs; //TODO
        //}
    }

    public enum InputType
    {
        //TODO raccrocher à des actions clavier pour Player, et comportements pour NPC?
        DO_NOTHING,
        RESET_POSE,
        MOVE_LEFT,
        MOVE_RIGHT,
        JUMP, // fall n'est pas un "input"
        ATTACK1,
        START,
        RETURNTOMENU,
        SINGLE_UP,
        SINGLE_DOWN,
        LEFT_CLICK
    }
    public enum InputMethod
    {
        KEYBOARD,
        MOUSE,
        MOUSE_AND_KEYBOARD_,
        FILE,
        NONE
    }
}