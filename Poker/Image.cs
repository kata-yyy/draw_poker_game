using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PlayingCards.Properties;

namespace PlayingCards
{
    internal class Image
    {
        /// <summary>
        /// トランプ（クラブ）の画像イメージリスト
        /// </summary>
        public static Bitmap[] cardClub = new Bitmap[]
        {
            Resources.card_club_01, Resources.card_club_02, Resources.card_club_03, Resources.card_club_04,
            Resources.card_club_05, Resources.card_club_06, Resources.card_club_07, Resources.card_club_08,
            Resources.card_club_09, Resources.card_club_10, Resources.card_club_11, Resources.card_club_12,
            Resources.card_club_13
        };
        /// <summary>
        /// トランプ（スペード）の画像イメージリスト
        /// </summary>
        public static Bitmap[] cardSpade = new Bitmap[]
        {
            Resources.card_spade_01, Resources.card_spade_02, Resources.card_spade_03, Resources.card_spade_04,
            Resources.card_spade_05, Resources.card_spade_06, Resources.card_spade_07, Resources.card_spade_08,
            Resources.card_spade_09, Resources.card_spade_10, Resources.card_spade_11, Resources.card_spade_12,
            Resources.card_spade_13
        };
        /// <summary>
        /// トランプ（ハート）の画像イメージリスト
        /// </summary>
        public static Bitmap[] cardHeart = new Bitmap[]
        {
            Resources.card_heart_01, Resources.card_heart_02, Resources.card_heart_03, Resources.card_heart_04,
            Resources.card_heart_05, Resources.card_heart_06, Resources.card_heart_07, Resources.card_heart_08,
            Resources.card_heart_09, Resources.card_heart_10, Resources.card_heart_11, Resources.card_heart_12,
            Resources.card_heart_13
        };
        /// <summary>
        /// トランプ（ダイアモンド）の画像イメージリスト
        /// </summary>
        public static Bitmap[] cardDiamond = new Bitmap[]
        {
            Resources.card_diamond_01, Resources.card_diamond_02, Resources.card_diamond_03,
            Resources.card_diamond_04, Resources.card_diamond_05, Resources.card_diamond_06,
            Resources.card_diamond_07, Resources.card_diamond_08, Resources.card_diamond_09,
            Resources.card_diamond_10, Resources.card_diamond_11, Resources.card_diamond_12,
            Resources.card_diamond_13
        };
        /// <summary>
        /// トランプ（ジョーカー）の画像イメージ
        /// </summary>
        public static Bitmap cardJoker = Resources.card_joker;
        /// <summary>
        /// トランプ（裏面）の画像イメージ
        /// </summary>
        public static Bitmap cardBack = Resources.card_back;

