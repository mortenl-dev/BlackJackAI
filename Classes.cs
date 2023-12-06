using System.Reflection.PortableExecutable;

public class Seat {
    public int total = 0;
    public List<Card> cards = new List<Card>();
    public bool ace = false;
    public bool firstTurn = true;
    public int bet = 0;
}

public class Card {
    public string? Family;
    public int Number;
    public int Value;
}

public class Deck {
        public static Dictionary<int,int> Numbers = new Dictionary<int,int>(){{1,1},{2,2},{3,3},{4,4},{5,5},{6,6},{7,7},{8,8},{9,9},{10,10},{11,10},{12,10},{13,10}};
        public static Random rmd = new Random();
        public Dictionary<string, Dictionary<int,int>> Cards = new Dictionary<string, Dictionary<int,int>>(){
            {"Clubs", Numbers},
            {"Diamonds", Numbers},
            {"Hearts", Numbers},
            {"Spades", Numbers},
            };
        public Card Draw (Seat seat, string Drawer) {
            
            Card c = new Card();
            int i = rmd.Next(0,4);
            int n = rmd.Next(0,13);

            c.Family = Cards.ElementAt(i).Key;
            c.Number = Numbers.ElementAt(n).Key;
            c.Value = Numbers[n+1];

            if (c.Number == 1) {
                if (seat.total < 12 && !seat.ace) {
                    seat.total+=11;
                    seat.ace = true;
                }
                else {
                    seat.total+=c.Value;
                }
                return c;
            }

            seat.total+=c.Value;
            
            return c;
        }

        public Card DrawDebug (Seat seat, string Drawer, int cardNumber) {
            BlackJackSingle.pd++;
            Card c = new Card();
            int i = rmd.Next(0,4);
            int n = cardNumber;

            c.Family = Cards.ElementAt(i).Key;
            c.Number = Numbers.ElementAt(n).Key;
            c.Value = Numbers[n];

            if (c.Number == 1) {
                if (seat.total < 12 && !seat.ace) {
                    seat.total+=11;
                    seat.ace = true;
                }
                else {
                    seat.total+=c.Value;
                }
                return c;
            }

            seat.total+=c.Value;
            
            
            return c;
        }
}

