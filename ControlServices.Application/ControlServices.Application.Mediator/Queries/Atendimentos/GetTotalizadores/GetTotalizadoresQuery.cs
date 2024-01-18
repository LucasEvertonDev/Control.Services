using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Atendimentos.GetTotalizadores;
public class GetTotalizadoresQuery : IRequest<Result>, IHandler<GetTotalizadoresQueryHandler>
{
    [FromQuery(Name = "datainicial")]
    public DateTime? DataInicial { get; set; }

    [FromQuery(Name = "datafinal")]
    public DateTime? DataFinal { get; set; }
}
