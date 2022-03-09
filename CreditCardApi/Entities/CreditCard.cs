using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApi.Entities
{
    public class CreditCard
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CardId { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpireDate { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
       
    }
}
