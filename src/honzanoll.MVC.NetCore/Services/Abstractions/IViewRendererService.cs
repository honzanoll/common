using System.Threading.Tasks;

namespace honzanoll.MVC.NetCore.Services.Abstractions
{
    public interface IViewRendererService
    {
        #region Public methods

        Task<string> RenderAsync<TModel>(string viewPath, TModel model) where TModel : new();

        #endregion
    }
}
