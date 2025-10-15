using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Evote360.Core.Application.ViewModels.PoliticalParty;

public class PoliticalPartyChangeStateViewModel
{
    public required int Id { get; set; }
    public bool IsActive { get; set; }
}
