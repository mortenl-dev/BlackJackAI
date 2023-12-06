
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography.X509Certificates;

public class BlackJackSingle {

    #region INIT
    public static void Main () {
        BlackJackSingle Game = new BlackJackSingle();
        
        // ASK PLAYER IF STD DEV OR AUTO

        string act = Console.ReadLine()!;
        switch (act) {
            case "run std game":
                Game.Start("std");
                break;
            case "run dev game":
                Game.Start("dev");
                break;
            case "run auto game":
                Game.Start("auto");
                int simCount = Int32.Parse(Console.ReadLine()!);
                while(simCount > 0) {
                    simCount--;
                }
                break;
        }
        
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

    List<int> cardNumbers = new List<int>();
    int i = 0;
    bool debug = false;
    public static int pd = 0; //GETS INCREMENTED EACH NEW DRAWN CARD IN DEBUGMODE IN THE DRAWDEBUG FUNCTION

    #endregion
    public void Start(string version) {

        switch (version) {
            case "std":
                //ask for bets
                string inputBet = Console.ReadLine()!;

                Player.bet = Int16.Parse(inputBet); //int16 because why would i need more
                stack-=Player.bet;
                
                seats.Add(Player);

                //reveal players two cards
                Player.cards.Add(deck.Draw(Player, "Player"));
                Player.cards.Add(deck.Draw(Player, "Player"));

                //reveal bank 1 card
                Bank.cards.Add(deck.Draw(Bank, "Bank"));
                //ask player options
                Ask(Player); 
                break;
            case "dev":
                //ask for bets
                debug = true;
                Player.bet = Int16.Parse(Console.ReadLine()!); //int16 because why would i need more
                stack-=Player.bet;
                
                seats.Add(Player);
                Console.WriteLine("Please state the numbers to be drawn for the player in-order.");
                Console.WriteLine("To continue, please enter \"continue\"");

                int counter = 0;
                
                
                string cont = "";
                while (true) {
                    cont = Console.ReadLine()!;
                    if (cont == "continue") {
                        break;
                    }
                    cardNumbers.Add(Int32.Parse(cont));
                    counter++;
                }

                //reveal players two cards
                Player.cards.Add(deck.DrawDebug(Player, "Player",cardNumbers[i]));
                Player.cards.Add(deck.DrawDebug(Player, "Player",cardNumbers[i]));
                //reveal bank 1 card
                Bank.cards.Add(deck.DrawDebug(Bank, "Bank",cardNumbers[i]));
                //ask player options
                Ask(Player); 

                break;
            case "auto":

                break;
        }
              
        
    }

    public void Hit() {
        if (debug) {
            Player.cards.Add(deck.DrawDebug(Player, "Player",cardNumbers[i]));
            CheckFor21AndAsk();
        }
        else {
            Player.cards.Add(deck.Draw(Player, "Player"));
            CheckFor21AndAsk();
        }
        
        
        
    }

    public void CheckFor21AndAsk() {
        if (Player.total > 21) {
            if (Player.ace) {
                Player.total-=10;
            }
            else {

                if (currentSeat != players) {
                    MoveOnToNextPlayer();
                }

                //TIMER

                Thread.Sleep(5000);
                return;
            }
        }
        Ask(Player);
    }

    public void MoveOnToNextPlayer () {
        currentSeat++;
        Player = seats[currentSeat-1];
        Player.cards.Add(deck.Draw(Player, "Player"));
        Ask(Player);
    }
    public void Ask (Seat seat) {
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
        stack-=split.bet;
        seats.Add(split);
        Seat prevSeat = seats[seats.Count-2];
        //give one card to the new seat
        split.cards.Add(prevSeat.cards[0]);
        //remove the card from the previous seat
        prevSeat.cards.RemoveAt(0);
        //subtract it from total
        if (prevSeat.cards[0].Number == 1) {
            prevSeat.total -= 1;
            split.total+=11;
            split.ace = true;
        }
        else {
            prevSeat.total-=prevSeat.cards[0].Value;
            split.total+=prevSeat.cards[0].Value;
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
        if (players > 1) {
            if (debug) {
               while (Bank.total < 17) {
                int winCount = 0;
                Bank.cards.Add(deck.DrawDebug(Bank, "Bank",cardNumbers[i]));
                foreach (Seat s in seats) {
                    if (Bank.total > s.total) {
                        winCount++;
                    }
                }
                if (winCount == players) {
                    Compare();
                    break;
                }
                if (Bank.total > 21) {
                    Player.bet *= 2;
        
                    //TIMER

                    Thread.Sleep(5000);

                    return;
                }

            } 
            }
            else {
                while (Bank.total < 17) {
                int winCount = 0;
                Bank.cards.Add(deck.Draw(Bank, "Bank"));
                foreach (Seat s in seats) {
                    if (Bank.total > s.total) {
                        winCount++;
                    }
                }
                if (winCount == players) {
                    Compare();
                    break;
                }
                if (Bank.total > 21) {
                    Player.bet *= 2;
        
                    //TIMER

                    Thread.Sleep(5000);

                    return;
                }

            }
            }
            
        }
        else {

        }
        while (Bank.total < 17) {
            Bank.cards.Add(deck.Draw(Bank, "Bank"));
            if (Bank.total > 21) {
                Player.bet *= 2;
                return;
            }
            if (Bank.total > Player.total) {
                Compare();
                break;
            }

        }
        Compare();

    }

    public void Compare() {
        for (int i = 0; i < seats.Count; i++)
        {   
            Seat s = seats[i];
            if (s.total > Bank.total) {

                s.bet *= 2;
            }
            else if (s.total == Bank.total) {
            }
            else {

                s.bet = 0;
            }
            Console.WriteLine(s.bet.ToString());
        }
        
        //TIMER

        Thread.Sleep(5000);
    }
}

