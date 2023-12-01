
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography.X509Certificates;

public class BlackJackSingle {

    #region INIT
    public static void Main () {
        BlackJackSingle Game = new BlackJackSingle();
        
        Game.Start();
    }
    #endregion

    #region VARIABLES
    int stack = 100;
    int currentSeat = 1;
    int players = 1;
    Seat Player = new Seat();
    Seat Bank = new Seat();
    Deck deck = new Deck();
    List<Seat> seats = new List<Seat>();

    #endregion
    public void Start() {
        //ask for bets
        Console.WriteLine("Please state your bet.");
        string inputBet = Console.ReadLine()!;

        Player.bet = Int16.Parse(inputBet); //int16 because why would i need more
        
        seats.Add(Player);

        //reveal players two cards
        Player.cards.Add(deck.Draw(Player, "Player"));
        Player.cards.Add(deck.Draw(Player, "Player"));

        if (Player.total == 21) {
            Console.WriteLine("Blackjack!");
        }
        //Console.WriteLine($"{}");

        Console.WriteLine($"Your total is now {Player.total}.");

        //reveal bank 1 card
        Bank.cards.Add(deck.Draw(Bank, "Bank"));
        //ask player options
        Ask(Player);
        
        //hit
        //draw new card
        //show new card
        //if bust: end game
        //else: ask options again but not double

        //stand
        //start with banks turns
        
        //double
        //double bets, otherwise its just a hit

        //split
        //most complicated, ig just create new seat instance  --> do this last

        //finally: compare totals of Player and Bank seats
        //win or loss
        //start again
        
    }

    public void Hit() {
        Player.cards.Add(deck.Draw(Player, "Player"));
        if (Player.total > 21) {
            if (Player.ace) {
                Player.total-=10;
            }
            else {
                Console.WriteLine($"Your total is now {Player.total}.");
                Console.WriteLine("You busted!");
                if (currentSeat != players) {
                    MoveOnToNextPlayer();
                }
                return;
            }
        }
        Console.WriteLine($"Your total is now {Player.total}.");
        Ask(Player);
        
    }

    public void MoveOnToNextPlayer () {
        currentSeat++;
        Player = seats[currentSeat-1];
        Player.cards.Add(deck.DrawTest(Player, "Player"));
        Ask(Player);
    }
    public void Ask (Seat seat) {
        Console.WriteLine("Please choose your next option.");
        string Choice = Console.ReadLine()!;
            if (seat.firstTurn) {
                switch (Choice) {
                case "Hit":
                    seat.firstTurn = false;
                    Hit();
                    break;
                case "Stand":
                    Stand();
                    break;
                case "Split":
                    if (seat.cards[0].Number == seat.cards[1].Number) {
                        seat.firstTurn = false;
                        Split();
                        Ask(seat);
                    }
                    break;
                case "Double":
                    seat.firstTurn = false;
                    Double();
                    break;
                default:
                    Console.WriteLine("Please try again.");
                    break;
                }
                
            }
            else {
                switch (Choice) {
                case "Hit":
                    Hit();
                    break;
                case "Stand":
                    Stand();
                    break;
                default:
                    Console.WriteLine("Please try again.");
                    break;
                }
            }
            
    }
    public void Stand() {
        if (players == currentSeat) {
            BanksTurn();
        }
        else {
            MoveOnToNextPlayer();
        }
    }

    public void Split() {
        
        Seat split = new Seat();
        split.bet = Player.bet;
        seats.Add(split);
        Seat prevSeat = seats[seats.Count-2];
        //give one card to the new seat
        split.cards.Add(prevSeat.cards[0]);
        //remove the card from the previous seat
        prevSeat.cards.RemoveAt(0);
        //subtract it from total
        if (prevSeat.cards[0].Number > 10) {
            prevSeat.total -= 10;
            split.total+=10;
        }
        else if (prevSeat.cards[0].Number == 1) {
            prevSeat.total -= 1;
            split.total+=11;
            split.ace = true;
        }
        else {
            prevSeat.total -= prevSeat.cards[0].Number;
            split.total+=prevSeat.cards[0].Number;
        }
        
        players++;
        
    }

    public void Double() {
        Player.bet*=2;
        Hit();
        //double bets
        //hit
    }

    public void BanksTurn() {
        while (Bank.total < 17) {
            Bank.cards.Add(deck.Draw(Bank, "Bank"));
            Console.WriteLine($"Banks total is now {Bank.total}.");
            if (Bank.total > 21) {
                Console.WriteLine("You win!");
                Player.bet *= 2;
                Console.WriteLine(Player.bet.ToString());
                return;
            }

        }
        Compare();

    }

    public void Compare() {
        for (int i = 0; i < seats.Count; i++)
        {   
            Seat s = seats[i];
            if (s.total > Bank.total) {
                Console.WriteLine("You win!");

                s.bet *= 2;
            }
            else if (s.total == Bank.total) {
                Console.WriteLine("Pushing...");
            }
            else {
                Console.WriteLine("The Bank wins!");

                s.bet = 0;
            }
            Console.WriteLine(s.bet.ToString());
        }
    }
}

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
}

public class Deck {
        public static List<int> Numbers = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13};
        public static Random rmd = new Random();
        public Dictionary<string, List<int>> Cards = new Dictionary<string, List<int>>(){
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
            c.Number = Numbers[n];

            switch (c.Number) {
            case 1: 
                if (seat.total < 12 && !seat.ace) {
                    seat.total+=11;
                    seat.ace = true;
                }
                else {
                    seat.total+=c.Number;
                }
                Console.WriteLine($"{Drawer} drew the Ace of {c.Family}.");
                break;
            case 13:
                Console.WriteLine($"{Drawer} drew the King of {c.Family}.");
                seat.total+=10;
                break;
            case 12:
                Console.WriteLine($"{Drawer} drew the Queen of {c.Family}.");
                seat.total+=10;
                break;
            case 11:
                Console.WriteLine($"{Drawer} drew the Jack of {c.Family}.");
                seat.total+=10;
                break;
            default:
                Console.WriteLine($"{Drawer} drew the {c.Number} of {c.Family}.");
                seat.total+=c.Number;
                break;
            }
            
            
            return c;
        }

        public Card DrawTest (Seat seat, string Drawer) {
            
            Card c = new Card();
            int i = rmd.Next(0,4);
            int n = 0;

            c.Family = Cards.ElementAt(i).Key;
            c.Number = Numbers[n];

            switch (c.Number) {
            case 1: 
                if (seat.total < 12 && !seat.ace) {
                    seat.total+=11;
                    seat.ace = true;
                }
                else {
                    seat.total+=c.Number;
                }
                Console.WriteLine($"{Drawer} drew the Ace of {c.Family}.");
                break;
            case 13:
                Console.WriteLine($"{Drawer} drew the King of {c.Family}.");
                seat.total+=10;
                break;
            case 12:
                Console.WriteLine($"{Drawer} drew the Queen of {c.Family}.");
                seat.total+=10;
                break;
            case 11:
                Console.WriteLine($"{Drawer} drew the Jack of {c.Family}.");
                seat.total+=10;
                break;
            default:
                Console.WriteLine($"{Drawer} drew the {c.Number} of {c.Family}.");
                seat.total+=c.Number;
                break;
            }
            
            
            return c;
        }
}

