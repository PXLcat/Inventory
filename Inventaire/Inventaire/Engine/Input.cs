﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Input 
    {
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
            if (newKbState.IsKeyDown(Keys.Up) && newKbState != oldKbState) //TODO valable que pour des input uniques
            { //mettre à part les conditions à rallonge?
                inputs.Add(InputType.JUMP);
                Console.Write("input jump");
            }
            //if (newKbState.IsKeyDown(Keys.Space) && newKbState != oldKbState)
            //{
            //    inputs.Add(InputType.ATTACK1);
            //    Console.Write("input attack1 (space)");
            //}

      
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
        LEFT_CLICK
    }
    public enum InputMethod
    {
        KEYBOARD,
        MOUSE,
        FILE,
        NONE
    }
}