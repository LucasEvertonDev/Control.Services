namespace ControlServices.Application.Mediator.Commands.Custos.PostCusto;
public class PostCustoCommand : IRequest<Result>, IHandler<PostCustoCommandHandler>
{
    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public string Descricao { get; set; }
}