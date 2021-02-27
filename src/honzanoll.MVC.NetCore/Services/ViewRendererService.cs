using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using honzanoll.MVC.NetCore.Services.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace honzanoll.MVC.NetCore.Services
{
    public class ViewRendererService : IViewRendererService
    {
        #region Fields

        private readonly IRazorViewEngine razorViewEngine;

        private readonly ITempDataProvider tempDataProvider;

        private readonly IActionContextAccessor actionContextAccessor;

        #endregion

        #region Constructors

        public ViewRendererService(
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IActionContextAccessor actionContextAccessor)
        {
            this.razorViewEngine = razorViewEngine;

            this.tempDataProvider = tempDataProvider;

            this.actionContextAccessor = actionContextAccessor;
        }

        #endregion

        #region Public methods

        public async Task<string> RenderAsync<TModel>(string viewPath, TModel model) where TModel : new()
        {
            ViewEngineResult viewEngineResult = razorViewEngine.FindView(actionContextAccessor.ActionContext, viewPath, false);

            if (!viewEngineResult.Success)
                throw new InvalidOperationException($"Couldn't find view {viewPath}");

            ViewDataDictionary viewDictionary = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = model
            };

            using (StringWriter stringWriter = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(
                    actionContextAccessor.ActionContext,
                    viewEngineResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContextAccessor.ActionContext.HttpContext, tempDataProvider),
                    stringWriter,
                    new HtmlHelperOptions()
                );

                await viewEngineResult.View.RenderAsync(viewContext);

                return stringWriter.ToString();
            }
        }

        #endregion
    }
}
