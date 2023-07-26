using Alura_JornadaMilhas.DTOs;
using Alura_JornadaMilhas.Models;
using AutoMapper;

namespace Alura_JornadaMilhas.Profiles;

public class DepoimentoProfile: Profile
{
    public DepoimentoProfile()
    {
        CreateMap<DepoimentoDto, Depoimento>();
    }
}
