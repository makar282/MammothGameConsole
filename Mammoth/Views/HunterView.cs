using System;
using MammothHunting.Models;

namespace MammothHunting.Views
{
    public static class HunterView
    {
        public static void Draw(Hunter hunter)
        {
            hunter.Head.Draw();
            foreach (var pixel in hunter.Body)
            {
                pixel.Draw();
            }
        }

        public static void Clear(Hunter hunter)
        {
            hunter.Head.Clear();
            foreach (var pixel in hunter.Body)
            {   
                pixel.Clear();
            }
        }
    }
}