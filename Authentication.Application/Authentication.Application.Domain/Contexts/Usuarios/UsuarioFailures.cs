namespace Authentication.Application.Domain.Contexts.Usuarios;

public static class UsuarioFailures
{
    public static readonly FailureModel EmailObrigatorio = new ("EmailObrigatorio", "Email é obrigatório.");

    public static readonly FailureModel EmailInvalido = new ("EmailInvalido", "Email não se encontra em um formato válido.");

    public static readonly FailureModel SenhaObrigatoria = new ("SenhaObrigatoria", "Senha é obrigatória.");

    public static readonly FailureModel SenhaDeveTer8Caracteres = new ("SenhaDeveTer8Caracteres", "Senha deve ter no mínimo 8 caracteres.");

    public static readonly FailureModel EmailSenhaInvalidos = new ("EmailSenhaInvalidos", "Email ou senha inválidos");

    public static readonly FailureModel NaoFoiPossivelRecuperarUsuarioLogado = new ("NaoFoiPossivelRecuperarUsuarioLogado", "Não foi possível recuperar o usuário logado");
}
