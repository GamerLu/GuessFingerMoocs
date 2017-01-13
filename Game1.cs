﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GuessFingerMoocs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // 替每張圖片宣告一個變數
        Texture2D image1, image2, image3, imageGameStart, imageBackgroud;

        // 紀錄玩家出拳，0=初始值，1=剪刀，2=石頭，3=布
        int player = 0;
        // 紀錄場景，0=開始畫面，1=出拳畫面
        int gameState = 0;
        // 紀錄電腦出拳，0=初始值，1=剪刀，2=石頭，3=布
        int computer = 0;
        // 宣告隨機的變數，之後再產生數字
        Random random = new Random();
        // 記錄玩家的先前的鍵盤動作，之前在Update執行階段已有變數newState取得最新的鍵盤狀態
        KeyboardState previousState = Keyboard.GetState();

        // 紀錄贏家是誰，1是電腦贏，2是玩家贏，999代表平手
        int winner = 0;

        // 宣告字型變數
        SpriteFont fontArial;

        // 貼圖位置的位移量，之後將由隨機產生
        int moveVibration = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // 設定起始畫面大小
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // 載入變數，並對應每個圖片變數
            image1 = Content.Load<Texture2D>("Scissor400x600");
            image2 = Content.Load<Texture2D>("Rock400x600");
            image3 = Content.Load<Texture2D>("Paper400x600");
            imageGameStart = Content.Load<Texture2D>("GameStart1024x768");
            imageBackgroud = Content.Load<Texture2D>("background1024x768");

            // 載入字型資源給予字型變數
            fontArial = Content.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            // 取得鍵盤按下的狀態
            KeyboardState newState = Keyboard.GetState();
            // 按下ESC 可以離開遊戲
            if (newState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // [舊有程式碼]
            /*
            // 按下數字鍵1，把玩家出拳的變數值修改為1，代表剪刀
            if (newState.IsKeyDown(Keys.D1))
            {
                player = 1;
            }
            // 按下數字鍵1，把玩家出拳的變數值修改為2，代表石頭
            if (newState.IsKeyDown(Keys.D2))
            {
                player = 2;
            }
            // 按下數字鍵1，把玩家出拳的變數值修改為3，代表布
            if (newState.IsKeyDown(Keys.D3))
            {
                player = 3;
            }
             */

            if (newState.IsKeyDown(Keys.D1) || newState.IsKeyDown(Keys.D2) || newState.IsKeyDown(Keys.D3))
            {
                gameState = 1;
                // 產生隨機的位移量，範圍 -10 到 10
                moveVibration = random.Next(0, 21)-10;
            }

            /*
            // [舊有程式碼] 隨機產生1-3數字
            computer = random.Next(1, 4);
            */

            // [修正電腦出拳後的程式碼]
            // 判斷玩家按下且放開後，才隨機產生一次數字  
            // 注意! 如果以下判斷式是前後對調 改成 if (previousState.IsKeyUp(Keys.D1) && newState.IsKeyDown(Keys.D1)) 的話，是玩家一按下按鍵電腦就會出拳
            if (newState.IsKeyUp(Keys.D1) && previousState.IsKeyDown(Keys.D1))
            {
                player = 1;
                computer = random.Next(1, 4);
            }
            if (newState.IsKeyUp(Keys.D2) && previousState.IsKeyDown(Keys.D2))
            {
                player = 2;
                computer = random.Next(1, 4);
            }
            if (newState.IsKeyUp(Keys.D3) && previousState.IsKeyDown(Keys.D3))
            {
                player = 3;
                computer = random.Next(1, 4);
            }
            // 記得把鍵盤的完成狀態記錄到之前的狀態變數
            previousState = newState;

            // 呼叫判斷轉贏的函數
            checkWinner();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // sprite.draw 的使用方法很多種，這裡載入的只有3個參數
            // 參數1表示要顯示的圖片，參數2表示顯示圖片的起始位置，參數3表示填色，White表示不填色
            // 原本繪出開始畫面也要更改，只有當還沒出拳時，才會繪製開始畫面
            if (gameState == 0)
            {
                spriteBatch.Draw(imageGameStart, Vector2.Zero, Color.White);
            }
            if (gameState == 1)
            {
                // 繪製出拳後的遊戲背景
                spriteBatch.Draw(imageBackgroud, Vector2.Zero, Color.White);
                // 繪製玩家出拳的貼圖
                if (player == 1)
                {
                    spriteBatch.Draw(image1, new Vector2(550+ moveVibration, 100+ moveVibration), Color.White);
                }
                if (player == 2)
                {
                    spriteBatch.Draw(image2, new Vector2(550 + moveVibration, 100 + moveVibration), Color.White);
                }
                if (player == 3)
                {
                    spriteBatch.Draw(image3, new Vector2(550 + moveVibration, 100 + moveVibration), Color.White);
                }


                // 繪製電腦出拳的貼圖
                if (computer == 1)
                {
                    spriteBatch.Draw(image1, new Vector2(50 + moveVibration, 100 + moveVibration), Color.White);
                }
                if (computer == 2)
                {
                    spriteBatch.Draw(image2, new Vector2(50 + moveVibration, 100 + moveVibration), Color.White);
                }
                if (computer == 3)
                {
                    spriteBatch.Draw(image3, new Vector2(50 + moveVibration, 100 + moveVibration), Color.White);
                }

                // 繪出人物誰是玩家，誰是電腦
                spriteBatch.DrawString(fontArial, "電腦", new Vector2(180, 700), Color.White);
                spriteBatch.DrawString(fontArial, "玩家", new Vector2(700, 700), Color.Brown);

                // 繪出輸贏的結果
                if (winner == 999)
                {
                    spriteBatch.DrawString(fontArial, "平手", new Vector2(450, 10), Color.Brown);
                }
                if (winner == 1)
                {
                    spriteBatch.DrawString(fontArial, "電腦贏", new Vector2(410, 10), Color.Black);
                }
                if (winner == 2)
                {
                    spriteBatch.DrawString(fontArial, "玩家贏", new Vector2(410, 10), Color.DarkGreen);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // 判斷輸贏的函數
        void checkWinner()
        {
            if (player== computer)
            {
                winner = 999;
            }
            if (player==1 && computer == 2)
            {
                winner = 1;
            }
            if (player == 1 && computer == 3)
            {
                winner = 2;
            }

            if (player == 2 && computer == 1)
            {
                winner = 2;
            }
            if (player == 2 && computer == 3)
            {
                winner = 1;
            }

            if (player == 3 && computer == 1)
            {
                winner = 1;
            }
            if (player == 3 && computer == 2)
            {
                winner = 2;
            }

        }
    }
}
