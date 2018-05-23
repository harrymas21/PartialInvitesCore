using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace PartyInvites.DTO
{
    public class GuestResponseDTO
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please specify whether you will attend")]
        public bool? WillAttend { get; set; }
    }
}
