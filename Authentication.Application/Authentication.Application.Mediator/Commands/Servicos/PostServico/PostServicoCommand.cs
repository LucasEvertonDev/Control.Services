﻿namespace Authentication.Application.Mediator.Commands.Servicos.PostServico;
public class PostServicoCommand : IRequest<Result>, IHandler<PostServicoCommandHandler>
{
    public string Nome { get; set; }

    public string Descricao { get; set; }
}