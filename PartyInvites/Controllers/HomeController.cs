using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PartyInvites.DTO;
using PartyInvites.Infrastructure;
using PartyInvites.Models;
using PartyInvites.RabbitProducer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        private IMongoDatabase mongoDatabase;

        private readonly IMapper _iMapper;

        private readonly IProducer _producer;

        public HomeController(IMapper iMapper, IProducer producer)
        {
            _iMapper = iMapper;

            _producer = producer;

        }
        //Generic method to get the mongodb database details
        public IMongoDatabase GetMongoDatabase()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(DefaultSettings.Instance.Hostname, 27017);
            var mongoClient = new MongoClient(settings);
            return mongoClient.GetDatabase(DefaultSettings.Instance.Database);
        }

        public IActionResult Index()
        {
            int hour = DateTime.Now.Hour;

            ViewBag.Greetings = hour < 12 ? "Good Morning World!" : "Good Afternoon World";

            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponseDTO guestResponseDTO)
        {
            if (ModelState.IsValid)
            {
                var guestResponse = _iMapper.Map<GuestResponse>(guestResponseDTO);

                mongoDatabase = GetMongoDatabase();

                mongoDatabase.GetCollection<GuestResponse>(DefaultSettings.Instance.Collection).InsertOne(guestResponse);

                _producer.SendMessage(guestResponseDTO.Email);

                return View("Thanks", guestResponseDTO);
            }
            else
            {
                //there's some validation error
                return View("RsvpForm", guestResponseDTO);
            }
        }

        public ViewResult ListResponses()
        {
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();

            //fetch the details from CustomerDB and pass into view  
            var result = mongoDatabase.GetCollection<GuestResponse>(DefaultSettings.Instance.Collection).Find(FilterDefinition<GuestResponse>.Empty).ToList();

            var guestResponseDTOList = _iMapper.Map<List<GuestResponseDTO>>(result);

            return View(guestResponseDTOList);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
