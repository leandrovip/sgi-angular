using Vip.SGI.Shared.Extensions;
using Vip.Validator.Notifications;

namespace Vip.SGI.Application.Wrapper;

public static class ResultFactory
{
    public static Result Success(object data)
    {
        return Result.Factory(true, "Requisição efetuada com sucesso", data: data);
    }

    public static Result Success(string message, object data)
    {
        if (message.IsNullOrEmpty()) message = "Requisição efetuada com sucesso";
        return Result.Factory(true, message, data: data);
    }

    public static Result BadRequest()
    {
        return Result.Factory(false, "Erro de requisição");
    }

    public static Result BadRequest(Exception exception)
    {
        return Result.Factory(false, "Ops :(", exception.ToEvents());
    }

    public static Result BadRequest(string message)
    {
        return Result.Factory(false, message);
    }

    public static Result BadRequest(string message, object data)
    {
        return Result.Factory(false, message, data: data);
    }

    public static Result BadRequest(IEnumerable<Notification> notifications)
    {
        return Result.Factory(false, "Ops :(", notifications.ToEvents(true));
    }

    public static Result ApplicationError(string message)
    {
        return Result.Factory(false, $"Ocorreu algum erro interno na aplicação. Mensagem: {message}");
    }

    public static Result Unauthorized(string message = "")
    {
        message = message.IsNullOrEmpty() ? "Sem permissão de acesso" : message;
        return Result.Factory(false, message);
    }

    public static Result InvalidModel(Notifiable model)
    {
        return Result.Factory(false, "Dados inválidos, verifique as ocorrências", model.Notifications.ToEvents());
    }
}