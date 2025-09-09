﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonogameTutorial.Scenes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MonogameTutorial
{
    public class Game1 : Core
    {
        // The background theme song.
        private Song _themeSong;

        public Game1() : base("My Game", 1280, 720, false)
        {
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //simpleInputBuffer = new SimpleInputBuffer(_slimePosition, MOVEMENT_SPEED);
            base.Initialize();

            // Start playing the background music.
            Audio.PlaySong(_themeSong);

            // Start the game with the title scene.
            ChangeScene(new TitleScene());
        }

        protected override void LoadContent()
        {
            // Load the background theme music.
            _themeSong = Content.Load<Song>("audio/theme");
        }

        protected override void Update(GameTime gameTime)
        {
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
