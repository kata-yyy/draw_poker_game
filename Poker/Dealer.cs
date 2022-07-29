using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PlayingCards.Properties;

namespace PlayingCards
{
    static class Dealer
    {
        /// <summary>
        /// 山札からランダムにカードを渡す時に使う
        /// </summary>
        static Random random = new Random();
        /// <summary>
        /// ジョーカー抜き５２枚のカードリスト
        /// </summary>
        static Card[] originalDeck = new Card[]
        {
            new Card(Suit.Club, 1), new Card(Suit.Club, 2), new Card(Suit.Club, 3),
            new Card(Suit.Club, 4), new Card(Suit.Club, 5), new Card(Suit.Club, 6),
            new Card(Suit.Club, 7), new Card(Suit.Club, 8), new Card(Suit.Club, 9),
            new Card(Suit.Club, 10), new Card(Suit.Club, 11), new Card(Suit.Club, 12),
            new Card(Suit.Club, 13), new Card(Suit.Spade, 1), new Card(Suit.Spade, 2),
            new Card(Suit.Spade, 3), new Card(Suit.Spade, 4), new Card(Suit.Spade, 5),
            new Card(Suit.Spade, 6), new Card(Suit.Spade, 7), new Card(Suit.Spade, 8),
            new Card(Suit.Spade, 9), new Card(Suit.Spade, 10), new Card(Suit.Spade, 11),
            new Card(Suit.Spade, 12), new Card(Suit.Spade, 13), new Card(Suit.Heart, 1),
            new Card(Suit.Heart, 2), new Card(Suit.Heart, 3), new Card(Suit.Heart, 4),
            new Card(Suit.Heart, 5), new Card(Suit.Heart, 6), new Card(Suit.Heart, 7),
            new Card(Suit.Heart, 8), new Card(Suit.Heart, 9), new Card(Suit.Heart, 10),
            new Card(Suit.Heart, 11), new Card(Suit.Heart, 12), new Card(Suit.Heart, 13),
            new Card(Suit.Diamond, 1), new Card(Suit.Diamond, 2), new Card(Suit.Diamond, 3),
            new Card(Suit.Diamond, 4), new Card(Suit.Diamond, 5), new Card(Suit.Diamond, 6),
            new Card(Suit.Diamond, 7), new Card(Suit.Diamond, 8), new Card(Suit.Diamond, 9),
            new Card(Suit.Diamond, 10), new Card(Suit.Diamond, 11), new Card(Suit.Diamond, 12),
            new Card(Suit.Diamond, 13)
        };
        /// <summary>
        /// ジョーカー
        /// </summary>
        static Card originalJoker = new Card(Suit.Joker, 0);

        /// <summary>
        /// 山札
        /// </summary>
        public static List<Card> Deck { get; set; } = new List<Card>();
        /// <summary>
        /// 捨て札
        /// </summary>
        public static List<Card> DiscardList { get; set; } = new List<Card>();

        /// <summary>
        /// 新しく山札を作る
        /// </summary>
        /// <param name="jokerCount">含めるジョーカーの枚数</param>
        public static void CreateDeck(int jokerCount)
        {
            Deck.Clear();
            DiscardList.Clear();

            for(int i = 0; i < 52; i++)
            {
                Deck.Add(originalDeck[i]);
            }

            for (int i = 0; i < jokerCount; i++)
            {
                Deck.Add(originalJoker);
            }
        }

        /// <summary>
        /// 山札からカードを１枚渡す
        /// </summary>
        /// <returns>渡すカード</returns>
        public static Card DealCard()
        {
            int randomNum = random.Next(0, Deck.Count);
            Card card = Deck[randomNum];
            Deck.RemoveAt(randomNum);
            return card;
        }

        /// <summary>
        /// カードを捨て札に追加する
        /// </summary>
        /// <param name="card">捨てるカード</param>
        public static void Discard(Card card)
        {
            DiscardList.Add(card);
        }

        /// <summary>
        /// 手札から役を決定する
        /// </summary>
        /// <param name="hand">手札</param>
        /// <returns>役</returns>
        public static Role GetRole(Card[] hand)
        {
            return Role.Flush;
        }

        /// <summary>
        /// 勝者（一番強い役を作ったプレイヤー）を決める
        /// </summary>
        /// <param name="characterList">フォールドしていない参加プレイヤー</param>
        /// <returns>勝者</returns>
        public static Character WinCharacter(Character[] characterList)
        {
            return new PlayerCharacter("a");
        }

        /// <summary>
        /// 場のチップを勝者へ渡す
        /// </summary>
        /// <param name="winner">勝者</param>
        /// <param name="characterList">全参加プレイヤー</param>
        public static void ChipMove(Character winner, Character[] characterList)
        {

        }

        /// <summary>
        /// 場のチップを全て回収する
        /// </summary>

        public static void ChipDelete()
        {

        }
    }
}