        /// <summary>
        /// トランプ（クラブ）の画像イメージリスト（９０度回転）
        /// </summary>
        public static Bitmap[] cardClubRotate90 = new Bitmap[]
        {
            Resources.card_club_01, Resources.card_club_02, Resources.card_club_03, Resources.card_club_04,
            Resources.card_club_05, Resources.card_club_06, Resources.card_club_07, Resources.card_club_08,
            Resources.card_club_09, Resources.card_club_10, Resources.card_club_11, Resources.card_club_12,
            Resources.card_club_13
        };
        /// <summary>
        /// トランプ（スペード）の画像イメージリスト（９０度回転）
        /// </summary>
        public static Bitmap[] cardSpadeRotate90 = new Bitmap[]
        {
            Resources.card_spade_01, Resources.card_spade_02, Resources.card_spade_03, Resources.card_spade_04,
            Resources.card_spade_05, Resources.card_spade_06, Resources.card_spade_07, Resources.card_spade_08,
            Resources.card_spade_09, Resources.card_spade_10, Resources.card_spade_11, Resources.card_spade_12,
            Resources.card_spade_13
        };
        /// <summary>
        /// トランプ（ハート）の画像イメージリスト（９０度回転）
        /// </summary>
        public static Bitmap[] cardHeartRotate90 = new Bitmap[]
        {
            Resources.card_heart_01, Resources.card_heart_02, Resources.card_heart_03, Resources.card_heart_04,
            Resources.card_heart_05, Resources.card_heart_06, Resources.card_heart_07, Resources.card_heart_08,
            Resources.card_heart_09, Resources.card_heart_10, Resources.card_heart_11, Resources.card_heart_12,
            Resources.card_heart_13
        };
        /// <summary>
        /// トランプ（ダイアモンド）の画像イメージリスト（９０度回転）
        /// </summary>
        public static Bitmap[] cardDiamondRotate90 = new Bitmap[]
        {
            Resources.card_diamond_01, Resources.card_diamond_02, Resources.card_diamond_03,
            Resources.card_diamond_04, Resources.card_diamond_05, Resources.card_diamond_06,
            Resources.card_diamond_07, Resources.card_diamond_08, Resources.card_diamond_09,
            Resources.card_diamond_10, Resources.card_diamond_11, Resources.card_diamond_12,
            Resources.card_diamond_13
        };
        /// <summary>
        /// トランプ（ジョーカー）の画像イメージ（９０度回転）
        /// </summary>
        public static Bitmap cardJokerRotate90 = Resources.card_joker;
        /// <summary>
        /// トランプ（裏面）の画像イメージ（９０度回転）
        /// </summary>
        public static Bitmap cardBackRotate90 = Resources.card_back;

        /// <summary>
        /// トランプ（クラブ）の画像イメージリスト（１８０度回転）
        /// </summary>
        public static Bitmap[] cardClubRotate180 = new Bitmap[]
        {
            Resources.card_club_01, Resources.card_club_02, Resources.card_club_03, Resources.card_club_04,
            Resources.card_club_05, Resources.card_club_06, Resources.card_club_07, Resources.card_club_08,
            Resources.card_club_09, Resources.card_club_10, Resources.card_club_11, Resources.card_club_12,
            Resources.card_club_13
        };
        /// <summary>
        /// トランプ（スペード）の画像イメージリスト（１８０度回転）
        /// </summary>
        public static Bitmap[] cardSpadeRotate180 = new Bitmap[]
        {
            Resources.card_spade_01, Resources.card_spade_02, Resources.card_spade_03, Resources.card_spade_04,
            Resources.card_spade_05, Resources.card_spade_06, Resources.card_spade_07, Resources.card_spade_08,
            Resources.card_spade_09, Resources.card_spade_10, Resources.card_spade_11, Resources.card_spade_12,
            Resources.card_spade_13
        };
        /// <summary>
        /// トランプ（ハート）の画像イメージリスト（１８０度回転）
        /// </summary>
        public static Bitmap[] cardHeartRotate180 = new Bitmap[]
        {
            Resources.card_heart_01, Resources.card_heart_02, Resources.card_heart_03, Resources.card_heart_04,
            Resources.card_heart_05, Resources.card_heart_06, Resources.card_heart_07, Resources.card_heart_08,
            Resources.card_heart_09, Resources.card_heart_10, Resources.card_heart_11, Resources.card_heart_12,
            Resources.card_heart_13
        };
        /// <summary>
        /// トランプ（ダイアモンド）の画像イメージリスト（１８０度回転）
        /// </summary>
        public static Bitmap[] cardDiamondRotate180 = new Bitmap[]
        {
            Resources.card_diamond_01, Resources.card_diamond_02, Resources.card_diamond_03,
            Resources.card_diamond_04, Resources.card_diamond_05, Resources.card_diamond_06,
            Resources.card_diamond_07, Resources.card_diamond_08, Resources.card_diamond_09,
            Resources.card_diamond_10, Resources.card_diamond_11, Resources.card_diamond_12,
            Resources.card_diamond_13
        };
        /// <summary>
        /// トランプ（ジョーカー）の画像イメージ（１８０度回転）
        /// </summary>
        public static Bitmap cardJokerRotate180 = Resources.card_joker;
        /// <summary>
        /// トランプ（裏面）の画像イメージ（１８０度回転）
        /// </summary>
        public static Bitmap cardBackRotate180 = Resources.card_back;

