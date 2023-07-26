using Alura_JornadaMilhas.DTOs;
using Alura_JornadaMilhas.Models;
using AutoMapper;

namespace Alura_JornadaMilhas.Profiles;

public class DestinoProfile: Profile
{
    public DestinoProfile()
    {
        CreateMap<CreateDestinoDto, Destino>();
    }
}
