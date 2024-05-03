using AutoMapper;
using FribergHomes.API.DTOs;
using FribergHomes.API.Models;

namespace FribergHomes.API.Mappers
{
    public class RealtorFullNameResolver : IValueResolver<RealtorDTO, Realtor, string>
    {
        public string Resolve(RealtorDTO src, Realtor dest, string fullName, ResolutionContext context)
        {
            var names = src.FullName.Split(' ');
            dest.FirstName = names[0];
            dest.LastName = names[1];

            return fullName;
        }
    }
}
