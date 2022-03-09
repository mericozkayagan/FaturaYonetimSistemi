using CreditCardApi.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApi.DbOperations
{
    public interface IDbClient
    {
        IMongoCollection<CreditCard> GetCreditCardsCollection();
    }
}
