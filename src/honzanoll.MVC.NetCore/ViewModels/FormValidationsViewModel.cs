using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace honzanoll.MVC.NetCore.ViewModels
{
    public class FormValidationsViewModel
    {
        #region Private members

        private string _url;

        #endregion

        #region Properties

        public List<FormValidationError> Errors { get; set; }

        public string Url
        {
            get
            {
                return !string.IsNullOrEmpty(_url) ? _url : string.Empty;
            }
        }

        public object Data { get; set; }

        public bool Success => Errors == null || Errors.Count == 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Add validation errors via model state
        /// </summary>
        /// <param name="modelState"></param>
        public FormValidationsViewModel(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys
                .SelectMany(k => modelState[k].Errors.Select(e => new FormValidationError(k, e.ErrorMessage)))
                .ToList();
        }

        /// <summary>
        /// Add url address for redirect to different controller page (no errors found)
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="controllerContext"></param>
        public FormValidationsViewModel(LinkGenerator linkGenerator, string action, string controller, object routeParams = null)
        {
            _url = linkGenerator.GetPathByAction(action, controller, routeParams);
        }

        /// <summary>
        /// Add url address for redirect (no errors found)
        /// </summary>
        /// <param name="uri"></param>
        public FormValidationsViewModel(Uri uri)
        {
            _url = uri.AbsoluteUri;
        }

        /// <summary>
        /// Add some data to model (no errors found)
        /// </summary>
        /// <param name="data"></param>
        public FormValidationsViewModel(object data)
        {
            Data = data;
        }

        /// <summary>
        /// Add message to model (no errors found)
        /// </summary>
        /// <param name="data"></param>
        public FormValidationsViewModel(string message)
        {
            Data = new { Message = message };
        }

        #endregion

        public class FormValidationError
        {
            #region Properties

            public string Field { get; set; }

            public string Message { get; set; }

            #endregion

            #region Constructors

            public FormValidationError(
                string field,
                string message)
            {
                Field = field;
                Message = message;
            }

            #endregion
        }
    }
}
