using AutoMapper;
using PartyInvites.DTO;
using PartyInvites.Models;

namespace PartyInvites.AutomapperProfiles
{
    public class PartyInvitesProfile:Profile
    {
        public PartyInvitesProfile()
        {
            CreateMap<GuestResponse, GuestResponseDTO>();
        }
    }
}
