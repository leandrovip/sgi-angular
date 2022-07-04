using Microsoft.EntityFrameworkCore;
using Vip.SGI.Domain.Models.Base;
using Vip.SGI.Infra.Context;
using Vip.Validator.Notifications;

namespace Vip.SGI.Application.Services.Base;

public abstract class BaseService : Notifiable
{
    #region Propriedades

    protected readonly SgiContext _db;

    #endregion

    #region Construtores

    protected BaseService(SgiContext context)
    {
        _db = context;
    }

    #endregion

    #region Métodos Contexto

    protected Task<bool> Salvar<T>(T entidade, bool novoCadastro) where T : EntidadeBase
    {
        if (InvalidModel(entidade)) return Task.FromResult(false);
        return novoCadastro ? Incluir(entidade) : Editar(entidade);
    }

    protected async Task<bool> Incluir<T>(T entidade) where T : EntidadeBase
    {
        try
        {
            _db.Set<T>();
            await _db.AddAsync(entidade);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            AddNotification("BaseService.Incluir", $"Não foi possível incluir {typeof(T).Name}");
            AddNotification("Erro", ex.Message);
            return false;
        }
    }

    protected async Task<bool> Editar<T>(T entidade) where T : EntidadeBase
    {
        try
        {
            _db.Entry(entidade).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            AddNotification("BaseService.Editar", $"Não foi possível editar {typeof(T).Name}");
            AddNotification("Erro", ex.Message);
            return false;
        }
    }

    protected async Task Excluir<T>(Guid id) where T : EntidadeBase
    {
        try
        {
            var entidade = await _db.Set<T>().FindAsync(id);
            if (entidade.IsNull())
            {
                AddNotification("BaseService.Excluir", "Registro não encontrado");
                return;
            }

            _db.Remove(entidade);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            AddNotification("BaseService.Excluir", $"Não foi possível excluir o registro {typeof(T).Name}");
            AddNotification("Erro", ex.Message);
        }
    }

    #endregion

    #region Métodos Validação

    protected bool InvalidModel(Notifiable model)
    {
        if (model.IsNotNull() && model.Valid) return false;

        AddNotification("BaseService.InvalidModel", "Objeto inválido. Por favor, tente novamente");
        return true;
    }

    protected bool ValidModel(Notifiable model)
    {
        return !InvalidModel(model);
    }

    #endregion
}