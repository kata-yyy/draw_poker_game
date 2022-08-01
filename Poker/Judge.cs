using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayingCards
{
    internal class Judge
    {
        /// <summary>
        /// 手札の役を判定する
        /// </summary>
        /// <param name="hand">手札</param>
        /// <returns>役</returns>
        public static Role GetRole(Card[] hand)
        {
            // 手札の各数字と各スートをカウントする
            int[] numCount = HandNumCount(hand);
            int[] suitCount = HandSuitCount(hand);

            // ロイヤルストレートフラッシュの判定
            if (IsRoyalFlush(numCount, suitCount))
            {
                return Role.RoyalFlush;
            }
            // ストレートフラッシュの判定
            if (IsStraightFlush(numCount, suitCount))
            {
                return Role.StraightFlush;
            }
            // フォーカードの判定
            if (IsFourOfKind(numCount))
            {
                return Role.FourOfKind;
            }
            // フルハウスの判定
            if (IsFullHouse(numCount))
            {
                return Role.FullHouse;
            }
            // フラッシュの判定
            if (IsFlush(suitCount))
            {
                return Role.Flush;
            }
            // ストレートの判定
            if (IsStraight(numCount))
            {
                return Role.Straight;
            }
            // スリーカードの判定
            if (IsThreeOfKind(numCount))
            {
                return Role.ThreeOfKind;
            }
            // ツーペアの判定
            if (IsTwoPair(numCount))
            {
                return Role.TwoPair;
            }
            // ワンペアの判定
            if (IsPair(numCount))
            {
                return Role.Pair;
            }
            // 役なし
            return Role.HighCard;
        }

        /// <summary>
        /// 手札の各数字をカウントする
        /// </summary>
        /// <param name="hand">手札</param>
        /// <returns>各数字のカウント（添え字とカードの数字が対応）</returns>
        public static int[] HandNumCount(Card[] hand)
        {
            int[] numCount = new int[14] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < hand.Length; i++)
            {
                numCount[hand[i].Number]++;
            }

            return numCount;
        }

        /// <summary>
        /// 手札の各スートをカウントする
        /// </summary>
        /// <param name="hand">手札</param>
        /// <returns>各スートのカウント（添え字とスートの列挙型が対応）</returns>
        public static int[] HandSuitCount(Card[] hand)
        {
            int[] suitCount = new int[4] { 0, 0, 0, 0 };

            for (int i = 0; i < hand.Length; i++)
            {
                suitCount[(int)hand[i].Suit]++;
            }

            return suitCount;
        }

        /// <summary>
        /// ロイヤルストレートフラッシュの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <param name="suitCount">手札にある各スートのカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsRoyalFlush(int[] numCount, int[] suitCount)
        {
            // フラッシュが成立する
            if (IsFlush(suitCount))
            {
                // 数字が「１０」、「１１」、「１２」、「１３」、「１」
                if (numCount[10] == 1 && numCount[11] == 1 && numCount[12] == 1 &&
                    numCount[13] == 1 && numCount[1] == 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ストレートフラッシュの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <param name="suitCount">手札にある各スートのカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsStraightFlush(int[] numCount, int[] suitCount)
        {
            // フラッシュとストレートが両方成立するならtrue
            if (IsFlush(suitCount) && IsStraight(numCount))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// フォーカードの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsFourOfKind(int[] numCount)
        {
            // カウントが４の数字があればtrue
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == 4)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// フルハウスの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsFullHouse(int[] numCount)
        {
            bool isCount3 = false;
            bool isCount2 = false;

            // カウントが３である数字が存在する
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == 3)
                {
                    isCount3 = true;
                }
            }

            // カウントが２である数字が存在する
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == 2)
                {
                    isCount2 = true;
                }
            }

            // 両方存在していた場合はtrue
            if (isCount3 && isCount2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// フラッシュの判定
        /// </summary>
        /// <param name="suitCount">手札にある各スートのカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsFlush(int[] suitCount)
        {
            // カウントが５であるスートが存在するならtrue
            if (suitCount[(int)Suit.Spade] == 5 || suitCount[(int)Suit.Heart] == 5 ||
                suitCount[(int)Suit.Diamond] == 5 || suitCount[(int)Suit.Club] == 5)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ストレートの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsStraight(int[] numCount)
        {
            // 数字が「１０」、「１１」、「１２」、「１３」、「１」ならtrue
            if (numCount[10] == 1 && numCount[11] == 1 && numCount[12] == 1 &&
                numCount[13] == 1 && numCount[1] == 1)
            {
                return true;
            }

            // ５連続で数字のカウントが１ならture
            for (int i = 1; i <= 9; i++)
            {
                for (int j = i; j <= i + 4; j++)
                {
                    if (numCount[j] != 1)
                    {
                        break;
                    }

                    if (j == i + 4)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// スリーカードの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsThreeOfKind(int[] numCount)
        {
            // カウントが３の数字があればtrue
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == 3)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ツーペアの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsTwoPair(int[] numCount)
        {
            int pairCount = 0;

            // ペアの数をカウントする
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == 2)
                {
                    pairCount++;
                }
            }

            // ペアの数が２以上であればtrue
            if (pairCount >= 2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ワンペアの判定
        /// </summary>
        /// <param name="numCount">手札にある各数字のカウント</param>
        /// <returns>true：成立、false：不成立</returns>
        public static bool IsPair(int[] numCount)
        {
            // カウントが２の数字があればtrue
            for (int i = 1; i <= 13; i++)
            {
                if (numCount[i] == 2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 参加者一覧から勝者を判定する
        /// </summary>
        /// <param name="characterList">参加者</param>
        /// <returns>勝者</returns>
        public static Character WinCharacter(List<Character> characterList)
        {
            Character winner = characterList[0];

            for (int i = 1; i < characterList.Count; i++)
            {
                winner = GetWinner(winner, characterList[i]);
            }

            return winner;
        }

        /// <summary>
        /// ２人のキャラクターから勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character GetWinner(Character character1, Character character2)
        {
            if (character1.MyRole > character2.MyRole)
            {
                return character1;
            }
            else if (character1.MyRole < character2.MyRole)
            {
                return character2;
            }
            else
            {
                return WinnerRoleDraw(character1, character2);
            }
        }

        /// <summary>
        /// 役が同じ場合の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerRoleDraw(Character character1, Character character2)
        {
            switch (character1.MyRole)
            {
                case Role.RoyalFlush:
                    return WinnerRoyalFlush(character1, character2);
                case Role.StraightFlush:
                    return WinnerStraightFlush(character1, character2);
                case Role.FourOfKind:
                    return WinnerFourOfKind(character1, character2);
                case Role.FullHouse:
                    return WinnerFullHouse(character1, character2);
                case Role.Flush:
                    return WinnerFlush(character1, character2);
                case Role.Straight:
                    return WinnerStraight(character1, character2);
                case Role.ThreeOfKind:
                    return WinnerThreeOfKind(character1, character2);
                case Role.TwoPair:
                    return WinnerTwoPair(character1, character2);
                case Role.Pair:
                    return WinnerPair(character1, character2);
                case Role.HighCard:
                    return WinnerHighCard(character1, character2);
                default:
                    return character1;
            }
        }

        /// <summary>
        /// ロイヤルストレートフラッシュ同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerRoyalFlush(Character character1, Character character2)
        {
            Suit roleSuit1 = Suit.Club;
            Suit roleSuit2 = Suit.Club;

            int[] suitCount1 = HandSuitCount(character1.Hand);
            int[] suitCount2 = HandSuitCount(character2.Hand);

            // 各キャラクターの手札からスートを取得する
            for (int i = 0; i < 4; i++)
            {
                if (suitCount1[i] == 5)
                {
                    roleSuit1 = (Suit)i;
                }

                if (suitCount2[i] == 5)
                {
                    roleSuit2 = (Suit)i;
                }
            }

            // スートが強い方を勝者とする
            if (roleSuit1 > roleSuit2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// ストレートフラッシュ同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerStraightFlush(Character character1, Character character2)
        {
            // 手札の中で１番大きい数字で判定を行う
            int roleNum1 = numConversion(character1.Hand.ToList().Max(x => x.Number));
            int roleNum2 = numConversion(character2.Hand.ToList().Max(x => x.Number));

            // 手札の数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }
            if (roleNum1 < roleNum2)
            {
                return character2;
            }

            // ロイヤルストレートフラッシュと同様の判定を行う
            return WinnerRoyalFlush(character1, character2);
        }

        /// <summary>
        /// フォーカード同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerFourOfKind(Character character1, Character character2)
        {
            int roleNum1 = 0;
            int roleNum2 = 0;

            int[] numCount1 = HandNumCount(character1.Hand);
            int[] numCount2 = HandNumCount(character2.Hand);

            // フォーカードになっている数字を取得する
            for (int i = 1; i <= 13; i++)
            {
                if (numCount1[i] == 4)
                {
                    roleNum1 = numConversion(i);
                }

                if (numCount2[i] == 4)
                {
                    roleNum2 = numConversion(i);
                }
            }

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// フルハウス同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerFullHouse(Character character1, Character character2)
        {
            int roleNum1 = 0;
            int roleNum2 = 0;

            int[] numCount1 = HandNumCount(character1.Hand);
            int[] numCount2 = HandNumCount(character2.Hand);

            // スリーカードになっている数字を取得する
            for (int i = 1; i <= 13; i++)
            {
                if (numCount1[i] == 3)
                {
                    roleNum1 = numConversion(i);
                }

                if (numCount2[i] == 3)
                {
                    roleNum2 = numConversion(i);
                }
            }

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// フラッシュ同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerFlush(Character character1, Character character2)
        {
            // ストレートフラッシュと同じ判定を行う
            return WinnerStraightFlush(character1, character2);
        }

        /// <summary>
        /// ストレート同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerStraight(Character character1, Character character2)
        {
            // 手札の中で一番大きい数字を取得する
            int roleNum1 = numConversion(character1.Hand.ToList().Max(x => x.Number));
            int roleNum2 = numConversion(character2.Hand.ToList().Max(x => x.Number));

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }
            if (roleNum1 < roleNum2)
            {
                return character2;
            }

            // 数字が同じ場合は、そのスートを取得する
            Suit roleSuit1 = character1.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);
            Suit roleSuit2 = character2.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);

            // スートが強い方を勝者とする
            if (roleSuit1 > roleSuit2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// スリーカード同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerThreeOfKind(Character character1, Character character2)
        {
            int roleNum1 = 0;
            int roleNum2 = 0;

            int[] numCount1 = HandNumCount(character1.Hand);
            int[] numCount2 = HandNumCount(character2.Hand);

            // スリーカードになっている数字を取得する
            for (int i = 1; i <= 13; i++)
            {
                if (numCount1[i] == 3)
                {
                    roleNum1 = numConversion(i);
                }

                if (numCount2[i] == 3)
                {
                    roleNum2 = numConversion(i);
                }
            }

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// ツーペア同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerTwoPair(Character character1, Character character2)
        {
            int roleNum1 = 0;
            int roleNum2 = 0;

            int[] numCount1 = HandNumCount(character1.Hand);
            int[] numCount2 = HandNumCount(character2.Hand);

            // ペアになっている数字の強い方を取得する
            for (int i = 2; i <= 13; i++)
            {
                if (numCount1[i] == 2)
                {
                    roleNum1 = i;
                }

                if (numCount2[i] == 2)
                {
                    roleNum2 = i;
                }
            }

            // １がペアになっている場合、それが一番強い数字となる
            if (numCount1[1] == 2)
            {
                roleNum1 = 14;
            }
            if (numCount2[1] == 2)
            {
                roleNum2 = 14;
            }

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }
            else if (roleNum1 < roleNum2)
            {
                return character2;
            }

            // 数字が同じ場合は、そのスート（強い方）を取得する
            Suit roleSuit1 = character1.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);
            Suit roleSuit2 = character2.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);

            // スートが強い方を勝者とする
            if (roleSuit1 > roleSuit2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// ワンペア同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerPair(Character character1, Character character2)
        {
            int roleNum1 = 0;
            int roleNum2 = 0;

            int[] numCount1 = HandNumCount(character1.Hand);
            int[] numCount2 = HandNumCount(character2.Hand);

            // ペアになっている数字を取得する
            for (int i = 1; i <= 13; i++)
            {
                if (numCount1[i] == 2)
                {
                    roleNum1 = numConversion(i);
                }

                if (numCount2[i] == 2)
                {
                    roleNum2 = numConversion(i);
                }
            }

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }
            else if (roleNum1 < roleNum2)
            {
                return character2;

            }

            // 数字が同じ場合は、そのスート（強い方）を取得する
            Suit roleSuit1 = character1.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);
            Suit roleSuit2 = character2.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);

            // スートが強い方を勝者とする
            if (roleSuit1 > roleSuit2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// ハイカード同士の勝者を判定する
        /// </summary>
        /// <param name="character1">１人目</param>
        /// <param name="character2">２人目</param>
        /// <returns>勝者</returns>
        public static Character WinnerHighCard(Character character1, Character character2)
        {
            // 手札の中で一番大きい数字を取得する
            int roleNum1 = numConversion(character1.Hand.ToList().Max(x => x.Number));
            int roleNum2 = numConversion(character2.Hand.ToList().Max(x => x.Number));

            // 数字が大きいほうを勝者とする
            if (roleNum1 > roleNum2)
            {
                return character1;
            }
            if (roleNum1 < roleNum2)
            {
                return character2;
            }

            // 数字が同じ場合は、そのスートを取得する
            Suit roleSuit1 = character1.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);
            Suit roleSuit2 = character2.Hand.ToList().Where(x => x.Number == roleNum1).Max(x => x.Suit);

            // スートが強い方を勝者とする
            if (roleSuit1 > roleSuit2)
            {
                return character1;
            }

            return character2;
        }

        /// <summary>
        /// １を一番強い数字とするための変換を行う
        /// １が渡されたら１４を返し、１以外が渡されたら同じ数字を返す
        /// </summary>
        /// <param name="num">カードの数字</param>
        /// <returns></returns>
        public static int numConversion(int num)
        {
            if (num == 1)
            {
                return 14;
            }

            return num;
        }
    }
}
