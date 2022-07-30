using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayingCards
{
    internal class CharacterCreater
    {
        /// <summary>
        /// キャラクターのインスタンスを生成する
        /// </summary>
        /// <param name="CharCount">参加人数</param>
        public static void NewCreate(int characterCount)
        {
            if (characterCount <= 2)
            {
                Create2();
            }
            else if (characterCount == 3)
            {
                Create3();
            }
            else
            {
                Create4();
            }
        }

        /// <summary>
        /// キャラクターのインスタンスを生成する（参加人数：２人）
        /// </summary>
        public static void Create2()
        {
            // プレイヤーのインスタンス生成
            PlayerCharacter player = new PlayerCharacter("プレイヤー");
            player.MyArea = new Area1(player);
            player.MyController = new Controller(player);
            PokerMain.characterList.Add(player);

            // CPU1のインスタンス生成
            NonPlayerCharacter cpu1 = new NonPlayerCharacter("CPU1");
            cpu1.MyArea = new Area2(cpu1);
            PokerMain.characterList.Add(cpu1);

            // 順番を決める
            player.NextCharacter = cpu1;
            cpu1.NextCharacter = player;
        }

        /// <summary>
        /// キャラクターのインスタンスを生成する（参加人数：３人）
        /// </summary>
        public static void Create3()
        {
            // プレイヤーのインスタンス生成
            PlayerCharacter player = new PlayerCharacter("プレイヤー");
            player.MyArea = new Area1(player);
            player.MyController = new Controller(player);
            PokerMain.characterList.Add(player);

            // CPU1のインスタンス生成
            NonPlayerCharacter cpu1 = new NonPlayerCharacter("CPU1");
            cpu1.MyArea = new Area3(cpu1);
            PokerMain.characterList.Add(cpu1);

            // CPU2のインスタンス生成
            NonPlayerCharacter cpu2 = new NonPlayerCharacter("CPU2");
            cpu2.MyArea = new Area2(cpu2);
            PokerMain.characterList.Add(cpu2);

            // 順番を決める
            player.NextCharacter = cpu1;
            cpu1.NextCharacter = cpu2;
            cpu2.NextCharacter = player;
        }

        /// <summary>
        /// キャラクターのインスタンスを生成する（参加人数：４人）
        /// </summary>
        public static void Create4()
        {
            // プレイヤーのインスタンス生成
            PlayerCharacter player = new PlayerCharacter("プレイヤー");
            player.MyArea = new Area1(player);
            player.MyController = new Controller(player);
            PokerMain.characterList.Add(player);

            // CPU1のインスタンス生成
            NonPlayerCharacter cpu1 = new NonPlayerCharacter("CPU1");
            cpu1.MyArea = new Area3(cpu1);
            PokerMain.characterList.Add(cpu1);

            // CPU2のインスタンス生成
            NonPlayerCharacter cpu2 = new NonPlayerCharacter("CPU2");
            cpu2.MyArea = new Area2(cpu2);
            PokerMain.characterList.Add(cpu2);

            // CPU3のインスタンス生成
            NonPlayerCharacter cpu3 = new NonPlayerCharacter("CPU3");
            cpu3.MyArea = new Area4(cpu3);
            PokerMain.characterList.Add(cpu3);

            // 順番を決める
            player.NextCharacter = cpu1;
            cpu1.NextCharacter = cpu2;
            cpu2.NextCharacter = cpu3;
            cpu3.NextCharacter = player;
        }

    }
}
