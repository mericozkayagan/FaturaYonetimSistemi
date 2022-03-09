﻿using CreditCardApi.Application.CreditCardOperations.Commands.CreateCreditCardCommand;
using CreditCardApi.DbOperations;
using CreditCardApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _cardService;
        public CreditCardController(ICreditCardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public IActionResult GetCards()
        {
            return Ok(_cardService.GetCards());
        }

        //[HttpGet("{id}", Name = "GetCard")]
        //public IActionResult GetCard(string id)
        //{
        //    return Ok(_cardService.GetCard(id));
        //}

        [HttpPost]
        public IActionResult AddCreditCard(CreditCard card)
        {
            _cardService.AddCreditCard(card);
            return CreatedAtRoute("GetCard", new { id = card.CardId }, card);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(string id)
        {
            _cardService.DeleteCard(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateCard(CreditCard card)
        {
            return Ok(_cardService.UpdateCard(card));
        }

        [HttpGet("{id}")]
        public IActionResult GetUserCard(int id)
        {
            return Ok(_cardService.GetUserCard(id));
        }
    }
}
