using System;

namespace Online_Money_Transfer_MVC.Models
{
    public class MoneyTransfer
    {

        public int Id { get; set; }

        public decimal TransferAmount { get; set; }


        public DateTime TransferDateTime { get; set; }

        public int MoneySenderId { get; set; }

        public int MoneyReceiverId { get; set; }

        public int ProviderId { get; set; }

        public MoneySender MoneySender { get; set; }

        public MoneyReceiver MoneyReceiver { get; set; }

        public Provider Provider { get; set; }
    }
}
