namespace honzanoll.MVC.NetCore.ViewModels
{
    public class DialogViewModel
    {
        #region Properties

        public string Title { get; set; }

        public string Content { get; set; }

        #endregion

        #region Constructors

        public DialogViewModel(string title, string content)
        {
            Title = title;
            Content = content;
        }

        #endregion
    }
}
