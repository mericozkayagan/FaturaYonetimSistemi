using CreditCardApi.Application.CreditCardOperations.Commands.CreateCreditCardCommand;
using CreditCardApi.DbOperations;
using CreditCardApi.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApi.Application.CreditCardOperations
{
    public class CreditCardServices : ICreditCardService
    {
        private readonly IMongoCollection<CreditCard> _cards;
        public CreditCardServices(IDbClient dbClient)
        {
           _cards = dbClient.GetCreditCardsCollection();
        }

        public CreditCard AddCreditCard(CreditCard card)
        {
            _cards.InsertOne(card);
            return card;
        }

        public void DeleteCard(string id)
        {
            _cards.DeleteOne(x => x.CardId == id);
        }

        public CreditCard GetCard(string id)
        {
            return _cards.Find(x => x.CardId == id).FirstOrDefault();
        }

        public List<CreditCard> GetCards()
        {
            return _cards.Find(x => x.UserId > 0).ToList();
        }

        public CreditCard GetUserCard(int id)
        {
            return _cards.Find(x => x.UserId == id).FirstOrDefault();
        }

        public CreditCard UpdateCard(CreditCard card)
        {
            GetCard(card.CardId);
            _cards.ReplaceOne(x => x.CardId == card.CardId, card);
            return card;
        }
    }
}
