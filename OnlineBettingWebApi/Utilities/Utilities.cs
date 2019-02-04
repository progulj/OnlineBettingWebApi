using OnlineBettingWebApi.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBettingWebApi.Utilities
{

    public class Validator
    {
        public bool IsValid => GetViolations().Count() == 0;
        public TicketViewModel ticketViewModel;

        public Validator(TicketViewModel tVM)
        {
            this.ticketViewModel = tVM;
        }


        public IEnumerable<string> GetViolations()
        {

            if (AreGamesOnTicket(this.ticketViewModel))
                yield return "Please select at least one pair!";
            else if (IsSpecialConditionMet(this.ticketViewModel))
                yield return "Need five regular offers with odds 1.1 or more!";
            else if (IsMinWagerValid(this.ticketViewModel))
                yield return "Minimum wager is 10$!";
            else if (IsMaxWagerValid(this.ticketViewModel))
                yield return "Maximum wager is 100$!";

            yield break;
        }

        public bool AreGamesOnTicket(TicketViewModel ticketViewModel)
        {

            if (ticketViewModel != null && ticketViewModel.Games.Count() > 0)
                return false;
            else
                return true;
        }

        public bool IsSpecialConditionMet(TicketViewModel ticketViewModel)
        {
            bool special = false;
            int regularCount = 0;

            foreach (GameViewModel game in ticketViewModel.Games)
            {
                if (game.Special == true)
                {
                    special = true;
                }
                else
                {
                    if (decimal.Parse(game.Odds) >= decimal.Parse("1.1"))
                    {
                        regularCount++;
                    }
                }
            }
            if (special)
            {
                if (regularCount >= 5)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsMinWagerValid(TicketViewModel ticketViewModel)
        {
            if ((decimal)ticketViewModel.FullPayment >= 10)
            {
                return false;

            }
            return true;

        }

        public bool IsMaxWagerValid(TicketViewModel ticketViewModel)
        {
            if ((decimal)ticketViewModel.FullPayment <= 100)
            {
                return false;

            }
            return true;

        }
    }


}