        /// <summary>
        /// トランプ（クラブ）の画像イメージリスト（２７０度回転）
        /// </summary>
        public static Bitmap[] cardClubRotate270 = new Bitmap[]
        {
            Resources.card_club_01, Resources.card_club_02, Resources.card_club_03, Resources.card_club_04,
            Resources.card_club_05, Resources.card_club_06, Resources.card_club_07, Resources.card_club_08,
            Resources.card_club_09, Resources.card_club_10, Resources.card_club_11, Resources.card_club_12,
            Resources.card_club_13
        };
        /// <summary>
        /// トランプ（スペード）の画像イメージリスト（２７０度回転）
        /// </summary>
        public static Bitmap[] cardSpadeRotate270 = new Bitmap[]
        {
            Resources.card_spade_01, Resources.card_spade_02, Resources.card_spade_03, Resources.card_spade_04,
            Resources.card_spade_05, Resources.card_spade_06, Resources.card_spade_07, Resources.card_spade_08,
            Resources.card_spade_09, Resources.card_spade_10, Resources.card_spade_11, Resources.card_spade_12,
            Resources.card_spade_13
        };
        /// <summary>
        /// トランプ（ハート）の画像イメージリスト（２７０度回転）
        /// </summary>
        public static Bitmap[] cardHeartRotate270 = new Bitmap[]
        {
            Resources.card_heart_01, Resources.card_heart_02, Resources.card_heart_03, Resources.card_heart_04,
            Resources.card_heart_05, Resources.card_heart_06, Resources.card_heart_07, Resources.card_heart_08,
            Resources.card_heart_09, Resources.card_heart_10, Resources.card_heart_11, Resources.card_heart_12,
            Resources.card_heart_13
        };
        /// <summary>
        /// トランプ（ダイアモンド）の画像イメージリスト（２７０度回転）
        /// </summary>
        public static Bitmap[] cardDiamondRotate270 = new Bitmap[]
        {
            Resources.card_diamond_01, Resources.card_diamond_02, Resources.card_diamond_03,
            Resources.card_diamond_04, Resources.card_diamond_05, Resources.card_diamond_06,
            Resources.card_diamond_07, Resources.card_diamond_08, Resources.card_diamond_09,
            Resources.card_diamond_10, Resources.card_diamond_11, Resources.card_diamond_12,
            Resources.card_diamond_13
        };
        /// <summary>
        /// トランプ（ジョーカー）の画像イメージ（２７０度回転）
        /// </summary>
        public static Bitmap cardJokerRotate270 = Resources.card_joker;
        /// <summary>
        /// トランプ（裏面）の画像イメージ（２７０度回転）
        /// </summary>
        public static Bitmap cardBackRotate270 = Resources.card_back;

