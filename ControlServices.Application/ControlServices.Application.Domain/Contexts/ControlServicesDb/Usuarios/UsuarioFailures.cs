namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios;

public static class UsuarioFailures
{
    public static readonly FailureModel EmailObrigatorio = new("EmailObrigatorio", "Email é obrigatório.");

    public static readonly FailureModel EmailInvalido = new("EmailInvalido", "Email não se encontra em um formato válido.");

    public static readonly FailureModel SenhaObrigatoria = new("SenhaObrigatoria", "Senha é obrigatória.");

    public static readonly FailureModel SenhaDeveTer8Caracteres = new("SenhaDeveTer8Caracteres", "Senha deve ter no mínimo 8 caracteres.");

    public static readonly FailureModel EmailSenhaInvalidos = new("EmailSenhaInvalidos", "Email ou senha inválidos");

    public static readonly FailureModel NaoFoiPossivelRecuperarUsuarioLogado = new("NaoFoiPossivelRecuperarUsuarioLogado", "Não foi possível recuperar o usuário logado");

    public static readonly FailureModel EmailExistente = new("EmailExistente", "Já existe um email cadastrado para o usuário corrente.");

    public static readonly FailureModel ChaveHashObrigatoria = new("ChaveHashObrigatoria", "Chave hash é obrigatória.");

    public static readonly FailureModel NomeObrigatorio = new("NomeObrigatorio", "O nome é obrigatório!");

    public static readonly FailureModel UsuarioInexistente = new("UsuarioInexistente", "O usuário não foi encontrado");
}