        /// <summary>
        /// カードの画像イメージを９０度回転させる
        /// </summary>
        public static void CardImageRotate90()
        {
            for (int i = 0; i < 13; i++)
            {
                cardClubRotate90[i].RotateFlip(RotateFlipType.Rotate90FlipNone);
                cardSpadeRotate90[i].RotateFlip(RotateFlipType.Rotate90FlipNone);
                cardHeartRotate90[i].RotateFlip(RotateFlipType.Rotate90FlipNone);
                cardDiamondRotate90[i].RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            cardJokerRotate90.RotateFlip(RotateFlipType.Rotate90FlipNone);
            cardBackRotate90.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        /// <summary>
        /// カードの画像イメージを１８０度回転させる
        /// </summary>
        public static void CardImageRotate180()
        {
            for (int i = 0; i < 13; i++)
            {
                cardClubRotate180[i].RotateFlip(RotateFlipType.Rotate180FlipNone);
                cardSpadeRotate180[i].RotateFlip(RotateFlipType.Rotate180FlipNone);
                cardHeartRotate180[i].RotateFlip(RotateFlipType.Rotate180FlipNone);
                cardDiamondRotate180[i].RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            cardJokerRotate180.RotateFlip(RotateFlipType.Rotate180FlipNone);
            cardBackRotate180.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        /// <summary>
        /// カードの画像イメージを２７０度回転させる
        /// </summary>
        public static void CardImageRotate270()
        {
            for (int i = 0; i < 13; i++)
            {
                cardClubRotate270[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
                cardSpadeRotate270[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
                cardHeartRotate270[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
                cardDiamondRotate270[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            cardJokerRotate270.RotateFlip(RotateFlipType.Rotate270FlipNone);
            cardBackRotate270.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        /// <summary>
        /// カードの画像イメージを取得する
        /// </summary>
        /// <param name="card">画像イメージを取得したいカード</param>
        /// <returns>カードの画像イメージ</returns>
        public static Bitmap GetCardImage(Card card)
        {
            switch (card.Suit)
            {
                case Suit.Club:
                    return cardClub[card.Number - 1];
                case Suit.Spade:
                    return cardSpade[card.Number - 1];
                case Suit.Heart:
                    return cardHeart[card.Number - 1];
                case Suit.Diamond:
                    return cardDiamond[card.Number - 1];
                case Suit.Joker:
                    return cardJoker;
                default:
                    return null;
            }
        }

        /// <summary>
        /// カードの画像イメージを取得する（９０度回転）
        /// </summary>
        /// <param name="card">画像イメージを取得したいカード</param>
        /// <returns>カードの画像イメージ</returns>
        public static Bitmap GetCardImageRotate90(Card card)
        {
            switch (card.Suit)
            {
                case Suit.Club:
                    return cardClubRotate90[card.Number - 1];
                case Suit.Spade:
                    return cardSpadeRotate90[card.Number - 1];
                case Suit.Heart:
                    return cardHeartRotate90[card.Number - 1];
                case Suit.Diamond:
                    return cardDiamondRotate90[card.Number - 1];
                case Suit.Joker:
                    return cardJokerRotate90;
                default:
                    return null;
            }
        }

        /// <summary>
        /// カードの画像イメージを取得する（１８０度回転）
        /// </summary>
        /// <param name="card">画像イメージを取得したいカード</param>
        /// <returns>カードの画像イメージ</returns>
        public static Bitmap GetCardImageRotate180(Card card)
        {
            switch (card.Suit)
            {
                case Suit.Club:
                    return cardClubRotate180[card.Number - 1];
                case Suit.Spade:
                    return cardSpadeRotate180[card.Number - 1];
                case Suit.Heart:
                    return cardHeartRotate180[card.Number - 1];
                case Suit.Diamond:
                    return cardDiamondRotate180[card.Number - 1];
                case Suit.Joker:
                    return cardJokerRotate180;
                default:
                    return null;
            }
        }

        /// <summary>
        /// カードの画像イメージを取得する（２７０度回転）
        /// </summary>
        /// <param name="card">画像イメージを取得したいカード</param>
        /// <returns>カードの画像イメージ</returns>
        public static Bitmap GetCardImageRotate270(Card card)
        {
            switch (card.Suit)
            {
                case Suit.Club:
                    return cardClubRotate270[card.Number - 1];
                case Suit.Spade:
                    return cardSpadeRotate270[card.Number - 1];
                case Suit.Heart:
                    return cardHeartRotate270[card.Number - 1];
                case Suit.Diamond:
                    return cardDiamondRotate270[card.Number - 1];
                case Suit.Joker:
                    return cardJokerRotate270;
                default:
                    return null;
            }
        }
    }
}
